namespace Virgil.SDK.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Virgil.SDK.Clients.Models;
    using Virgil.SDK.Http;

    /// <summary>
    /// Provides common methods to interact with Virgil Card resource endpoints.
    /// </summary>
    /// <seealso cref="EndpointClient" />
    /// <seealso cref="ICardsServiceClient" />
    internal class CardsServiceClient : ResponseVerifyClient, ICardsServiceClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CardsServiceClient" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="cache">The services key cache.</param>
        public CardsServiceClient(IConnection connection, IServiceKeyCache cache) : base(connection)
        {
            this.EndpointApplicationId = ServiceIdentities.PublicService;
            this.Cache = cache;
        }

        public SecureRequest BuildCreateRequest(VirgilCardCreateModel model)
        {
            var request = Request.Create(RequestMethod.Post)
                .WithBody(model)
                .WithEndpoint("/v4/virgil-card");

            var secureRequest = new SecureRequest(request);
            return secureRequest;
        }

        public SecureRequest BuildRevokeRequest(Guid cardId)
        {
            var request = Request.Create(RequestMethod.Delete)
                .WithEndpoint($"v4/virgil-card/{cardId}");

            var secureRequest = new SecureRequest(request);
            return secureRequest;
        }
        
        public async Task<IEnumerable<VirgilCardModel>> SearchInGlobalScopeAsync(IEnumerable<string> identities, string identityType = null)
        {
            var body = new Dictionary<string, object>
            {
                ["identities"] = identities,
                ["scope"] = "global"
            };

            if (!string.IsNullOrWhiteSpace(identityType))
            {
                body["identity_type"] = identityType;
            }
            
            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .WithEndpoint("/v4/virgil-card/actions/search");

            return await this.Send<IEnumerable<VirgilCardModel>>(request).ConfigureAwait(true);
        }

        public async Task<IEnumerable<VirgilCardModel>> SearchInAppScopeAsync(IEnumerable<string> identities, string identityType = null, bool isConfirmed = false)
        {
            var body = new Dictionary<string, object>
            {
                ["identities"] = identities,
                ["scope"] = "application"
            };

            if (!string.IsNullOrWhiteSpace(identityType))
            {
                body["identity_type"] = identityType;
            }

            if (isConfirmed)
            {
                body["is_confirmed"] = true;
            }   

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .WithEndpoint("/v4/virgil-card/actions/search");

            return await this.Send<IEnumerable<VirgilCardModel>>(request).ConfigureAwait(true);
        }

        public Task<VirgilCardModel> GetAsync(Guid cardId)
        {
            var request = Request.Create(RequestMethod.Get)
                .WithEndpoint($"/v4/virgil-card/{cardId}");
            
            return this.Send<VirgilCardModel>(request);
        }

        public async Task<TResponse> SendSecurityRequest<TResponse>(SecureRequest request)
        {
            return await this.Send<TResponse>(request.Request).ConfigureAwait(false);
        }
    }
}