namespace Virgil.SDK.Domain
{
    using System.Threading.Tasks;
    using Virgil.SDK.Infrastructure;
    using Virgil.SDK.TransferObject;

    public class IdentityTokenRequest
    {
        private readonly VirgilVerifyResponse response;

        internal IdentityTokenRequest()
        {
        }

        internal IdentityTokenRequest(VirgilVerifyResponse virgilVerifyResponse)
        {
            this.response = virgilVerifyResponse;
        }

        public string Identity { get; private set; }

        public IdentityType IdentityType { get; private set; }

        internal static async Task<IdentityTokenRequest> Verify(string value, IdentityType type)
        {
            var identityService = ServiceLocator.Services.Identity;
            var request = await identityService.Verify(value, type);
            return new IdentityTokenRequest(request)
            {
                Identity = value,
                IdentityType = type
            };
        }

        public async Task<IdentityTokenDto> Confirm(string confirmationCode, ConfirmOptions options = null)
        {
            options = options ?? ConfirmOptions.Default;

            var identityService = ServiceLocator.Services.Identity;
            var token = await identityService.Confirm(this.response.ActionId, 
                        confirmationCode, options.TimeToLive, options.CountToLive);

            return token;
        }
    }
}