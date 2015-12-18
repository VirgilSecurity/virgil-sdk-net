namespace Virgil.SDK.Keys.Http
{
    using System;
    using System.Net.Http;
    using Exceptions;

    public class IdentityConnection : ConnectionBase, IConnection
    {
        public IdentityConnection(string appToken, Uri baseAddress) : base(appToken, baseAddress)
        {
        }

        protected override void ExceptionHandler(HttpResponseMessage message)
        {
            if (!message.IsSuccessStatusCode)
            {
                throw new IdentityServiceException("");
            }
        }
    }
}