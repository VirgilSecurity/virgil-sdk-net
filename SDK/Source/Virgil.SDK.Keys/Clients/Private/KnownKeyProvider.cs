namespace Virgil.SDK.Keys.Clients
{
    using System;
    using System.Threading.Tasks;
    using TransferObject;

    /// <summary>
    /// Provides cached value of known public key for channel ecnryption
    /// </summary>
    /// <seealso cref="Virgil.SDK.Keys.Clients.IKnownKeyProvider" />
    public class KnownKeyProvider : IKnownKeyProvider
    {
        private readonly IPublicKeysClient publicKeysClient;
        private PublicKeyDto publicKeyDto;

        /// <summary>
        /// Initializes a new instance of the <see cref="KnownKeyProvider"/> class.
        /// </summary>
        /// <param name="publicKeysClient">The public keys client.</param>
        public KnownKeyProvider(IPublicKeysClient publicKeysClient)
        {
            this.publicKeysClient = publicKeysClient;
        }

        /// <summary>
        /// Gets the known key.
        /// </summary>
        /// <returns>Known key</returns>
        public async Task<KnownKey> GetKnownKey()
        {
            if (publicKeyDto == null)
            {
                publicKeyDto = await publicKeysClient.Get(Guid.NewGuid());
            }

            return new KnownKey(publicKeyDto.Id, publicKeyDto.PublicKey);
        }
    }
}