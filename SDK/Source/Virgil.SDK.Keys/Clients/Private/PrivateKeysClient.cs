namespace Virgil.SDK.Keys.Clients
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using Crypto;
    using Http;
    using Newtonsoft.Json;
    using TransferObject;

    /// <summary>
    ///     Provides common methods to interact with Private Keys resource endpoints.
    /// </summary>
    /// <seealso cref="Virgil.SDK.Keys.Clients.EndpointClient" />
    /// <seealso cref="Virgil.SDK.Keys.Clients.IPrivateKeysClient" />
    public class PrivateKeysClient : EndpointClient, IPrivateKeysClient
    {
        private readonly IKnownKeyProvider provider;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PrivateKeysClient" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="provider">The known key provider.</param>
        public PrivateKeysClient(IConnection connection, IKnownKeyProvider provider) : base(connection)
        {
            this.provider = provider;
        }

        /// <summary>
        ///     Uploads private key to private key store.
        /// </summary>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <param name="privateKey">The private key value. Private key is used to produce sign. It is not transfered over network</param>
        public async Task Put(Guid publicKeyId, byte[] privateKey)
        {
            var body = new
            {
                private_key = privateKey,
                public_key_id = publicKeyId,
                request_sign_uuid = Guid.NewGuid().ToString().ToLowerInvariant()
            };

            var args = await this.provider.GetKnownKey();

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .SignRequestForPrivateService(privateKey)
                .EncryptJsonBody(args.KnownPublicKeyId, args.KnownPublicKey)
                .WithEndpoint("/v3/private-key");

            await this.Send(request);
        }

        /// <summary>
        ///     Downloads private part of key by its public id.
        /// </summary>
        /// <param name="publicKeyId">The public key identifier.</param>
        public async Task Get(Guid publicKeyId)
        {
            var randomPassword = Guid.NewGuid().ToString();

            var body = new
            {
                response_password = randomPassword,
                public_key_id = publicKeyId,
                request_sign_uuid = Guid.NewGuid().ToString().ToLowerInvariant()
            };

            var args = await this.provider.GetKnownKey();

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .EncryptJsonBody(args.KnownPublicKeyId, args.KnownPublicKey)
                .WithEndpoint("/v3/private-key/actions/grab");

            var response = await this.Send(request);

            var encryptedBody = response.Body;

            using (var cipher = new VirgilCipher())
            {
                var bytes = cipher.DecryptWithPassword(
                    Encoding.UTF8.GetBytes(encryptedBody),
                    Encoding.UTF8.GetBytes(randomPassword));

                var decryptedBody = Encoding.UTF8.GetString(bytes, 0, bytes.Length);

                JsonConvert.DeserializeObject<GrabResponse>(decryptedBody);
            }
        }

        /// <summary>
        ///     Deletes private key by its id.
        /// </summary>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <param name="privateKey">The private key value. Private key is used to produce sign. It is not transfered over network</param>
        public async Task Delete(Guid publicKeyId, byte[] privateKey)
        {
            var body = new
            {
                public_key_id = publicKeyId,
                request_sign_uuid = Guid.NewGuid().ToString().ToLowerInvariant()
            };

            var args = await this.provider.GetKnownKey();

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .SignRequestForPrivateService(privateKey)
                .EncryptJsonBody(args.KnownPublicKeyId, args.KnownPublicKey)
                .WithEndpoint("/v3/private-key/actions/delete");

            await this.Send(request);
        }
    }
}