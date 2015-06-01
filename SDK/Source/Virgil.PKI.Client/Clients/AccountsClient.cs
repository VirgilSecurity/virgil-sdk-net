namespace Virgil.PKI.Clients
{
    using Virgil.PKI.Http;
    using Virgil.PKI.Models;

    public class AccountsClient : ApiClient, IAccountsClient
    {
        public AccountsClient(IConnection connection) : base(connection)
        {
        }

        public VirgilAccount Register(VirgilUserData userData, byte[] publicKey)
        {
            // Here is an example of how it works.
            // this.Post<RegisterAccountResult>("public-keys", new {  });

            throw new System.NotImplementedException();
        }
    }
}