namespace Virgil.SDK.Keys.Clients
{
    using System.Threading.Tasks;
    using TransferObject;

    public interface IIdentityClient : IVirgilService
    {
        Task<VirgilVerifyResponse> Verify(string value, IdentityType type);
        Task<IndentityTokenDto> Confirm(string code, string actionId, int timeToLive = 3600, int countToLive = 1);
        Task<bool> IsValid(IdentityType type, string value, string validationToken);
        Task<bool> IsValid(IndentityTokenDto token);
    }
}