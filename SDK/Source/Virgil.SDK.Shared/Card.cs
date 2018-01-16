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

namespace Virgil.SDK
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Virgil.CryptoAPI;

    using Virgil.SDK.Common;
    using Virgil.SDK.Web;

    /// <summary>
    /// The <see cref="Card"/> class is the main entity of Virgil Services. Every user/device is 
    /// represented with a Virgil Card which contains a public key and information about identity.
    /// </summary>
    public class Card
    {
        private List<CardSignature> signatures;

        private Card()
        {
        }

        internal Card(
            string cardId,
            string identity,
            byte[] fingerprint,
            IPublicKey publicKey,
            string version,
            DateTime createdAt,
            List<CardSignature> signautes)
        {
            this.Id = cardId;
            this.Identity = identity;
            this.Fingerprint = fingerprint;
            this.PublicKey = publicKey;
            this.Version = version;
            this.CreatedAt = createdAt;
            this.signatures = signautes;
        }

        /// <summary>
        /// Gets the Card ID that uniquely identifies the Card in Virgil Services.
        /// </summary>
        public string Id { get; private set; }
        
        /// <summary>
        /// Gets the identity value that can be anything which identifies the user in your application.
        /// </summary>
        public string Identity { get; private set; }

        /// <summary>
        /// Gets the fingerprint of the card.
        /// </summary>
        public byte[] Fingerprint { get; private set; }
        
        /// <summary>
        /// Gets the public key.
        /// </summary>
        public IPublicKey PublicKey { get; private set; }
        
        /// <summary>
        /// Gets the version of the card.
        /// </summary>
        public string Version { get; private set; }
        
        /// <summary>
        /// Gets the date and time fo card creation in UTC.
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Get previous Card ID  that current card is used to override to
        /// </summary>
        public string PreviousCardId { get; private set; }

        /// <summary>
        /// Get previous Card that current card is used to override to
        /// </summary>
        public Card PreviousCard { get; internal set; }

        /// <summary>
        /// Gets a list of signatures.
        /// </summary>
        public IReadOnlyList<CardSignature> Signatures => this.signatures;

        public static Card Parse(ICardCrypto cardCrypto, RawSignedModel request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            
            var requestInfo = CardUtils.ParseSnapshot<RawCardContent>(request.ContentSnapshot);
            var fingerprint = cardCrypto.GenerateSHA256(request.ContentSnapshot);
            var cardId = Bytes.ToString(fingerprint, StringEncoding.HEX);

            List<CardSignature> signatures = null;
            if (request.Signatures != null)
            {
                signatures = request.Signatures
                    .Select(s => new CardSignature
                    {
                        SignerId = s.SignerId,
                        SignerType   = (SignerType)Enum.Parse(typeof(SignerType), s.SignerType, true),
                        Signature    = s.Signature,
                        ExtraFields  = s.Snapshot != null ? CardUtils.ExtractSignatureFields(s.Snapshot) : null
                    })
                    .ToList();
            }

            var card = new Card
            {
                Id = cardId,
                Identity = requestInfo.Identity,
                PublicKey = cardCrypto.ImportPublicKey(requestInfo.PublicKey),
                Version = requestInfo.Version,
                Fingerprint = fingerprint,
                CreatedAt = requestInfo.CreatedAt,
                signatures = signatures,
                PreviousCardId = requestInfo.PreviousCardId
            };
            
            return card;
        }

        public static IList<Card> Parse(ICardCrypto cardCrypto, IEnumerable<RawSignedModel> requests)
        {
            if (requests == null)
            {
                throw new ArgumentNullException(nameof(requests));
            }

            return requests.Select(r => Parse(cardCrypto, r)).ToList();
        }
    }
}
