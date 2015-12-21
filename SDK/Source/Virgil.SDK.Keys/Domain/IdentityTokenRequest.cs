namespace Virgil.SDK.Keys.Domain
{
    using System.Threading.Tasks;
    using TransferObject;

    public class IdentityTokenRequest
    {
        private readonly VirgilVerifyResponse request;

        internal IdentityTokenRequest()
        {
        }

        internal IdentityTokenRequest(VirgilVerifyResponse virgilVerifyResponse)
        {
            request = virgilVerifyResponse;
        }

        internal static async Task<IdentityTokenRequest> Verify(string value, IdentityType type)
        {
            var identityService = ServiceLocator.IdentityService;
            var request = await identityService.Verify(value, type);
            return new IdentityTokenRequest(request)
            {
                Identity = value,
                IdentityType = type
            };
        }

        internal static Task<IdentityTokenRequest> Verify(Identity identity)
        {
            return Verify(identity.Value, identity.Type);
        }

        public string Identity { get; private set; }

        public IdentityType IdentityType { get; private set; }
        
        public async Task<IdentityToken> Confirm(string confirmationCode, ConfirmOptions options = null)
        {
            options = options ?? ConfirmOptions.Default;

            var identityService = ServiceLocator.IdentityService;
            var token = await identityService.Confirm(confirmationCode, request.Id, options.TimeToLive, options.CountToLive);
            return new IdentityToken(this, token);
        }
    }
}