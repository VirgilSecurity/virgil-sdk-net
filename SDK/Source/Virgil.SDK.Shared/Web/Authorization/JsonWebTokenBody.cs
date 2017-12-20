using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Virgil.SDK.Shared.Web.Authorization
{
    [DataContract]
    public class JsonWebTokenBody
    {
        [DataMember(Name = "accid")]
        public string AccountId { get; set; }

        [DataMember(Name = "appids")]
        public string[] AppIds { get; set; }

        [DataMember(Name = "iat")]
        public DateTime CreatedAt { get; set; }

        [DataMember(Name = "exp")]
        public DateTime ExpireAt { get; set; }

        [DataMember(Name = "ver")]
        public string Version { get; set; }

        [DataMember(Name = "data")]
        public Dictionary<string, string> Data { get; set; }

        public JsonWebTokenBody(string accId, string[] appIds, string version, TimeSpan lifeTime, Dictionary<string, string> data)
        {
            this.AccountId = accId;
            this.AppIds = appIds;
            this.Version = version;
            this.CreatedAt = DateTime.UtcNow;
            this.ExpireAt = this.CreatedAt.AddMilliseconds(lifeTime.TotalMilliseconds);
            this.Data = data;
        }

        public JsonWebTokenBody()
        {
        }

        public bool IsExpired()
        {
            return DateTime.UtcNow >= this.ExpireAt;
        }
    }

}
