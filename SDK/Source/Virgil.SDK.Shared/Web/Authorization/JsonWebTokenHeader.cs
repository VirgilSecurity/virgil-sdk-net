using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Virgil.SDK.Web.Authorization
{
    [DataContract]
    public class JsonWebTokenHeader
    {
        [DataMember(Name = "alg")]
        public string Algorithm { get; set; }

        [DataMember(Name = "typ")]
        public string Type { get; set; }

        public JsonWebTokenHeader(string algorithm, string type)
        {
            Algorithm = algorithm;
            Type = type;
        }

        public JsonWebTokenHeader()
        {
            
        }
    }
}
