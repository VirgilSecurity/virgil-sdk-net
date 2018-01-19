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
        public IList<RawSignature> Signatures { get; internal set; }

        [DataMember(Name = "meta")]
        public Dictionary<string, string> Meta { get; internal set; }

         const string CardVersion = "5.0";


        /// <summary>
        /// Imports CSR from string.
        /// </summary>
        /// <param name="cardCrypto"></param>
        /// <param name="csr">The exported request string.</param>
        /// <returns>The instance of <see cref="CSR"/> class.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static RawSignedModel GenerateFromString(string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));

            }
            var requestString = Bytes.FromString(str, StringEncoding.BASE64);
            var requestJson = Bytes.ToString(requestString);
            return GenerateFromJson(requestJson);
        }

        public static RawSignedModel GenerateFromJson(string json)
        {
            if (json == null)
            {
                throw new ArgumentNullException(nameof(json));

            }
            RawSignedModel rawSignedModel;
            try
            {
                rawSignedModel = Configuration.Serializer.Deserialize<RawSignedModel>(json);
            }
            catch (Exception)
            {
                throw new ArgumentException($"{nameof(json)} wrong format.");
            }
            return rawSignedModel;
        }


        /// <summary>
        /// Exports a CSR into string. Use this method to transmit the card 
        /// signing request through the network.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public string ExportAsString()
        {
            var rawCardBytes = Bytes.FromString(ExportAsJson());
            var rawCardString = Bytes.ToString(rawCardBytes, StringEncoding.BASE64);
            return rawCardString;
        }

        public string ExportAsJson()
        {
            return Configuration.Serializer.Serialize(this);
        }

        internal static RawSignedModel Generate(ICardCrypto cardCrypto, CardParams @params)
        {
            ValidateParams(cardCrypto, @params);

            var timeNow = DateTime.UtcNow;
            //to truncate milliseconds and microseconds
            timeNow = timeNow.AddTicks(-timeNow.Ticks % TimeSpan.TicksPerSecond);
            return RawModel(cardCrypto, @params, timeNow, CardVersion);
        }

        internal static RawSignedModel RawModel(
            ICardCrypto cardCrypto, 
            CardParams @params, 
            DateTime createdAt, 
            string version)
        {
            var details = new RawCardContent
            {
                Identity = @params.Identity,
                PublicKey = cardCrypto.ExportPublicKey(@params.PublicKey),
                Version = version,
                CreatedAt = createdAt,
                PreviousCardId = @params.PreviousCardId
            };

            return new RawSignedModel()
            {
                ContentSnapshot = SnapshotUtils.TakeSnapshot(details),
                Meta = @params.Meta
            };
        }

        private static void ValidateParams(ICardCrypto cardCrypto, CardParams @params)
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
