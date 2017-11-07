#region Copyright (C) Virgil Security Inc.
// Copyright (C) 2015-2017 Virgil Security Inc.
// 
// Lead Maintainer: Virgil Security Inc. <support@virgilsecurity.com>
// 
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions 
// are met:
// 
//   (1) Redistributions of source code must retain the above copyright
//   notice, this list of conditions and the following disclaimer.
//   
//   (2) Redistributions in binary form must reproduce the above copyright
//   notice, this list of conditions and the following disclaimer in
//   the documentation and/or other materials provided with the
//   distribution.
//   
//   (3) Neither the name of the copyright holder nor the names of its
//   contributors may be used to endorse or promote products derived 
//   from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE AUTHOR ''AS IS'' AND ANY EXPRESS OR
// IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT,
// INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
// HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
// STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING
// IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
// POSSIBILITY OF SUCH DAMAGE.
#endregion

namespace Virgil.SDK.Common
{
    using System;
    using PetaJson;
    using System.Text;

    public class PetaJsonSerializer : IJsonSerializer
    {
        public PetaJsonSerializer()
        {
            Json.RegisterFormatter<DateTime>(this.DateTimeFormatter);
            Json.RegisterParser(this.DateTimeParser);
        }

        public string Serialize(object model)
        {
            return Json.Format(model);  
        }
        
        public TModel Deserialize<TModel>(string json)
        {
            return Json.Parse<TModel>(json);
        }

        private DateTime DateTimeParser(object o)
        {
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            // Add the timestamp (number of seconds since the Epoch) to be converted
            var unixTimestampBytes = Bytes.FromString(o.ToString(), StringEncoding.BASE64);
            var unixTimeStamp = Int32.Parse(Bytes.ToString(unixTimestampBytes));
            return dateTime.AddSeconds(unixTimeStamp);
        }

        private void DateTimeFormatter(IJsonWriter jsonWriter, DateTime dateTime)
        {
            var timeSpan = dateTime - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var unixTimestampBytes = Bytes.FromString(((Int32)timeSpan.TotalSeconds).ToString());
            var unixTimestamp = Bytes.ToString(unixTimestampBytes, StringEncoding.BASE64);
            jsonWriter.WriteStringLiteral(unixTimestamp);
        }
    }
}