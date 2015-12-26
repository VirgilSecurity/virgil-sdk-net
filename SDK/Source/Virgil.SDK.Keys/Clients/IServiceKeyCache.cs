namespace Virgil.SDK.Keys.Clients
{
    using System;
    using System.Threading.Tasks;
    using TransferObject;

    /// <summary>
    ///     Provides cached value of known public key for channel ecnryption
    /// </summary>
    public interface IServiceKeyCache
    {
        Task<PublicKeyDto> GetServiceKey(Guid servicePublicKeyId);
    }
}