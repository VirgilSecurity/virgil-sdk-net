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

    using Virgil.CryptoApi;
    using Virgil.SDK.Client;
    using Virgil.SDK.Utils;

    public class CardManager
    {
        private readonly ICrypto crypto;
        private readonly ISerializer serializer;
        private readonly Snapshotter snapshotter;

        public CardManager(ICrypto crypto)
        {
            this.crypto = crypto;
            this.serializer = new JsonSerializer();
            this.snapshotter = new Snapshotter(this.serializer);
        }

        public Card CreateNew(CardParams parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (string.IsNullOrWhiteSpace(parameters.Identity))
            {
                throw new ArgumentException($"{parameters.Identity} property is mandatory");
            }

            if (parameters.KeyPair == null)
			{
				throw new ArgumentException($"{parameters.KeyPair} property is mandatory");
			}

            var identityType = string
                .IsNullOrWhiteSpace(parameters.IdentityType) ? "unknown" : parameters.IdentityType;

            var snapshotModel = new CardRawSnapshot
            {
                Identity = parameters.Identity,
                IdentityType = identityType,
                PublicKeyBytes = this.crypto.ExportPublicKey(parameters.KeyPair.PublicKey),
                CustomFields = parameters.CustomFields,
                Scope = Enum.GetName(typeof(CardScope), parameters.Scope).ToLower()
            };

            var snapshotBytes = this.snapshotter.Capture(snapshotModel); 
            var fingerprint = this.crypto.ComputeFingerprint(snapshotBytes);
            var selfSignature = this.crypto.GenerateSignature(fingerprint, parameters.KeyPair.PrivateKey);
            var cardId = BytesConvert.ToHEXString(fingerprint);

            var card = new Card
            {
                Id = cardId,
                Identity = snapshotModel.Identity,
                IdentityType = snapshotModel.IdentityType,
                PublicKey = parameters.KeyPair.PublicKey,
                CustomFields = snapshotModel.CustomFields != null 
                    ? new ReadOnlyDictionary<string, string>(snapshotModel.CustomFields)
                    : null,
                Scope = parameters.Scope,
                Version = "4.0",
                Snapshot = snapshotBytes,
                CreatedAt = DateTime.UtcNow
            };

            card.AddSignature(new CardSignature { CardId = cardId, Signature = selfSignature });

            return card;
        }

        public Card ImportFromString(string cardString)
        {
			if (string.IsNullOrWhiteSpace(cardString))
			{
				throw new ArgumentNullException();
			}

			var cardRawBytes = BytesConvert.FromBASE64String(cardString);
			var cardRawJson = BytesConvert.ToUTF8String(cardRawBytes);
            var cardRaw = this.serializer.Deserialize<CardRaw>(cardRawJson);

            return this.ImportFromRaw(cardRaw);
        }

        public string ExportToString(Card card)
        {
			if (card == null)
			{
				throw new ArgumentNullException(nameof(card)); 
			}

            var cardRaw = this.ConvertCardToRaw(card);

            var cardRawJson = serializer.Serialize(cardRaw);
            var cardRawBytes = BytesConvert.FromUTF8String(cardRawJson);
            var cardRawBase64 = BytesConvert.ToBASE64String(cardRawBytes);

            return cardRawBase64;
        }

        public Card ImportFromRaw(CardRaw cardRaw)
        {
			var snapshotBytes = cardRaw.ContentSnapshot;
			var snapshotJson = BytesConvert.ToUTF8String(snapshotBytes);
            var snapshotModel = this.serializer.Deserialize<CardRawSnapshot>(snapshotJson);

			var card = new Card
			{
				Id = cardRaw.Id,
				Identity = snapshotModel.Identity,
				IdentityType = snapshotModel.IdentityType,
				PublicKey = this.crypto.ImportPublicKey(snapshotModel.PublicKeyBytes),
				CustomFields = snapshotModel.CustomFields != null
					? new ReadOnlyDictionary<string, string>(snapshotModel.CustomFields)
					: null,
				Scope = (CardScope)Enum.Parse(typeof(CardScope), snapshotModel.Scope, true),
				Version = cardRaw.Meta.Version,
				Snapshot = snapshotBytes,
				CreatedAt = cardRaw.Meta.CreatedAt
			};

            cardRaw.Meta.Signatures.ToList().ForEach(s =>
                card.AddSignature(new CardSignature { CardId = s.Key, Signature = s.Value }));

			return card;
        }

        public IList<Card> ImportFromRawAll(IEnumerable<CardRaw> rawCards)
        {
            if (rawCards == null)
            {
                throw new ArgumentNullException(nameof(rawCards));
            }

            return rawCards.Select(r => this.ImportFromRaw(r)).ToList();
        }

        public void SignBy(Card card, string signerCardId, IPrivateKey signerPrivateKey)
        {
            if (card == null)
            {
                throw new ArgumentNullException(nameof(card));
            }

            if (string.IsNullOrWhiteSpace(signerCardId))
			{
				throw new ArgumentNullException(nameof(signerCardId));
			}

			if (signerPrivateKey == null)
			{
				throw new ArgumentNullException(nameof(signerCardId));
			}

            var fingerprint = this.crypto.ComputeFingerprint(card.Snapshot);
            var signature = this.crypto.GenerateSignature(fingerprint, signerPrivateKey);

            card.AddSignature(new CardSignature{ CardId = signerCardId, Signature = signature });
        }

		private CardRaw ConvertCardToRaw(Card card)
		{
			var cardRaw = new CardRaw
			{
				Id = card.Id,
				ContentSnapshot = card.Snapshot,
				Meta = new CardRawMeta
				{
					Signatures = card.Signatures.ToDictionary(it => it.CardId, it => it.Signature),
					Version = card.Version,
					CreatedAt = card.CreatedAt
				}
			};

			return cardRaw;
		}
    }
}
