using System.Threading.Tasks;

namespace Virgil.PKI.Clients
{
    using System;

    using Virgil.PKI.Models;

    public interface IUserDataClient
    {
        /// <summary>
        /// Returns a user data specified by user data ID.
        /// </summary>
        /// <param name="userDataId">The user data ID.</param>
        /// <returns>An <see cref="VirgilUserData"/></returns>
        Task<VirgilUserData> Get(Guid userDataId);
        
        /// <summary>
        /// Inserts new user data to exiasting certificate given certificate ID and user data.
        /// </summary>
        /// <param name="publicKeyId">The public key ID</param>
        /// <param name="userData">The user data</param>
        /// <returns>An just created <see cref="VirgilUserData"/></returns>
        Task<VirgilUserData> Insert(Guid publicKeyId, VirgilUserData userData);
        
        Task Confirm(Guid userDataId, string confirmationCode);

        Task ResendConfirmation(Guid userDataId);
    }
}