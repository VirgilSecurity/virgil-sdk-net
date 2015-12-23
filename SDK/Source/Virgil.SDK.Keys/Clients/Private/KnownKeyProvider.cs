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
        private PublicKeyDto privateKeyDto;
        private KnownKey identityServiceKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="KnownKeyProvider" /> class.
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
        public async Task<KnownKey> GetPrivateKeySerivcePublicKey()
        {
            if (this.privateKeyDto == null)
            {
                this.privateKeyDto = await this.publicKeysClient.Get(Guid.NewGuid());
            }

            return new KnownKey(this.privateKeyDto.Id, this.privateKeyDto.PublicKey);
        }

        public Task<KnownKey> GetIdentitySerivcePublicKey()
        {
            const string base64Key =
                @"LS0tLS1CRUdJTiBQVUJMSUMgS0VZLS0tLS0KTUlHYk1CUUdCeXFHU000OUFnRUdDU3NrQXdNQ0NBRUJEUU9CZ2dBRVUvYjVLd3VsdVd6UXRnQjlqOUFmanl6Mgpob0NjRTlCS2ZYNW1haVh5UnEvUzI2TTlGaW96SXhlREphMlpSVW1TZHk4VVJPc1pWNUdzMDNaOHhpSDliWWdECm93dHNLSWhFZUc5UTZ4VkQvWndXTm4rYmM2VEVOWUY0cVY1dkVUYkMyYjk0Qm5mNnRlRDhNUzFBeXh5QldZaUQKMFdOb3ByS01KczlzZUF1VW8wRT0KLS0tLS1FTkQgUFVCTElDIEtFWS0tLS0tCg==";

            if (this.identityServiceKey == null)
            {
                this.identityServiceKey = new KnownKey(Guid.NewGuid(), Convert.FromBase64String(base64Key));
            }

            return Task.FromResult(this.identityServiceKey);
        }
    }
}