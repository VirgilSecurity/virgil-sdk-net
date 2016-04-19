namespace Virgil.SDK.Clients
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Http;
    using Newtonsoft.Json;

    using Virgil.SDK.Exceptions;
    using Virgil.SDK.TransferObject;

    /// <summary>
    /// Provides cached value of known public key for channel encryption and response verification
    /// </summary>
    /// <seealso cref="IServiceKeyCache" />
    internal class DynamicKeyCache : IServiceKeyCache
    {
        private readonly IConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicKeyCache" /> class.
        /// </summary>
        /// <param name="connection">The Virgil Public Services connection instance.</param>
        public DynamicKeyCache(IConnection connection)
        {
            this.connection = connection;
        }

        private readonly Dictionary<string, VirgilCardDto> cache = new Dictionary<string, VirgilCardDto>();

        /// <summary>
        /// Gets the service's public key by specified identifier.
        /// </summary>
        /// <param name="servicePublicKeyId">The service's public key identifier.</param>
        /// <returns>
        /// An instance of <see cref="PublicKeyDto" />, that represents Public Key.
        /// </returns>
        public async Task<VirgilCardDto> GetServiceCard(string servicePublicKeyId)
        {
            VirgilCardDto dto;

            if (!this.cache.TryGetValue(servicePublicKeyId, out dto))
            {
                dto = (await this.GetApplicationCards(servicePublicKeyId).ConfigureAwait(false)).FirstOrDefault();
                
                if (dto?.PublicKey != null)
                {
                    this.cache[servicePublicKeyId] = dto;
                }
                else
                {
                    throw new VirgilException($"Can't get virgil service card using {servicePublicKeyId} app identity");
                }
            }

            return dto;
        }

        private async Task<IEnumerable<VirgilCardDto>> GetApplicationCards(string applicationIdentity)
        {
            var request = Request.Create(RequestMethod.Post)
               .WithBody(new
               {
                   value = applicationIdentity
               })
               .WithEndpoint("v3/virgil-card/actions/search/app");

            var response = await this.connection.Send(request).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<IEnumerable<VirgilCardDto>>(response.Body);
        }
    }
}