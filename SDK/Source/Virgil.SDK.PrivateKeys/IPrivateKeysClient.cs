namespace Virgil.SDK.PrivateKeys
{
    using System;
    using System.Threading.Tasks;

    using Virgil.SDK.PrivateKeys.Dtos;

    public interface IPrivateKeysClient
    {
        Task<KeyRingAccount> Register(Guid accountId, string password, bool encryptKeys, byte[] sign);
    }
}