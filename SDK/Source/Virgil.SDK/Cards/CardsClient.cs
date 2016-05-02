namespace Virgil.SDK.Cards
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Virgil.Crypto;

    using Virgil.SDK.Common;
    using Virgil.SDK.Helpers;
    using Virgil.SDK.Http;
    using Virgil.SDK.Identities;
    using Virgil.SDK.Models;

    /// <summary>
    /// Provides common methods to interact with Virgil Card resource endpoints.
    /// </summary>
    /// <seealso cref="EndpointClient" />
    /// <seealso cref="ICardsClient" />
    internal class CardsClient : ResponseVerifyClient, ICardsClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CardsClient" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="cache">The services key cache.</param>
        public CardsClient(IConnection connection, IServiceKeyCache cache) : base(connection)
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
        
        /// <summary>
        /// Searches the private cards by specified criteria.
        /// </summary>
        /// <param name="identityValue">The value of identifier.</param>
        /// <param name="identityType">The type of identifier.</param>
        /// <param name="includeUnconfirmed">Unconfirmed Virgil cards will be included in output. Optional</param>
        /// <returns>The collection of Virgil Cards.</returns>
        public async Task<IEnumerable<CardModel>> Search (string identityValue, string identityType = null, 
            bool? includeUnconfirmed = null)
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

            if (includeUnconfirmed.HasValue)
            {
                body["include_unconfirmed"] = includeUnconfirmed.Value;
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
        /// Gets the cards by specified public key.
        /// </summary>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <param name="cardId">The private/public keys associated card identifier.</param>
        /// <param name="privateKey">The private key. Private key is used to produce sign. 
        /// It is not transfered over network</param>
        /// <param name="privateKeyPassword">The private key password.</param>
        public async Task<IEnumerable<CardModel>> GetCardsRealtedToThePublicKey
        (
            Guid publicKeyId,
            Guid cardId,
            byte[] privateKey,
            string privateKeyPassword = null
        )
        {
            Ensure.ArgumentNotNull(privateKey, nameof(privateKey));

            var request = Request.Create(RequestMethod.Get)
                .WithEndpoint($"/v3/public-key/{publicKeyId}")
                .SignRequest(cardId, privateKey, privateKeyPassword);

            var response = await this.Send<PublicKeyExtendedResponse>(request).ConfigureAwait(false);
            var publicKey = new PublicKeyModel
            {
                Id = response.Id,
                Value = response.Value,
                CreatedAt = response.CreatedAt
            };

            foreach (var cardModel in response.Cards)
            {
                cardModel.PublicKey = publicKey;
            }
            
            return response.Cards.ToList();
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

        /// <summary>
        /// Trusts the specified card by signing the card's Hash attribute.
        /// </summary>
        /// <param name="trustedCardId">The trusting Virgil Card.</param>
        /// <param name="trustedCardHash">The trusting Virgil Card Hash value.</param>
        /// <param name="ownerCardId">The signer virgil card identifier.</param>
        /// <param name="privateKey">The signer private key. Private key is used to produce sign. It is not transfered over network</param>
        /// <param name="privateKeyPassword">The private key password.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<SignModel> Trust
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
                
                return await this.Send<SignModel>(request).ConfigureAwait(false);
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