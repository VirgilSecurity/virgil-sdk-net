using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Virgil.SDK.Shared.Web.Authorization
{
    [DataContract]
    public class JsonWebTokenHeader
    {
        [DataMember(Name = "alg")]
        public string Algorithm { get; private set; }

        [DataMember(Name = "typ")]
        public string Type { get; private set; }

        public JsonWebTokenHeader(string algorithm, string type)
        {
            Algorithm = algorithm;
            Type = type;
        }
    }
}
