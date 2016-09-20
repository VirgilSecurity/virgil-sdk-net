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
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;

    using Newtonsoft.Json;

    using Virgil.SDK.Client.Models;

    public class RegistrationRequest : SigningRequest
    {
        protected CardSigningRequestModel model;
        private byte[] requestData;

        /// <summary>
        /// Prevents a default instance of the <see cref="RegistrationRequest"/> class from being created.
        /// </summary>
        protected RegistrationRequest()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationRequest"/> class.
        /// </summary>
        public RegistrationRequest
        (
            string identity, 
            string identityType, 
            byte[] publicKey,
            IDictionary<string, string> data = null
        )
        {
            this.model = new CardSigningRequestModel
            {   
                Identity = identity,
                IdentityType = identityType,
                PublicKey = publicKey,
                Data = data ?? new Dictionary<string, string>()
            };

            this.Data = new ReadOnlyDictionary<string, string>(this.model.Data);
        }

        /// <summary>
        /// Gets or sets the identity.
        /// </summary>
        public string Identity => this.model.Identity;

        /// <summary>
        /// Gets or sets the type of the identity.
        /// </summary>
        public string IdentityType => this.model.IdentityType;

        /// <summary>
        /// Gets or sets the public key.
        /// </summary>
        public byte[] PublicKey => this.model.PublicKey;

        /// <summary>
        /// Gets the custom data.
        /// </summary>
        public IReadOnlyDictionary<string, string> Data { get; private set; }

        public override byte[] ToRequestData()
        {
            if (this.requestData == null)
            {
                var json = JsonConvert.SerializeObject(this.model);
                this.requestData = Encoding.UTF8.GetBytes(json);
            }

            return this.requestData;
        }   

        internal static RegistrationRequest FromRequestData(byte[] requestData)
        {
            var json = Encoding.UTF8.GetString(requestData);
            var model = JsonConvert.DeserializeObject<CardSigningRequestModel>(json);

            var request = new RegistrationRequest
            {
                model = model,
                requestData = requestData 
            };
                
            return request;
        }
    }
}