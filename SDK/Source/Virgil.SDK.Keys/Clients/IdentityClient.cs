namespace Virgil.SDK.Keys.Clients
{
    using System;
    using System.Threading.Tasks;
    using Helpers;
    using Http;
    using Infrastructurte;
    using TransferObject;

    public class IdentityClient : EndpointClient, IIdentityClient
    {
        private readonly IServiceKeyCache cache;

        public IdentityClient(IConnection connection, IServiceKeyCache cache) : base(connection)
        {
            this.cache = cache;
            this.EndpointPublicKeyId = KnownKeyIds.IdentityService;
        }

        public IdentityClient(string accessToken, string baseUri = ApiConfig.IdentityServiceAddress)
            :base(new IdentityConnection(new Uri(baseUri)))
        {
            this.cache = new ServiceKeyCache(new PublicKeysClient(accessToken));
            this.EndpointPublicKeyId = KnownKeyIds.IdentityService;
        }

        public async Task<VirgilVerifyResponse> Verify(string value, IdentityType type)
        {
            Ensure.ArgumentNotNull(value, nameof(value));

            var body = new
            {
                type = type,
                value = value,
            };

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .WithEndpoint("v1/verify");

            return await this.Send<VirgilVerifyResponse>(request);
        }

        public async Task<IndentityTokenDto> Confirm(string code, string actionId, int timeToLive = 3600, int countToLive = 1)
        {
            Ensure.ArgumentNotNull(code, nameof(code));

            var body = new
            {
                confirmation_code = code,
                action_id = actionId,
                token = new
                {
                    time_to_live = timeToLive,
                    count_to_live = countToLive
                }
            };

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .WithEndpoint("v1/confirm");

            return await this.Send<IndentityTokenDto>(request);
        }

        public async Task<bool> IsValid(IdentityType type, string value, string validationToken)
        {
            Ensure.ArgumentNotNull(value, nameof(value));
            Ensure.ArgumentNotNull(validationToken, nameof(validationToken));

            var request = Request.Create(RequestMethod.Post)
                .WithBody(new
                {
                    type = type,
                    value = value,
                    validation_token = validationToken
                })
                .WithEndpoint("v1/validate");

            try
            {
                await this.Send(request);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Task<bool> IsValid(IndentityTokenDto token)
        {
            Ensure.ArgumentNotNull(token, nameof(token));
            return this.IsValid(token.Type, token.Value, token.ValidationToken);
        }

        protected override async Task<IResponse> Send(IRequest request)
        {
            var result = await base.Send(request);
            var key = await this.cache.GetServiceKey(this.EndpointPublicKeyId);
            this.VerifyResponse(result, key.PublicKey);
            return result;
        }
    }
}