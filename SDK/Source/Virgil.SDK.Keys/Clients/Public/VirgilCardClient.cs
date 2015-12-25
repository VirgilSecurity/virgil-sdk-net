namespace Virgil.SDK.Keys.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Crypto;
    using Domain;
    using Helpers;
    using Http;
    using Newtonsoft.Json;
    using TransferObject;
    
    /// <summary>
    ///     Provides common methods to interact with Virgil Card resource endpoints.
    /// </summary>
    /// <seealso cref="Virgil.SDK.Keys.Clients.EndpointClient" />
    public class VirgilCardClient : EndpointClient, IVirgilCardClient
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VirgilCardClient" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public VirgilCardClient(IConnection connection) : base(connection)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilCardClient"/> class.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="baseUri">The base URI.</param>
        public VirgilCardClient(string accessToken, string baseUri = ApiConfig.PublicServicesAddress) 
            : base(new PublicServicesConnection(accessToken, new Uri(baseUri)))
        {
        }

        /// <summary>
        /// Creates a new Virgil Card attached to known public key with unconfirmed ientity.
        /// </summary>
        /// <param name="value">The value of identity.</param>
        /// <param name="type">The type of virgil card.</param>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <param name="privateKey">The private key. Private key is used to produce sign. It is not transfered over network</param>
        /// <param name="customData">The custom data.</param>
        /// <remarks>This card will not be searchable by default.</remarks>
        /// <returns>An instance of <see cref="VirgilCardDto"/></returns>
        public Task<VirgilCardDto> Create(
            string value,
            IdentityType type,
            Guid publicKeyId,
            byte[] privateKey,
            Dictionary<string, string> customData = null)
        {
            return this.AttachInternal(publicKeyId, type, value, privateKey, customData);
        }

        /// <summary>
        /// Creates a new Virgil Card with unconfirmed identity.
        /// </summary>
        /// <param name="value">The value of identity.</param>
        /// <param name="type">The type of virgil card.</param>
        /// <param name="publicKey">The public key.</param>
        /// <param name="privateKey">The private key. Private key is used to produce sign. It is not transfered over network</param>
        /// <param name="customData">The custom data.</param>
        /// <remarks>This card will not be searchable by default.</remarks>
        /// <returns>An instance of <see cref="VirgilCardDto"/></returns>
        public Task<VirgilCardDto> Create(
            string value,
            IdentityType type,
            byte[] publicKey,
            byte[] privateKey,
            Dictionary<string, string> customData = null)
        {
            return this.CreateInternal(publicKey, type, value, privateKey, customData);
        }

        /// <summary>
        /// Creates a new Virgil Card attached to known public key with confirmed ientity.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <param name="privateKey">The private key. Private key is used to produce sign. It is not transfered over network</param>
        /// <param name="customData">The custom data.</param>
        /// <returns>An instance of <see cref="VirgilCardDto"/></returns>
        public Task<VirgilCardDto> Create(
            IndentityTokenDto token,
            Guid publicKeyId,
            byte[] privateKey,
            Dictionary<string, string> customData = null)
        {
            return this.AttachInternal(publicKeyId, token.Type, token.Value, privateKey, customData, token.ValidationToken);
        }

        /// <summary>
        /// Creates a new Virgil Card with confirmed identity.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="publicKey">The public key.</param>
        /// <param name="privateKey">The private key. Private key is used to produce sign. It is not transfered over network</param>
        /// <param name="customData">The custom data.</param>
        /// <returns>An instance of <see cref="VirgilCardDto"/></returns>
        public Task<VirgilCardDto> Create(
            IndentityTokenDto token,
            byte[] publicKey,
            byte[] privateKey,
            Dictionary<string, string> customData = null)
        {
            return this.CreateInternal(publicKey, token.Type, token.Value, privateKey, customData, token.ValidationToken);
        }
        
        private async Task<VirgilCardDto> CreateInternal(
            byte[] publicKey, IdentityType type, string value, byte[] privateKey,
            Dictionary<string, string> customData = null, string identityToken = null)
        {
            var body = new
            {
                public_key = publicKey,
                identity = new
                {
                    type,
                    value
                },
                data = customData,
                validation_token = identityToken
            };

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .WithEndpoint("/v3/virgil-card")
                .SignRequest(privateKey);

            return await this.Send<VirgilCardDto>(request);
        }
        
        private async Task<VirgilCardDto> AttachInternal(
            Guid publicKeyId,
            IdentityType type,
            string value,
            byte[] privateKey,
            Dictionary<string, string> customData = null,
            string identityToken = null)
        {
            var body = new
            {
                public_key_id = publicKeyId,
                identity = new
                {
                    type,
                    value
                },
                data = customData,
                validation_token = identityToken
            };

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .WithEndpoint("/v3/virgil-card")
                .SignRequest(privateKey);

            return await this.Send<VirgilCardDto>(request);
        }


        /// <summary>
        ///     Signs virgil card.
        /// </summary>
        /// <param name="signedVirgilCardId">The signed virgil card identifier.</param>
        /// <param name="signedVirgilCardHash">The signed virgil card hash.</param>
        /// <param name="signerVirgilCardId">The signer virgil card identifier.</param>
        /// <param name="signerPrivateKey">
        ///     The signer private key. Private key is used to produce sign. It is not transfered over
        ///     network
        /// </param>
        /// <returns></returns>
        public async Task<VirgilSignResponse> Sign(
            Guid signedVirgilCardId,
            string signedVirgilCardHash,
            Guid signerVirgilCardId,
            byte[] signerPrivateKey)
        {
            using (var virgilSigner = new VirgilSigner())
            {
                var body = new
                {
                    signed_virgil_card_id = signedVirgilCardId,
                    signed_digest = virgilSigner.Sign(Encoding.UTF8.GetBytes(signedVirgilCardHash), signerPrivateKey)
                };

                var request = Request.Create(RequestMethod.Post)
                    .WithBody(body)
                    .WithEndpoint($"/v3/virgil-card/{signerVirgilCardId}/actions/sign")
                    .SignRequest(signerPrivateKey, signerVirgilCardId);

                var @params = new
                {
                    signedVirgilCardId,
                    signedVirgilCardHash,
                    signerVirgilCardId,
                    signerPrivateKey
                };

                var all = new
                {
                    body,
                    @params,
                    request
                };

                var allJ = JsonConvert.SerializeObject(all, Formatting.Indented);

                return await this.Send<VirgilSignResponse>(request);
            }
        }

        /// <summary>
        ///     Unsigns the specified signed virgil card identifier.
        /// </summary>
        /// <param name="signedVirgilCardId">The signed virgil card identifier.</param>
        /// <param name="signerVirgilCardId">The signer virgil card identifier.</param>
        /// <param name="privateKey">The private key. Private key is used to produce sign. It is not transfered over network</param>
        public async Task Unsign(
            Guid signedVirgilCardId,
            Guid signerVirgilCardId,
            byte[] privateKey)
        {
            var body = new
            {
                signed_virgil_card_id = signedVirgilCardId
            };

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .WithEndpoint($"/v3/virgil-card/{signerVirgilCardId}/actions/unsign")
                .SignRequest(privateKey, signerVirgilCardId);

            await this.Send(request);
        }

        /// <summary>
        ///     Searches the specified value.
        /// </summary>
        /// <param name="value">The value of identifier. Required.</param>
        /// <param name="type">The type of identifier. Optional.</param>
        /// <param name="relations">Relations between Virgil cards. Optional</param>
        /// <param name="includeUnconfirmed">Unconfirmed Virgil cards will be included in output. Optional</param>
        /// <returns>List of virgil card dtos</returns>
        public async Task<List<VirgilCardDto>> Search(
            string value,
            IdentityType? type,
            IEnumerable<Guid> relations,
            bool? includeUnconfirmed)
        {
            Ensure.ArgumentNotNull(value, nameof(value));

            var body = new Dictionary<string, object>
            {
                ["value"] = value
            };

            if (type != null)
            {
                body["type"] = type.Value;
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

            return await this.Send<List<VirgilCardDto>>(request);
        }
    }
}