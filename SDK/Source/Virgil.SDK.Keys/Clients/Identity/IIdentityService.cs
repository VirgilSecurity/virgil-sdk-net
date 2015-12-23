namespace Virgil.SDK.Keys.Clients.Authority
{
    using System;
    using System.Threading.Tasks;
    using TransferObject;

    public interface IIdentityService
    {
        Task<VirgilVerifyResponse> Verify(string value, IdentityType type);
        Task<VirgilIndentityToken> Confirm(string code, string actionId, int timeToLive, int countToLive);
        Task<bool> IsValid(IdentityType type, string value, string validationToken);
        Task<bool> IsValid(VirgilIndentityToken token);
    }
}