namespace Virgil.SDK.Keys.Clients
{
    using System;
    using System.Threading.Tasks;

    using Model;

    /// <summary>
    /// Provides common methods to interact with UserData resource endpoints.
    /// </summary>
    public interface IUserDataClient
    {
        /// <summary>
        /// Deletes the specified user data byt its identifier.
        /// </summary>
        /// <param name="userDataId">The user data identifier.</param>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <param name="privateKey">The private key value. Private key is not being sent, but used to sign the HTTP request body.</param>
        /// <returns><see cref="UserData"/></returns>
        Task<UserData> Delete(Guid userDataId, Guid publicKeyId, byte[] privateKey);

        /// <summary>
        /// Adds the specified user data to known public key.
        /// </summary>
        /// <param name="userData">The <see cref="UserData"/> object.</param>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <param name="privateKey">The private key value. Private key is not being sent, but used to sign the HTTP request body.</param>
        /// <returns><see cref="UserData"/></returns>
        Task<UserData> Insert(UserData userData, Guid publicKeyId, byte[] privateKey);

        /// <summary>
        /// Confirms the specified user data.
        /// Unless confirmed user data stored on server would not show up in search requests.
        /// On public key creation, public keys server will send confirmation code to the specified user id.
        /// </summary>
        /// <param name="userDataId">The user data identifier.</param>
        /// <param name="confirmationCode">The confirmation code.</param>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <param name="privateKey">The private key valye. Private key is not being sent, but used to sign the HTTP request body.</param>
        Task Confirm(Guid userDataId, string confirmationCode, Guid publicKeyId, byte[] privateKey);

        /// <summary>
        /// Ask server to generate new confirmation code in the case when previous was lost or not delivered.
        /// </summary>
        /// <param name="userDataId">The user data identifier.</param>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <param name="privateKey">The private key valye. Private key is not being sent, but used to sign the HTTP request body.</param>
        Task ResendConfirmation(Guid userDataId, Guid publicKeyId, byte[] privateKey);
    }
}