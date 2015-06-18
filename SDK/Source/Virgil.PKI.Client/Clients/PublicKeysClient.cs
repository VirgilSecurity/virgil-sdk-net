using System.Net;
using Virgil.PKI.Exceptions;
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

        public async Task<VirgilPublicKey> Get(Guid publicKeyId)
        {
            var url = string.Format("public-key/{0}", publicKeyId);
            var dto =  await this.Get<PkiPublicKey>(url);
            return new VirgilPublicKey(dto);
        }

        public async Task<VirgilPublicKey> Get(string userId, UserDataType type)
        {
            const string url = "user-data/actions/search";
            var userIdType = type.ToJsonValue();
            var dtos = await this.Post<PkiUserData[]>(url, new Dictionary<string, string> { { userIdType, userId } });
            var foundUserData = dtos.FirstOrDefault();
            if (foundUserData == null)
            {
                throw new PkiWebException(20200, "UserData object not found for id specified", HttpStatusCode.BadRequest, "");
            }

            return await Get(foundUserData.Id.PublicKeyId);
        }

        //public IEnumerable<VirgilPublicKey> GetAll(Guid accountId)
        //{
            
        //}

        public async Task<VirgilPublicKey> Insert(Guid accountId, byte[] publicKey, IEnumerable<VirgilUserData> userData)
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