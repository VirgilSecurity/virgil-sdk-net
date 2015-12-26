namespace Virgil.SDK.Keys.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;
    using Virgil.SDK.Keys.Infrastructure;
    using TransferObject;

    /// <summary>
    /// Provides cached value of known public key for channel ecnryption
    /// </summary>
    /// <seealso cref="IServiceKeyCache" />
    public class ServiceKeyCache : IServiceKeyCache
    {
        private readonly IPublicKeysClient publicKeysClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceKeyCache" /> class.
        /// </summary>
        /// <param name="publicKeysClient">The public keys client.</param>
        public ServiceKeyCache(IPublicKeysClient publicKeysClient)
        {
            this.publicKeysClient = publicKeysClient;
        }

#if DEBUG

        private const string IdentityKey = @"-----BEGIN PUBLIC KEY-----
MIGbMBQGByqGSM49AgEGCSskAwMCCAEBDQOBggAEU/b5KwuluWzQtgB9j9Afjyz2
hoCcE9BKfX5maiXyRq/S26M9FiozIxeDJa2ZRUmSdy8UROsZV5Gs03Z8xiH9bYgD
owtsKIhEeG9Q6xVD/ZwWNn+bc6TENYF4qV5vETbC2b94Bnf6teD8MS1AyxyBWYiD
0WNoprKMJs9seAuUo0E=
-----END PUBLIC KEY-----
";
        private readonly Dictionary<Guid, PublicKeyDto> cache = new Dictionary<Guid, PublicKeyDto>()
        {
            [KnownKeyIds.IdentityService] = new PublicKeyDto
            {
                Id = KnownKeyIds.IdentityService,
                PublicKey = IdentityKey.GetBytes()
            }
        };

#else
        private readonly Dictionary<Guid, PublicKeyDto> cache = new Dictionary<Guid, PublicKeyDto>()
#endif

        public async Task<PublicKeyDto> GetServiceKey(Guid servicePublicKeyId)
        {
            PublicKeyDto dto;
            if (this.cache.TryGetValue(servicePublicKeyId, out dto))
            {
                dto = await this.publicKeysClient.Get(servicePublicKeyId);
                this.cache[servicePublicKeyId] = dto;
            }

            return dto;
        }
    }
}