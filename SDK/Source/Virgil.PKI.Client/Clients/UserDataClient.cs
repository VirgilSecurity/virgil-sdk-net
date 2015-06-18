using System;
using System.Threading.Tasks;
using Virgil.PKI.Dtos;
using Virgil.PKI.Helpers;
using Virgil.PKI.Http;
using Virgil.PKI.Models;

namespace Virgil.PKI.Clients
{
    public class UserDataClient : ApiClient, IUserDataClient
    {
        public UserDataClient(IConnection connection) : base(connection)
        {
        }

        public async Task<VirgilUserData> Get(Guid userDataId)
        {
            var data = await this.Get<PkiUserData>("user-data/" + userDataId);
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

            var result = await this.Post<PkiUserData>("user-data", body);

            return new VirgilUserData(result);
        }

        public async Task Confirm(Guid userDataId, string confirmationCode)
        {
            var body = new
            {
                code = confirmationCode
            };

            await this.Post<string>("user-data/" + userDataId + "/actions/confirm", body);
        }

        public async Task ResendConfirmation(Guid userDataId)
        {
            await this.Post<string>("user-data/" + userDataId + "/actions/resend-confirmation", null);
        }
    }
}