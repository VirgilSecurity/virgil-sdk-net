namespace Virgil.SDK.Keys.Clients
{
    using System;
    using System.Threading.Tasks;
    using Dtos;
    using Helpers;
    using Http;
    using Models;

    public class UserDataClient : ApiClient, IUserDataClient
    {
        public UserDataClient(IConnection connection) : base(connection)
        {
        }

        public async Task<VirgilUserData> Get(Guid userDataId)
        {
            PkiUserData data = await Get<PkiUserData>("user-data/" + userDataId);
            return new VirgilUserData(data);
        }

        public async Task<VirgilUserData> Insert(Guid publicKeyId, VirgilUserData userData)
        {
            var body = new
            {
                public_key_id = publicKeyId,
                @class = userData.Class.ToJsonValue(),
                type = userData.Type.ToJsonValue(),
                value = userData.Value
            };

            PkiUserData result = await Post<PkiUserData>("user-data", body);

            return new VirgilUserData(result);
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