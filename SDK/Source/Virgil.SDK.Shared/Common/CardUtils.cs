#region Copyright (C) Virgil Security Inc.
// Copyright (C) 2015-2017 Virgil Security Inc.
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
    using System.Collections.ObjectModel;
    using System.Linq;
    
    using Virgil.CryptoApi;
    using Virgil.SDK.Web;

    public class CardUtils
    {
        public static Card ParseRawCard(ICrypto crypto, RawCard rawCard)
        {
            if (rawCard == null)
            {
                throw new ArgumentNullException(nameof(rawCard));
            }
            
            var snapshotModel = ParseSnapshot<RawCardSnapshot>(rawCard.ContentSnapshot);
            var cardId = GenerateCardId(crypto, rawCard.ContentSnapshot);

            IEnumerable<CardSignature> signatures = null;
            if (rawCard.Meta.Signatures != null)
            {
                signatures = rawCard.Meta.Signatures
                    .Select(s => new CardSignature { CardId = s.Key, Signature = s.Value })
                    .ToList();
            }

            var card = new Card
            (
                cardId,
                snapshotModel.Identity,
                snapshotModel.IdentityType,
                crypto.ImportPublicKey(snapshotModel.PublicKeyBytes),
                snapshotModel.CustomFields != null
                    ? new ReadOnlyDictionary<string, string>(snapshotModel.CustomFields)
                    : null,
                rawCard.Meta.Version,
                rawCard.ContentSnapshot,
                rawCard.Meta.CreatedAt,
                signatures
            );

            return card;
        }
        
        public static IList<Card> ParseRawCards(ICrypto crypto, IEnumerable<RawCard> rawCards)
        {
            if (rawCards == null)
            {
                throw new ArgumentNullException(nameof(rawCards));
            }

            return rawCards.Select(rc => ParseRawCard(crypto, rc)).ToList();
        }
        
        public static string GenerateCardId(ICrypto crypto, byte[] payload)
        {
            var fingerprint = crypto.CalculateFingerprint(payload);
            var id = BytesConvert.ToString(fingerprint, StringEncoding.HEX);

            return id;
        }
        
        public static byte[] TakeSnapshot(object snapshotModel)
        {
            var serializer = Configuration.Serializer; 
            
            var snapshotModelJson = serializer.Serialize(snapshotModel);
            var takenSnapshot = BytesConvert.FromString(snapshotModelJson);

            return takenSnapshot;
        }

        public static TModel ParseSnapshot<TModel>(byte[] snapshot)
        {
            var serializer = Configuration.Serializer; 
            
            var snapshotModelJson = BytesConvert.ToString(snapshot);
            var snapshotModel = serializer.Deserialize<TModel>(snapshotModelJson);
            
            return snapshotModel;
        }
    }    
}