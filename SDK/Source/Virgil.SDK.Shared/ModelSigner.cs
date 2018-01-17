using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Virgil.CryptoAPI;
using Virgil.SDK.Common;
using Virgil.SDK.Web;
using System.Linq;

namespace Virgil.SDK
{
    public class ModelSigner
    {
        public readonly ICardCrypto Crypto;
        public ModelSigner(ICardCrypto crypto)
        {
            this.Crypto = crypto;
        }

        public void SelfSign(RawSignedModel model, SignParams @params)
        {
            //todo: validate params
            var fingerprint = Crypto.GenerateSHA256(model.ContentSnapshot);
            var cardId = Bytes.ToString(fingerprint, StringEncoding.HEX);
            Sign(model,
                new ExtendedSignParams()
                {
                    SignerId = cardId,
                    SignerPrivateKey = @params.SignerPrivateKey,
                    SignerType = SignerType.Self,
                    AdditionalData = @params.AdditionalData,
                    ExtraFields = @params.ExtraFields
                });
        }

        public void Sign(RawSignedModel model, ExtendedSignParams @params)
        {
            //todo: validate params
            if ((@params.SignerType == SignerType.Self) &&
                 model.Signatures != null &&
                 ((List<RawSignature>)model.Signatures).Exists(s => s.SignerType == SignerType.Self.ToLowerString()))
            {
                throw new VirgilException("The model already has self signature.");
            }

            if ((@params.SignerType == SignerType.App) &&
                model.Signatures != null &&
                ((List<RawSignature>)model.Signatures).Exists(s => s.SignerType == SignerType.App.ToLowerString()))
            {
                throw new VirgilException("The model already has application signature.");
            }

            byte[] extraFieldsBytes = null;
            var fingerprintPayload = model.ContentSnapshot;

            // todo check: should we add extra data to self sign?
            //  if (@params.SignerType != SignerType.Self)
            //  {
            if (@params.ExtraFields != null)
            {
                extraFieldsBytes = CardUtils.TakeSnapshot(@params.ExtraFields);
                fingerprintPayload = Bytes.Combine(fingerprintPayload, extraFieldsBytes);
            }

            if (@params.AdditionalData != null)
            {
                fingerprintPayload = Bytes.Combine(fingerprintPayload, @params.AdditionalData);
            }
            //  }

            var fingerprint = Crypto.GenerateSHA256(fingerprintPayload);
            var signatureBytes = Crypto.GenerateSignature(fingerprint, @params.SignerPrivateKey);

            var signature = new RawSignature
            {
                SignerId = @params.SignerId,
                SignerType = @params.SignerType.ToLowerString(),
                Signature = signatureBytes,
                Snapshot = extraFieldsBytes
            };
            if (model.Signatures == null)
            {
                model.Signatures = new List<RawSignature>();
            }
            model.Signatures.Add(signature);
        }
    }
}
