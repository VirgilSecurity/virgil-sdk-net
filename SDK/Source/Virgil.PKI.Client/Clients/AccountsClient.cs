using System.Threading.Tasks;
using Virgil.PKI.Dtos;
using Virgil.PKI.Helpers;

namespace Virgil.PKI.Clients
{
    using Virgil.PKI.Http;
    using Virgil.PKI.Models;

    public class AccountsClient : ApiClient, IAccountsClient
    {
        public AccountsClient(IConnection connection) : base(connection)
        {
        }

        public async Task<VirgilAccount> Register(VirgilUserData userData, byte[] publicKey)
        {
            var body = new
            {
                public_key = publicKey,
                user_data = new[]
                {
                    new
                    {
                        @class = userData.Class.ToJsonValue(),
                        type = userData.Type.ToJsonValue(),
                        value = userData.Value
                    }
                }
            };

            var result = await this.Post<PkiPublicKey>("public-key", body);
            return new VirgilAccount(result);
        }
    }
}