namespace Virgil.SDK.Keys.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Dtos;
    using Exceptions;
    using Helpers;
    using Http;
    using Models;

    public class PublicKeysClient : ApiClient, IPublicKeysClient
    {
        public PublicKeysClient(IConnection connection) : base(connection)
        {
        }

        public async Task<VirgilPublicKey> Get(Guid publicKeyId)
        {
            string url = string.Format("public-key/{0}", publicKeyId);
            PkiPublicKey dto = await Get<PkiPublicKey>(url);
            return new VirgilPublicKey(dto);
        }

        public async Task<VirgilPublicKey> Get(string userId, UserDataType type)
        {
            const string url = "user-data/actions/search";
            string userIdType = type.ToJsonValue();
            PkiUserData[] dtos = await Post<PkiUserData[]>(url, new Dictionary<string, string> {{userIdType, userId}});
            PkiUserData foundUserData = dtos.FirstOrDefault();
            if (foundUserData == null)
            {
                throw new KeysServiceException(20200, "UserData object not found for id specified", HttpStatusCode.BadRequest,
                    "");
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

            PkiPublicKey result = await Post<PkiPublicKey>("public-key", body);

            return new VirgilPublicKey(result);
        }
    }
}