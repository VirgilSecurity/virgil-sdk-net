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

        private const int DaysLifeTime = 2; 

        public JsonWebTokenBody(string accId, string[] appIds, string version)
        {
            this.AccountId = accId;
            this.AppIds = appIds;
            this.Version = version;
            this.SetUpLifeTime();
        }

        private void SetUpLifeTime()
        {
            var timeNow = DateTime.UtcNow;
            //to truncate milliseconds and microseconds
            timeNow = timeNow.AddTicks(-timeNow.Ticks % TimeSpan.TicksPerSecond);
            this.CreatedAt = timeNow;
            this.ExpireAt = this.CreatedAt.AddDays(JsonWebTokenBody.DaysLifeTime);
        }

        public bool IsExpired()
        {
            return DateTime.UtcNow >= this.ExpireAt;
        }
        public void Refresh()
        {
            SetUpLifeTime();
        }

    }

}
