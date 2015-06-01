namespace Virgil.PKI.Clients
{
    using Virgil.PKI.Models;

    public interface IAccountsClient
    {
        /// <summary>
        /// Registers an account specified by the <see cref="VirgilUserData"/> user data and public key.
        /// </summary>
        /// <param name="userData">The user data information</param>
        /// <param name="publicKey">Generated Public Key</param>
        /// <returns>An <see cref="VirgilAccount"/></returns>
        VirgilAccount Register(VirgilUserData userData, byte[] publicKey);
    }
}