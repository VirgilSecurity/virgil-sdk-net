namespace Virgil.PKI.Http
{
    using System;
    using System.Collections.Generic;

    public interface IRequest
    {
        object Body { get; set; }
        Dictionary<string, string> Headers { get; }
        HttpRequestMethod Method { get; }
        Dictionary<string, string> Parameters { get; }
        Uri BaseAddress { get; }
        Uri Endpoint { get; }
        string ContentType { get; }
    }
}