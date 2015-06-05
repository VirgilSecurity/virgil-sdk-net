namespace Virgil.PKI.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using Virgil.PKI.Models;

    public interface IPublicKeysClient
    {
        /// <summary>
        /// Returns an certificate specified by certificate ID.
        /// </summary>
        /// <param name="certificateId">The cetificate ID.</param>
        /// <returns>A <see cref="VirgilPublicKey"/></returns>
        VirgilPublicKey Get(Guid certificateId);

        /// <summary>
        /// Returns an certificate specified by user ID.
        /// </summary>
        /// <param name="userId">The user data identifier value.</param>
        /// <returns>A <see cref="VirgilPublicKey"/></returns>
        VirgilPublicKey Get(string userId);

        /// <summary>
        /// Returns a list of certificates appurtenant to specified account ID.
        /// </summary>
        /// <returns>A list of <see cref="VirgilPublicKey"/></returns>
        IEnumerable<VirgilPublicKey> GetAll(Guid accountId);

        /// <summary>
        /// Inserts new certificate given account ID and user data.
        /// </summary>
        /// <param name="accountId">The account ID</param>
        /// <param name="userData">The user data</param>
        /// <returns>Newly created <see cref="VirgilPublicKey"/></returns>
        VirgilPublicKey Insert(Guid accountId, VirgilUserData userData);

        /// <summary>
        /// Deletes an certificate specified by certificate ID.
        /// </summary>
        /// <param name="certificateId">The certificate ID</param>
        void Delete(Guid certificateId);
    }
}   