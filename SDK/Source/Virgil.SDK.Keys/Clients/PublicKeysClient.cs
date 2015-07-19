namespace Virgil.SDK.Keys.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    using Virgil.SDK.Keys.Exceptions;
    using Virgil.SDK.Keys.Helpers;
    using Virgil.SDK.Keys.Http;
    using Virgil.SDK.Keys.Model;
    using Virgil.SDK.Keys.TransferObject;

    /// <summary>
    /// Provides common methods to interact with Public Keys resource endpoints.
    /// </summary>
    public class PublicKeysClient : EndpointClient, IPublicKeysClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublicKeysClient" /> class with the default implemetations. 
        /// </summary>
        public PublicKeysClient(IConnection connection) : base(connection)
        {
        }

        /// <summary>
        /// Gets the Virgil Public Key by it's identifier.
        /// </summary>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <exception cref="PublicKeyNotFoundException">Throws when public key is not found by given id.</exception>
        /// <returns>an <see cref="PublicKey"/> instance</returns>
        public async Task<PublicKey> Get(Guid publicKeyId)
        {
            try
            {
                string url = string.Format("public-key/{0}", publicKeyId);
                PubPublicKey dto = await Get<PubPublicKey>(url);
                return new PublicKey(dto);
            }
            catch (KeysServiceException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new UserDataNotFoundException();
                }

                throw;
            }
        }
        
        /// <summary>
        /// Adds new public key to API given user data and account details.
        /// </summary>
        /// <param name="accountId">The account identifier.</param>
        /// <param name="publicKey">The actual public key</param>
        /// <param name="userData">The user data</param>
        /// <returns>instance of created <see cref="PublicKey"/></returns>
        public async Task<PublicKey> Add(Guid accountId, byte[] publicKey, UserData userData)
        {
            return await Add(accountId, publicKey, new[] { userData });
        }

        /// <summary>
        /// Adds new public key to API given several user data.
        /// </summary>
        /// <param name="accountId">The account identifier.</param>
        /// <param name="publicKey">The actual public key</param>
        /// <param name="userData">List of user data</param>
        /// <returns>instance of created <see cref="PublicKey"/></returns>
        public async Task<PublicKey> Add(Guid accountId, byte[] publicKey, IEnumerable<UserData> userData)
        {
            var body = new
            {
                account_id = accountId,
                public_key = publicKey,
                user_data = userData
            };

            PubPublicKey result = await Post<PubPublicKey>("public-key", body);

            return new PublicKey(result);
        }

        /// <summary>
        /// Searches the key by type and value of UserData.
        /// </summary>
        /// <param name="value">The user data value.</param>
        /// <param name="dataType">The user data type.</param>
        /// <returns>found <see cref="PublicKey"/> instances, otherwise empty list</returns>
        public async Task<IEnumerable<PublicKey>> Search(string value, UserDataType dataType)
        {
            Ensure.ArgumentNotNullOrEmptyString(value, "value");
            Ensure.UserDataTypeIsNotUnknown(dataType, "dataType");

            const string url = "user-data/actions/search";
            string userIdType = dataType.ToJsonValue();

            List<PubUserData> dtos = await Post<List<PubUserData>>(url,
                new Dictionary<string, string> { { userIdType, value } });

            if (!dtos.Any())
            {
                return new List<PublicKey>();
            }

            var publicKeys = await Task.WhenAll(dtos.Select(it => this.Get(it.Id.PublicKeyId)));
            return publicKeys;
        }
    }
}