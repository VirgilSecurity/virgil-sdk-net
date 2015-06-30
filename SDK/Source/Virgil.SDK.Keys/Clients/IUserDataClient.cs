namespace Virgil.SDK.Keys.Clients
{
    using System;
    using System.Threading.Tasks;

    using Virgil.SDK.Keys.Models;

    /// <summary>
    /// Provides common methods to interact with UserData resource endpoints.
    /// </summary>
    public interface IUserDataClient
    {
        /// <summary>
        /// Returns a user data specified by user data ID.
        /// </summary>
        /// <param name="userDataId">The user data ID.</param>
        /// <returns>An <see cref="UserData"/></returns>
        Task<UserData> Get(Guid userDataId);

        /// <summary>
        ///     Inserts new user data to exiasting certificate given certificate ID and user data.
        /// </summary>
        /// <param name="publicKeyId">The public key ID</param>
        /// <param name="userData">The user data</param>
        /// <returns>An just created <see cref="UserData" /></returns>
        Task<UserData> Insert(Guid publicKeyId, UserData userData);

        /// <summary>
        ///     Sends confirmation code to activate user data specified by it ID.
        /// </summary>
        /// <param name="userDataId">The user data identifier.</param>
        /// <param name="confirmationCode">The confirmation code.</param>
        /// <returns></returns>
        Task Confirm(Guid userDataId, string confirmationCode);

        /// <summary>
        ///     Requests another confirmation code to be sent to the user email.
        /// </summary>
        /// <param name="userDataId">The user data identifier.</param>
        /// <returns></returns>
        Task ResendConfirmation(Guid userDataId);
    }
}