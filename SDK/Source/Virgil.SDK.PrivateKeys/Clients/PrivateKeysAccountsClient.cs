namespace Virgil.SDK.PrivateKeys.Clients
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    
    using Virgil.SDK.PrivateKeys.Model;
    using Virgil.SDK.PrivateKeys.Http;
    using Virgil.SDK.PrivateKeys.TransferObject;

    /// <summary>
    /// Provides common methods to interact with Account (of Private Keys API) resource endpoints.
    /// </summary>
    public class PrivateKeysAccountsClient : EndpointClient, IPrivateKeysAccountsClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateKeysAccountsClient"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public PrivateKeysAccountsClient(IConnection connection) : base(connection)
        {
        }

        /// <summary>
        /// Creates the Private Keys storage account.
        /// </summary>
        /// <param name="accountId">The account ID from Keys service.</param>
        /// <param name="accountType">The account ID type.</param>
        /// <param name="publicKeyId">The public key ID from Keys service.</param>
        /// <param name="sign">The public key ID digital signature. Verifies the possession of the private key.</param>
        /// <param name="password">The account password.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task Create(Guid accountId, PrivateKeysAccountType accountType, Guid publicKeyId, byte[] sign, string password)
        {
            var body = new
            {
                account_id = accountId,
                account_type = accountType == PrivateKeysAccountType.Easy ? "easy" : "normal",
                public_key_id = publicKeyId,
                sign,
                password
            };

            await this.Post<CreateAccountResult>("account", body);
        }

        /// <summary>
        /// Removes account from Private Keys Service.
        /// </summary>
        /// <param name="accountId">The account ID.</param>
        /// <param name="publicKeyId">The public key ID.</param>
        /// <param name="sign">The public key ID digital signature. Verifies the possession of the private key.</param>
        public Task Remove(Guid accountId, Guid publicKeyId, byte[] sign)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Resets the account password.
        /// </summary>
        /// <param name="publicKeyId">The public key ID from Keys service.</param>
        /// <param name="newPassword">New Private Keys account password.</param>
        public Task ResetPassword(Guid publicKeyId, string newPassword)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Verifies the Private Keys account password reset.
        /// </summary>
        /// <param name="publicKeyId">The public key ID from Keys service.</param>
        /// <param name="token">The confirmation token.</param>
        /// <returns>
        /// The list of account's private keys.
        /// </returns>
        public Task<IEnumerable<PrivateKey>> VerifyResetPassword(Guid publicKeyId, string token)
        {
            throw new NotImplementedException();
        }
    }
}