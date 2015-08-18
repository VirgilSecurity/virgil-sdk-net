namespace Virgil.SDK.Keys.Clients
{
    using System;
    using System.Threading.Tasks;

    using Virgil.SDK.Keys.Helpers;
    using Virgil.SDK.Keys.Http;
    using Virgil.SDK.Keys.Model;
    using Virgil.SDK.Keys.TransferObject;

    public class UserDataClient : EndpointClient, IUserDataClient
    {
        public UserDataClient(IConnection connection) : base(connection)
        {
        }

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

        public async Task ResendConfirmation(Guid userDataId)
        {
            var request = Request.Create(RequestMethod.Post)
                .WithEndpoint($"/v2/user-data/{userDataId}/actions/resend-confirmation");

            await this.Send(request);
        }
    }
}