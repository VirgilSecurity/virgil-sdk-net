using System;
using System.Collections.Generic;
using Virgil.CryptoAPI;
using Virgil.SDK.Common;
using Virgil.SDK.Web;

namespace Virgil.SDK.Signer
{
    /// <summary>
    /// The <see cref="ModelSigner"/> class provides signing operation for <see cref="RawSignedModel"/>.
    /// </summary>
    public class ModelSigner
    {
        public readonly ICardCrypto Crypto;
        public const string SelfSigner = "self";
        public const string VirgilSigner = "virgil";


        public ModelSigner(ICardCrypto crypto)
        {
            this.Crypto = crypto;
        }

        /// <summary>
        /// Adds owner's signature to the specified <see cref="RawSignedModel"/> using specified signer 
        /// parameters included private key and additional raw bytes.
        /// </summary>
        /// <param name="model"> the instance of <see cref="RawSignedModel"/> to be signed.</param>
        /// <param name="signerPrivateKey"> the instance of <see cref="IPrivateKey"/> to sign with.</param>
        /// <param name="signatureSnapshot"> Some additional raw bytes to be signed with model.</param>
        public void SelfSign(RawSignedModel model, IPrivateKey signerPrivateKey, byte[] signatureSnapshot = null)
        {
            ValidateSignParams(model, signerPrivateKey);

            Sign(model,
                new SignParams()
                {
                    SignerPrivateKey = signerPrivateKey,
                    Signer = SelfSigner
                },
                signatureSnapshot
                );
        }


        /// /// <summary>
        /// Adds owner's signature to the specified <see cref="RawSignedModel"/> using specified signer 
        /// parameters included private key and dictionary with additional data.
        /// </summary>
        /// <param name="model"> the instance of <see cref="RawSignedModel"/> to be signed.</param>
        /// <param name="signerPrivateKey"> the instance of <see cref="IPrivateKey"/> to sign with.</param>
        /// <param name="extraFields"> Dictionary with additional data to be signed with model.</param>
        public void SelfSign(RawSignedModel model, IPrivateKey signerPrivateKey, Dictionary<string, string> extraFields)
        {
            SelfSign(model, signerPrivateKey,
                (extraFields != null) ? SnapshotUtils.TakeSnapshot(extraFields) : null
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
                throw new ArgumentException($"{nameof(signerPrivateKey)} property is mandatory");
            }
        }

        /// <summary>
        /// Adds signature to the specified <see cref="RawSignedModel"/> using specified signer 
        /// parameters included private key, signer type and additional raw bytes.
        /// </summary>
        /// <param name="model"> the instance of <see cref="RawSignedModel"/> to be signed.</param>
        /// <param name="@params"> the instance of <see cref="SignParams"/> to sign with.</param>
        /// <param name="signatureSnapshot"> Some additional raw bytes to be signed with model.</param>
        public void Sign(RawSignedModel model, SignParams @params, byte[] signatureSnapshot = null)
        {
            ValidateExtendedSignParams(model, @params);
            ThrowExceptionIfSignatureExists(@params, model.Signatures);

            var extendedSnapshot = signatureSnapshot != null ?
                Bytes.Combine(model.ContentSnapshot, signatureSnapshot)
                : model.ContentSnapshot;

            var signatureBytes = Crypto.GenerateSignature(extendedSnapshot, @params.SignerPrivateKey);

            var signature = new RawSignature
            {
                Signer = @params.Signer,
                Signature = signatureBytes,
                Snapshot = signatureSnapshot
            };
            model.Signatures.Add(signature);
        }

        private static void ThrowExceptionIfSignatureExists(SignParams @params, IList<RawSignature> signatures)
        {
            if (signatures != null &&
                ((List<RawSignature>)signatures).Exists(
                    s => s.Signer == @params.Signer))
            {
                throw new VirgilException("The model already has this signature.");
            }
        }

        /// <summary>
        /// Adds signature to the specified <see cref="RawSignedModel"/> using specified signer 
        /// parameters included private key, signer type and dictionary with additional data.
        /// </summary>
        /// <param name="model"> the instance of <see cref="RawSignedModel"/> to be signed.</param>
        /// <param name="@params"> the instance of <see cref="SignParams"/> to sign with.</param>
        /// <param name="extraFields"> Dictionary with additional data to be signed with model.</param>
        public void Sign(RawSignedModel model, SignParams @params, Dictionary<string, string> ExtraFields)
        {
            Sign(model, @params,
                (ExtraFields != null) ? SnapshotUtils.TakeSnapshot(ExtraFields) : null
            );
        }

        private static void ValidateExtendedSignParams(RawSignedModel model, SignParams @params)
        {
            ValidateSignParams(model, @params.SignerPrivateKey);

            if (@params.Signer == null)
            {
                throw new ArgumentException($"{nameof(@params.Signer)} property is mandatory");
            }
        }
    }
}
