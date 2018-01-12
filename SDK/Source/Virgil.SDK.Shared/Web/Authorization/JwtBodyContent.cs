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
        public const string IdentityPrefix = "identity-";
        public const string SubjectPrefix = "virgil-";

        public string AppId { get; set; }
        public string Identity{ get; set; }

        [DataMember(Name = "iss")]
        public string Issuer { get; set; } 

        [DataMember(Name = "sub")]
        public string Subject { get; set; } 

        [DataMember(Name = "iat")]
        public DateTime IssuedAt { get; set; }

        [DataMember(Name = "exp")]
        public DateTime ExpiresAt { get; set; }

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
            this.ExpiresAt = this.IssuedAt.AddMilliseconds(lifeTime.TotalMilliseconds);
            this.Identity = identity;
            this.AdditionalData = data;
            this.Issuer = $"{SubjectPrefix}{AppId}";
            this.Subject = $"{IdentityPrefix}{Identity}";
        }

        public JwtBodyContent()
        {
        }
    }

}
