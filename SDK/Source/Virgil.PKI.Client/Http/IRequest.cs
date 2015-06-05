namespace Virgil.PKI.Http
{
    using System;
    using System.Collections.Generic;

    public interface IRequest
    {
        string Endpoint { get; }

        RequestMethod Method { get; }

        IDictionary<string, string> Headers { get; }

        string Body { get; }
        
    }
}