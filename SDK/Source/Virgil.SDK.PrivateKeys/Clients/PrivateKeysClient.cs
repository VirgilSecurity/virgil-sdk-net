namespace Virgil.SDK.PrivateKeys.Clients
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Crypto;
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
        /// <returns>
        /// The instance of <see cref="PrivateKey" />
        /// </returns>
        public Task<PrivateKey> Get(Guid publicKeyId)
        {
            return this.Get(publicKeyId, this.Connection.Credentials.Password);
        }

        /// <summary>
        /// Gets the private key by public key ID.
        /// </summary>
        /// <param name="publicKeyId">Public key identifier.</param>
        /// <param name="privateKeyPassword"></param>
        /// <returns>
        /// The instance of <see cref="PrivateKey" />
        /// </returns>
        public async Task<PrivateKey> Get(Guid publicKeyId, string privateKeyPassword)
        {
            var request = Request.Create(RequestMethod.Get)
                .WithEndpoint($"/v2/private-key/public-key-id/{publicKeyId}");

            var vPrivateKey = await this.Send<GetPrivateKeyByIdResult>(request);
            byte[] privateKey;

            using (var cripto = new VirgilCipher())
            {
                privateKey = cripto.DecryptWithPassword(vPrivateKey.EncryptedPrivateKey, 
                    Encoding.UTF8.GetBytes(privateKeyPassword));
            }

            return new PrivateKey { PublicKeyId = publicKeyId, Key = privateKey };
        }

        /// <summary>
        /// Adds new private key for storage.
        /// </summary>
        /// <param name="publicKeyId">The public key ID</param>
        /// <param name="privateKey">
        /// The private key associated for this public key. It should be encrypted if 
        /// account type is <c>Normal</c></param>
        public Task Add(Guid publicKeyId, byte[] privateKey)
        {
            return this.Add(publicKeyId, privateKey, this.Connection.Credentials.Password);
        }

        /// <summary>
        /// Adds new encrypted private key to Virgil Private Keys storage 
        /// </summary>
        /// <param name="publicKeyId">The public key ID</param>
        /// <param name="privateKey">The private key associated for this public key.</param>
        /// <param name="privateKeyPassword"></param>
        public async Task Add(Guid publicKeyId, byte[] privateKey, string privateKeyPassword)
        {
            byte[] encryptedPrivateKey;

            using (var cipher = new VirgilCipher())
            {
                cipher.AddPasswordRecipient(Encoding.UTF8.GetBytes(privateKeyPassword));
                encryptedPrivateKey = cipher.Encrypt(privateKey, true);
            }
            
            var request = Request.Create(RequestMethod.Post)
                .WithEndpoint("/v2/private-key")
                .WithBody(new
                {
                    private_key = encryptedPrivateKey,
                    request_sign_uuid = Guid.NewGuid().ToString()
                })
                .SignRequest(publicKeyId, privateKey);

            await this.Send(request);
        }

        /// <summary>
        /// Removes the private key from service by specified public key id.
        /// </summary>
        /// <param name="publicKeyId">The public key ID</param>
        /// <param name="privateKey">The public key ID digital signature. Verifies the possession of the private key.</param>
        public async Task Remove(Guid publicKeyId, byte[] privateKey)
        {
            // TODO: 
            var request = Request.Create(RequestMethod.Delete)
                .WithEndpoint("/v2/private-key")
                .WithBody(new { request_sign_uuid = Guid.NewGuid().ToString() })
                .SignRequest(publicKeyId, privateKey);

            await this.Send(request);
        }
    }
}