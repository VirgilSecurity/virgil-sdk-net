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
    using System.Text;
    using Virgil.Crypto;

    public class VirgilCrypto : Crypto, IDisposable
    {
        public override PrivateKey GeneratePrivateKey()
        {
            using (var keyPair = VirgilKeyPair.Generate())
            {
                var privateKey = new VirgilPrivateKey(keyPair.PrivateKey());
                return privateKey;
            }
        }

        public override PrivateKey GeneratePrivateKey(PrivateKeyPatameters parameters)
        {
            using (var keyPair = VirgilKeyPair.Generate())
            {
                var privateKey = new VirgilPrivateKey(keyPair.PrivateKey());
                return privateKey;
            }
        }

        public override PrivateKey RevealPrivateKey(byte[] privateKey)
        {
            return new VirgilPrivateKey(privateKey);
        }

        public override byte[] ExportPrivateKey(PrivateKey privateKey)
        {
            var virgilPrivateKey = (VirgilPrivateKey)privateKey;
            return virgilPrivateKey.GetValue();
        }

        public override byte[] Encrypt(byte[] data, IEnumerable<IRecipient> recipients)
        {
            using (var cipher = new VirgilCipher())
            {
                foreach (var recipient in recipients)
                {
                    cipher.AddKeyRecipient(recipient.RecipientId, recipient.PublicKey.GetValue());
                }

                var encryptedData = cipher.Encrypt(data, true);
                return encryptedData;
            }
        }

        public override Stream Encrypt(Stream stream, IEnumerable<IRecipient> recipients)
        {
            using (var cipher = new VirgilStreamCipher())
            {
                foreach (var recipient in recipients)
                {
                    cipher.AddKeyRecipient(recipient.RecipientId, recipient.PublicKey.GetValue());
                }

                var source = new VirgilStreamDataSource(stream);
                var sinkStream = new MemoryStream();

                var sink = new VirgilStreamDataSink(sinkStream);
                cipher.Encrypt(source, sink);

                return sinkStream;
            }
        }

        public override byte[] Encrypt(byte[] data, string password)
        {
            using (var cipher = new VirgilCipher())
            {
                var passwordData = Encoding.UTF8.GetBytes(password);
                cipher.AddPasswordRecipient(passwordData);

                return cipher.Encrypt(data, true);
            }
        }

        public override byte[] Decrypt(byte[] cipherdata, string password)
        {
            using (var cipher = new VirgilCipher())
            {
                var passwordData = Encoding.UTF8.GetBytes(password);
                return cipher.DecryptWithPassword(cipherdata, passwordData);
            }
        }

        public override byte[] Decrypt(byte[] cipherdata, byte[] recipientId, PrivateKey privateKey)
        {
            var virgilPrivateKey = (VirgilPrivateKey)privateKey;

            using (var cipher = new VirgilCipher())
            {
                var data = cipher.DecryptWithKey(cipherdata, recipientId, virgilPrivateKey.GetValue());
                return data;
            }
        }

        public override Stream Decrypt(Stream cipherstream, byte[] recipientId, PrivateKey privateKey)
        {
            var virgilPrivateKey = (VirgilPrivateKey)privateKey;

            using (var cipher = new VirgilStreamCipher())
            {
                var source = new VirgilStreamDataSource(cipherstream);
                var sinkStream = new MemoryStream();

                var sink = new VirgilStreamDataSink(sinkStream);
                cipher.DecryptWithKey(source, sink, recipientId, virgilPrivateKey.GetValue());

                return sinkStream;
            }
        }

        public override byte[] Sign(byte[] data, PrivateKey privateKey)
        {
            var virgilPrivateKey = (VirgilPrivateKey)privateKey;

            using (var signer = new VirgilSigner())
            {
                var signature = signer.Sign(data, virgilPrivateKey.GetValue());
                return signature;
            }
        }

        public override byte[] Sign(Stream stream, PrivateKey privateKey)
        {
            using (var signer = new VirgilStreamSigner())
            {
                var source = new VirgilStreamDataSource(stream);

                var signature = signer.Sign(source, ((VirgilPrivateKey)privateKey).GetValue());
                return signature;
            }
        }

        public override bool Verify(byte[] data, byte[] signature, PublicKey signer)
        {
            using (var streamSigner = new VirgilSigner())
            {
                var isValid = streamSigner.Verify(data, signature, signer.GetValue());
                return isValid;
            }
        }

        public override bool Verify(Stream stream, byte[] signature, PublicKey signer)
        {
            using (var streamSigner = new VirgilStreamSigner())
            {
                var source = new VirgilStreamDataSource(stream);

                var isValid = streamSigner.Verify(source, signature, signer.GetValue());
                return isValid;
            }
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}   