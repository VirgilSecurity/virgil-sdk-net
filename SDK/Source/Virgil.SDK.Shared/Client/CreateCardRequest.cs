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

    public class CreateCardRequest : SignedRequest
    {
        protected CardRequestModel model;
        private IReadOnlyDictionary<string, string> data;

        /// <summary>
        /// Prevents a default instance of the <see cref="CreateCardRequest"/> class from being created.
        /// </summary>
        private CreateCardRequest()
        {
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
        public IReadOnlyDictionary<string, string> Data
        {
            get
            {
                if (this.data == null)
                {
                    this.data = new ReadOnlyDictionary<string, string>(this.model.Data);
                }
                
                return this.data;
            }
        }

        /// <summary>
        /// Gets the device identifier.
        /// </summary>
        public string Device => this.model?.Info.Device;

        /// <summary>
        /// Gets the device name.
        /// </summary>
        public string DeviceName => this.model?.Info.DeviceName;

        public static CreateCardRequest Create(CardRequestModel model)
        {
            var creationRequest = new CreateCardRequest
            {
                model = model,
                CanonicalRequest = GetCanonicalForm(model)
            };

            return creationRequest; 
        }

        public static CreateCardRequest Create
        (
            string identity, 
            string identityType,
            byte[] publicKey,
            IDictionary<string, string> data = null
        )
        {
            var model = new CardRequestModel
            {
                Identity = identity,
                IdentityType = identityType,
                PublicKey = publicKey,
                Data = data,
                Scope = CardScope.Application
            };

            var creationRequest = new CreateCardRequest
            {
                model = model,
                CanonicalRequest = GetCanonicalForm(model)
            };

            return creationRequest;
        }

        public static CreateCardRequest CreateGlobal
        (
            string identity,
            byte[] publicKey,
            GlobalIdentityType identityType = GlobalIdentityType.Email,
            IDictionary<string, string> data = null
        )
        {
            var model = new CardRequestModel
            {
                Identity = identity,
                IdentityType = identityType.ToString().ToLower(),
                PublicKey = publicKey,
                Data = data,
                Scope = CardScope.Global
            };
            
            var creationRequest = new CreateCardRequest
            {
                model = model,
                CanonicalRequest = GetCanonicalForm(model)
            };

            return creationRequest;
        }
        
        private static byte[] GetCanonicalForm(CardRequestModel model)
        {
            var json = JsonConvert.SerializeObject(model);
            var canonicalForm = Encoding.UTF8.GetBytes(json);

            return canonicalForm;
        }
    }
}