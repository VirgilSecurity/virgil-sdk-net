using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Virgil.SDK.Keys.TransferObject;

namespace Virgil.SDK.Keys.Clients
{
    

    /// <summary>
    /// Provides common methods to interact with Public Keys resource endpoints.
    /// </summary>
    public interface IPublicKeysClient
    {
        Task<PublicKeyDto> Get(Guid publicKeyId);

        Task<GetPublicKeyExtendedResponse> GetExtended(Guid publicKeyId, byte[] privateKey);
    }

    public interface IVirgilCardsClient
    {
        Task<VirgilCardDto> Create(byte[] publicKey, VirgilIdentityType type, string value, Dictionary<string,string> customData);

        Task<VirgilCardDto> AttachTo(Guid publicKeyId, VirgilIdentityType type, string value, Dictionary<string, string> customData);
    }
}