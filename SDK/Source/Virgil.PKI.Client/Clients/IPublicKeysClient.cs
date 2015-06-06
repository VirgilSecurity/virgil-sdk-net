namespace Virgil.PKI.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using Virgil.PKI.Models;
    using System.Threading.Tasks;

    public interface IPublicKeysClient
    {
        Task<VirgilPublicKey> GetKey(Guid publicKeyId);
        Task<IEnumerable<VirgilPublicKey>> SearchKey(string userId, UserDataType type);
        Task<VirgilPublicKey> AddKey(Guid accountId, byte[] publicKey, IEnumerable<VirgilUserData> userData);
    }
}   