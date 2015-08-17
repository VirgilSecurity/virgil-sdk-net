namespace Virgil.SDK.Keys.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Virgil.SDK.Keys.Model;

    /// <summary>
    /// Provides common methods to interact with Public Keys resource endpoints.
    /// </summary>
    public interface IPublicKeysClient
    {
        Task<PublicKey> Create(byte[] publicKey, byte[] privateKey, IEnumerable<UserData> userData);
        Task<PublicKey> Create(byte[] publicKey, byte[] privateKey, UserData userData);
        Task<PublicKey> Update(Guid publicKeyId, byte[] publicKey, byte[] privateKey);
        Task Delete(Guid publicKeyId, byte[] privateKey);
        Task<PublicKey> Search(string userId);
        Task<PublicKeyExtended> SearchExtended(Guid publicKeyId, byte[] privateKey);
    }
}