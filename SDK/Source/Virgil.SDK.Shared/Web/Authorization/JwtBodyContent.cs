using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Bogus.DataSets;

namespace Virgil.SDK.Web.Authorization
{
    [DataContract]
    public class JwtBodyContent
    {

        public string AppId { get; set; }
        public string Identity{ get; set; }

        [DataMember(Name = "iss")]
        public string Issuer { get; set; } 

        [DataMember(Name = "sub")]
        public string Subject { get; set; } 

        [DataMember(Name = "ada")]
        public DateTime IssuedAt { get; set; }

        [DataMember(Name = "iat")]
        public DateTime ExpireAt { get; set; }

        [DataMember(Name = "ada")]
        public Dictionary<string, string> AdditionalData { get; set; }

        public JwtBodyContent(string appId, 
            string identity, 
            TimeSpan lifeTime, 
            Dictionary<string, string> data)
        {
            this.AppId = appId;
            this.Identity = identity;
            this.IssuedAt = DateTime.UtcNow;
            this.ExpireAt = this.IssuedAt.AddMilliseconds(lifeTime.TotalMilliseconds);
            this.Identity = identity;
            this.AdditionalData = data;
            this.Issuer = $"virgil-{AppId}";
            this.Subject = $"identity-{Identity}";
        }

        public JwtBodyContent()
        {
        }
    }

}
