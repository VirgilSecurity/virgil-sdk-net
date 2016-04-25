namespace Virgil.SDK.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Virgil.SDK.Models;

    /// <summary>
    /// Interface that specifies communication with Virgil Security Identity service.
    /// </summary>
    public interface IIdentityClient : IVirgilService
    {
        /// <summary>
        /// Sends the request for identity verification, that's will be processed depending of specified type.
        /// </summary>
        /// <param name="identityValue">An unique string that represents identity.</param>
        /// <param name="identityType">The identity type that going to be verified.</param>
        /// <param name="extraFields"></param>
        /// <remarks>
        /// Use method <see cref="Confirm(Guid, string, int, int)" /> to confirm and get the indentity token.
        /// </remarks>
        Task<IdentityVerificationResponse> Verify(string identityValue, VerifiableIdentityType identityType, IDictionary<string, string> extraFields);

        /// <summary>
        /// Confirms the identity using confirmation code, that has been generated to confirm an identity.
        /// </summary>
        /// <param name="actionId">The action identifier.</param>
        /// <param name="code">The confirmation code.</param>
        /// <param name="timeToLive">The time to live.</param>
        /// <param name="countToLive">The count to live.</param>
        Task<IdentityConfirmationResponse> Confirm(Guid actionId, string code, int timeToLive = 3600, int countToLive = 1);

        /// <summary>
        /// Checks whether the validation token is valid for specified identity.
        /// </summary>
        /// <param name="identityValue">The type of identity.</param>
        /// <param name="identityType">The identity value.</param>
        /// <param name="validationToken">
        /// The string value that represents validation token for Virgil Identity Service.
        /// </param>
        Task<bool> IsValid(string identityValue, VerifiableIdentityType identityType, string validationToken);

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
        Task<IEmailVerifier> VerifyEmail(string emailAddress, IDictionary<string, string> extraFields = null);
    }
}