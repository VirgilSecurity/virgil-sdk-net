using System;
using Virgil.PKI.Clients;
using Virgil.PKI.Http;

namespace Virgil.PKI
{
    public class PkiClient : IPkiClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PkiClient"/> class with the default implemetations
        /// </summary>
        /// <param name="appToken">The application token to be used for requests authorisation.</param>
        public PkiClient(string appToken)
        {
            var connection = new Connection(appToken, new Uri(@"https://pki.virgilsecurity.com/v1/"));
            this.Accounts = new AccountsClient(connection);
            this.PublicKeys = new PublicKeysClient(connection);
            this.UserData = new UserDataClient(connection);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PkiClient"/> class.
        /// </summary>
        /// <param name="accounts">The accounts client.</param>
        /// <param name="publicKeys">The public keys client.</param>
        /// <param name="userData">The user data client.</param>
        public PkiClient(
            IAccountsClient accounts,
            IPublicKeysClient publicKeys, 
            IUserDataClient userData)
        {
            Accounts = accounts;
            PublicKeys = publicKeys;
            UserData = userData;
        }

        public IAccountsClient Accounts { get; private set; }

        public IPublicKeysClient PublicKeys { get; private set; }

        public IUserDataClient UserData { get; private set; }
        
    }
}