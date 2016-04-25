namespace Virgil.SDK.Identities
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Virgil.SDK.Exceptions;

    /// <summary>
    /// Represents an implementation of <see cref="IIdentityBuilder"/> which support to initialize email identity. 
    /// </summary>
    internal class EmailIdentityBuilder : IEmailIdentityBuilder
    {
        private readonly IIdentityClient identityClient;
        private Guid? actionId;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailIdentityBuilder"/> class.
        /// </summary>
        public EmailIdentityBuilder(string identityValue, IIdentityClient identityClient)
        {
            this.Value = identityValue;
            this.Type = IdentityType.Email;

            this.identityClient = identityClient;
        }

        /// <summary>
        /// Gets the identity value.
        /// </summary>
        private string Value { get; }

        /// <summary>
        /// Gets the identity type.
        /// </summary>
        private IdentityType Type { get; }

        /// <summary>
        /// Gets the validation token.
        /// </summary>
        private string ValidationToken { get; set; }

        /// <summary>
        /// Initiates a process to verify the email address and confirm an identity.
        /// </summary>
        /// <param name="extraFields">
        /// In some cases it could be necessary to pass some parameters and receive them back 
        /// in an email. For this special case an optional <c>extraFields</c> dictionary can be used. 
        /// All values passed in <c>extraFields</c> parameter will be passed back in an email 
        /// in a hidden form with extra hidden fields.
        /// </param>
        /// <remarks>
        /// Use method <see cref="Confirm"/> to confirm building identity.
        /// </remarks>
        public async Task Verify(Dictionary<string, string> extraFields = null)
        {
            var verificationResponse = await this.identityClient.Verify(this.Value, VerifiableIdentityType.Email, extraFields);
            this.actionId = verificationResponse.ActionId;
        }

        /// <summary>
        /// Confirms the identity using confirmation code, that has been generated to confirm an identity.
        /// </summary>
        /// <param name="code">The confirmation code that was recived on email box.</param>
        /// <param name="timeToLive">
        /// The parameter is used to limit the lifetime of the token in seconds 
        /// (maximum value is 60 * 60 * 24 * 365 = 1 year)
        /// </param>
        /// <param name="countToLive">
        /// The parameter is used to restrict the number of token 
        /// usages (maximum value is 100)
        /// </param>
        /// <exception cref="VerificationRequestIsNotSentException"></exception>
        public async Task Confirm(string code, int timeToLive = 3600, int countToLive = 1)
        {
            if (!this.actionId.HasValue)
            {
                throw new VerificationRequestIsNotSentException();
            }

            var confirmation = await this.identityClient.Confirm(this.actionId.Value, code, timeToLive, countToLive);
            this.ValidationToken = confirmation.ValidationToken;
        }
        
        /// <summary>
        /// Gets the built identity.
        /// </summary>
        public IdentityInfo GetIdentity()
        {
            return new IdentityInfo(this.Value, this.Type, this.ValidationToken);
        }
    }
}