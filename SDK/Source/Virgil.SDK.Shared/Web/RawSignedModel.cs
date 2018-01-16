#region Copyright (C) Virgil Security Inc.
// Copyright (C) 2015-2018 Virgil Security Inc.
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

using System;
using NSubstitute.Core;
using Virgil.SDK.Common;

namespace Virgil.SDK.Web
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Virgil.CryptoAPI;

    [DataContract]
    public class RawSignedModel
    {
        [DataMember(Name = "content_snapshot")]
        public byte[] ContentSnapshot { get; internal set; }

        [DataMember(Name = "signatures")]
        public List<RawSignature> Signatures { get; internal set; }

        public RawSignedModel()
        {
        }

        public RawSignedModel(string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));

            }
            RawSignedModel rawSignedModel;
            try
            {
                var requestString = Bytes.FromString(str, StringEncoding.BASE64);
                var requestJson = Bytes.ToString(requestString);
                rawSignedModel = Configuration.Serializer.Deserialize<RawSignedModel>(requestJson);
            }
            catch (Exception)
            {
                throw new ArgumentException($"{nameof(str)} wrong format.");
            }
            ContentSnapshot = rawSignedModel.ContentSnapshot;
            Signatures = rawSignedModel.Signatures;
        }

        public RawSignedModel(Dictionary<string, string> json)
        {
            if (json == null || json.Keys.Count == 0)
            {
                throw new ArgumentNullException(nameof(json));

            }
            RawSignedModel rawSignedModel;
            try
            {
                var str = Configuration.Serializer.Serialize(json);
                rawSignedModel = Configuration.Serializer.Deserialize<RawSignedModel>(str);
            }
            catch (Exception)
            {
                throw new ArgumentException($"{nameof(json)} wrong format.");
            }
            ContentSnapshot = rawSignedModel.ContentSnapshot;
            Signatures = rawSignedModel.Signatures;
        }

        public string ExportAsString()
        {
            var serializer = Configuration.Serializer;
            var rawCardJson = serializer.Serialize(this);
            var rawCardBytes = Bytes.FromString(rawCardJson);
            var rawCardString = Bytes.ToString(rawCardBytes, StringEncoding.BASE64);
            return rawCardString;
        }

        public Dictionary<string, string> ExportAsJson()
        {
            var rawCardJson = Configuration.Serializer.Serialize(this);
            return Configuration.Serializer.Deserialize<Dictionary<string, string>>(rawCardJson); 
        }

        public static RawSignedModel Generate(ICardCrypto cardCrypto, CSRParams @params)
        {
            ValidateParams(cardCrypto, @params);

            var timeNow = DateTime.UtcNow;
            //to truncate milliseconds and microseconds
            timeNow = timeNow.AddTicks(-timeNow.Ticks % TimeSpan.TicksPerSecond);
            var details = new RawCardContent
            {
                Identity = @params.Identity,
                PublicKey = cardCrypto.ExportPublicKey(@params.PublicKey),
                Version = "5.0",
                CreatedAt = timeNow,
                PreviousCardId = @params.PreviousCardId
            };

            var rawSignedModel = new RawSignedModel() { ContentSnapshot = CardUtils.TakeSnapshot(details) };
            return rawSignedModel;
        }

        private static void ValidateParams(ICardCrypto cardCrypto, CSRParams @params)
        {
            if (cardCrypto == null)
            {
                throw new ArgumentNullException(nameof(cardCrypto));
            }

            if (@params == null)
            {
                throw new ArgumentNullException(nameof(@params));
            }

            if (@params.Identity == null)
            {
                throw new ArgumentException($"{@params.Identity} property is mandatory");
            }
            if (@params.PublicKey == null)
            {
                throw new ArgumentException($"{@params.PublicKey} property is mandatory");
            }

            if (@params.PrivateKey == null)
            {
                throw new ArgumentException($"{@params.PrivateKey} property is mandatory");
            }
        }
    }
}
