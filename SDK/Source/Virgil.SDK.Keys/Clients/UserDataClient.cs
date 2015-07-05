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

        public async Task<UserData> Get(Guid userDataId)
        {
            PubUserData data = await Get<PubUserData>("user-data/" + userDataId);
            return new UserData(data);
        }

        public async Task<UserData> Insert(Guid publicKeyId, UserData userData)
        {
            var body = new
            {
                public_key_id = publicKeyId,
                @class = userData.Class.ToJsonValue(),
                type = userData.Type.ToJsonValue(),
                value = userData.Value
            };

            PubUserData result = await Post<PubUserData>("user-data", body);

            return new UserData(result);
        }

        public async Task Confirm(Guid userDataId, string confirmationCode)
        {
            var body = new
            {
                code = confirmationCode
            };

            await Post<string>("user-data/" + userDataId + "/actions/confirm", body);
        }

        public async Task ResendConfirmation(Guid userDataId)
        {
            await Post<string>("user-data/" + userDataId + "/actions/resend-confirmation", null);
        }
    }
}