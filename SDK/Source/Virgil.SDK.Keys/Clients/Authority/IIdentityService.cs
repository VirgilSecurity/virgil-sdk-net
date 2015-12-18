namespace Virgil.SDK.Keys.Clients.Authority
{
    using System;
    using System.Threading.Tasks;
    using TransferObject;

    public interface IIdentityService
    {
        Task<VirgilVerifyResponse> Verify(string value, IdentityType type);
        Task<VirgilIndentityToken> Confirm(string code, Guid rquestId, int timeToLive, int countToLive);
        Task<bool> Validate(VirgilIndentityToken indentityToken);
    }
}