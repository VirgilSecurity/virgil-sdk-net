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

namespace Virgil.SDK
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Virgil.CryptoApi;
    using Virgil.SDK.Common;
    using Virgil.SDK.Web;
    
    public class CSR
    {
        private RawCardInfo info;
        private byte[] snapshot;
        private List<RawCardSignature> signatures;

        private CSR()
        {
        }

        public string CardId { get; private set; }
        public string Identity          => this.info.Identity;
        public byte[] PublicKeyBytes    => this.info.PublicKeyBytes;
        public string Version           => this.info.Version;  
        public DateTime CreatedAt       => this.info.CreatedAt;
        public RawCard RawCard          => new RawCard { ContentSnapshot = this.snapshot, Signatures = this.signatures };

        /// <summary>
        /// Signs the CSR using specified signer parameters.
        /// </summary>
        public void Sign(ICardManagerCrypto cardManagerCrypto, SignParams @params)
        {

            if ((@params.SignerType == SignerType.Self) 
                && this.signatures.Exists(s => s.SignerType == SignerType.Self.ToLowerString()))
            {
                throw new VirgilException("The CSR is already has self signature.");
            }

            if ((@params.SignerType == SignerType.App) &&
                this.signatures.Exists(s => s.SignerType == SignerType.App.ToLowerString()))
            {
                throw new VirgilException("The CSR is already has application signature");
            }

            byte[] extraBytes = null;
            byte[] fingerprintPayload;

            if (@params.SignerType != SignerType.Self && @params.ExtraFields != null)
            {
                extraBytes = CardUtils.TakeSnapshot(@params.ExtraFields);
                fingerprintPayload = Bytes.Combine(this.snapshot, extraBytes);
            }
            else
            {
                fingerprintPayload = this.snapshot;
            }

            var fingerprint = cardManagerCrypto.SHA256(fingerprintPayload);
            var signatureBytes = cardManagerCrypto.GenerateSignature(fingerprint, @params.SignerPrivateKey);
            var signature = new RawCardSignature
            {
                SignerCardId = @params.SignerCardId,
                SignerType   = @params.SignerType.ToLowerString(),
                Signature    = signatureBytes,
                ExtraData    = extraBytes
            };

            this.signatures.Add(signature);
        }

        /// <summary>
        /// Exports a CSR into string. Use this method to transmit the card 
        /// signing request through the network.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public string Export()
        {
            var serializer = Configuration.Serializer;
            var rawCard = new RawCard
            {
                ContentSnapshot = this.snapshot,
                Signatures = this.signatures 
            };

            var rawCardJson = serializer.Serialize(rawCard);
            var rawCardBytes = Bytes.FromString(rawCardJson);
            var rawCardString = Bytes.ToString(rawCardBytes, StringEncoding.BASE64);

            return rawCardString;
        }

        /// <summary>
        /// Imports CSR from string.
        /// </summary>
        /// <param name="cardManagerCrypto"></param>
        /// <param name="csr">The exported request string.</param>
        /// <returns>The instance of <see cref="CSR"/> class.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static CSR Import(ICardManagerCrypto cardManagerCrypto, string csr)
        {
            if (csr == null)
            {
                throw new ArgumentNullException(nameof(csr));
                
            }

            RawCard rawCard;
            try
            {
                var requestString = Bytes.FromString(csr, StringEncoding.BASE64);
                var requestJson = Bytes.ToString(requestString);
                rawCard = Configuration.Serializer.Deserialize<RawCard>(requestJson);
            }
            catch (Exception)
            {
                throw new ArgumentException($"{nameof(csr)} wrong format.");
            }

            var infoJson = Bytes.ToString(rawCard.ContentSnapshot);
            var info = Configuration.Serializer.Deserialize<RawCardInfo>(infoJson);
            var fingerprint = cardManagerCrypto.SHA256(rawCard.ContentSnapshot);
            var cardId = Bytes.ToString(fingerprint, StringEncoding.HEX);

            return new CSR
            {
                CardId = cardId,
                info = info,
                snapshot = rawCard.ContentSnapshot,
                signatures = rawCard.Signatures.ToList()
            };
        }

        /// <summary>
        /// Generates a new request in order to apply for a card registration. It contains the public key for 
        /// which the card should be registered, identity information (such as a user name) and integrity 
        /// protection in form of digital self signature.
        /// </summary> 
        /// <param name="cardManagerCrypto"></param>
        /// <param name="params">The information about identity and public key.</param>
        /// <returns>A new instance of <see cref="CSR"/> class.</returns>
        public static CSR Generate(ICardManagerCrypto cardManagerCrypto, CSRParams @params)
        {
            if (cardManagerCrypto == null)
            {
                throw new ArgumentNullException(nameof(cardManagerCrypto));
            }

            if (@params == null)
            {
                throw new ArgumentNullException(nameof(@params));
            }

            if (string.IsNullOrWhiteSpace(@params.Identity))
            {
                throw new ArgumentException($"{@params.Identity} property is mandatory");
            }

            if (@params.PublicKey == null)
            {
                throw new ArgumentException($"{@params.PublicKey} property is mandatory");
            }

            var timeNow = DateTime.UtcNow;
            //to truncate milliseconds and microseconds
            timeNow = timeNow.AddTicks(-timeNow.Ticks % TimeSpan.TicksPerSecond);
            var details = new RawCardInfo
            {
                Identity = @params.Identity,
                PublicKeyBytes = cardManagerCrypto.ExportPublicKey(@params.PublicKey),
                Version = "5.0",
                CreatedAt = timeNow,
                PreviousCardId = @params.PreviousCardId
            };

            var snapshot = CardUtils.TakeSnapshot(details);
            var fingerprint = cardManagerCrypto.SHA256(snapshot);
            var cardId = Bytes.ToString(fingerprint, StringEncoding.HEX);

            var csr = new CSR
            {
                CardId = cardId,
                info = details,
                snapshot = snapshot,
                signatures = new List<RawCardSignature>()
            };
            
            if (@params.PrivateKey != null)
            {
                csr.Sign(cardManagerCrypto, new SignParams
                {
                    SignerCardId = cardId,
                    SignerType = SignerType.Self,
                    SignerPrivateKey = @params.PrivateKey
                });
            }

            return csr;
        }
    }
}   