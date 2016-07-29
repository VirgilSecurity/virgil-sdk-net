namespace Virgil.SDK.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Virgil.Crypto;
    using Virgil.SDK.Clients.Models;
    using Virgil.SDK.Helpers;
    using Virgil.SDK.Http;
    using Virgil.SDK.Identities;
    using Virgil.SDK.Models;

    /// <summary>
    /// Provides common methods to interact with Virgil Card resource endpoints.
    /// </summary>
    /// <seealso cref="EndpointClient" />
    /// <seealso cref="ICardsServiceClient" />
    internal class CardsServiceClient : ResponseVerifyClient, ICardsServiceClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CardsServiceClient" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="cache">The services key cache.</param>
        public CardsServiceClient(IConnection connection, IServiceKeyCache cache) : base(connection)
        {
            this.EndpointApplicationId = ServiceIdentities.PublicService;
            this.Cache = cache;
        }

        /// <summary>
        /// Creates a new card with specified identity and existing public key.
        /// </summary>
        /// <param name="identityInfo">The information about identity.</param>
        /// <param name="publicKeyId">The public key identifier in Virgil Services.</param>
        /// <param name="privateKey">
        /// The private key. Private key is used to produce sign. It is not transfered over network
        /// </param>
        /// <param name="privateKeyPassword">
        /// The private key password. Pass this parameter if your private key is encrypted with password</param>
        /// <param name="customData">
        /// The dictionary of key/value pairs with custom values that can be used by different applications
        /// </param>
        public async Task<CardModel> Create(IdentityInfo identityInfo, Guid publicKeyId, byte[] privateKey, string privateKeyPassword = null,
            IDictionary<string, string> customData = null)
        {
            var request = this.BuildAttachRequest(publicKeyId, identityInfo.Type, identityInfo.Value, privateKey,
                privateKeyPassword, null, customData, identityInfo.ValidationToken);

            var cardModel = await this.Send<CardModel>(request).ConfigureAwait(false);
            return cardModel;
        }

        /// <summary>
        /// Creates a new card with specified identity and public key.
        /// </summary>
        /// <param name="identityInfo">The information about identity.</param>
        /// <param name="publicKey">The generated public key value.</param>
        /// <param name="privateKey">
        /// The private key. Private key is used to produce sign. It is not transfered over network
        /// </param>
        /// <param name="privateKeyPassword">
        /// The private key password. Pass this parameter if your private key is encrypted with password</param>
        /// <param name="customData">
        /// The dictionary of key/value pairs with custom values that can be used by different applications
        /// </param>
        public async Task<CardModel> Create(IdentityInfo identityInfo, byte[] publicKey, byte[] privateKey, string privateKeyPassword = null,
            IDictionary<string, string> customData = null)
        {
            var request = this.BuildCreateRequest(publicKey, identityInfo.Type, identityInfo.Value, privateKey,
                privateKeyPassword, null, customData, identityInfo.ValidationToken);

            var cardModel = await this.Send<CardModel>(request).ConfigureAwait(false);
            return cardModel;
        }

        public Task<VirgilCard> PublishAsync(VirgilCardRequest request)
        {
            throw new NotImplementedException();
        }
        
        public Task<IEnumerable<VirgilCard>> SearchAsync(string identity, string identityType = null)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Searches the private cards by specified criteria.
        /// </summary>
        /// <param name="identityValue">The value of identifier.</param>
        /// <param name="identityType">The type of identifier.</param>
        /// <param name="includeUnauthorized">Unconfirmed Virgil cards will be included in output. Optional</param>
        /// <returns>The collection of Virgil Cards.</returns>
        public async Task<IEnumerable<CardModel>> Search (string identityValue, string identityType = null, 
            bool? includeUnauthorized = null)
        {
            Ensure.ArgumentNotNull(identityValue, nameof(identityValue));

            var body = new Dictionary<string, object>
            {
                ["value"] = identityValue
            };

            if (!string.IsNullOrWhiteSpace(identityType))
            {
                body["type"] = identityType;
            }

            if (includeUnauthorized.HasValue)
            {
                body["include_unauthorized"] = includeUnauthorized.Value;
            }

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .WithEndpoint("/v3/virgil-card/actions/search");

            return await this.Send<IEnumerable<CardModel>>(request).ConfigureAwait(false);
        }

        /// <summary>
        /// Searches the global cards by specified criteria.
        /// </summary>
        /// <param name="identityValue">The value of identifier.</param>
        /// <param name="identityType">The type of identifier.</param>
        /// <returns>The collection of Virgil Cards.</returns>
        public async Task<IEnumerable<CardModel>> Search(string identityValue, IdentityType identityType)
        {
            Ensure.ArgumentNotNull(identityValue, nameof(identityValue));

            var body = new Dictionary<string, object>
            {
                ["value"] = identityValue
            };

            var type = identityType.ExtractEnumValue();

            var endpointUrl = identityType == IdentityType.Application 
                ?  "/v3/virgil-card/actions/search/app" 
                : $"/v3/virgil-card/actions/search/{type}";
                
            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .WithEndpoint(endpointUrl);

            return await this.Send<IEnumerable<CardModel>>(request).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the card by ID.
        /// </summary>
        /// <param name="cardId">The card ID.</param>
        /// <returns>Virgil card model.</returns>
        public async Task<CardModel> Get(Guid cardId)
        {
            var request = Request.Create(RequestMethod.Get)
                .WithEndpoint($"v3/virgil-card/{cardId}");

            return await this.Send<CardModel>(request).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the <see cref="VirgilCard" /> by specified identifier.
        /// </summary>
        /// <param name="cardId">The <see cref="VirgilCard" /> identifier.</param>
        /// <returns>
        /// An instance of <see cref="VirgilCard" /> entity.
        /// </returns>
        public Task<VirgilCard> GetAsync(Guid cardId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Revokes the specified public key.
        /// </summary>
        /// <param name="cardId">The card ID.</param>
        /// <param name="identityInfo">Validation token for card's identity.</param>
        /// <param name="privateKey">The private key. Private key is used to produce sign. It is not transfered over network</param>
        /// <param name="privateKeyPassword">The private key password.</param>
        public async Task Revoke
        (
            Guid cardId, 
            IdentityInfo identityInfo, 
            byte[] privateKey,  
            string privateKeyPassword = null
        )
        {
            var request = Request.Create(RequestMethod.Delete)
                .WithBody(new
                {
                    identity = new
                    {
                        value = identityInfo.Value,
                        type = identityInfo.Type,
                        validation_token = identityInfo.ValidationToken
                    }
                })
                .WithEndpoint($"v3/virgil-card/{cardId}")
                .SignRequest(cardId, privateKey, privateKeyPassword);

            await this.Send(request).ConfigureAwait(false);
        }
        
        private Request BuildCreateRequest
        (
            byte[] publicKey, 
            string type, 
            string value, 
            byte[] privateKey,
            string privateKeyPassword = null,
            IDictionary<Guid, string> cardsHashes = null,
            IDictionary<string, string> customData = null, 
            string identityToken = null
        )
        {
            Ensure.ArgumentNotNull(publicKey, nameof(publicKey));
            Ensure.ArgumentNotNull(value, nameof(value));
            Ensure.ArgumentNotNull(privateKey, nameof(privateKey));

            var signs = CreateSignsHashes(cardsHashes, privateKey, privateKeyPassword);
            var body = new
            {
                public_key = publicKey,
                identity = new
                {
                    type,
                    value,
                    validation_token = identityToken
                },
                data = customData,
                signs = signs
            };

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .WithEndpoint("/v3/virgil-card")
                .SignRequest(privateKey, privateKeyPassword);

            return request;
        }
        
        private Request BuildAttachRequest(
            Guid publicKeyId,
            string type,
            string value,
            byte[] privateKey,
            string privateKeyPassword = null,
            IDictionary<Guid, string> cardsHashes = null,
            IDictionary<string, string> customData = null,
            string identityToken = null)
        {
            Ensure.ArgumentNotNull(value, nameof(value));
            Ensure.ArgumentNotNull(privateKey, nameof(privateKey));

            var signs = CreateSignsHashes(cardsHashes, privateKey, privateKeyPassword);
            var body = new
            {
                public_key_id = publicKeyId,
                identity = new
                {
                    type,
                    value,
                    validation_token = identityToken
                },
                data = customData,
                signs = signs
            };

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .WithEndpoint("/v3/virgil-card")
                .SignRequest(privateKey, privateKeyPassword);

            return request; 
        }

        private static IEnumerable<object> CreateSignsHashes(IDictionary<Guid, string> cardsHashes, byte[] privateKey, string privateKeyPassword)
        {
            if (cardsHashes == null)
                return null;

            using (var signer = new VirgilSigner())
            {
                var signedHashes = new List<object>();
                foreach (var cardHash in cardsHashes)
                {
                    var hashData = Encoding.UTF8.GetBytes(cardHash.Value);

                    byte[] sign = privateKeyPassword == null
                        ? signer.Sign(hashData, privateKey)
                        : signer.Sign(hashData, privateKey, Encoding.UTF8.GetBytes(privateKeyPassword));

                    signedHashes.Add(new
                    {
                        signed_virgil_card_id = cardHash.Key,
                        signed_digest = sign
                    });
                }

                return signedHashes;
            }
        }
    }
}