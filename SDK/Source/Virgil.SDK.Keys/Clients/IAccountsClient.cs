namespace Virgil.SDK.Keys.Clients
{
    using System.Threading.Tasks;
    using Models;

    public interface IAccountsClient
    {
        /// <summary>
        ///     Registers an account specified by the <see cref="VirgilUserData" /> user data and public key.
        /// </summary>
        /// <param name="userData">The user data information</param>
        /// <param name="publicKey">Generated Public Key</param>
        /// <returns>An <see cref="VirgilAccount" /></returns>
        Task<VirgilAccount> Register(VirgilUserData userData, byte[] publicKey);
    }
}