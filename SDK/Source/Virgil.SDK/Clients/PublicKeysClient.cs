namespace Virgil.SDK.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Virgil.SDK.Helpers;
    using Virgil.SDK.Http;
    using Virgil.SDK.Infrastructure;
    using Virgil.SDK.TransferObject;

    /// <summary>
    ///     Provides common methods to interact with Public Keys resource endpoints.
    /// </summary>
    public class PublicKeysClient : ResponseVerifyClient, IPublicKeysClient
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PublicKeysClient" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="cache">The service keys cache.</param>
        public PublicKeysClient(IConnection connection, IServiceKeyCache cache) : base(connection, cache)
        {
            this.EndpointApplicationId = VirgilApplicationIds.PublicService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PublicKeysClient"/> class.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="baseUri">The base URI.</param>
        public PublicKeysClient(string accessToken, string baseUri = ApiConfig.PublicServicesAddress) 
            : base(new PublicServicesConnection(accessToken, new Uri(baseUri)))
        {
            this.EndpointApplicationId = VirgilApplicationIds.PublicService;
            this.Cache = new ServiceKeyCache(new VirgilCardsClient(accessToken));
        }

        /// <summary>
        ///     Gets the specified public key by it identifier.
        /// </summary>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <returns>Public key dto</returns>
        public async Task<PublicKeyDto> Get(Guid publicKeyId)
        {
            var request = Request.Create(RequestMethod.Get)
                .WithEndpoint($"/v3/public-key/{publicKeyId}");

            return await this.Send<PublicKeyDto>(request);
        }

        /// <summary>
        ///     Gets the specified public key by it identifier with extended data.
        /// </summary>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <param name="virgilCardId">The virgil card identifier.</param>
        /// <param name="privateKey">The private key. Private key is used to produce sign. It is not transfered over network</param>
        /// <returns>List of virgil cards</returns>
        public async Task<IEnumerable<VirgilCardDto>> GetExtended(Guid publicKeyId,
            Guid virgilCardId,
            byte[] privateKey)
        {
            Ensure.ArgumentNotNull(privateKey, nameof(privateKey));

            var request = Request.Create(RequestMethod.Get)
                .WithEndpoint($"/v3/public-key/{publicKeyId}")
                .SignRequest(virgilCardId, privateKey);

            var response = await this.Send<GetPublicKeyExtendedResponse>(request);
            return response.VirgilCards.Select(card => new VirgilCardDto(card, response)).ToList();
        }
    }
}