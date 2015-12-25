namespace Virgil.SDK.Keys.Clients
{
    using System;
    using System.Threading.Tasks;
    using TransferObject;

    /// <summary>
    ///     Provides common methods to interact with Public Keys resource endpoints.
    /// </summary>
    public interface IPublicKeysClient : IVirgilService
    {
        Task<PublicKeyDto> Get(Guid publicKeyId);

        Task<GetPublicKeyExtendedResponse> GetExtended(Guid publicKeyId,
            Guid virgilCardId,
            byte[] privateKey);
    }
}