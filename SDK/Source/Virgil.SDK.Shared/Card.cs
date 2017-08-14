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

namespace Virgil.SDK
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    
    using Virgil.SDK.Utils;
    using Virgil.SDK.Client;
    using Virgil.SDK.Validation;
    using Virgil.Crypto.Interfaces;

    public class Card
    {
        private readonly List<CardSignature> signatures;

        internal Card()
        {
            this.signatures = new List<CardSignature>();
        }

        public string Id { get; internal set; }
        public string Identity { get; internal set; }
        public string IdentityType { get; internal set; }
        public IPublicKey PublicKey { get; internal set; }
        public ReadOnlyDictionary<string, string> CustomFields { get; internal set; }
        public string Version { get; internal set; }
        public DateTime CreatedAt { get; internal set; }
        public IReadOnlyList<CardSignature> Signatures => this.signatures;
        public byte[] Snapshot { get; internal set; }

        public static Card ImportRaw(ICrypto crypto, CardRaw cardRaw)
        {
            var snapshotter = ServiceLocator.GetService<ISnapshotter>();
            var idGenerator = ServiceLocator.GetService<ICardIdGenerator>();
            
            var snapshotModel = snapshotter.Parse<CardRawSnapshot>(cardRaw.ContentSnapshot);
            var cardId = idGenerator.Generate(crypto, cardRaw.ContentSnapshot);

            var card = new Card
            {
                Id = cardId,
                Identity = snapshotModel.Identity,
                IdentityType = snapshotModel.IdentityType,
                PublicKey = crypto.ImportPublicKey(snapshotModel.PublicKeyBytes),
                CustomFields = snapshotModel.CustomFields != null
                    ? new ReadOnlyDictionary<string, string>(snapshotModel.CustomFields)
                    : null,
                Version = cardRaw.Meta.Version,
                Snapshot = cardRaw.ContentSnapshot,
                CreatedAt = cardRaw.Meta.CreatedAt
            };

            cardRaw.Meta.Signatures.ToList().ForEach(s =>
                card.signatures.Add(new CardSignature { CardId = s.Key, Signature = s.Value }));

            return card;
        }
        
        public static IList<Card> ImportRawAll(ICrypto crypto, IEnumerable<CardRaw> rawCards)
        {
            if (rawCards == null)
            {
                throw new ArgumentNullException(nameof(rawCards));
            }

            return rawCards.Select(rc => ImportRaw(crypto, rc)).ToList();
        }

        public ValidationResult Validate(CardValidator validator)
        {
            throw new NotImplementedException();
        }
    }
}
