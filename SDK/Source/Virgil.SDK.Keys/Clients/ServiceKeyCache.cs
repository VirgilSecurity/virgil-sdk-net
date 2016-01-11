namespace Virgil.SDK.Keys.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Domain;
    using Exceptions;
    using Virgil.SDK.Keys.Infrastructure;
    using TransferObject;

    /// <summary>
    /// Provides cached value of known public key for channel encryption
    /// </summary>
    /// <seealso cref="IServiceKeyCache" />
    public class ServiceKeyCache : IServiceKeyCache
    {
        private readonly IVirgilCardsClient virgilCardsClient;
        

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceKeyCache" /> class.
        /// </summary>
        /// <param name="virgilCardsClient">The Virgil cards client.</param>
        public ServiceKeyCache(IVirgilCardsClient virgilCardsClient)
        {
            this.virgilCardsClient = virgilCardsClient;
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
                dto = (await this.virgilCardsClient.GetApplicationCard(servicePublicKeyId)).FirstOrDefault();
                
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
    }
}