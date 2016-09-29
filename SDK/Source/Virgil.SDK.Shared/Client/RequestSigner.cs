#region Copyright (C) Virgil Security Inc.
// Copyright (C) 2015-2016 Virgil Security Inc.
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

namespace Virgil.SDK.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Virgil.SDK.Client.Models;
    using Virgil.SDK.Cryptography;
    
    public class RequestSigner<TRequest> where TRequest : ClientRequest
    {
        private readonly Crypto crypto; 
        private Dictionary<string, byte[]> signs;
            
        protected internal RequestSigner(Crypto crypto)
        {
            this.crypto = crypto;
            this.signs = new Dictionary<string, byte[]>();
        }
        
        /// <summary>
        /// Gets the canonical request form.
        /// </summary>
        public byte[] RequestSnapshot { get; protected set; }
        
        /// <summary>
        /// Gets the signs.
        /// </summary>
        public IReadOnlyDictionary<string, byte[]> Signs => this.signs;

        /// <summary>
        /// Initializes the request given specified request details.
        /// </summary>
        public void Initialize(TRequest details)
        {
            this.RequestSnapshot = this.MakeSnapshot(details);
        }

        /// <summary>
        /// Initializes the request given exported request.
        /// </summary>
        public void Initialize(string exportedRequest)
        {
            var jsonModel = Encoding.UTF8.GetString(Convert.FromBase64String(exportedRequest));
            var model = JsonSerializer.Deserialize<SignedRequest>(jsonModel);
            
            this.RequestSnapshot = model.ContentSnapshot;
            this.signs = model.Meta.Signs;
        }

        /// <summary>
        /// Exports this request to it's string representation.
        /// </summary>
        public string Export()
        {
            var requestModel = new SignedRequest
            {
                ContentSnapshot = this.RequestSnapshot,
                Meta = new SignedRequestMeta
                {
                    Signs = this.Signs.ToDictionary(it => it.Key, it => it.Value)
                }
            };

            var json = JsonSerializer.Serialize(requestModel);
            var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));

            return base64;
        }

        /// <summary>
        /// Gets the request details.
        /// </summary>
        public TRequest GetRequestDetails()
        {
            var jsonDetails = Encoding.UTF8.GetString(this.RequestSnapshot);
            var details = JsonSerializer.Deserialize<TRequest>(jsonDetails);

            return details;
        }

        /// <summary>
        /// Signs the request using owner's <see cref="PrivateKey"/>.
        /// </summary>
        public void SelfSign(PrivateKey privateKey)
        {
            var fingerprint = this.crypto.CalculateFingerprint(this.RequestSnapshot);
            var signature = this.crypto.Sign(fingerprint.GetValue(), privateKey);

            this.AppendSignature(fingerprint.ToHEX(), signature);
        }

        /// <summary>
        /// Signs the request using third party authority's <see cref="PrivateKey" />.
        /// </summary>
        /// <param name="cardId">The authority's Virgil Card fingerprint.</param>
        /// <param name="privateKey">The authority's private key.</param>
        public void Sign(string cardId, PrivateKey privateKey)
        {
            var fingerprint = this.crypto.CalculateFingerprint(this.RequestSnapshot);

            var signature = this.crypto.Sign(fingerprint.GetValue(), privateKey);
            this.AppendSignature(cardId, signature);
        }

        /// <summary>
        /// Appends the signature of request fingerprint.
        /// </summary>
        public void AppendSignature(string cardId, byte[] signature)
        {
            if (string.IsNullOrWhiteSpace(cardId))
                throw new ArgumentException(Localization.ExceptionArgumentIsNullOrWhitespace, nameof(cardId));

            if (signature == null)
                throw new ArgumentNullException(nameof(signature));

            this.signs.Add(cardId, signature);
        }

        protected byte[] MakeSnapshot(TRequest details)
        {
            var json = JsonSerializer.Serialize(details);
            var snapshot = Encoding.UTF8.GetBytes(json);

            return snapshot;
        }

        public SignedRequest Build()
        {
            var requestModel = new SignedRequest
            {
                ContentSnapshot = this.RequestSnapshot,
                Meta = new SignedRequestMeta
                {
                    Signs = this.Signs.ToDictionary(it => it.Key, it => it.Value)
                }
            };

            return requestModel;
        }
    }
}