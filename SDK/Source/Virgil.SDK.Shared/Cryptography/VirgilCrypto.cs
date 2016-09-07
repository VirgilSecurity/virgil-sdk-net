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

    using Virgil.Crypto;
    using Virgil.Crypto.Foundation;

    public class VirgilCrypto : Crypto<PublicKey, PrivateKey>
    {
        public override PrivateKey GenerateKey()
        {
            using (var keyPair = VirgilKeyPair.GenerateRecommended())
            {
                var receiverId = this.GetRecipientId(keyPair.PublicKey());
                var publicKey = new PublicKey(receiverId, keyPair.PrivateKey());
                var privateKey = new PrivateKey(keyPair.PrivateKey(), publicKey);
                return privateKey;
            }
        }

        public override PrivateKey ImportKey(byte[] data)
        {
            var publicKeyData = Virgil.Crypto.VirgilKeyPair.ExtractPublicKey(data, new byte[] {});
            var receiverId = this.GetRecipientId(publicKeyData);
            var publicKey = new PublicKey(receiverId, publicKeyData);
            return new PrivateKey(data, publicKey);
        }
            
        public override byte[] ExportKey(PrivateKey privateKey)
        {
            return privateKey.Value;
        }
        

        public override byte[] Encrypt(byte[] data, params IPublicKey[] recipients)
        {
            using (var cipher = new VirgilCipher())
            {
                foreach (var recipient in recipients)
                {
                    var key = (PublicKey)recipient;
                    cipher.AddKeyRecipient(key.ReceiverId, recipient.Value);
                }

                var encryptedData = cipher.Encrypt(data, true);
                return encryptedData;
            }
        }

        public override Stream Encrypt(Stream stream, params IPublicKey[] recipients)
        {
            using (var cipher = new VirgilStreamCipher())
            {
                foreach (var recipient in recipients)
                {
                    var key = (PublicKey)recipient;
                    cipher.AddKeyRecipient(key.ReceiverId, recipient.Value);
                }

                var source = new VirgilStreamDataSource(stream);
                var sinkStream = new MemoryStream();

                var sink = new VirgilStreamDataSink(sinkStream);
                cipher.Encrypt(source, sink);

                return sinkStream;
            }
        }

        public override bool Verify(byte[] data, byte[] signature, IPublicKey signer)
        {
            using (var virgilSigner = new VirgilSigner())
            {
                var isValid = virgilSigner.Verify(data, signature, signer.Value);
                return isValid;
            }
        }

        public override bool Verify(Stream stream, byte[] signature, IPublicKey signer)
        {
            using (var streamSigner = new VirgilStreamSigner())
            {
                var source = new VirgilStreamDataSource(stream);

                var isValid = streamSigner.Verify(source, signature, signer.Value);
                return isValid;
            }
        }

        public override byte[] Decrypt(byte[] cipherdata, IPrivateKey privateKey)
        {
            var key = (PrivateKey)privateKey;

            using (var cipher = new VirgilCipher())
            {
                var data = cipher.DecryptWithKey(cipherdata, key.PublicKey.ReceiverId, key.Value);
                return data;
            }
        }

        public override Stream Decrypt(Stream cipherstream, IPrivateKey privateKey)
        {
            var key = (PrivateKey)privateKey;

            using (var cipher = new VirgilStreamCipher())
            {
                var source = new VirgilStreamDataSource(cipherstream);
                var sinkStream = new MemoryStream();

                var sink = new VirgilStreamDataSink(sinkStream);
                cipher.DecryptWithKey(source, sink, key.PublicKey.ReceiverId, key.Value);

                return sinkStream;
            }
        }

        public override byte[] Sign(byte[] data, IPrivateKey privateKey)
        {
            var key = (PrivateKey)privateKey;

            using (var signer = new VirgilSigner())
            {
                var signature = signer.Sign(data, key.Value);
                return signature;
            }
        }

        public override byte[] Sign(Stream stream, IPrivateKey privateKey)
        {
            using (var signer = new VirgilStreamSigner())
            {
                var source = new VirgilStreamDataSource(stream);

                var signature = signer.Sign(source, ((PrivateKey)privateKey).Value);
                return signature;
            }
        }

        private byte[] GetRecipientId(byte[] publicKey)
        {
            var sha256 = new VirgilHash(VirgilHash.Algorithm.SHA256);
            var hash = sha256.Hash(publicKey);

            return hash;
        }
    }
}