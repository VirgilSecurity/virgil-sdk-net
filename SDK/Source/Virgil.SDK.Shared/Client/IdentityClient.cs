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
    using System.Threading.Tasks;
    using Shared.Client.TransferObjects;

    using Http;
    using Exceptions;

    public class IdentityClient : VirgilClient
    {
        private readonly Lazy<IConnection> identityConnection;
        private IConnection IdentityConnection => this.identityConnection.Value;
        private readonly IdentityClientParams parameters;


        /// <summary>
        /// Initializes a new instance of the <see cref="CardsClient"/> class.
        /// </summary>  
        public IdentityClient() : this(new IdentityClientParams())
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="CardsClient"/> class.
        /// </summary>
        public IdentityClient(IdentityClientParams parameters)
        {
            this.parameters = parameters;
            this.identityConnection = new Lazy<IConnection>(this.InitializeIdentityConnection);
        }


        /// <summary>
        /// Sends the request for identity verification, that's will be processed depending of specified type.
        /// </summary>
        /// <param name="identity">An unique string that represents identity.</param>
        /// <param name="extraFields">The extra fields.</param>
        /// <returns>The action identifier that is required for confirmation the identity.</returns>
        /// <remarks>
        /// Use method <see cref="ConfirmIdentityAsync" /> to confirm and get the indentity token.
        /// </remarks>
        public async Task<Guid> VerifyEmailAsync
        (
            string identity,
            IDictionary<string, string> extraFields = null
        )
        {
            var body = new
            {
                type = IdentityType.Email,
                value = identity,
                extra_fields = extraFields
            };

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .WithEndpoint("v1/verify");

            var response = await this.SendAsync(IdentityConnection, request).ConfigureAwait(false);
            var result = response.Parse<IdentityVerificationResponseModel>();

            return result.ActionId;
        }

        /// <summary>
        /// Confirms the identity using confirmation code, that has been generated to confirm an identity.
        /// </summary>
        /// <param name="actionId">The action identifier that was obtained on verification step.</param>
        /// <param name="code">The confirmation code that was recived on email box.</param>
        /// <param name="timeToLive">The time to live.</param>
        /// <param name="countToLive">The count to live.</param>
        /// <returns>A string that represent an identity validattion token.</returns>
        public async Task<string> ConfirmEmailAsync(Guid actionId, string code, int timeToLive = 3600, int countToLive = 1)
        {
            var body = new
            {
                confirmation_code = code,
                action_id = actionId,
                token = new
                {
                    time_to_live = timeToLive,
                    count_to_live = countToLive
                }
            };

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .WithEndpoint("v1/confirm");

            var response = await this.SendAsync(IdentityConnection, request).ConfigureAwait(false);
            var result = response.Parse<IdentityConfirmationResponseModel>();

            return result.ValidationToken;
        }

        /// <summary>
        /// Returns true if validation token is valid.
        /// </summary>
        /// <param name="identityValue">The type of identity.</param>
        /// <param name="validationToken">The validation token.</param>
        public async Task<bool> ValidateTokenAsync(string identityValue, string validationToken)
        {
            var request = Request.Create(RequestMethod.Post)
                .WithBody(new
                {
                    value = identityValue,
                    type = IdentityType.Email,
                    validation_token = validationToken
                })
                .WithEndpoint("v1/validate");

            var response = await this.SendAsync(IdentityConnection, request, true).ConfigureAwait(false);
            return response.StatusCode == 400;
        }

        #region Private Methods

        private IConnection InitializeIdentityConnection()
        {
            var baseUrl = new Uri(this.parameters.IdentityServiceAddress);
            return new IdentityServiceConnection(baseUrl);
        }


        #endregion
    }
}
