using System;
using System.Collections.Generic;
using System.Text;
using Virgil.CryptoAPI;
using Virgil.SDK.Common;
using Virgil.SDK.Web;

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
                    AdditionalData = @params.AdditionalData,
                    ExtraFields = @params.ExtraFields,
                    SignerId = cardId,
                    SignerPrivateKey = @params.SignerPrivateKey,
                    SignerType = SignerType.Self
                });
        }

        public void Sign(RawSignedModel model, ExtendedSignParams @params)
        {
            //todo: validate params
            if ((@params.SignerType == SignerType.Self)
                && model.Signatures.Exists(s => s.SignerType == SignerType.Self.ToLowerString()))
            {
                throw new VirgilException("The model already has self signature.");
            }

            if ((@params.SignerType == SignerType.App) &&
                model.Signatures.Exists(s => s.SignerType == SignerType.App.ToLowerString()))
            {
                throw new VirgilException("The model already has application signature.");
            }

            byte[] extraFieldsBytes = null;
            byte[] fingerprintPayload;

            if (@params.SignerType != SignerType.Self && @params.ExtraFields != null)
            {
                extraFieldsBytes = CardUtils.TakeSnapshot(@params.ExtraFields);

                fingerprintPayload = Bytes.Combine(model.ContentSnapshot, extraFieldsBytes);
            }
            else
            {
                fingerprintPayload = model.ContentSnapshot;
            }

            var fingerprint = Crypto.GenerateSHA256(fingerprintPayload);
            var signatureBytes = Crypto.GenerateSignature(fingerprint, @params.SignerPrivateKey);
            var signature = new RawSignature
            {
                SignerId = @params.SignerId,
                SignerType = @params.SignerType.ToLowerString(),
                Signature = signatureBytes,
                Snapshot = extraFieldsBytes
            };
            model.Signatures.Add(signature);
        }
    }
}
