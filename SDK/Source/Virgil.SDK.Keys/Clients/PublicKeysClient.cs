namespace Virgil.SDK.Keys.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Helpers;
    using Http;
    using Model;
    using TransferObject;

    /// <summary>
    /// Provides common methods to interact with Public Keys resource endpoints.
    /// </summary>
    public class PublicKeysClient : EndpointClient, IPublicKeysClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublicKeysClient" /> class with the default implemetations. 
        /// </summary>
        public PublicKeysClient(IConnection connection) : base(connection)
        {
        }

        public async Task<PublicKey> Create(byte[] publicKey, byte[] privateKey, IEnumerable<UserData> userData)
        {
            Ensure.ArgumentNotNull(publicKey, nameof(publicKey));
            Ensure.ArgumentNotNull(privateKey, nameof(privateKey));
            Ensure.ArgumentNotNull(userData, nameof(userData));

            var body = new
            {
                public_key = publicKey,
                userData = userData.Select(it => new UserDataCreateRequest(it)),
                request_sign_random_uuid = Guid.NewGuid().ToString()
            };

            var request = Request.Create(RequestMethod.Post)
                .WithEndpoint("/v2/public-key")
                .WithBody(body)
                .SignRequest(privateKey);

            var dto = await this.Send<PubPublicKey>(request);
            return new PublicKey(dto);
        }

        public Task<PublicKey> Create(byte[] publicKey, byte[] privateKey, UserData userData)
        {
            Ensure.ArgumentNotNull(publicKey, nameof(publicKey));
            Ensure.ArgumentNotNull(privateKey, nameof(privateKey));
            Ensure.ArgumentNotNull(userData, nameof(userData));

            return Create(publicKey, privateKey, new[] {userData});
        }
        
        public async Task<PublicKey> Update(Guid publicKeyId, byte[] publicKey, byte[] privateKey)
        {
            Ensure.ArgumentNotNull(publicKey, nameof(publicKey));
            Ensure.ArgumentNotNull(privateKey, nameof(privateKey));

            var body = new
            {
                public_key = publicKey,
                request_sign_random_uuid = Guid.NewGuid().ToString()
            };

            var request = Request.Create(RequestMethod.Put)
                .WithEndpoint($"/v2/public-key/{publicKeyId}")
                .WithBody(body)
                .SignRequest(privateKey);

            var dto = await this.Send<PubPublicKey>(request);
            return new PublicKey(dto);
        }
        
        public async Task Delete(Guid publicKeyId, byte[] privateKey)
        {
            Ensure.ArgumentNotNull(privateKey, nameof(privateKey));

            var body = new
            {
                request_sign_random_uuid = Guid.NewGuid().ToString()
            };

            var request = Request.Create(RequestMethod.Delete)
                .WithEndpoint($"/v2/public-key/{publicKeyId}")
                .WithBody(body)
                .SignRequest(privateKey);

            await this.Send(request);
        }

        public async Task<PublicKey> Search(string userId)
        {
            Ensure.ArgumentNotNullOrEmptyString(userId, nameof(userId));

            var body = new
            {
                value = userId,
                request_sign_random_uuid = Guid.NewGuid().ToString()
            };

            var request = Request.Create(RequestMethod.Post)
                .WithEndpoint("/v2/public-key/actions/grab")
                .WithBody(body);

            var dto = await this.Send<PubPublicKey>(request);
            return new PublicKey(dto);
        }

        public async Task<PublicKeyExtended> SearchExtended(Guid publicKeyId, byte[] privateKey)
        {
            Ensure.ArgumentNotNull(privateKey, nameof(privateKey));

            var body = new
            {
                request_sign_random_uuid = Guid.NewGuid().ToString()
            };

            var request = Request.Create(RequestMethod.Post)
                .WithEndpoint("/v2/public-key/actions/grab")
                .WithBody(body)
                .WithPublicKeyIdHeader(publicKeyId)
                .SignRequest(privateKey);

            var dto = await this.Send<PubPublicKey>(request);
            return new PublicKeyExtended(dto);
        }
    }
}