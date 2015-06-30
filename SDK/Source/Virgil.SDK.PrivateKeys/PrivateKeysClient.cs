namespace Virgil.SDK.PrivateKeys
{
    using System;
    using System.Threading.Tasks;
    
    using Virgil.SDK.PrivateKeys.Dtos;

    /// <summary>
    /// Provides common methods for sending data to and receiving data from a Private Keys API service.
    /// </summary>
    public class PrivateKeysClient : IPrivateKeysClient
    {
        /// <summary>
        /// Registers an accoint on Private Keys API service. 
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="password"></param>
        /// <param name="encryptKeys"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public Task<KeyRingAccount> Register(Guid accountId, string password, bool encryptKeys, byte[] sign)
        {
            throw new NotImplementedException();
        }
    }
}