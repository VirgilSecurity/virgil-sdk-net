namespace Virgil.SDK.PrivateKeys.Clients
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Virgil.SDK.PrivateKeys.Model;

    /// <summary>
    /// Provides common methods to interact with Account (of Private Keys API) resource endpoints.
    /// </summary>
    public interface IPrivateKeysAccountsClient
    {
        /// <summary>
        /// Creates the Private Keys storage account.
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
        /// <param name="publicKeyId">The public key ID from Keys service.</param>
        /// <param name="newPassword">New Private Keys account password.</param>
        Task ResetPassword(Guid publicKeyId, string newPassword);

        /// <summary>
        /// Verifies the Private Keys account password reset.
        /// </summary>
        /// <param name="publicKeyId">The public key ID from Keys service.</param>
        /// <param name="token">The confirmation token.</param>
        /// <returns>The list of account's private keys.</returns>
        Task<IEnumerable<PrivateKey>> VerifyResetPassword(Guid publicKeyId, string token);
    }
}