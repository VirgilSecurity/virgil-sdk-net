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
    
    using Virgil.SDK.Client;
    using Virgil.SDK.Utils;
    using Virgil.Crypto.Interfaces;

    public class RequestManager
    {
        private readonly ICrypto crypto;

        public RequestManager(ICrypto crypto)
        {
            this.crypto = crypto;
        }

        public CardRequest CreateCardRequest(CreateCardParams @params)
        {
            if (@params == null)
            {
                throw new ArgumentNullException(nameof(@params));
            }

            if (string.IsNullOrWhiteSpace(@params.Identity))
            {
                throw new ArgumentException($"{@params.Identity} property is mandatory");
            }

            if (@params.KeyPair == null)
            {
                throw new ArgumentException($"{@params.KeyPair} property is mandatory");
            }

            var identityType = string.IsNullOrWhiteSpace(@params.IdentityType) ? "unknown" : @params.IdentityType;
            
            var snapshotModel = new CardRawSnapshot
            {
                Identity = @params.Identity,
                IdentityType = identityType,
                PublicKeyBytes = this.crypto.ExportPublicKey(@params.KeyPair.PublicKey),
                CustomFields = @params.CustomFields,
                Scope = "application"
            };

            var snapshotter = ServiceLocator.GetService<ISnapshotter>();
            var snapshot = snapshotter.Capture(snapshotModel); 
            var request = new CardRequest
            {
                ContentSnapshot = snapshot, 
                Meta = new CardRequestMeta
                {
                    Signatures = new Dictionary<string, byte[]>()
                }
            };
            
            var signers = @params.RequestSigners == null 
                ? new List<CardSigner>() 
                : new List<CardSigner>(@params.RequestSigners) ;

            if (@params.IncludeSelfSignature)
            {
                var idGenerator = ServiceLocator.GetService<ICardIdGenerator>();
                var cardId = idGenerator.Generate(crypto, snapshot);
                
                signers.Insert(0, new CardSigner { CardId = cardId, PrivateKey = @params.KeyPair.PrivateKey });
            }
            
            SignRequest(request, signers.ToArray());
            
            return request;
        }
        
        public CardRequest RevokeCardRequest(RevokeCardParams @params)
        {
            if (@params == null)
            {
                throw new ArgumentNullException(nameof(@params));
            }

            if (string.IsNullOrWhiteSpace(@params.CardId))
            {
                throw new ArgumentException($"{@params.CardId} property is mandatory");
            }
            
            if (@params.RequestSigners == null)
            {
                throw new ArgumentException($"{@params.RequestSigners} property is mandatory");
            }
            
            if (!@params.RequestSigners.Any())
            {
                throw new ArgumentException($"An {@params.RequestSigners} should contains at least 1 signer");
            }
            
            var snapshotter = ServiceLocator.GetService<ISnapshotter>();
            var snapshotModel = new 
            {
                card_id = @params.CardId,
                revocation_reason = "unspecified"
            };
            var snapshot = snapshotter.Capture(snapshotModel);
            var request = new RevokeCardRequest
            {
                CardId = @params.CardId,
                ContentSnapshot = snapshot, 
                Meta = new CardRequestMeta
                {
                    Signatures = new Dictionary<string, byte[]>()
                }
            };
            
            SignRequest(request, @params.RequestSigners.ToArray());
            return request;
        }

        public string ExportRequest(CardRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request)); 
            }

            var serializer = ServiceLocator.GetService<ISerializer>();
            
            var requestJson = serializer.Serialize(request);
            var requestBytes = BytesConvert.FromString(requestJson);
            var requestBase64 = BytesConvert.ToString(requestBytes, StringEncoding.BASE64);

            return requestBase64;
        }
        
        public TRequest ImportRequest<TRequest>(string exportedRequest) where TRequest : CardRequest
        {
            if (string.IsNullOrWhiteSpace(exportedRequest))
            {
                throw new ArgumentNullException();
            }
            
            var serializer = ServiceLocator.GetService<ISerializer>();

            var requestJsonBytes = BytesConvert.FromString(exportedRequest, StringEncoding.BASE64);
            var requestJson = BytesConvert.ToString(requestJsonBytes);
            var request = serializer.Deserialize<TRequest>(requestJson);

            return request;
        }

        public void SignRequest(CardRequest request, params CardSigner[] signers)
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