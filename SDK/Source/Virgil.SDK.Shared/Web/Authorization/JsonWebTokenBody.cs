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
        public string AccountId { get; private set; }

        [DataMember(Name = "appids")]
        public string[] AppIds { get; private set; }

        [DataMember(Name = "iat")]
        public DateTime CreatedAt { get; private set; }

        [DataMember(Name = "exp")]
        public DateTime ExpireAt { get; private set; }

        [DataMember(Name = "ver")]
        public string Version { get; private set; }

        public JsonWebTokenBody(string accId, string[] appIds, string version)
        {
            this.AccountId = accId;
            this.AppIds = appIds;
            this.CreatedAt = DateTime.UtcNow;
            var timeNow = DateTime.UtcNow;
            //to truncate milliseconds and microseconds
            timeNow = timeNow.AddTicks(-timeNow.Ticks % TimeSpan.TicksPerSecond);
            this.CreatedAt = timeNow;
            this.ExpireAt = this.CreatedAt.AddDays(2);

            this.Version = version;
        }


    }

}
