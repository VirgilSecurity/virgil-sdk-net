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
    public class PublicKeysClient : EndpointClient
    {
        public PublicKeysClient(IConnection connection) : base(connection)
        {
        }

        public async Task<PublicKeyDto> Get(Guid publicKeyId)
        {
            var request = Request.Create(RequestMethod.Get)
                .WithEndpoint($"/v3/public-key/{publicKeyId}");

            return await this.Send<PublicKeyDto>(request);
        }

        public async Task<GetPublicKeyExtendedResponse> GetExtended(
            Guid publicKeyId,
            
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