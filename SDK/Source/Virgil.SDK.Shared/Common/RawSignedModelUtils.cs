using System;
using System.Collections.Generic;
using System.Text;
using Virgil.CryptoAPI;
using Virgil.SDK.Web;

namespace Virgil.SDK.Common
{
    public class RawSignedModelUtils
    {
        public static RawSignedModel Parse(ICardCrypto cardCrypto, Card card)
        {
            ValidateParams(cardCrypto, card);
            var cardParams = new CardParams()
            {
                Identity = card.Identity,
                ExtraFields = card.Meta,
                PreviousCardId = card.PreviousCardId,
                PublicKey = card.PublicKey
            };

            var rawSignedModel = RawSignedModel.RawModel(
                cardCrypto,
                cardParams,
                card.CreatedAt,
                card.Version);
            rawSignedModel.Signatures = new List<RawSignature>();

            foreach (var signature in card.Signatures)
            {
                rawSignedModel.Signatures.Add(
                    new RawSignature()
                    {
                        Signature = signature.Signature,
                        SignerId = signature.SignerId,
                        SignerType = signature.SignerType,
                        Snapshot = signature.Snapshot
                    }
                );
            }
            return rawSignedModel;
        }

        private static void ValidateParams(ICardCrypto cardCrypto, Card rawSignedModel)
        {
            if (rawSignedModel == null)
            {
                throw new ArgumentNullException(nameof(rawSignedModel));
            }

            if (cardCrypto == null)
            {
                throw new ArgumentNullException(nameof(cardCrypto));
            }
        }
    }
}
