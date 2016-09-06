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
    using System;
    using System.Collections.Generic;
    using System.IO;
    
    using Virgil.Crypto;

    public partial class Crypto
    {
        private readonly IKeyStorageProvider storage;

        /// <summary>
        /// Initializes a new instance of the <see cref="Crypto"/> class.
        /// </summary>
        public Crypto(IKeyStorageProvider storage)
        {
            this.storage = storage;
        }
        
        /// <summary>
        /// Creates a new <see cref="PrivateKey"/> using specified.
        /// </summary>
        public TKey CreateKey<TKey>(string keyName, KeyParameters parameters) where TKey : CryptoKey
        {
            if (typeof(PrivateKey) != typeof(TKey))
            {
                throw new NotSupportedException();
            }

            using (var keyPair = VirgilKeyPair.GenerateRecommended())
            {
                var keyEntry = new KeyEntry
                {
                    Name = keyName,
                    Value = keyPair.PrivateKey(),
                    MetaData = parameters.Attributes
                };

                var attributes = new Dictionary<string, object>(parameters.Attributes)
                {
                    { "NAME", keyName }
                };

                this.storage.Store(keyEntry);

                return (TKey)(CryptoKey)new PrivateKey(attributes);
            }
        }

        /// <summary>
        /// Exports the key.
        /// </summary>
        public byte[] ExportKey<TKey>(CryptoKey key, string format)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reveals a previously created <see cref="PrivateKey"/> using key name.
        /// </summary>
        public TCryptoKey RevealKey<TCryptoKey>(string keyName) where TCryptoKey : CryptoKey
        {
            if (typeof(PrivateKey) == typeof(TCryptoKey))
            {

                
                return 
            }

            var keyEntry = this.storage.Load(keyName);
            var attributes = new Dictionary<string, object>(keyEntry.MetaData)
            {
                { "NAME", keyEntry.Name }
            };

            var privateKey = new PrivateKey(attributes);

            return (TCryptoKey)privateKey;
        }
        
        public byte[] Encrypt(byte[] data, params PublicKey[] recipients)
        {
            using (var cipher = new VirgilCipher())
            {
                foreach (var recipient in recipients)
                {
                    var receiverId = recipient.

                    cipher.AddKeyRecipient(recipient.ReceiverId, recipient.PublicKey.GetValue());
                }

                var encryptedData = cipher.Encrypt(data, true);
                return encryptedData;
            }
        }

        public Stream Encrypt(Stream stream, params IRecipient[] recipients)
        {
            using (var cipher = new VirgilStreamCipher())
            {
                foreach (var recipient in recipients)
                {
                    cipher.AddKeyRecipient(recipient.ReceiverId, recipient.PublicKey.GetValue());
                }

                var source = new VirgilStreamDataSource(stream);
                var sinkStream = new MemoryStream();

                var sink = new VirgilStreamDataSink(sinkStream);
                cipher.Encrypt(source, sink);

                return sinkStream;
            }
        }


        public byte[] Decrypt(byte[] cipherdata, PrivateKey privateKey)
        {
            var key = (VirgilPrivateKey) privateKey;

            using (var cipher = new VirgilCipher())
            {
                var data = cipher.DecryptWithKey(cipherdata, receiverId, key.GetValue());
                return data;
            }
        }

        public Stream Decrypt(Stream cipherstream, PrivateKey privateKey)
        {
            var virgilPrivateKey = (VirgilPrivateKey) privateKey;

            using (var cipher = new VirgilStreamCipher())
            {
                var source = new VirgilStreamDataSource(cipherstream);
                var sinkStream = new MemoryStream();

                var sink = new VirgilStreamDataSink(sinkStream);
                cipher.DecryptWithKey(source, sink, receiverId, virgilPrivateKey.GetValue());

                return sinkStream;
            }
        }


        public byte[] Sign(byte[] data, PrivateKey privateKey)
        {
            var key = (VirgilPrivateKey) privateKey;

            using (var signer = new VirgilSigner())
            {
                var signature = signer.Sign(data, key.GetValue());
                return signature;
            }
        }

        public byte[] Sign(Stream stream, PrivateKey privateKey)
        {
            using (var signer = new VirgilStreamSigner())   
            {
                var source = new VirgilStreamDataSource(stream);

                var signature = signer.Sign(source, ((VirgilPrivateKey)privateKey).GetValue());
                return signature;
            }
        }


        public bool Verify(byte[] data, byte[] signature, PublicKey signer)
        {
            using (var virgilSigner = new VirgilSigner())
            {
                var isValid = virgilSigner.Verify(data, signature, signer.GetValue());
                return isValid;
            }
        }

        public bool Verify(Stream stream, byte[] signature, PublicKey signer)
        {
            using (var streamSigner = new VirgilStreamSigner())
            {
                var source = new VirgilStreamDataSource(stream);

                var isValid = streamSigner.Verify(source, signature, signer.GetValue());
                return isValid;
            }
        }
    }

    public class 
}       