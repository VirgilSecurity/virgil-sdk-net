namespace Virgil.SDK.Clients
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    using Virgil.Crypto;
    using Virgil.SDK.Helpers;
    using Virgil.SDK.Http;
    using Virgil.SDK.Identities;
    using Virgil.SDK.Models;

    /// <summary>
    /// Provides common methods to interact with Private Keys resource endpoints.
    /// </summary>
    /// <seealso cref="EndpointClient" />
    /// <seealso cref="IPrivateKeysServiceClient" />
    internal class PrivateKeysServiceClient : EndpointClient, IPrivateKeysServiceClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateKeysServiceClient" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="cache">The known key provider.</param>
        public PrivateKeysServiceClient(IConnection connection, IServiceKeyCache cache) : base(connection, cache)
        {
            this.EndpointApplicationId = ServiceIdentities.PrivateService;
        }
        
        public async Task Stash(Guid cardId, byte[] privateKey, string privateKeyPassword = null)
        {
            Ensure.ArgumentNotNull(privateKey, nameof(privateKey));
            
            var body = new
            {
                virgil_card_id = cardId,
                private_key = privateKey,
            };

            var privateServiceCard = await this.Cache.GetServiceCard(this.EndpointApplicationId).ConfigureAwait(false);

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .SignRequest(cardId, privateKey, privateKeyPassword)
                .EncryptJsonBody(privateServiceCard)
                .WithEndpoint("/v3/private-key");

            await this.Send(request).ConfigureAwait(false);
        }

        public Task<PrivateKeyModel> Get(Guid cardId, IdentityInfo identityInfo)
        {
            var randomPassword = Guid.NewGuid().ToString().Replace("-","").Substring(0, 31);
            return this.Get(cardId, identityInfo, randomPassword);
        }

        public async Task<PrivateKeyModel> Get(Guid cardId, IdentityInfo identityInfo, string responsePassword)
        {
            Ensure.ArgumentNotNull(identityInfo, nameof(identityInfo));
            Ensure.ArgumentNotNull(responsePassword, nameof(responsePassword));
            Ensure.ArgumentNotNull(identityInfo.ValidationToken, nameof(identityInfo.ValidationToken));

            var body = new
            {
                identity = new
                {
                    type = identityInfo.Type,
                    value = identityInfo.Value,
                    validation_token = identityInfo.ValidationToken
                },
                response_password = responsePassword,
                virgil_card_id = cardId
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
                return JsonConvert.DeserializeObject<PrivateKeyModel>(decryptedBody);
            }
        }

        public async Task Destroy(Guid cardId, byte[] privateKey, string privateKeyPassword = null)
        {
            Ensure.ArgumentNotNull(privateKey, nameof(privateKey));

            var body = new
            {
                virgil_card_id = cardId
            };

            var privateServiceCard = await this.Cache.GetServiceCard(this.EndpointApplicationId).ConfigureAwait(false);

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .SignRequest(cardId, privateKey, privateKeyPassword)
                .EncryptJsonBody(privateServiceCard)
                .WithEndpoint("/v3/private-key/actions/delete");

            await this.Send(request).ConfigureAwait(false);
        }
    }
}