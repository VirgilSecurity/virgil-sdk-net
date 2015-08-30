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

        /// <summary>
        /// Creates the new public key.
        /// </summary>
        /// <param name="publicKey">The public key value.</param>
        /// <param name="privateKey">The private key value. Private key is not being sent, but used to sign the HTTP request body.</param>
        /// <param name="userData">The user data collection</param>
        /// <returns><see cref="PublicKeyExtended"/></returns>
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

        /// <summary>
        /// Creates the new public key.
        /// </summary>
        /// <param name="publicKey">The public key.</param>
        /// <param name="privateKey">The private key. Private key is not being sent, but used to sign the HTTP request body.</param>
        /// <param name="userData">The user data.</param>
        /// <returns><see cref="PublicKeyExtended"/></returns>
        public Task<PublicKeyExtended> Create(byte[] publicKey, byte[] privateKey, UserData userData)
        {
            Ensure.ArgumentNotNull(userData, nameof(userData));

            return Create(publicKey, privateKey, new[] {userData});
        }

        /// <summary>
        /// Updates the specified public key.
        /// </summary>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <param name="newPublicKey">The new public key value.</param>
        /// <param name="newPrivateKey">The new private key value. Private key is not being sent, but used to sign the part of HTTP request body.</param>
        /// <param name="oldPrivateKey">The old private key. Private key is not being sent, but used to sign the HTTP request body.</param>
        /// <returns><see cref="PublicKey"/></returns>
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

        /// <summary>
        /// Deletes public key.
        /// </summary>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <param name="privateKey">The private key valye. Private key is not being sent, but used to sign the HTTP request body.</param>
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

        /// <summary>
        /// Deletes public key without HTTP request sign by known private key. 
        /// Should be used when private key is lost.
        /// </summary>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <returns><see cref="PublicKeyOperationRequest"/></returns>
        public async Task<PublicKeyOperationRequest> Delete(Guid publicKeyId)
        {
            var body = new
            {
                request_sign_uuid = Guid.NewGuid().ToString()
            };

            var request = Request.Create(RequestMethod.Delete)
                .WithEndpoint($"/v2/public-key/{publicKeyId}")
                .WithBody(body);

            var dto = await this.Send<ResetPublicKeyResponse>(request);
            return new PublicKeyOperationRequest(dto.UserIds, dto.Token);
        }

        /// <summary>
        /// Confirms the delete operation.
        /// </summary>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <param name="comfirmation">The <see cref="PublicKeyOperationComfirmation"/> object.</param>
        public async Task ConfirmDelete(Guid publicKeyId, PublicKeyOperationComfirmation comfirmation)
        {
            Ensure.ArgumentNotNull(comfirmation, nameof(comfirmation));

            var body = new ResetPublicKeyConfirmation
            {
                Token = comfirmation.ActionToken,
                ConfirmationCodes = comfirmation.ConfirmationCodes
            };

            var request = Request.Create(RequestMethod.Post)
                .WithEndpoint($"/v2/public-key/{publicKeyId}/persist")
                .WithBody(body);

            await this.Send(request);
        }

        /// <summary>
        /// Gets the PublicKey by its identifier.
        /// </summary>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <returns><see cref="PublicKey"/></returns>
        public async Task<PublicKey> GetById(Guid publicKeyId)
        {
            var request = Request.Create(RequestMethod.Get)
                .WithEndpoint($"/v2/public-key/{publicKeyId}");

            var dto = await this.Send<PubPublicKey>(request);
            return new PublicKey(dto);
        }

        /// <summary>
        /// Searches PublicKey by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns><see cref="PublicKey"/></returns>
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

        /// <summary>
        /// Resets the specified old public key with new value.
        /// </summary>
        /// <param name="oldPublicKeyId">The old public key identifier.</param>
        /// <param name="newPublicKey">The new public key value.</param>
        /// <param name="newPrivateKey">The new private key value. Private key is not being sent, but used to sign the HTTP request body</param>
        /// <returns><see cref="PublicKeyOperationRequest"/></returns>
        public async Task<PublicKeyOperationRequest> Reset(Guid oldPublicKeyId, byte[] newPublicKey, byte[] newPrivateKey)
        {
            Ensure.ArgumentNotNull(newPublicKey, nameof(newPublicKey));
            Ensure.ArgumentNotNull(newPrivateKey, nameof(newPrivateKey));

            var body = new
            {
                public_key = newPublicKey,
                request_sign_uuid = Guid.NewGuid().ToString()
            };

            var request = Request.Create(RequestMethod.Post)
                .WithEndpoint($"/v2/public-key/{oldPublicKeyId}/actions/reset")
                .WithBody(body)
                .SignRequest(newPrivateKey);

            var dto = await this.Send<ResetPublicKeyResponse>(request);
            return new PublicKeyOperationRequest(dto.UserIds, dto.Token);
        }

        /// <summary>
        /// Confirms the reset.
        /// </summary>
        /// <param name="oldPublicKeyId">The old public key identifier.</param>
        /// <param name="newPrivateKey">The new private key value. Private key is not being sent, but used to sign the HTTP request body</param>
        /// <param name="comfirmation">The <see cref="PublicKeyOperationComfirmation"/> object.</param>
        /// <returns><see cref="PublicKey"/></returns>
        public async Task<PublicKey> ConfirmReset(Guid oldPublicKeyId, byte[] newPrivateKey,  PublicKeyOperationComfirmation comfirmation)
        {
            Ensure.ArgumentNotNull(newPrivateKey, nameof(newPrivateKey));
            Ensure.ArgumentNotNull(comfirmation, nameof(comfirmation));

            var body = new ResetPublicKeyConfirmation
            {
                Token = comfirmation.ActionToken,
                ConfirmationCodes = comfirmation.ConfirmationCodes
            };

            var request = Request.Create(RequestMethod.Post)
                .WithEndpoint($"/v2/public-key/{oldPublicKeyId}/persist")
                .WithBody(body)
                .SignRequest(newPrivateKey);

            var dto = await this.Send<PubPublicKey>(request);
            return new PublicKey(dto);
        }

        /// <summary>
        /// Gets extended public key data by signing request with private key.
        /// </summary>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <param name="privateKey">The private key value. Private key is not being sent, but used to sign the HTTP request body</param>
        /// <returns><see cref="PublicKeyExtended"/></returns>
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