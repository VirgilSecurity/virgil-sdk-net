namespace Virgil.SDK.Keys.Clients
{
    using System;
    using System.Threading.Tasks;

    using Helpers;
    using Http;
    using Model;
    using TransferObject;

    /// <summary>
    ///  Provides common methods to interact with User Data resource endpoints.
    /// </summary>
    public class UserDataClient : EndpointClient, IUserDataClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserDataClient"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public UserDataClient(IConnection connection) : base(connection)
        {
        }

        /// <summary>
        /// Deletes the specified user data byt its identifier.
        /// </summary>
        /// <param name="userDataId">The user data identifier.</param>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <param name="privateKey">The private key value. Private key is not being sent, but used to sign the HTTP request body.</param>
        /// <returns><see cref="UserData"/></returns>
        public async Task<UserData> Delete(Guid userDataId, Guid publicKeyId, byte[] privateKey)
        {
            Ensure.ArgumentNotNull(privateKey, nameof(privateKey));

            var body = new
            {
                request_sign_uuid = Guid.NewGuid().ToString()
            };

            var request = Request.Create(RequestMethod.Delete)
               .WithEndpoint($"/v2/user-data/{userDataId}")
               .WithBody(body)
               .SignRequest(privateKey, publicKeyId);

            var dto = await this.Send<PubUserData>(request);
            return new UserData(dto);
        }

        /// <summary>
        /// Adds the specified user data to known public key.
        /// </summary>
        /// <param name="userData">The <see cref="UserData"/> object.</param>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <param name="privateKey">The private key value. Private key is not being sent, but used to sign the HTTP request body.</param>
        /// <returns><see cref="UserData"/></returns>
        public async Task<UserData> Insert(UserData userData, Guid publicKeyId, byte[] privateKey)
        {
            Ensure.ArgumentNotNull(privateKey, nameof(privateKey));
            Ensure.ArgumentNotNull(userData, nameof(userData));
            Ensure.UserDataValid(userData, nameof(userData));

            var body = new
            {
                @class = userData.Class.ToJsonValue(),
                type = userData.Type.ToJsonValue(),
                value = userData.Value,
                request_sign_uuid = Guid.NewGuid().ToString()
            };

            var request = Request.Create(RequestMethod.Post)
               .WithEndpoint("/v2/user-data")
               .WithBody(body)
               .SignRequest(privateKey, publicKeyId);

            var dto = await this.Send<PubUserData>(request);
            return new UserData(dto);
        }

        /// <summary>
        /// Confirms the specified user data.
        /// Unless confirmed user data stored on server would not show up in search requests.
        /// On public key creation, public keys server will send confirmation code to the specified user id.
        /// </summary>
        /// <param name="userDataId">The user data identifier.</param>
        /// <param name="confirmationCode">The confirmation code.</param>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <param name="privateKey">The private key valye. Private key is not being sent, but used to sign the HTTP request body.</param>
        public async Task Confirm(Guid userDataId, string confirmationCode, Guid publicKeyId, byte[] privateKey)
        {
            Ensure.ArgumentNotNull(privateKey, nameof(privateKey));
            Ensure.ArgumentNotNullOrEmptyString(confirmationCode, nameof(confirmationCode));

            var body = new
            {
                confirmation_code = confirmationCode,
                request_sign_uuid = Guid.NewGuid().ToString()
            };

            var request = Request.Create(RequestMethod.Post)
               .WithEndpoint($"/v2/user-data/{userDataId}/persist")
               .WithBody(body)
               .SignRequest(privateKey, publicKeyId);

            await this.Send(request);
        }

        /// <summary>
        /// Ask server to generate new confirmation code in the case when previous was lost or not delivered.
        /// </summary>
        /// <param name="userDataId">The user data identifier.</param>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <param name="privateKey">The private key valye. Private key is not being sent, but used to sign the HTTP request body.</param>
        public async Task ResendConfirmation(Guid userDataId, Guid publicKeyId, byte[] privateKey)
        {
            var body = new
            {
                request_sign_uuid = Guid.NewGuid().ToString()
            };

            var request = Request.Create(RequestMethod.Post)
                .WithEndpoint($"/v2/user-data/{userDataId}/actions/resend-confirmation")
                .WithBody(body)
                .SignRequest(privateKey, publicKeyId);

            await this.Send(request);
        }
    }
}