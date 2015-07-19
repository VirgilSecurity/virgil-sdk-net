using System.Linq;

namespace Virgil.SDK.PrivateKeys.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Virgil.SDK.PrivateKeys.Http;
    using Virgil.SDK.PrivateKeys.Model;
    using Virgil.SDK.PrivateKeys.TransferObject;

    /// <summary>
    /// Provides common methods to interact with private keys resource endpoints.
    /// </summary>
    public class PrivateKeysClient : EndpointClient, IPrivateKeysClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateKeysClient" /> class with the default implemetations. 
        /// </summary>
        public PrivateKeysClient(IConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// Gets the private key by public key ID.
        /// </summary>
        /// <param name="publicKeyId">Public key identifier.</param>
        /// <returns>The instance of <see cref="PrivateKey"/></returns>
        public async Task<PrivateKey> Get(Guid publicKeyId)
        {
            var privateKey = await this.Get<GetPrivateKeyByIdResult>(String.Format("private-key/public-key/{0}", publicKeyId));
            return new PrivateKey { PublicKeyId = publicKeyId, Key = privateKey.PrivateKey };
        }

        /// <summary>
        /// Gets all private keys by account Id.
        /// </summary>
        /// <param name="accountId">The account identifier.</param>
        /// <returns>The list of <see cref="PrivateKey"/> instances</returns>
        public async Task<IEnumerable<PrivateKey>> GetAll(Guid accountId)
        {
            var privateKeys = await this.Get<GetAllPrivateKeysResult>(String.Format("private-key/account/{0}", accountId));

            return privateKeys.PrivateKeys
                .Select(it => new PrivateKey {PublicKeyId = it.PublicKeyId, Key = it.PrivateKey})
                .ToList();
        }

        /// <summary>
        /// Adds new private key for storage.
        /// </summary>
        /// <param name="accountId">The account identifier</param>
        /// <param name="publicKeyId">The public key ID</param>
        /// <param name="sign">The public key ID digital signature. Verifies the possession of the private key.</param>
        /// <param name="privateKey">
        /// The private key associated for this public key. It should be encrypted if 
        /// account type is <c>Normal</c></param>
        public async Task Add(Guid accountId, Guid publicKeyId, byte[] sign, byte[] privateKey)
        {
            var body = new
            {
                account_id = accountId,
                public_key_id = publicKeyId,
                sign,
                private_key = privateKey
            };

            await this.Post<AddPrivateKeyResult>("private-key", body);
        }

        /// <summary>
        /// Removes the private key from service by specified public key id.
        /// </summary>
        /// <param name="publicKeyId">The public key ID</param>
        /// <param name="sign">The public key ID digital signature. Verifies the possession of the private key.</param>
        public async Task Remove(Guid publicKeyId, byte[] sign)
        {
            var body = new
            {
                public_key_id = publicKeyId,
                sign
            };

            await this.Delete("private-key", body);
        }
    }
}