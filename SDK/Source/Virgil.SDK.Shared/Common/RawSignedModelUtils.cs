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

namespace Virgil.SDK.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Virgil.CryptoAPI;
    using Virgil.SDK.Web;

    public class RawSignedModelUtils
    {
        const string CardVersion = "5.0";

        /// <summary>
        /// Loads <see cref="RawSignedModel"/> from the specified <see cref="Card"/>.
        /// </summary>
        /// <param name="cardCrypto">an instance of <see cref="ICardCrypto"/>.</param>
        /// <param name="card">an instance of <see cref="Card"/> to get 
        /// <see cref="RawSignedModel"/> from.</param>
        /// <returns>Loaded instance of <see cref="RawSignedModel"/>.</returns>
        public static RawSignedModel Parse(ICardCrypto cardCrypto, Card card)
        {
            ValidateParams(cardCrypto, card);
            var rawSignedModel = Generate(
                cardCrypto, 
                new CardParams()
                {
                    Identity = card.Identity,
                    PreviousCardId = card.PreviousCardId,
                    PublicKey = card.PublicKey
                },
                card.CreatedAt);
            rawSignedModel.Signatures = new List<RawSignature>();

            foreach (var signature in card.Signatures)
            {
                rawSignedModel.Signatures.Add(
                    new RawSignature()
                    {
                        Signature = signature.Signature,
                        Signer = signature.Signer,
                        Snapshot = signature.Snapshot
                    }
                );
            }
            return rawSignedModel;
        }

        private static void ValidateParams(ICardCrypto cardCrypto, Card rawSignedModel)
        {
            if (rawSignedModel == null)
            {
                throw new ArgumentNullException(nameof(rawSignedModel));
            }

            if (cardCrypto == null)
            {
                throw new ArgumentNullException(nameof(cardCrypto));
            }
        }


        internal static RawSignedModel Generate(
            ICardCrypto cardCrypto,
            CardParams @params,
            DateTime createdAt)
        {
            ValidateGenerateParams(cardCrypto, @params);

            var details = new RawCardContent
            {
                Identity = @params.Identity,
                PublicKey = cardCrypto.ExportPublicKey(@params.PublicKey),
                Version = CardVersion,
                CreatedAt = createdAt,
                PreviousCardId = @params.PreviousCardId
            };

            return new RawSignedModel()
            {
                ContentSnapshot = SnapshotUtils.TakeSnapshot(details)
            };
        }


        private static void ValidateGenerateParams(ICardCrypto cardCrypto, CardParams @params)
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
                throw new ArgumentException($"{nameof(@params.Identity)} property is mandatory");
            }
            if (@params.PublicKey == null)
            {
                throw new ArgumentException($"{nameof(@params.PublicKey)} property is mandatory");
            }

        }

        /// <summary>
        /// Imports RawSignedModel from BASE64 string.
        /// </summary>
        /// <param name="str">The exported <see cref="RawSignedModel"/> as BASE64 string.</param>
        /// <returns>The instance of <see cref="RawSignedModel"/> class.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static RawSignedModel GenerateFromString(string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));

            }
            var strBytes = Bytes.FromString(str, StringEncoding.BASE64);
            var requestJson = Bytes.ToString(strBytes);
            return GenerateFromJson(requestJson);
        }

        /// <summary>
        /// Imports RawSignedModel from specified json.
        /// </summary>
        /// <param name="json">The exported <see cref="RawSignedModel"/> as json.</param>
        /// <returns>The instance of <see cref="RawSignedModel"/> class.</returns>
        /// <exception cref="ArgumentNullException"></exception>
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

    }
}
