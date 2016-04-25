namespace Virgil.SDK.PublicKeys
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Virgil.SDK.Common;
    using Virgil.SDK.Helpers;
    using Virgil.SDK.Http;
    using Virgil.SDK.Identities;
    using Virgil.SDK.Models;

    /// <summary>
    /// Provides common methods to interact with Public Keys resource endpoints.
    /// </summary>
    internal class PublicKeysClient : ResponseVerifyClient, IPublicKeysClient
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PublicKeysClient" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="cache">The service keys cache.</param>
        public PublicKeysClient(IConnection connection, IServiceKeyCache cache) : base(connection, cache)
        {
            this.EndpointApplicationId = ServiceIdentities.PublicService;
        }
        
        public async Task<PublicKeyModel> Get(Guid publicKeyId)
        {
            var request = Request.Create(RequestMethod.Get)
                .WithEndpoint($"/v3/public-key/{publicKeyId}");

            return await this.Send<PublicKeyModel>(request).ConfigureAwait(false);
        }

        public async Task Revoke(Guid publicKeyId, IEnumerable<IdentityInfo> indentityInfos, Guid cardId,
            byte[] privateKey, string privateKeyPassword = null)
        {
            Ensure.ArgumentNotNull(indentityInfos, nameof(indentityInfos));

            var request = Request.Create(RequestMethod.Delete)
                .WithBody(new
                {
                    identities = indentityInfos.ToArray()
                })
                .WithEndpoint($"/v3/public-key/{publicKeyId}")
                .SignRequest(cardId, privateKey, privateKeyPassword);

            await this.Send(request).ConfigureAwait(false);
        }
    }
}