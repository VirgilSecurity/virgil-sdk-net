namespace Virgil.PKI.Clients
{
    using System;
    using System.Collections.Generic;

    using Virgil.PKI.Http;

    public abstract class ApiClient
    {
        private IConnection connection;

        protected ApiClient(IConnection connection)
        {
            this.connection = connection;
        }

        public TResult Get<TResult>(string endpoint, IDictionary<string, string> parameters)
        {
            throw  new NotImplementedException();
        }

        public TResult Post<TResult>(string endpoint, object body)
        {
            throw new NotImplementedException();
        }

        public TResult Put<TResult>(string endpoint, object body)
        {
            throw new NotImplementedException();
        }

        public void Delete(string endpoint, object body)
        {
            throw new NotImplementedException();
        }
    }
}