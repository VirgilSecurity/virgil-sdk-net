namespace Virgil.SDK.Keys.Clients
{
    using System.Threading.Tasks;

    using Virgil.SDK.Keys.Exceptions;
    using Virgil.SDK.Keys.Model;

    /// <summary>
    /// Provides common methods to interact with Account resource endpoints.
    /// </summary>
    public interface IAccountsClient
    {
        /// <summary>
        /// Registers an account specified by the <see cref="UserData" /> user data and public key.
        /// </summary>
        /// <param name="dataType">The user data type information</param>
        /// <param name="userId">The user data ID value</param>
        /// <param name="publicKey">Generated Public Key</param>
        /// <exception cref="UserDataAlreadyExistsException">Appears when UserData already exists with given value</exception>
        /// <returns>An <see cref="Account" /></returns>
        Task<Account> Register(UserDataType dataType, string userId, byte[] publicKey);
    }
}