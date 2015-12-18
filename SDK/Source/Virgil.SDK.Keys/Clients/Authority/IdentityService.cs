namespace Virgil.SDK.Keys.Clients.Authority
{
    using System;
    using System.Threading.Tasks;
    using Helpers;
    using Http;
    using TransferObject;

    public class IdentityService : EndpointClient, IIdentityService
    {
        public IdentityService(IConnection connection) : base(connection)
        {
        }

        public async Task<VirgilVerifyResponse> Verify(string value, IdentityType type)
        {
            Ensure.ArgumentNotNull(value, nameof(value));

            var body = new
            {
                value = value,
                type = type
            };

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .WithEndpoint("v1/verify");

            return await this.Send<VirgilVerifyResponse>(request);
        }

        public async Task<VirgilIndentityToken> Confirm(string code, Guid rquestId, int timeToLive = 3600, int countToLive = 1)
        {
            Ensure.ArgumentNotNull(code, nameof(code));

            var body = new
            {
                confirmation_code = code,
                identity = new
                {
                    id = rquestId,
                    ttl = timeToLive,
                    ctl = countToLive
                }
            };

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .WithEndpoint("v1/confirm");

            return await this.Send<VirgilIndentityToken>(request);
        }

        public async Task<bool> Validate(VirgilIndentityToken indentityToken)
        {
            Ensure.ArgumentNotNull(indentityToken, nameof(indentityToken));
            
            var request = Request.Create(RequestMethod.Post)
                .WithBody(indentityToken)
                .WithEndpoint("v1/confirm");

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
    }
}