namespace Virgil.SDK.PrivateKeys.Clients
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Virgil.SDK.PrivateKeys.Model;

    /// <summary>
    /// Provides common methods to interact with Account (of Private Keys API) resource endpoints.
    /// </summary>
    public interface IContainerClient
    {
        /// <summary>
        /// Gets the specified account by identifier.
        /// </summary>
        /// <param name="accountId">The account identifier.</param>
        Task<PrivateKeysAccount> Get(Guid accountId);

        /// <summary>
        /// Creates an account to store Private Keys.
        /// </summary>
        /// <param name="accountId">The account ID from Keys service.</param>
        /// <param name="accountType">The account ID type.</param>
        /// <param name="publicKeyId">The public key ID from Keys service.</param>
        /// <param name="sign">The public key ID digital signature. Verifies the possession of the private key.</param>
        /// <param name="password">The account password.</param>
        Task Create(Guid accountId, PrivateKeysAccountType accountType, Guid publicKeyId, byte[] sign, string password);
        
        /// <summary>
        /// Removes account from Private Keys Service.
        /// </summary>
        /// <param name="accountId">The account ID.</param>
        /// <param name="publicKeyId">The public key ID.</param>
        /// <param name="sign">The public key ID digital signature. Verifies the possession of the private key.</param>
        Task Remove(Guid accountId, Guid publicKeyId, byte[] sign);

        /// <summary>
        /// Resets the account password.
        /// </summary>
        /// <param name="userId">The user ID from Keys service.</param>
        /// <param name="newPassword">New Private Keys account password.</param>
        Task ResetPassword(string userId, string newPassword);

        /// <summary>
        /// Verifies the Private Keys account password reset.
        /// </summary>
        /// <param name="token">The confirmation token.</param>
        /// <returns>The list of account's private keys.</returns>
        Task VerifyResetPassword(string token);
    }
}