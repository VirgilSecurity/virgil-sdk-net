using Virgil.PKI.Helpers;

namespace Virgil.PKI.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Virgil.PKI.Dtos;
    using Virgil.PKI.Http;
    using Virgil.PKI.Models;

   

    public class PublicKeysClient : ApiClient, IPublicKeysClient
    {
        public PublicKeysClient(IConnection connection) : base(connection)
        {
            
        }

        public async Task<VirgilPublicKey> GetKey(Guid publicKeyId)
        {
            var url = string.Format("public-key/{0}", publicKeyId);
            var dto =  await this.Get<PkiPublicKey>(url);
            return new VirgilPublicKey(dto);
        }

        public async Task<IEnumerable<VirgilPublicKey>> SearchKey(string userId, UserDataType type)
        {
            var url = string.Format("user-data/actions/search");
            var userIdType = type.ToJsonValue();

            var dtos = await this.Post<PkiUserData[]>(url, new Dictionary<string, string> { { userIdType, userId } });
            var tasks = dtos.Select(it => it.Id.PublicKeyId).Select(id => this.GetKey(id)).ToArray();

            await Task.WhenAll(tasks);

            return tasks.Select(t => t.Result).ToList();
        }

        //public IEnumerable<VirgilPublicKey> GetAll(Guid accountId)
        //{
            
        //}

        public async Task<VirgilPublicKey> AddKey(Guid accountId, byte[] publicKey, IEnumerable<VirgilUserData> userData)
        {
            var body = new
            {
                account_id = accountId,
                public_key = publicKey,
                user_data = userData
            };

            var result = await this.Post<PkiPublicKey>("public-key", body);

            return new VirgilPublicKey(result);
        }
    }
}