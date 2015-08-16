namespace Virgil.SDK.PrivateKeys.Clients
{
    using System;
    using System.Linq;
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
        /// <returns>
        /// The instance of <see cref="PrivateKey" />
        /// </returns>
        public async Task<PrivateKey> Get(Guid publicKeyId)
        {
            var request = Request.Create(RequestMethod.Get)
                .WithEndpoint($"private-key/public-key-id/{publicKeyId}");

            var privateKey = await this.Send<GetPrivateKeyByIdResult>(request);
            return new PrivateKey { PublicKeyId = publicKeyId, Key = privateKey.PrivateKey };
        }

        /// <summary>
        /// Adds new private key for storage.
        /// </summary>
        /// <param name="publicKeyId">The public key ID</param>
        /// <param name="sign">The public key ID digital signature. Verifies the possession of the private key.</param>
        /// <param name="privateKey">
        /// The private key associated for this public key. It should be encrypted if 
        /// account type is <c>Normal</c></param>
        public async Task Add(Guid publicKeyId, byte[] sign, byte[] privateKey)
        {
            var request = Request.Create(RequestMethod.Post)
                .WithEndpoint("/v2/private-key")
                .WithBody(new { private_key = privateKey, request_sign_random_uuid = Guid.NewGuid() })
                .SignRequest(publicKeyId, privateKey);

            await this.Send<AddPrivateKeyResult>(request);
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
                .WithBody(new { request_sign_random_uuid = Guid.NewGuid() })
                .SignRequest(publicKeyId, privateKey);

            await this.Send(request);
        }
    }
}