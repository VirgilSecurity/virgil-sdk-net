namespace Virgil.SDK.Keys.Clients
{
    using System.Threading.Tasks;

    /// <summary>
    ///     Provides cached value of known public key for channel ecnryption
    /// </summary>
    public interface IKnownKeyProvider
    {
        /// <summary>
        ///     Gets the known key.
        /// </summary>
        /// <returns>Known key</returns>
        Task<KnownKey> GetPrivateKeySerivcePublicKey();

        Task<KnownKey> GetIdentitySerivcePublicKey();
    }
}