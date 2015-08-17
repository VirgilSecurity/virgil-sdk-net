namespace Virgil.SDK.Keys.Clients
{
    using System;
    using System.Threading.Tasks;

    using Virgil.SDK.Keys.Model;

    /// <summary>
    /// Provides common methods to interact with UserData resource endpoints.
    /// </summary>
    public interface IUserDataClient
    {
        Task<UserData> Delete(Guid userDataId, Guid publicKeyId, byte[] privateKey);
        Task<UserData> Insert(UserData userData, Guid publicKeyId, byte[] privateKey);
        Task Confirm(Guid userDataId, string confirmationCode, Guid publicKeyId, byte[] privateKey);
        Task ResendConfirmation(Guid userDataId);
    }
}