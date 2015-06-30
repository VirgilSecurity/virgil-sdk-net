using Virgil.SDK.Keys.Model;
using Virgil.SDK.Keys.TransferObject;

namespace Virgil.SDK.Keys.Clients
{
    using System.Threading.Tasks;
    using Virgil.SDK.Keys.Helpers;
    using Virgil.SDK.Keys.Http;
    using Virgil.SDK.Keys.Models;
    using Virgil.SDK.Keys.Exceptions;

    /// <summary>
    /// Provides common methods to interact with Account resource endpoints.
    /// </summary>
    public class AccountsClient : EndpointClient, IAccountsClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountsClient" /> class with the default implemetations. 
        /// </summary>
        public AccountsClient(IConnection connection) : base(connection)
        {
        }

        /// <summary>
        /// Registers an account specified by the <see cref="UserData" /> user data and public key.
        /// </summary>
        /// <param name="dataType">The user data type information</param>
        /// <param name="userId">The user data ID value</param>
        /// <param name="publicKey">Generated Public Key</param>
        /// <exception cref="UserDataAlreadyExistsException">Appears when UserData already exists with given value</exception>
        /// <returns>An <see cref="Account" /></returns>
        public async Task<Account> Register(UserDataType dataType, string userId, byte[] publicKey)
        {
            Ensure.UserDataTypeIsUserId(dataType, "dataType");
            Ensure.ArgumentNotNullOrEmptyString(userId, "userId");
            Ensure.ArgumentNotNull(publicKey, "publicKey");

            var body = new
            {
                public_key = publicKey,
                user_data = new[]
                {
                    new
                    {
                        @class = UserDataClass.UserId.ToJsonValue(),
                        type = dataType.ToJsonValue(),
                        value = userId
                    }
                }
            };
            
            try
            {
                PkiPublicKey result = await Post<PkiPublicKey>("public-key", body);
                return new Account(result);
            }
            catch (KeysServiceException ex)
            {
                switch (ex.ErrorCode)
                {
                    case 20210: throw new UserDataAlreadyExistsException();
                    default:
                        throw;
                }
            }
        }
    }
}