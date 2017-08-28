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
    using Virgil.SDK.Common;
    using Virgil.SDK.Web;
    
    public class RequestManager
    {
        private readonly ICrypto crypto;

        public RequestManager(ICrypto crypto)
        {
            this.crypto = crypto;
        }

        /// <summary>
        /// Generates a new request in order to apply for a card registration. It contains the public key for 
        /// which the card should be registered, identity information (such as a user name) and integrity 
        /// protection in form of digital self signature.
        /// </summary>
        /// <param name="info">The information about identity and public key.</param>
        /// <param name="privateKey">The private key used to generate self signature.</param>
        /// <returns>A new instance of <see cref="CreateCardRequest"/> class.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public CreateCardRequest CreateCardRequest(CardInfo info, IPrivateKey privateKey = null)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (string.IsNullOrWhiteSpace(info.Identity))
            {
                throw new ArgumentException($"{info.Identity} property is mandatory");
            }

            if (info.PublicKey == null)
            {
                throw new ArgumentException($"{info.PublicKey} property is mandatory");
            }

            var identityType = string.IsNullOrWhiteSpace(info.IdentityType) ? "unknown" : info.IdentityType;
            
            var snapshotModel = new RawCardSnapshot
            {
                Identity = info.Identity,
                IdentityType = identityType,
                PublicKeyBytes = this.crypto.ExportPublicKey(info.PublicKey),
                CustomFields = info.CustomFields,
                Scope = "application"
            };

            var request = new CreateCardRequest
            {
                ContentSnapshot = CardUtils.TakeSnapshot(snapshotModel), 
                Meta = new CardRequestMeta
                {
                    Signatures = new Dictionary<string, byte[]>()
                }
            };

            if (privateKey == null)
            {
                return request;
            }
            
            var cardId = CardUtils.GenerateCardId(this.crypto, request.ContentSnapshot);
            this.SignRequest(request, new SignerInfo { CardId = cardId, PrivateKey = privateKey });

            return request;
        }
        
        /// <summary>
        /// Generates a new request in order to apply for a card revocation. It contains the card ID, 
        /// revocation reason and integrity protection in form of digital signature by application private key.
        /// </summary>
        /// <param name="cardId">The card ID to be revoked.</param>
        /// <param name="signers">The list of authority signers such as application.</param>
        /// <returns>A new instance of <see cref="RevokeCardRequest"/> class</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public RevokeCardRequest RevokeCardRequest(string cardId, params SignerInfo[] signers)
        {
            if (string.IsNullOrWhiteSpace(cardId))
            {
                throw new ArgumentNullException(nameof(cardId));
            }
            
            if (signers == null)
            {
                throw new ArgumentNullException(nameof(signers));
            }
            
            if (!signers.Any())
            {
                throw new ArgumentException($"An {signers} should contains at least 1 signer");
            }
            
            var snapshotModel = new 
            {
                card_id = cardId,
                revocation_reason = "unspecified"
            };
            var snapshot = CardUtils.TakeSnapshot(snapshotModel);
            var request = new RevokeCardRequest
            {
                CardId = cardId,
                ContentSnapshot = snapshot, 
                Meta = new CardRequestMeta
                {
                    Signatures = new Dictionary<string, byte[]>()
                }
            };

            this.SignRequest(request, signers);
            return request;
        }

        /// <summary>
        /// Exports a card request into string. Use this method to transmit the request through the network.
        /// </summary>
        /// <param name="request">The instance of <see cref="CardRequest"/> class.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public string ExportRequest(CardRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request)); 
            }

            var serializer = Configuration.GetService<ISerializer>();
            
            var requestJson = serializer.Serialize(request);
            var requestBytes = BytesConvert.FromString(requestJson);
            var requestBase64 = BytesConvert.ToString(requestBytes, StringEncoding.BASE64);

            return requestBase64;
        }
        
        /// <summary>
        /// Imports request from string.
        /// </summary>
        /// <param name="exportedRequest">The exported request string.</param>
        /// <typeparam name="TRequest">The type of possible requests.</typeparam>
        /// <returns>The instance of request.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public TRequest ImportRequest<TRequest>(string exportedRequest) where TRequest : CardRequest
        {
            if (string.IsNullOrWhiteSpace(exportedRequest))
            {
                throw new ArgumentNullException();
            }
            
            var serializer = Configuration.GetService<ISerializer>();

            var requestJsonBytes = BytesConvert.FromString(exportedRequest, StringEncoding.BASE64);
            var requestJson = BytesConvert.ToString(requestJsonBytes);
            var request = serializer.Deserialize<TRequest>(requestJson);

            return request;
        }

        /// <summary>
        /// Signs a request using a list of passed signers.
        /// </summary>
        /// <param name="request">The request to be signed.</param>
        /// <param name="signers">The list of signers.</param>
        public void SignRequest(CardRequest request, params SignerInfo[] signers)
        {
            var fingerprint = this.crypto.CalculateFingerprint(request.ContentSnapshot);
            
            foreach (var signer in signers)
            {
                var signature = this.crypto.GenerateSignature(fingerprint, signer.PrivateKey);
                request.Meta.Signatures.Add(signer.CardId, signature);
            }
        }
    }
}