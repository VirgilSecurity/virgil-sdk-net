namespace Virgil.SDK.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Virgil.SDK.Models;
    using Virgil.SDK.Helpers;
    using Virgil.SDK.Http;

    /// <summary>
    /// Provides common methods for validating and authorization a different types of identities.
    /// </summary>
    internal class IdentityClient : ResponseVerifyClient, IIdentityClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityClient"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="cache">The cache.</param>
        public IdentityClient(IConnection connection, IServiceKeyCache cache) : base(connection, cache)
        {
            this.EndpointApplicationId = ServiceIdentities.IdentityService;
        }
        
        /// <summary>
        /// Sends the request for identity verification, that's will be processed depending of specified type.
        /// </summary>
        /// <param name="identityValue">An unique string that represents identity.</param>
        /// <param name="identityType">The type of identity.</param>
        /// <param name="extraFields"></param>
        /// <remarks>
        /// Use method <see cref="Confirm(Guid, string, int, int)" /> to confirm and get the indentity token.
        /// </remarks>
        public async Task<IdentityVerificationResponse> Verify(string identityValue, VerifiableIdentityType identityType, IDictionary<string, string> extraFields)
        {
            Ensure.ArgumentNotNull(identityValue, nameof(identityValue));

            var body = new
            {
                type = identityType,
                value = identityValue,
                extra_fields = extraFields
            };

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .WithEndpoint("v1/verify");

            return await this.Send<IdentityVerificationResponse>(request).ConfigureAwait(false);
        }

        /// <summary>
        /// Confirms the identity using confirmation code, that has been generated to confirm an identity.
        /// </summary>
        /// <param name="actionId">The action identifier that was obtained on verification step.</param>
        /// <param name="code">The confirmation code that was recived on email box.</param>
        /// <param name="timeToLive">
        /// The parameter is used to limit the lifetime of the token in seconds 
        /// (maximum value is 60 * 60 * 24 * 365 = 1 year)
        /// </param>
        /// <param name="countToLive">
        /// The parameter is used to restrict the number of token 
        /// usages (maximum value is 100)
        /// </param>
        public async Task<IdentityConfirmationResponse> Confirm(Guid actionId, string code, int timeToLive = 3600, int countToLive = 1)
        {
            Ensure.ArgumentNotNull(code, nameof(code));

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

            return await this.Send<IdentityConfirmationResponse>(request).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Returns true if validation token is valid.
        /// </summary>
        /// <param name="identityValue">The type of identity.</param>
        /// <param name="identityType">The identity value.</param>
        /// <param name="validationToken">The validation token.</param>
        public async Task<bool> IsValid(string identityValue, VerifiableIdentityType identityType, string validationToken)
        {
            Ensure.ArgumentNotNull(identityValue, nameof(identityValue));
            Ensure.ArgumentNotNull(validationToken, nameof(validationToken));

            var request = Request.Create(RequestMethod.Post)
                .WithBody(new
                {
                    value = identityValue,
                    type = identityType,
                    validation_token = validationToken
                })
                .WithEndpoint("v1/validate");

            try
            {
                await this.Send(request).ConfigureAwait(false);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        /// <summary>
        /// Initiates a process to verify a specified email address.
        /// </summary>
        /// <param name="emailAddress">
        /// The email address you are going to verify.
        /// </param>
        /// <param name="extraFields">
        /// In some cases it could be necessary to pass some parameters and receive them back 
        /// in an email. For this special case an optional <c>extraFields</c> dictionary can be used. 
        /// All values passed in <c>extraFields</c> parameter will be passed back in an email 
        /// in a hidden form with extra hidden fields.
        /// </param>
        /// <returns>The verification identuty class</returns>
        public async Task<IEmailVerifier> VerifyEmail(string emailAddress, IDictionary<string, string> extraFields = null)
        {
            var response = await this.Verify(emailAddress, VerifiableIdentityType.Email, extraFields);
            var emailVerifier = new EmailVerifier(response.ActionId, this);

            return emailVerifier;
        }
    }
}