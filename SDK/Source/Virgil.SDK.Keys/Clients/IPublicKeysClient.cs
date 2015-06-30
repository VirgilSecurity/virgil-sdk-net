using Virgil.SDK.Keys.Model;

namespace Virgil.SDK.Keys.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Virgil.SDK.Keys.Models;

    /// <summary>
    /// Provides common methods to interact with Public Keys resource endpoints.
    /// </summary>
    public interface IPublicKeysClient
    {
        /// <summary>
        /// Gets the key by public key id.
        /// </summary>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <returns></returns>
        Task<VirgilPublicKey> Get(Guid publicKeyId);

        /// <summary>
        /// Searches the key by userId ans UserDataType.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="type">The user data type.</param>
        /// <returns></returns>
        Task<IEnumerable<VirgilPublicKey>> Search(string userId, UserDataType type);

        /// <summary>
        /// Adds new public key to API given several user data.
        /// </summary>
        /// <param name="accountId">The account identifier.</param>
        /// <param name="publicKey">The public key.</param>
        /// <param name="userData">The list of user data.</param>
        /// <returns>instance of created <see cref="VirgilPublicKey"/></returns>
        Task<VirgilPublicKey> Add(Guid accountId, byte[] publicKey, IEnumerable<UserData> userData);

        /// <summary>
        /// Adds new public key to API given user data and account details.
        /// </summary>
        /// <param name="accountId">The account identifier.</param>
        /// <param name="publicKey">The actual public key</param>
        /// <param name="userData">The user data</param>
        /// <returns>instance of created <see cref="VirgilPublicKey"/></returns>
        Task<VirgilPublicKey> Add(Guid accountId, byte[] publicKey, UserData userData);
    }
}