using System;
using Virgil.PKI.Clients;
using Virgil.PKI.Http;

namespace Virgil.PKI
{
    public class PkiClient : IPkiClient
    {
        public PkiClient(string appToken)
        {
            var connection = new Connection(appToken, new Uri(@"https://pki.virgilsecurity.com/v1/"));
            this.Accounts = new AccountsClient(connection);
            this.PublicKeys = new PublicKeysClient(connection);
            this.UserData = new UserDataClient(connection);
        }

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