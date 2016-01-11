namespace Virgil.SDK.Keys.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    
    using Virgil.Crypto;
    using Virgil.SDK.Keys.Helpers;
    using Virgil.SDK.Keys.Http;
    using Virgil.SDK.Keys.Infrastructure;
    using Virgil.SDK.Keys.TransferObject;

    /// <summary>
    /// Provides common methods to interact with Virgil Card resource endpoints.
    /// </summary>
    /// <seealso cref="Virgil.SDK.Keys.Clients.EndpointClient" />
    /// <seealso cref="Virgil.SDK.Keys.Clients.IVirgilCardsClient" />
    public class VirgilCardsClient : ResponseVerifyClient, IVirgilCardsClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilCardsClient" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public VirgilCardsClient(IConnection connection) : base(connection)
        {
            this.EndpointApplicationId = VirgilApplicationIds.PublicService;
            this.Cache = new ServiceKeyCache(this);
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilCardsClient" /> class.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="baseUri">The base URI.</param>
        public VirgilCardsClient(string accessToken, string baseUri = ApiConfig.PublicServicesAddress) 
            : base(new PublicServicesConnection(accessToken, new Uri(baseUri)))
        {
            this.Cache = new ServiceKeyCache(this);
            this.EndpointApplicationId = VirgilApplicationIds.PublicService;
        }

        /// <summary>
        /// Creates a new Virgil Card attached to known public key with unconfirmed identity.
        /// </summary>
        /// <param name="identityValue">The string that represents the value of identity.</param>
        /// <param name="identityType">The type of identity.</param>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <param name="privateKey">The private key. Private key is used to produce sign. It is not transfered over network</param>
        /// <param name="privateKeyPassword">The private key password.</param>
        /// <param name="cardsHashes">The collection of hashes of card that need to trust.</param>
        /// <param name="customData">The collection of custom user information.</param>
        /// <returns>An instance of <see cref="VirgilCardDto" /></returns>
        /// <remarks>This card will not be searchable by default.</remarks>
        public Task<VirgilCardDto> Create
        (
            string identityValue,
            IdentityType identityType,
            Guid publicKeyId,
            byte[] privateKey,
            string privateKeyPassword = null,
            IDictionary<Guid, string> cardsHashes = null,
            IDictionary<string, string> customData = null
        )
        {
            return this.AttachInternal(publicKeyId, identityType, identityValue, privateKey,
                privateKeyPassword, cardsHashes, customData);
        }

        /// <summary>
        /// Creates a new Virgil Card with unconfirmed identity.
        /// </summary>
        /// <param name="identityValue">The string that represents the value of identity.</param>
        /// <param name="identityType">The type of identity.</param>
        /// <param name="publicKey">The public key.</param>
        /// <param name="privateKey">The private key. Private key is used to produce sign. It is not transfered over network</param>
        /// <param name="privateKeyPassword">The private key password.</param>
        /// <param name="cardsHash">The collection of hashes of card that need to trust.</param>
        /// <param name="customData">The custom data.</param>
        /// <returns>An instance of <see cref="VirgilCardDto" /></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        /// <remarks>This card will not be searchable by default.</remarks>
        public Task<VirgilCardDto> Create
        (
            string identityValue, 
            IdentityType identityType, 
            byte[] publicKey, 
            byte[] privateKey, 
            string privateKeyPassword = null,
            IDictionary<Guid, string> cardsHash = null, 
            IDictionary<string, string> customData = null
        )
        {
            return this.CreateInternal(publicKey, identityType, identityValue, privateKey, 
                privateKeyPassword, cardsHash, customData);
        }

        /// <summary>
        /// Creates a new Virgil Card attached to known public key with confirmed identity.
        /// </summary>
        /// <param name="identityToken">The token DTO object that contains validation token from Identity information.</param>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <param name="privateKey">The private key. Private key is used to produce sign. It is not transfered over network</param>
        /// <param name="privateKeyPassword">The private key password.</param>
        /// <param name="cardsHashes">The collection of hashes of card that need to trust.</param>
        /// <param name="customData">The custom data.</param>
        /// <returns>An instance of <see cref="VirgilCardDto" /></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<VirgilCardDto> Create
        (
            IndentityTokenDto identityToken, 
            Guid publicKeyId, 
            byte[] privateKey, 
            string privateKeyPassword = null,
            IDictionary<Guid, string> cardsHashes = null, 
            IDictionary<string, string> customData = null
        )
        {
            return this.AttachInternal(publicKeyId, identityToken.Type, identityToken.Value, privateKey, 
                privateKeyPassword, cardsHashes, customData, identityToken.ValidationToken);
        }

        /// <summary>
        /// Creates a new Virgil Card with confirmed identity and specified public key.
        /// </summary>
        /// <param name="identityToken">The token.</param>
        /// <param name="publicKey">The public key.</param>
        /// <param name="privateKey">The private key. Private key is used to produce sign. It is not transfered over network</param>
        /// <param name="privateKeyPassword">The private key password.</param>
        /// <param name="cardsHashes">The collection of hashes of card that need to trust.</param>
        /// <param name="customData">The custom data.</param>
        /// <returns>An instance of <see cref="VirgilCardDto"/></returns>
        public Task<VirgilCardDto> Create
        (
            IndentityTokenDto identityToken, 
            byte[] publicKey, 
            byte[] privateKey, 
            string privateKeyPassword = null,
            IDictionary<Guid, string> cardsHashes = null, 
            IDictionary<string, string> customData = null
        )
        {
            return this.CreateInternal(publicKey, identityToken.Type, identityToken.Value, privateKey, 
                privateKeyPassword, cardsHashes, customData,identityToken.ValidationToken);
        }

        /// <summary>
        /// Searches the cards by specified criteria.
        /// </summary>
        /// <param name="identityValue">The value of identifier.</param>
        /// <param name="identityType">The type of identifier.</param>
        /// <param name="relations">Relations between Virgil cards. Optional</param>
        /// <param name="includeUnconfirmed">Unconfirmed Virgil cards will be included in output. Optional</param>
        /// <returns>The collection of Virgil Cards.</returns>
        public async Task<IEnumerable<VirgilCardDto>> Search
        (
            string identityValue, 
            IdentityType? identityType, 
            IEnumerable<Guid> relations, 
            bool? includeUnconfirmed
        )
        {
            Ensure.ArgumentNotNull(identityValue, nameof(identityValue));

            var body = new Dictionary<string, object>
            {
                ["value"] = identityValue
            };

            if (identityType != null)
            {
                body["type"] = identityType.Value;
            }

            if (relations != null && relations.Any())
            {
                body["relations"] = relations;
            }

            if (includeUnconfirmed == true)
            {
                body["include_unconfirmed"] = true;
            }

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .WithEndpoint("/v3/virgil-card/actions/search");

            return await this.Send<IEnumerable<VirgilCardDto>>(request);
        }

        /// <summary>
        /// Gets the application card.
        /// </summary>
        /// <param name="applicationIdentity">The application identity.</param>
        /// <returns>Virgil card dto <see cref="VirgilCardDto"/></returns>
        public async Task<IEnumerable<VirgilCardDto>> GetApplicationCard(string applicationIdentity)
        {
            Ensure.ArgumentNotNull(applicationIdentity, nameof(applicationIdentity));

            var request = Request.Create(RequestMethod.Post)
                .WithBody(new
                {
                    value = applicationIdentity
                })
                .WithEndpoint("v3/virgil-card/actions/search/app");

            return await this.Send<IEnumerable<VirgilCardDto>>(request);
        }

        /// <summary>
        /// Revokes the specified public key.
        /// </summary>
        /// <param name="publicKeyId">Id of public key to revoke.</param>
        /// <param name="tokens">List of all tokens for this public key.</param>
        /// <returns></returns>
        public async Task Revoke(Guid publicKeyId, IEnumerable<IndentityTokenDto> tokens)
        {
            var request = Request.Create(RequestMethod.Post);

            if (tokens != null)
            {
                request.WithBody(new
                {
                    identities = tokens.ToArray()
                });
            }

            request.WithEndpoint("v3/virgil-card/actions/search/app");

            await this.Send(request);
        }

        /// <summary>
        /// Trusts the specified card by signing the card's Hash attribute.
        /// </summary>
        /// <param name="trustedCardId">The trusting Virgil Card.</param>
        /// <param name="trustedCardHash">The trusting Virgil Card Hash value.</param>
        /// <param name="ownerCardId">The signer virgil card identifier.</param>
        /// <param name="privateKey">The signer private key. Private key is used to produce sign. It is not transfered over network</param>
        /// <param name="privateKeyPassword">The private key password.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<TrustCardResponse> Trust
        (
            Guid trustedCardId, 
            string trustedCardHash, 
            Guid ownerCardId, 
            byte[] privateKey,
            string privateKeyPassword = null
        )
        {
            using (var virgilSigner = new VirgilSigner())
            {
                var body = new
                {
                    signed_virgil_card_id = trustedCardId,
                    signed_digest = virgilSigner.Sign(Encoding.UTF8.GetBytes(trustedCardHash), privateKey)
                };

                var request = Request.Create(RequestMethod.Post)
                    .WithBody(body)
                    .WithEndpoint($"/v3/virgil-card/{ownerCardId}/actions/sign")
                    .SignRequest(ownerCardId, privateKey, privateKeyPassword);
                
                return await this.Send<TrustCardResponse>(request);
            }
        }

        /// <summary>
        /// Stops trusting the specified card by deleting the sign digest.
        /// </summary>
        /// <param name="trustedCardId">The trusting Virgil Card.</param>
        /// <param name="ownerCardId">The owner Virgil Card identifier.</param>
        /// <param name="privateKey">The private key. Private key is used to produce sign. It is not transfered over network</param>
        /// <param name="privateKeyPassword">The private key password.</param>
        public async Task Untrust (Guid trustedCardId, Guid ownerCardId, byte[] privateKey, string privateKeyPassword = null)
        {
            Ensure.ArgumentNotNull(privateKey, nameof(privateKey));

            var body = new
            {
                signed_virgil_card_id = trustedCardId
            };

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .WithEndpoint($"/v3/virgil-card/{ownerCardId}/actions/unsign")
                .SignRequest(ownerCardId, privateKey, privateKeyPassword);

            await this.Send(request);
        }
        
        private async Task<VirgilCardDto> CreateInternal
        (
            byte[] publicKey, 
            IdentityType type, 
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

            return await this.Send<VirgilCardDto>(request);
        }
        
        private async Task<VirgilCardDto> AttachInternal(
            Guid publicKeyId,
            IdentityType type,
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

            return await this.Send<VirgilCardDto>(request);
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