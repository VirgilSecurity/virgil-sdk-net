using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Virgil.SDK.Web.Authorization
{
    [DataContract]
    public class JwtHeaderContent
    {
        [DataMember(Name = "alg")]
        public string Algorithm { get; set; }

        [DataMember(Name = "typ")]
        public string Type { get; set; }

        [DataMember(Name = "cty")]
        public string ContentType { get; set; }

        [DataMember(Name = "kid")]
        public string AccessKeyId { get; set; }

        public JwtHeaderContent(string algorithm, string accessKeyId)
        {
            this.Algorithm = algorithm;
            this.AccessKeyId = accessKeyId;
            this.Type = "JWT";
            this.ContentType = "virgil-jwt;v=1";
        }

        public JwtHeaderContent()
        {
            
        }
    }
}
