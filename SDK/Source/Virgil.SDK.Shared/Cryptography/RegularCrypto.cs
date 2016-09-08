#region Copyright (C) Virgil Security Inc.
// Copyright (C) 2015-2016 Virgil Security Inc.
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

namespace Virgil.SDK.Cryptography
{
    using System.IO;
    using System.Linq;

    using Virgil.Crypto;
    using Virgil.Crypto.Foundation;

    public sealed class RegularCrypto : ICrypto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegularCrypto"/> class.
        /// </summary>
        public RegularCrypto()  
        {
        }

        public PrivateKey GenerateKey()
        {
            using (var keyPair = VirgilKeyPair.GenerateRecommended())
            {
                var publicKey = keyPair.PublicKey();
                var publicKeyHash = ComputeHash(publicKey);
                
                var privateKey = new InternalPrivateKey
                {
                    PublicKey = publicKey,
                    ReceiverId = publicKeyHash,
                    Value = keyPair.PrivateKey()
                };

                return privateKey;
            }
        }

        public PrivateKey ImportKey(byte[] privateKey)
        {
            var publicKeyData = VirgilKeyPair.ExtractPublicKey(privateKey, new byte[] { });
            var receiverId = ComputeHash(publicKeyData);

            var internalPrivateKey = new InternalPrivateKey
            {
                ReceiverId = receiverId,
                PublicKey = publicKeyData,
                Value = publicKeyData
            };

            return internalPrivateKey;
        }

        public PublicKey ImportPublicKey(byte[] publicKey)
        {
            var receiverId = ComputeHash(publicKey);
            var internalPublicKey = new InternalPublicKey
            {
                ReceiverId = publicKey,
                Value = receiverId
            };  

            return internalPublicKey;
        }

        public byte[] ExportKey(PrivateKey privateKey)
        {
            var key = (PrivateKey) privateKey;
            return key.Value;
        }

        public byte[] ExportPublicKey(PublicKey publicKey)
        {
            var key = (PublicKey) publicKey;
            return key.Value;
        }

        public PublicKey ExtractPublicKey(PrivateKey privateKey)
        {
            var key = (PrivateKey) privateKey;
            return key.PublicKey;
        }


        public byte[] Encrypt(byte[] data, params PublicKey[] recipients)
        {
            using (var cipher = new VirgilCipher())
            {
                foreach (var key in recipients.Cast<PublicKey>())
                {
                    cipher.AddKeyRecipient(key.ReceiverId, key.Value);
                }

                var encryptedData = cipher.Encrypt(data, true);
                return encryptedData;   
            }
        }

        public Stream Encrypt(Stream stream, params PublicKey[] recipients)
        {
            using (var cipher = new VirgilStreamCipher())
            {
                foreach (var key in recipients.Cast<PublicKey>())
                {
                    cipher.AddKeyRecipient(key.ReceiverId, key.Value);
                }

                var source = new VirgilStreamDataSource(stream);
                var sinkStream = new MemoryStream();

                var sink = new VirgilStreamDataSink(sinkStream);
                cipher.Encrypt(source, sink);

                return sinkStream;
            }
        }

        public bool Verify(byte[] data, byte[] signature, PublicKey signer)
        {
            using (var virgilSigner = new VirgilSigner())
            {
                var key = (PublicKey) signer;

                var isValid = virgilSigner.Verify(data, signature, key.Value);
                return isValid;
            }
        }

        public bool Verify(Stream stream, byte[] signature, PublicKey signer)
        {
            using (var streamSigner = new VirgilStreamSigner())
            {
                var source = new VirgilStreamDataSource(stream);
                var key = (PublicKey)signer;

                var isValid = streamSigner.Verify(source, signature, key.Value);
                return isValid;
            }
        }

        public byte[] Decrypt(byte[] cipherdata, PrivateKey privateKey)
        {
            var key = (PrivateKey)privateKey;

            using (var cipher = new VirgilCipher())
            {
                var data = cipher.DecryptWithKey(cipherdata, key.PublicKey.ReceiverId, key.Value);
                return data;
            }
        }

        public Stream Decrypt(Stream cipherstream, PrivateKey privateKey)
        {
            var key = (PrivateKey) privateKey;

            using (var cipher = new VirgilStreamCipher())
            {
                var source = new VirgilStreamDataSource(cipherstream);
                var sinkStream = new MemoryStream();

                var sink = new VirgilStreamDataSink(sinkStream);
                cipher.DecryptWithKey(source, sink, key.PublicKey.ReceiverId, key.Value);

                return sinkStream;
            }
        }

        public byte[] Sign(byte[] data, PrivateKey privateKey)
        {
            var key = (PrivateKey) privateKey;

            using (var signer = new VirgilSigner())
            {
                var signature = signer.Sign(data, key.Value);
                return signature;
            }
        }

        public byte[] Sign(Stream stream, PrivateKey privateKey)
        {
            using (var signer = new VirgilStreamSigner())
            {
                var source = new VirgilStreamDataSource(stream);

                var signature = signer.Sign(source, ((PrivateKey)privateKey).Value);
                return signature;
            }
        }

        private static byte[] ComputeHash(byte[] publicKey)
        {
            var sha256 = new VirgilHash(VirgilHash.Algorithm.SHA256);
            var hash = sha256.Hash(publicKey);

            return hash;
        }
    }
}