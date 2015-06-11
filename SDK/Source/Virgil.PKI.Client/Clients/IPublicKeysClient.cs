namespace Virgil.PKI.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using Virgil.PKI.Models;
    using System.Threading.Tasks;

    public interface IPublicKeysClient
    {
        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <returns></returns>
        Task<VirgilPublicKey> GetKey(Guid publicKeyId);
        
        /// <summary>
        /// Searches the key.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        Task<IEnumerable<VirgilPublicKey>> SearchKey(string userId, UserDataType type);
        
        /// <summary>
        /// Adds the key.
        /// </summary>
        /// <param name="accountId">The account identifier.</param>
        /// <param name="publicKey">The public key.</param>
        /// <param name="userData">The user data.</param>
        /// <returns></returns>
        Task<VirgilPublicKey> AddKey(Guid accountId, byte[] publicKey, IEnumerable<VirgilUserData> userData);
    }
}   