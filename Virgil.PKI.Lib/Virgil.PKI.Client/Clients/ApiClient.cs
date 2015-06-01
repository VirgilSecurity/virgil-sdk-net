namespace Virgil.PKI.Clients
{
    using Virgil.PKI.Http;

    public abstract class ApiClient
    {
        private IConnection connection;

        protected ApiClient(IConnection connection)
        {
            this.connection = connection;
        }
    }
}