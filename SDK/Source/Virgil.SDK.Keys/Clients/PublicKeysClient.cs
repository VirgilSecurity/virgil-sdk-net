namespace Virgil.SDK.Keys.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Crypto;
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

        public async Task<PublicKeyExtended> Create(byte[] publicKey, byte[] privateKey, IEnumerable<UserData> userData)
        {
            Ensure.ArgumentNotNull(publicKey, nameof(publicKey));
            Ensure.ArgumentNotNull(privateKey, nameof(privateKey));
            Ensure.ArgumentNotNull(userData, nameof(userData));

            var body = new
            {
                public_key = publicKey,
                user_data = userData.Select(it => new UserDataCreateRequest(it)),
                request_sign_uuid = Guid.NewGuid().ToString()
            };

            var request = Request.Create(RequestMethod.Post)
                .WithEndpoint("v2/public-key")
                .WithBody(body)
                .SignRequest(privateKey, Guid.NewGuid());

            var dto = await this.Send<PubPublicKey>(request);
            return new PublicKeyExtended(dto);
        }

        public Task<PublicKeyExtended> Create(byte[] publicKey, byte[] privateKey, UserData userData)
        {
            Ensure.ArgumentNotNull(userData, nameof(userData));

            return Create(publicKey, privateKey, new[] {userData});
        }
        
        public async Task<PublicKey> Update(
            Guid publicKeyId,
            byte[] newPublicKey,
            byte[] newPrivateKey,
            byte[] oldPrivateKey)
        {
            Ensure.ArgumentNotNull(newPublicKey, nameof(newPublicKey));
            Ensure.ArgumentNotNull(newPrivateKey, nameof(newPrivateKey));

            var uuid = Guid.NewGuid().ToString();
            byte[] uuid_sign;
            using (var signer = new VirgilSigner())
            {
                uuid_sign = signer.Sign(Encoding.UTF8.GetBytes(uuid), newPrivateKey);
            }
            
            var body = new
            {
                public_key = newPublicKey,
                request_sign_uuid = uuid,
                uuid_sign = uuid_sign
            };

            var request = Request.Create(RequestMethod.Put)
                .WithEndpoint($"/v2/public-key/{publicKeyId}")
                .WithBody(body)
                .SignRequest(oldPrivateKey, publicKeyId);

            var dto = await this.Send<PubPublicKey>(request);
            return new PublicKey(dto);
        }
        
        public async Task Delete(Guid publicKeyId, byte[] privateKey)
        {
            Ensure.ArgumentNotNull(privateKey, nameof(privateKey));

            var body = new
            {
                request_sign_uuid = Guid.NewGuid().ToString()
            };

            var request = Request.Create(RequestMethod.Delete)
                .WithEndpoint($"/v2/public-key/{publicKeyId}")
                .WithBody(body)
                .SignRequest(privateKey, publicKeyId);

            await this.Send(request);
        }

        public async Task<PublicKey> GetById(Guid publicKeyId)
        {
            var request = Request.Create(RequestMethod.Get)
                .WithEndpoint($"/v2/public-key/{publicKeyId}");

            var dto = await this.Send<PubPublicKey>(request);
            return new PublicKey(dto);
        }

        public async Task<PublicKey> Search(string userId)
        {
            Ensure.ArgumentNotNullOrEmptyString(userId, nameof(userId));

            var body = new
            {
                value = userId,
                request_sign_uuid = Guid.NewGuid().ToString()
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
                request_sign_uuid = Guid.NewGuid().ToString()
            };

            var request = Request.Create(RequestMethod.Post)
                .WithEndpoint("/v2/public-key/actions/grab")
                .WithBody(body)
                .SignRequest(privateKey, publicKeyId);

            var dto = await this.Send<PubPublicKey>(request);
            return new PublicKeyExtended(dto);
        }
    }
}