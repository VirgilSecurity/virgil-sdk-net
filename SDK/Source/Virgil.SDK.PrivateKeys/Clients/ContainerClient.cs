namespace Virgil.SDK.PrivateKeys.Clients
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    
    using Virgil.SDK.PrivateKeys.Model;
    using Virgil.SDK.PrivateKeys.Http;
    using Virgil.SDK.PrivateKeys.TransferObject;

    /// <summary>
    /// Provides common methods to interact with Account (of Private Keys API) resource endpoints.
    /// </summary>
    public class ContainerClient : EndpointClient, IContainerClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerClient"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public ContainerClient(IConnection connection) : base(connection)
        {
        }

        /// <summary>
        /// Gets the specified account by identifier.
        /// </summary>
        /// <param name="accountId">The account identifier.</param>
        public async Task<PrivateKeysAccount> Get(Guid accountId)
        {
            var privateKeys = await this.Get<GetAllPrivateKeysResult>(String.Format("private-key/account/{0}", accountId));

            var account = new PrivateKeysAccount();

            account.AccountId = privateKeys.AccountId;
            account.Type = privateKeys.AccountType == "easy"
                ? PrivateKeysAccountType.Easy
                : PrivateKeysAccountType.Normal;
            account.PrivateKeys = privateKeys.PrivateKeys
                .Select(it => new PrivateKey {PublicKeyId = it.PublicKeyId, Key = it.PrivateKey})
                .ToList();

            return account;
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
        public async Task Remove(Guid accountId, Guid publicKeyId, byte[] sign)
        {
            var body = new
            {
                account_id = accountId,
                public_key_id = publicKeyId,
                sign
            };

            await this.Delete("account", body);
        }

        /// <summary>
        /// Resets the account password.
        /// </summary>
        /// <param name="userId">The user ID from Keys service.</param>
        /// <param name="newPassword">New Private Keys account password.</param>
        public async Task ResetPassword(string userId, string newPassword)
        {
            var body = new {
                user_data = new {
                    @class = "user_id",
                    type = "email",
                    value = userId
                },
                new_password = newPassword
            };

            await this.Put<object>("account/reset", body);
        }
        
        /// <summary>
        /// Verifies the Private Keys account password reset.
        /// </summary>
        /// <param name="token">The confirmation token.</param>
        /// <returns>
        /// The list of account's private keys.
        /// </returns>
        public async Task VerifyResetPassword(string token)
        {
            var body = new { token };
            await this.Put<object>("account/verify", body);
        }
    }
}