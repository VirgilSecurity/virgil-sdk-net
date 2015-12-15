namespace Virgil.SDK.Keys.Clients
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using Helpers;
    using Http;
   
    using TransferObject;

    /// <summary>
    /// Provides common methods to interact with Public Keys resource endpoints.
    /// </summary>
    public class PublicKeysClient : EndpointClient, IPublicKeysClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublicKeysClient"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public PublicKeysClient(IConnection connection) : base(connection)
        {
        }

        /// <summary>
        /// Gets the specified public key by it identifier.
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
        /// Gets the specified public key by it identifier with extended data.
        /// </summary>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <param name="virgilCardId">The virgil card identifier.</param>
        /// <param name="privateKey">The private key. Private key is used to produce sign. It is not transfered over network</param>
        /// <returns>Extended ublic key dto response</returns>
        public async Task<GetPublicKeyExtendedResponse> GetExtended(Guid publicKeyId,
            Guid virgilCardId,
            byte[] privateKey)
        {
            Ensure.ArgumentNotNull(privateKey, nameof(privateKey));

            var request = Request.Create(RequestMethod.Get)
               .WithEndpoint($"/v3/public-key/{publicKeyId}")
               .SignRequest(privateKey, virgilCardId);

            return await this.Send<GetPublicKeyExtendedResponse>(request);
        }

    }
}