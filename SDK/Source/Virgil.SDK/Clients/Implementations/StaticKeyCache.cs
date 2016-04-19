namespace Virgil.SDK.Clients
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using TransferObject;

    /// <summary>
    /// Represents static key cache
    /// </summary>
    /// <seealso cref="Virgil.SDK.Clients.IServiceKeyCache" />
    internal class StaticKeyCache : IServiceKeyCache
    {
        private readonly Dictionary<string, VirgilCardDto> cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticKeyCache"/> class.
        /// </summary>
        public StaticKeyCache()
        {
            this.cache = JsonConvert.DeserializeObject<Dictionary<string, VirgilCardDto>>(Keys.Data);
        }

        /// <summary>
        /// Gets the service's public key by specified identifier.
        /// </summary>
        /// <param name="servicePublicKeyId">The service's public key identifier.</param>
        /// <returns>
        /// An instance of <see cref="VirgilCardDto" />, that represents service card.
        /// </returns>
        public Task<VirgilCardDto> GetServiceCard(string servicePublicKeyId)
        {
            VirgilCardDto result = null;
            this.cache.TryGetValue(servicePublicKeyId, out result);
            return Task.FromResult(result);
        }
    }
}