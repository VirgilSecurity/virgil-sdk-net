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
    using System.Linq;

    using Virgil.CryptoApi;
    using Virgil.SDK.Client;
    using Virgil.SDK.Utils;

    public class RequestFactory
    {
        private readonly ICrypto crypto;
        private readonly Snapshotter snapshotter;

        public RequestFactory(ICrypto crypto)
        {
            this.crypto = crypto;
            this.snapshotter = new Snapshotter(new JsonSerializer());
        }

        public CardRequest CreatePublishRequest(Card card, params CardSigner[] signers) 
        {
            if (card == null)
            {
                throw new ArgumentNullException(nameof(card));
            }

			var signatures = card.Signatures
                .ToDictionary(s => s.CardId, s => s.Signature);

            this.ProcessSignatures(card.Snapshot, signers)
                .ToList()
                .ForEach(s => signatures.Add(s.Key, s.Value));

            var request = new CardRequest 
            {
                ContentSnapshot = card.Snapshot,
                Meta = new CardRequestMeta { Signatures = signatures }
            };

            return request;
        }

        public CardRequest CreateRevokeRequest(string cardId, params CardSigner[] signers)
        {
            if (string.IsNullOrWhiteSpace(cardId))
			{
				throw new ArgumentNullException(nameof(cardId));
			}

            var snapshotModel = new 
            {
                card_id = cardId,
                revocation_reason = "unspecified"
            };

            var snapshot = this.snapshotter.Capture(snapshotModel);
            var signatures = this.ProcessSignatures(snapshot, signers);

			var request = new CardRequest
			{
				ContentSnapshot = snapshot,
				Meta = new CardRequestMeta { Signatures = signatures }
			};

			return request;
        }

        public RelationCardRequest CreateRelationRequest(Card card, CardSigner signer)
        {
			if (card == null)
			{
				throw new ArgumentNullException(nameof(card));
			}

            var signatures = this.ProcessSignatures(card.Snapshot, new [] { signer });
			var request = new RelationCardRequest
			{
                SigningCardId = card.Id,
				ContentSnapshot = card.Snapshot,
				Meta = new CardRequestMeta { Signatures = signatures }
			};

			return request;
        }

		public RelationCardRequest CreateRelationRemoveRequest(string cardId, CardSigner signer)
		{
			if (string.IsNullOrWhiteSpace(cardId))
			{
				throw new ArgumentNullException(nameof(cardId));
			}

			var snapshotModel = new
			{
				card_id = cardId,
				revocation_reason = "unspecified"
			};

			var snapshot = this.snapshotter.Capture(snapshotModel);
			var signatures = this.ProcessSignatures(snapshot, new[] { signer });

			var request = new RelationCardRequest
			{
                SigningCardId = cardId,
				ContentSnapshot = snapshot,
				Meta = new CardRequestMeta { Signatures = signatures }
			};

			return request;
		}

        private IDictionary<string, byte[]> ProcessSignatures(byte[] snapshot, CardSigner[] signers)
        {
            var signatures = new Dictionary<string, byte[]>();
			if (signers != null && signers.Any())
			{
				var fingerprint = this.crypto.ComputeFingerprint(snapshot);
				foreach (var signer in signers)
				{
					var signature = this.crypto.GenerateSignature(fingerprint, signer.PrivateKey);
					signatures.Add(signer.CardId, signature);
				}
			}

            return signatures;
        }
    }
}