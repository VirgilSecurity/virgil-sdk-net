#region Copyright (C) Virgil Security Inc.
// Copyright (C) 2015-2018 Virgil Security Inc.
// 
// Lead Maintainer: Virgil Security Inc. <support@virgilsecurity.com>
// 
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions 
// are met:
// 
//   (1) Redistributions of source code must retain the above copyright
//   notice, this list of conditions and the following disclaimer.
//   
//   (2) Redistributions in binary form must reproduce the above copyright
//   notice, this list of conditions and the following disclaimer in
//   the documentation and/or other materials provided with the
//   distribution.
//   
//   (3) Neither the name of the copyright holder nor the names of its
//   contributors may be used to endorse or promote products derived 
//   from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE AUTHOR ''AS IS'' AND ANY EXPRESS OR
// IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT,
// INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
// HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
// STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING
// IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
// POSSIBILITY OF SUCH DAMAGE.
#endregion

namespace Virgil.SDK.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Virgil.CryptoAPI;
    using Virgil.SDK.Web;

    public class CardUtils
    {
        public static string GenerateCardId(ICardCrypto cardCrypto, byte[] snapshot)
        {
            var fingerprint = cardCrypto.GenerateSHA256(snapshot);
            var id = Bytes.ToString(fingerprint, StringEncoding.HEX);

            return id;
        }

        public static Card Parse(ICardCrypto cardCrypto, RawSignedModel rawSignedModel)
        {
            if (rawSignedModel == null)
            {
                throw new ArgumentNullException(nameof(rawSignedModel));
            }

            var rawCardContent = SnapshotUtils.ParseSnapshot<RawCardContent>(rawSignedModel.ContentSnapshot);
            var fingerprint = cardCrypto.GenerateSHA256(rawSignedModel.ContentSnapshot);
            var cardId = Bytes.ToString(fingerprint, StringEncoding.HEX);

            var signatures = new List<CardSignature>();
            if (rawSignedModel.Signatures != null)
            {
                foreach (var s in rawSignedModel.Signatures)
                {
                    var cardSignature = new CardSignature
                    {
                        SignerId = s.SignerId,
                        SignerType = s.SignerType,
                        Signature = s.Signature,
                        ExtraFields = TryParseExtraFields(s.Snapshot),
                        Snapshot = s.Snapshot
                    };
                    signatures.Add(cardSignature);
                }
            }

            return new Card(cardId,
                rawCardContent.Identity,
                fingerprint,
                cardCrypto.ImportPublicKey(rawCardContent.PublicKey),
                rawCardContent.Version,
                rawCardContent.CreatedAt,
                signatures,
                rawCardContent.PreviousCardId,
                rawSignedModel.Meta
                );
        }

        private static Dictionary<string, string> TryParseExtraFields(byte[] signatureSnapshot)
        {
            Dictionary<string, string> extraFields = null;
            if (signatureSnapshot != null)
            {
                try
                {
                    extraFields = SnapshotUtils.ParseSnapshot<Dictionary<string, string>>(signatureSnapshot);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            return extraFields;
        }

        public static IList<Card> Parse(ICardCrypto cardCrypto, IEnumerable<RawSignedModel> requests)
        {
            if (requests == null)
            {
                throw new ArgumentNullException(nameof(requests));
            }

            return requests.Select(r => CardUtils.Parse(cardCrypto, r)).ToList();
        }

        public static IList<Card> LinkedCardLists(Card[] cards)
        {
            // sort array DESC by date, the latest cards are at the beginning
            Array.Sort(cards, (a, b) => -1 * DateTime.Compare(a.CreatedAt, b.CreatedAt));

            // actualCards contains 'actual' cards: 
            //1. which aren't associated with another one
            //2. which wasn't overrided as 'previous card'
            var actualCards = new List<Card>();
            var previousIds = new List<string>();

            for (int i = 0; i < cards.Length; i++)
            {
                var card = cards[i];

                // there is no a card which references to current card, so it is the freshest one
                if (!previousIds.Contains(card.Id))
                    actualCards.Add(card);

                if (card.PreviousCardId != null)
                {
                    previousIds.Add(card.PreviousCardId);

                    // find previous card in the early cards
                    for (int j = i + 1; j < cards.Length; j++)
                    {
                        var earlyCard = cards[j];
                        if (earlyCard.Id == card.PreviousCardId)
                        {
                            card.PreviousCard = earlyCard;
                            break;
                        }
                    }
                }
            }
            return actualCards;
        }
    }
}