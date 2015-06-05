namespace Virgil.PKI.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Virgil.PKI.Dtos;
    using Virgil.PKI.Http;
    using Virgil.PKI.Models;

    public class PublicKeysClient : ApiClient
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

        public async Task<IEnumerable<VirgilPublicKey>> Search(string userId, UserDataType type)
        {
            var url = string.Format("user-data/actions/search");
            var userIdType = UserIdType(type);

            var dtos = await this.Post<PkiUserData[]>(url, new Dictionary<string, string> { { userIdType, userId } });
            var tasks = dtos.Select(it => it.Id.PublicKeyId).Select(id => this.Get(id)).ToArray();

            await Task.WhenAll(tasks);

            return tasks.Select(t => t.Result).ToList();
        }

        private static string UserIdType(UserDataType type)
        {
            string userIdType;
            switch (type)
            {
                case UserDataType.Email:
                    userIdType = "email";
                    break;
                case UserDataType.Domain:
                    userIdType = "domain";
                    break;
                case UserDataType.Application:
                    userIdType = "application";
                    break;

                default:
                    throw new ArgumentOutOfRangeException("type");
            }
            return userIdType;
        }

        //public IEnumerable<VirgilPublicKey> GetAll(Guid accountId)
        //{
            
        //}

        public VirgilPublicKey Insert(Guid accountId, VirgilUserData userData)
        {
            
        }

        public void Delete(Guid certificateId)
        {
            throw new NotImplementedException();
        }


    }
}