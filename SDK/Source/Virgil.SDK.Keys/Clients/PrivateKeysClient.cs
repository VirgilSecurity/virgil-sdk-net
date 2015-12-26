namespace Virgil.SDK.Keys.Clients
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using Crypto;
    using Domain;
    using Http;
    using Virgil.SDK.Keys.Infrastructure;
    using Newtonsoft.Json;
    using TransferObject;

    /// <summary>
    ///     Provides common methods to interact with Private Keys resource endpoints.
    /// </summary>
    /// <seealso cref="Virgil.SDK.Keys.Clients.EndpointClient" />
    /// <seealso cref="Virgil.SDK.Keys.Clients.IPrivateKeysClient" />
    public class PrivateKeysClient : EndpointClient, IPrivateKeysClient
    {
        private readonly IServiceKeyCache cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateKeysClient" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="cache">The known key provider.</param>
        public PrivateKeysClient(IConnection connection, IServiceKeyCache cache) : base(connection)
        {
            this.cache = cache;
            this.EndpointPublicKeyId = KnownKeyIds.PrivateService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateKeysClient"/> class.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="baseUri">The base URI.</param>
        public PrivateKeysClient(string accessToken, string baseUri = ApiConfig.PublicServicesAddress)
            : base(new PrivateKeysConnection(accessToken, new Uri(baseUri)))
        {
            this.cache = new ServiceKeyCache(new PublicKeysClient(accessToken));
        }

        /// <summary>
        /// Uploads private key to private key store.
        /// </summary>
        /// <param name="virgilCardId">The public key identifier.</param>
        /// <param name="privateKey">The private key value. Private key is used to produce sign. It is not transfered over network</param>
        public async Task Put(Guid virgilCardId, byte[] privateKey)
        {
            var body = new
            {
                virgil_card_id = virgilCardId,
                private_key = privateKey,
            };

            var knownKey = await this.cache.GetServiceKey(this.EndpointPublicKeyId);

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .SignRequest(privateKey, virgilCardId)
                .EncryptJsonBody(knownKey.Id, knownKey.PublicKey)
                .WithEndpoint("/v3/private-key");

            await this.Send(request);
        }

        /// <summary>
        /// Downloads private part of key by its public id.
        /// </summary>
        /// <param name="virgilCardId">The public key identifier.</param>
        /// <param name="token"></param>
        /// <remarks>Random password will be generated to encrypt server response</remarks>
        public Task<GrabResponse> Get(Guid virgilCardId, IndentityTokenDto token)
        {
            var randomPassword = Guid.NewGuid().ToString();
            return this.Get(virgilCardId, token, randomPassword);
        }

        /// <summary>
        /// Downloads private part of key by its public id.
        /// </summary>
        /// <param name="virgilCardId">The public key identifier.</param>
        /// <param name="token"></param>
        /// <param name="responsePassword"></param>
        public async Task<GrabResponse> Get(Guid virgilCardId, IndentityTokenDto token, string responsePassword)
        {
            var body = new
            {
                identity = new
                {
                    type = token.Type,
                    value = token.Value,
                    token = token.ValidationToken
                },
                response_password = responsePassword,
                virgil_card_id = virgilCardId
            };

            var args = await this.cache.GetServiceKey(this.EndpointPublicKeyId);

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .EncryptJsonBody(args.Id, args.PublicKey)
                .WithEndpoint("/v3/private-key/actions/grab");

            var response = await this.Send(request);

            var encryptedBody = response.Body;

            using (var cipher = new VirgilCipher())
            {
                var bytes = cipher.DecryptWithPassword(
                    Encoding.UTF8.GetBytes(encryptedBody),
                    Encoding.UTF8.GetBytes(responsePassword));

                var decryptedBody = Encoding.UTF8.GetString(bytes, 0, bytes.Length);

                return JsonConvert.DeserializeObject<GrabResponse>(decryptedBody);
            }
        }

        /// <summary>
        /// Deletes private key by its id.
        /// </summary>
        /// <param name="virgilCardId">The public key identifier.</param>
        /// <param name="privateKey">The private key value. Private key is used to produce sign. It is not transfered over network</param>
        public async Task Delete(Guid virgilCardId, byte[] privateKey)
        {
            var body = new
            {
                virgil_card_id = virgilCardId
            };

            var publicKey = await this.cache.GetServiceKey(this.EndpointPublicKeyId);

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .SignRequest(privateKey, virgilCardId)
                .EncryptJsonBody(publicKey.Id, publicKey.PublicKey)
                .WithEndpoint("/v3/private-key/actions/delete");

            await this.Send(request);
        }

        protected override async Task<IResponse> Send(IRequest request)
        {
            var result = await base.Send(request);
            var key = await this.cache.GetServiceKey(this.EndpointPublicKeyId);
            this.VerifyResponse(result, key.PublicKey);
            return result;
        }
    }
}