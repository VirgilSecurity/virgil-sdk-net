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
using System;
using System.Text;
using System.Threading.Tasks;
using Virgil.SDK.Client.Http;
using Virgil.SDK.Client.Models;
using Virgil.SDK.Shared.Client.Models;

namespace Virgil.SDK.Client
{
 
    public class AuthClient : VirgilClient
    {
        private readonly Lazy<IConnection> authLazyConnection;
        private IConnection AuthConnection => this.connection ?? this.authLazyConnection.Value;

        private readonly AuthClientParams parameters;

        public AuthClient(AuthClientParams parameters)
        {
            this.parameters = parameters;

            this.authLazyConnection = new Lazy<IConnection>(this.InitializeAuthConnection);
        }

        public AuthClient(AuthServiceConnection connection)
        {
            this.connection = connection;
        }


        public AuthClient() : this(new AuthClientParams())
        {

        }
        
        public async Task<ChallengeMessageModel> GetChallengeMessageAsync(string appId)
        {
            var body = new
            {
                resource_owner_virgil_card_id = appId
            };

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .WithEndpoint("/v4/authorization-grant/actions/get-challenge-message");

            var response = await this.SendAsync(AuthConnection, request).ConfigureAwait(false);
            var result = response.Parse<ChallengeMessageModel>();

            return result;
        }
        
        public async Task<AsknowledgeModel> AsknowledgeAsync(string authGrantId, byte[] reEncryptedMessage)
        {
            var body = new
            {
                encrypted_message = Convert.ToBase64String(reEncryptedMessage)
            };

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .WithEndpoint($"/v4/authorization-grant/{authGrantId}/actions/acknowledge");

            var response = await this.SendAsync(AuthConnection, request).ConfigureAwait(false);
            var result = response.Parse<AsknowledgeModel>();

            return result;
        }
        
        public async Task<AccessTokenModel> ObtainAccessTokenAsync(string accessCode)
        {
            var body = new
            {
                grant_type = "access_code",
                code = accessCode
            };

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .WithEndpoint($"/v4/authorization/actions/obtain-access-token");

            var response = await this.SendAsync(AuthConnection, request).ConfigureAwait(false);
            var result = response.Parse<AccessTokenModel>();

            return result;
        }
        
        public async Task<RefreshTokenModel> RefreshAccessTokenAsync(string refreshToken)
        {
            var body = new
            {
                grant_type = "refresh_token",
                code = refreshToken
            };

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .WithEndpoint($"/v4/authorization/actions/refresh-access-token");

            var response = await this.SendAsync(AuthConnection, request).ConfigureAwait(false);
            var result = response.Parse<RefreshTokenModel>();

            return result;
        }

        public async Task<VerifyAccessTokenModel> VerifyAccessTokenAsync(string accessToken)
        {
            var body = new
            {
                access_token = accessToken
            };

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .WithEndpoint($"/v4/authorization/actions/verify");

            var response = await this.SendAsync(AuthConnection, request).ConfigureAwait(false);
            var result = response.Parse<VerifyAccessTokenModel>();

            return result;
        }

        #region
        private IConnection InitializeAuthConnection()
        {
            var baseUrl = new Uri(this.parameters.AuthServiceAddress);
            return new AuthServiceConnection(baseUrl);
        }
        #endregion
    }
}
