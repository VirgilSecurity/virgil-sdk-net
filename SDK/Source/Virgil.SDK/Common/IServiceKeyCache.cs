namespace Virgil.SDK.Common
{
    using System.Threading.Tasks;
    using Virgil.SDK.Models;

    /// <summary>
    /// Provides cached value of known public key for channel encryption
    /// </summary>
    public interface IServiceKeyCache : IVirgilService
    {
        /// <summary>
        /// Gets the service's public key by specified identifier.
        /// </summary>
        /// <param name="servicePublicKeyId">The service's public key identifier.</param>
        /// <returns>An instance of <see cref="CardModel"/>, that represents service card.</returns>
        Task<CardModel> GetServiceCard(string servicePublicKeyId);
    }
}