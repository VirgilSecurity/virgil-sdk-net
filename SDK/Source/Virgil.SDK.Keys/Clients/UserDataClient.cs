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
                value = userData.Value,
                guid = Guid.NewGuid().ToString()
            };

            PubUserData result = await Post<PubUserData>("user-data", body);

            return new UserData(result);
        }

        public async Task Confirm(Guid userDataId, string confirmationCode)
        {
            var body = new
            {
                code = confirmationCode,
                guid = Guid.NewGuid().ToString()
            };

            await Post<object>(string.Format("user-data/{0}/actions/confirm", userDataId.ToString()), body);
        }

        public async Task ResendConfirmation(Guid userDataId)
        {
            await Post<object>(string.Format("user-data/{0}/actions/resend-confirmation", userDataId.ToString()), null);
        }
    }
}