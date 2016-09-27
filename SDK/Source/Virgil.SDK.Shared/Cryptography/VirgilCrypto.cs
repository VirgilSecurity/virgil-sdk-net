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
    using System.IO;
    using System.Text;

    using Virgil.Crypto;
    using Virgil.Crypto.Foundation;

    public sealed class VirgilCrypto : Crypto
    {
        public KeyPair GenerateKeys(KeysType keysType)    
        {
            using (var keyPair = VirgilKeyPair.Generate(keysType.ToVirgilKeyPairType()))
            {
                var keyPairId = this.ComputePublicKeyHash(keyPair.PublicKey());
                var privateKey = new PrivateKey
                {
                    Id = keyPairId,
                    Value = VirgilKeyPair.PrivateKeyToDER(keyPair.PrivateKey()),
                };

                var publicKey = new PublicKey
                {
                    Id = keyPairId,
                    Value = VirgilKeyPair.PublicKeyToDER(keyPair.PublicKey())
                };
                
                return new KeyPair(publicKey, privateKey);
            }
        }

        public override KeyPair GenerateKeys()
        {
            return this.GenerateKeys(KeysType.Default);
        }

        public override PrivateKey ImportPrivateKey(byte[] keyData, string password = null)
        {
            if (keyData == null)
                throw new ArgumentNullException(nameof(keyData));

            var privateKeyBytes = string.IsNullOrEmpty(password) ? VirgilKeyPair.PrivateKeyToDER(keyData) : VirgilKeyPair.DecryptPrivateKey(keyData, Encoding.UTF8.GetBytes(password));

            var publicKey = VirgilKeyPair.ExtractPublicKey(privateKeyBytes, new byte[] { });
            var privateKey = new PrivateKey
            {
                Id = this.ComputePublicKeyHash(publicKey),
                Value = VirgilKeyPair.PrivateKeyToDER(privateKeyBytes)
            };

            return privateKey;
        }

        public override PublicKey ImportPublicKey(byte[] keyData)
        {
            var publicKey = new PublicKey
            {
                Id = this.ComputePublicKeyHash(keyData),
                Value = VirgilKeyPair.PublicKeyToDER(keyData)
            };

            return publicKey;
        }

        public override byte[] ExportPrivateKey(PrivateKey privateKey, string password = null)
        {
            if (string.IsNullOrEmpty(password))
            {
                return VirgilKeyPair.PrivateKeyToDER(privateKey.Value);
            }

            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var encryptedKey = VirgilKeyPair.EncryptPrivateKey(privateKey.Value, passwordBytes);

            return VirgilKeyPair.PrivateKeyToDER(encryptedKey, passwordBytes);
        }
        
        public override byte[] ExportPublicKey(PublicKey publicKey)
        {
            return VirgilKeyPair.PublicKeyToDER(publicKey.Value);
        }

        public override byte[] Encrypt(byte[] data, params PublicKey[] recipients)
        {
            using (var cipher = new VirgilCipher())
            {
                foreach (var publicKey in recipients)
                {
                    cipher.AddKeyRecipient(publicKey.Id, publicKey.Value);
                }

                var encryptedData = cipher.Encrypt(data, true);
                return encryptedData;
            }
        }

        public override bool VerifyFingerprint(string fingerprint, byte[] signature, PublicKey signer)
        {
            var hash = VirgilByteArrayUtils.HexToBytes(fingerprint);
            return this.Verify(hash, signature, signer);
        }

        public override byte[] Decrypt(byte[] cipherData, PrivateKey privateKey)
        {
            using (var cipher = new VirgilCipher())
            {
                var data = cipher.DecryptWithKey(cipherData, privateKey.Id, privateKey.Value);
                return data;
            }
        }

        public override byte[] Sign(byte[] data, PrivateKey privateKey)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (privateKey == null)
                throw new ArgumentNullException(nameof(privateKey));

            using (var signer = new VirgilSigner())
            {
                var signature = signer.Sign(data, privateKey.Value);
                return signature;
            }
        }

        public override bool Verify(byte[] data, byte[] signature, PublicKey signer)
        {
            using (var virgilSigner = new VirgilSigner())
            {
                var isValid = virgilSigner.Verify(data, signature, signer.Value);
                return isValid;
            }
        }

        public override void Encrypt(Stream stream, Stream outputStream, params PublicKey[] recipients)
        {
            using (var cipher = new VirgilStreamCipher())
            {
                foreach (var publicKey in recipients)
                {
                    cipher.AddKeyRecipient(publicKey.Id, publicKey.Value);
                }

                var source = new VirgilStreamDataSource(stream);
                var sink = new VirgilStreamDataSink(outputStream);

                cipher.Encrypt(source, sink);
            }
        }

        public override void Decrypt(Stream inputStream, Stream outputStream, PrivateKey privateKey)
        {
            using (var cipher = new VirgilStreamCipher())
            {
                var source = new VirgilStreamDataSource(inputStream);
                var sink = new VirgilStreamDataSink(outputStream);

                cipher.DecryptWithKey(source, sink, privateKey.Id, privateKey.Value);
            }
        }

        public override byte[] Sign(Stream inputStream, PrivateKey privateKey)
        {
            using (var signer = new VirgilStreamSigner())
            {
                var source = new VirgilStreamDataSource(inputStream);

                var signature = signer.Sign(source, privateKey.Value);
                return signature;
            }
        }

        public override byte[] SignFingerprint(string fingerprint, PrivateKey privateKey)
        {
            var hash = VirgilByteArrayUtils.HexToBytes(fingerprint);
            return this.Sign(hash, privateKey);
        }

        public override byte[] SignThenEncrypt(byte[] data, PrivateKey privateKey, params PublicKey[] recipients)
        {
            throw new NotImplementedException();
        }

        public override byte[] DecryptThenVerify(byte[] data, PrivateKey privateKey, PublicKey publicKey)
        {
            throw new NotImplementedException();
        }

        public override string CalculateFingerprint(byte[] content)
        {
            var sha256 = new VirgilHash(VirgilHash.Algorithm.SHA256);
            var hash = sha256.Hash(content);

            return VirgilByteArrayUtils.BytesToHex(hash);
        }

        public override byte[] ComputeHash(byte[] data, HashAlgorithm algorithm)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            VirgilHash hasher;

            switch (algorithm)
            {
                case HashAlgorithm.MD5:
                    hasher = new VirgilHash(VirgilHash.Algorithm.MD5);
                    break;
                case HashAlgorithm.SHA1:
                    hasher = new VirgilHash(VirgilHash.Algorithm.SHA1);
                    break;
                case HashAlgorithm.SHA224:
                    hasher = new VirgilHash(VirgilHash.Algorithm.SHA224);
                    break;
                case HashAlgorithm.SHA256:
                    hasher = new VirgilHash(VirgilHash.Algorithm.SHA256);
                    break;
                case HashAlgorithm.SHA384:
                    hasher = new VirgilHash(VirgilHash.Algorithm.SHA384);
                    break;
                case HashAlgorithm.SHA512:
                    hasher = new VirgilHash(VirgilHash.Algorithm.SHA512);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(algorithm), algorithm, null);
            }

            using (hasher)
            {
                return hasher.Hash(data);
            }
        }

        public override bool Verify(Stream inputStream, byte[] signature, PublicKey signer)
        {
            using (var streamSigner = new VirgilStreamSigner())
            {
                var source = new VirgilStreamDataSource(inputStream);
                var publicKey = signer;

                var isValid = streamSigner.Verify(source, signature, publicKey.Value);
                return isValid;
            }
        }

        private byte[] ComputePublicKeyHash(byte[] publicKey)
        {
            var publicKeyDER = VirgilKeyPair.PublicKeyToDER(publicKey);
            return this.ComputeHash(publicKeyDER, HashAlgorithm.SHA256);
        }
    }
}