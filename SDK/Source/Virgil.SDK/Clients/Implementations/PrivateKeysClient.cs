namespace Virgil.SDK.Clients
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Virgil.Crypto;
    using Virgil.SDK.Helpers;
    using Virgil.SDK.Http;
    using Virgil.SDK.Infrastructure;
    using Virgil.SDK.TransferObject;

    /// <summary>
    /// Provides common methods to interact with Private Keys resource endpoints.
    /// </summary>
    /// <seealso cref="EndpointClient" />
    /// <seealso cref="IPrivateKeysClient" />
    public class PrivateKeysClient : EndpointClient, IPrivateKeysClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateKeysClient" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="cache">The known key provider.</param>
        public PrivateKeysClient(IConnection connection, IServiceKeyCache cache) : base(connection, cache)
        {
            this.EndpointApplicationId = VirgilApplicationIds.PrivateService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateKeysClient"/> class.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="baseUri">The base URI.</param>
        public PrivateKeysClient(string accessToken, string baseUri = VirgilConfig.PrivateServicesAddress)
            : base(new PrivateKeysConnection(accessToken, new Uri(baseUri)))
        {
            this.Cache = new StaticKeyCache();
            this.EndpointApplicationId = VirgilApplicationIds.PrivateService;
        }

        /// <summary>
        /// Uploads private key to private key store.
        /// </summary>
        /// <param name="virgilCardId">The public key identifier.</param>
        /// <param name="privateKey">The private key value. Private key is used to produce sign. It is not transfered over network</param>
        /// <param name="privateKeyPassword">The private key password.</param>
        public async Task Stash(Guid virgilCardId, byte[] privateKey, string privateKeyPassword = null)
        {
            Ensure.ArgumentNotNull(privateKey, nameof(privateKey));
            
            var body = new
            {
                virgil_card_id = virgilCardId,
                private_key = privateKey,
            };

            var privateServiceCard = await this.Cache.GetServiceCard(this.EndpointApplicationId).ConfigureAwait(false);

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .SignRequest(virgilCardId, privateKey, privateKeyPassword)
                .EncryptJsonBody(privateServiceCard)
                .WithEndpoint("/v3/private-key");

            await this.Send(request).ConfigureAwait(false);
        }

        /// <summary>
        /// Downloads private part of key by its public id.
        /// </summary>
        /// <param name="virgilCardId">The public key identifier.</param>
        /// <param name="token"></param>
        /// <remarks>Random password will be generated to encrypt server response</remarks>
        public Task<GrabResponse> Get(Guid virgilCardId, IdentityTokenDto token)
        {
            var randomPassword = Guid.NewGuid().ToString().Replace("-","").Substring(0, 31);
            return this.Get(virgilCardId, token, randomPassword);
        }

        /// <summary>
        /// Downloads private part of key by its public id.
        /// </summary>
        /// <param name="virgilCardId">The public key identifier.</param>
        /// <param name="token">Valid identity token with</param>
        /// <param name="responsePassword">Password to encrypt server response. Up to 31 characters</param>
        public async Task<GrabResponse> Get(Guid virgilCardId, IdentityTokenDto token, string responsePassword)
        {
            Ensure.ArgumentNotNull(token, nameof(token));
            Ensure.ArgumentNotNull(responsePassword, nameof(responsePassword));

            var body = new
            {
                identity = new
                {
                    type = token.Type,
                    value = token.Value,
                    validation_token = token.ValidationToken
                },
                response_password = responsePassword,
                virgil_card_id = virgilCardId
            };

            var privateServiceCard = await this.Cache.GetServiceCard(this.EndpointApplicationId).ConfigureAwait(false);

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .EncryptJsonBody(privateServiceCard)
                .WithEndpoint("/v3/private-key/actions/grab");

            var response = await this.Send(request).ConfigureAwait(false);

            var encryptedBody = Convert.FromBase64String(response.Body);

            using (var cipher = new VirgilCipher())
            {
                var bytes = cipher.DecryptWithPassword(encryptedBody, Encoding.UTF8.GetBytes(responsePassword));
                var decryptedBody = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                return JsonConvert.DeserializeObject<GrabResponse>(decryptedBody);
            }
        }

        /// <summary>
        /// Deletes the private key from service by specified card ID.
        /// </summary>
        /// <param name="virgilCardId">The public key identifier.</param>
        /// <param name="privateKey">The private key value. Private key is used to produce sign. It is not transfered over network</param>
        /// <param name="privateKeyPassword">The private key password.</param>
        /// <returns></returns>
        public async Task Destroy(Guid virgilCardId, byte[] privateKey, string privateKeyPassword = null)
        {
            Ensure.ArgumentNotNull(privateKey, nameof(privateKey));

            var body = new
            {
                virgil_card_id = virgilCardId
            };

            var privateServiceCard = await this.Cache.GetServiceCard(this.EndpointApplicationId).ConfigureAwait(false);

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .SignRequest(virgilCardId, privateKey, privateKeyPassword)
                .EncryptJsonBody(privateServiceCard)
                .WithEndpoint("/v3/private-key/actions/delete");

            await this.Send(request).ConfigureAwait(false);
        }
    }
}