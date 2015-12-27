﻿namespace Virgil.SDK.Keys.Clients
{
    using System;
    using System.Threading.Tasks;

    using Virgil.SDK.Keys.Helpers;
    using Virgil.SDK.Keys.Http;
    using Virgil.SDK.Keys.Infrastructure;
    using Virgil.SDK.Keys.TransferObject;

    /// <summary>
    /// Provides common methods for validating and authorization a different types of identities.
    /// </summary>
    public class IdentityClient : EndpointClient, IIdentityClient
    {
        private readonly IServiceKeyCache cache;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityClient"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="cache">The cache.</param>
        public IdentityClient(IConnection connection, IServiceKeyCache cache) : base(connection)
        {
            this.cache = cache;
            this.EndpointPublicKeyId = KnownKeyIds.IdentityService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityClient"/> class.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="baseUri">The base URI.</param>
        public IdentityClient(string accessToken, string baseUri = ApiConfig.IdentityServiceAddress)
            :base(new IdentityConnection(new Uri(baseUri)))
        {
            this.cache = new ServiceKeyCache(new PublicKeysClient(accessToken));
            this.EndpointPublicKeyId = KnownKeyIds.IdentityService;
        }

        /// <summary>
        /// Sends the request for identity verification, that's will be processed depending of specified type.
        /// </summary>
        /// <param name="identityValue">An unique string that represents identity.</param>
        /// <param name="type">The type of identity.</param>
        /// <returns>An instance of <see cref="IndentityTokenDto"/> response.</returns>
        /// <remarks>
        /// Use method <see cref="Confirm(Guid, string, int, int)" /> to confirm and get the indentity token.
        /// </remarks>
        public async Task<VirgilVerifyResponse> Verify(string identityValue, IdentityType type)
        {
            Ensure.ArgumentNotNull(identityValue, nameof(identityValue));

            var body = new
            {
                type = type,
                value = identityValue,
            };

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .WithEndpoint("v1/verify");

            return await this.Send<VirgilVerifyResponse>(request);
        }

        /// <summary>
        /// Confirms the identity using confirmation code, that has been generated to confirm an identity.
        /// </summary>
        /// <param name="actionId">The action identifier.</param>
        /// <param name="confirmationCode">The confirmation code.</param>
        /// <param name="timeToLive">The time to live.</param>
        /// <param name="countToLive">The count to live.</param>
        /// <returns></returns>
        public async Task<IndentityTokenDto> Confirm(Guid actionId, string confirmationCode, int timeToLive = 3600, int countToLive = 1)
        {
            Ensure.ArgumentNotNull(confirmationCode, nameof(confirmationCode));

            var body = new
            {
                confirmation_code = confirmationCode,
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

            return await this.Send<IndentityTokenDto>(request);
        }

        /// <summary>
        /// Returns true if validation token is valid.
        /// </summary>
        /// <param name="type">The type of identity.</param>
        /// <param name="value">The identity value.</param>
        /// <param name="validationToken">The validation token.</param>
        public async Task<bool> IsValid(IdentityType type, string value, string validationToken)
        {
            Ensure.ArgumentNotNull(value, nameof(value));
            Ensure.ArgumentNotNull(validationToken, nameof(validationToken));

            var request = Request.Create(RequestMethod.Post)
                .WithBody(new
                {
                    type = type,
                    value = value,
                    validation_token = validationToken
                })
                .WithEndpoint("v1/validate");

            try
            {
                await this.Send(request);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Returns true if validation token is valid.
        /// </summary>
        /// <param name="token">The identity token DTO that represent Identity and it's type.</param>
        public Task<bool> IsValid(IndentityTokenDto token)
        {
            Ensure.ArgumentNotNull(token, nameof(token));
            return this.IsValid(token.Type, token.Value, token.ValidationToken);
        }

        /// <summary>
        /// Performs an asynchronous HTTP request.
        /// </summary>
        /// <param name="request">The instance of request to send.</param>
        /// <returns></returns>
        protected override async Task<IResponse> Send(IRequest request)
        {
            var result = await base.Send(request);
            var key = await this.cache.GetServiceKey(this.EndpointPublicKeyId);
            this.VerifyResponse(result, key.PublicKey);
            return result;
        }
    }
}