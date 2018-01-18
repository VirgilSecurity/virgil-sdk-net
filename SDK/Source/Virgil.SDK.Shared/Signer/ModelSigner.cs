using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Virgil.CryptoAPI;
using Virgil.SDK.Common;
using Virgil.SDK.Web;
using System.Linq;

namespace Virgil.SDK.Signer
{
    public class ModelSigner
    {
        public readonly ICardCrypto Crypto;
        public ModelSigner(ICardCrypto crypto)
        {
            this.Crypto = crypto;
        }

        /// <summary>
        /// Signs the <see cref="RawSignedModel"/> using specified signer parameters included private key.
        /// </summary>
        public void SelfSign(RawSignedModel model, IPrivateKey signerPrivateKey, byte[] signatureSnapshot = null)
        {
            ValidateSignParams(model, signerPrivateKey);

            var fingerprint = Crypto.GenerateSHA256(model.ContentSnapshot);
            var cardId = Bytes.ToString(fingerprint, StringEncoding.HEX);
            Sign(model,
                new SignParams()
                {
                    SignerId = cardId,
                    SignerPrivateKey = signerPrivateKey,
                    SignerType = SignerType.Self.ToLowerString()
                },
                signatureSnapshot
                );
        }

        /// <summary>
        /// Signs the <see cref="RawSignedModel"/> using specified signer parameters included private key.
        /// </summary>
        public void SelfSign(RawSignedModel model, IPrivateKey signerPrivateKey, Dictionary<string, string> ExtraFields)
        {
            SelfSign(model, signerPrivateKey, 
                (ExtraFields !=null) ? SnapshotUtils.TakeSnapshot(ExtraFields) : null
                );
        }

        private static void ValidateSignParams(RawSignedModel model, IPrivateKey signerPrivateKey)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }


            if (signerPrivateKey == null)
            {
                throw new ArgumentException($"{signerPrivateKey} property is mandatory");
            }
        }

        /// <summary>
        /// Signs the <see cref="RawSignedModel"/> using specified signer parameters.
        /// </summary>
        public void Sign(RawSignedModel model, SignParams @params, byte[] signatureSnapshot = null)
        {
            ValidateExtendedSignParams(model, @params);

            if ((@params.SignerType == SignerType.Self.ToLowerString()) &&
                 model.Signatures != null &&
                 ((List<RawSignature>)model.Signatures).Exists(s => s.SignerType == SignerType.Self.ToLowerString()))
            {
                throw new VirgilException("The model already has self signature.");
            }

            if ((@params.SignerType == SignerType.App.ToLowerString()) &&
                model.Signatures != null &&
                ((List<RawSignature>)model.Signatures).Exists(s => s.SignerType == SignerType.App.ToLowerString()))
            {
                throw new VirgilException("The model already has application signature.");
            }

            var fingerprintPayload = model.ContentSnapshot;

            if (signatureSnapshot != null)
            {
                fingerprintPayload = Bytes.Combine(fingerprintPayload, signatureSnapshot);
            }

            var fingerprint = Crypto.GenerateSHA256(fingerprintPayload);
            var signatureBytes = Crypto.GenerateSignature(fingerprint, @params.SignerPrivateKey);

            var signature = new RawSignature
            {
                SignerId = @params.SignerId,
                SignerType = @params.SignerType,
                Signature = signatureBytes,
                Snapshot = signatureSnapshot
            };
            if (model.Signatures == null)
            {
                model.Signatures = new List<RawSignature>();
            }
            model.Signatures.Add(signature);
        }

        /// <summary>
        /// Signs the <see cref="RawSignedModel"/> using specified signer parameters.
        /// </summary>
        public void Sign(RawSignedModel model, SignParams @params, Dictionary<string, string> ExtraFields)
        {
            Sign(model, @params,
                (ExtraFields != null) ? SnapshotUtils.TakeSnapshot(ExtraFields) : null
            );
        }

        private static void ValidateExtendedSignParams(RawSignedModel model, SignParams @params)
        {
            ValidateSignParams(model, @params.SignerPrivateKey);

            if (@params.SignerId == null)
            {
                throw new ArgumentException($"{@params.SignerId} property is mandatory");
            }

            if (@params.SignerType == null)
            {
                throw new ArgumentException($"{@params.SignerType} property is mandatory");
            }
        }
    }
}
