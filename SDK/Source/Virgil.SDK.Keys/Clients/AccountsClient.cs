namespace Virgil.SDK.Keys.Clients
{
    using System.Threading.Tasks;
    using Dtos;
    using Helpers;
    using Http;
    using Models;

    public class AccountsClient : ApiClient, IAccountsClient
    {
        public AccountsClient(IConnection connection) : base(connection)
        {
        }

        /// <summary>
        /// Registers an account specified by the <see cref="VirgilUserData" /> user data and public key.
        /// </summary>
        /// <param name="userData">The user data information</param>
        /// <param name="publicKey">Generated Public Key</param>
        /// <returns>
        /// An <see cref="VirgilAccount" />
        /// </returns>
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

            PkiPublicKey result = await Post<PkiPublicKey>("public-key", body);
            return new VirgilAccount(result);
        }
    }
}