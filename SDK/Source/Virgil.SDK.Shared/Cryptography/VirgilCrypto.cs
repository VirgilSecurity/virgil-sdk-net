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

    using Virgil.SDK.Exceptions;

    /// <summary>
    /// The <see cref="VirgilCrypto"/> class provides a cryptographic operations in applications, such as hashing, 
    /// signature generation and verification, and encryption and decryption.
    /// </summary>
    public sealed class VirgilCrypto : Crypto
    {
        private readonly byte[] CustomParamKeySignature = Encoding.UTF8.GetBytes("VIRGIL-DATA-SIGNATURE");

        /// <summary>
        /// Generates asymmetric key pair that is comprised of both public and private keys by specified type.
        /// </summary>
        public KeyPair GenerateKeys(KeyPairType keyPairType)
        {
            try
            {
                using (var keyPair = VirgilKeyPair.Generate(keyPairType.ToVirgilKeyPairType()))
                {
                    var keyPairId = this.ComputePublicKeyHash(keyPair.PublicKey());
                    var privateKey = new PrivateKey
                    {
                        ReceiverId = keyPairId,
                        Value = VirgilKeyPair.PrivateKeyToDER(keyPair.PrivateKey()),
                    };

                    var publicKey = new PublicKey
                    {
                        ReceiverId = keyPairId,
                        Value = VirgilKeyPair.PublicKeyToDER(keyPair.PublicKey())
                    };

                    return new KeyPair(publicKey, privateKey);
                }
            }
            catch (ApplicationException ex)
            {
                throw new CryptoException(ex.Message);
            }
        }

        /// <summary>
        /// Generates recommended asymmetric key pair that is comprised of both Public and Private keys.
        /// </summary>
        public override KeyPair GenerateKeys()
        {
            return this.GenerateKeys(KeyPairType.Default);
        }

        /// <summary>
        /// Imports the Private key from material representation.
        /// </summary>
        public override PrivateKey ImportPrivateKey(byte[] keyData, string password = null)
        {
            if (keyData == null)
                throw new ArgumentNullException(nameof(keyData));

            try
            {
                var privateKeyBytes = string.IsNullOrEmpty(password)
                    ? VirgilKeyPair.PrivateKeyToDER(keyData)
                    : VirgilKeyPair.DecryptPrivateKey(keyData, Encoding.UTF8.GetBytes(password));

                var publicKey = VirgilKeyPair.ExtractPublicKey(privateKeyBytes, new byte[] { });
                var privateKey = new PrivateKey
                {
                    ReceiverId = this.ComputePublicKeyHash(publicKey),
                    Value = VirgilKeyPair.PrivateKeyToDER(privateKeyBytes)
                };

                return privateKey;
            }
            catch (ApplicationException ex)
            {
                throw new CryptoException(ex.Message);
            }
        }

        /// <summary>
        /// Imports the Public key from material representation.
        /// </summary>
        public override PublicKey ImportPublicKey(byte[] keyData)
        {
            try
            {
                var publicKey = new PublicKey
                {
                    ReceiverId = this.ComputePublicKeyHash(keyData),
                    Value = VirgilKeyPair.PublicKeyToDER(keyData)
                };

                return publicKey;
            }
            catch (ApplicationException ex)
            {
                throw new CryptoException(ex.Message);
            }
        }

        /// <summary>
        /// Exports the Private key into material representation.
        /// </summary>
        public override byte[] ExportPrivateKey(PrivateKey privateKey, string password = null)
        {
            try
            {
                if (string.IsNullOrEmpty(password))
                {
                    return VirgilKeyPair.PrivateKeyToDER(privateKey.Value);
                }

                var passwordBytes = Encoding.UTF8.GetBytes(password);
                var encryptedKey = VirgilKeyPair.EncryptPrivateKey(privateKey.Value, passwordBytes);

                return VirgilKeyPair.PrivateKeyToDER(encryptedKey, passwordBytes);
            }
            catch (ApplicationException ex)
            {
                throw new CryptoException(ex.Message);
            }
        }

        /// <summary>
        /// Exports the Public key into material representation.
        /// </summary>
        public override byte[] ExportPublicKey(PublicKey publicKey)
        {
            try
            {
                return VirgilKeyPair.PublicKeyToDER(publicKey.Value);
            }
            catch (ApplicationException ex)
            {
                throw new CryptoException(ex.Message);
            }
        }

        /// <summary>
        /// Extracts the Public key from Private key.
        /// </summary>
        public override PublicKey ExtractPublicKey(PrivateKey privateKey)
        {
            try
            {
                var publicKeyData = VirgilKeyPair.ExtractPublicKey(privateKey.Value, new byte[] { });

                var publicKey = new PublicKey
                {
                    ReceiverId = privateKey.ReceiverId,
                    Value = VirgilKeyPair.PublicKeyToDER(publicKeyData)
                };

                return publicKey;
            }
            catch (ApplicationException ex)
            {
                throw new CryptoException(ex.Message);
            }
        }

        /// <summary>
        /// Encrypts the specified data using recipients Public keys.
        /// </summary>
        public override byte[] Encrypt(byte[] data, params PublicKey[] recipients)
        {
            try
            {
                using (var cipher = new VirgilCipher())
                {
                    foreach (var publicKey in recipients)
                    {
                        cipher.AddKeyRecipient(publicKey.ReceiverId, publicKey.Value);
                    }

                    var encryptedData = cipher.Encrypt(data, true);
                    return encryptedData;
                }
            }
            catch (ApplicationException ex)
            {
                throw new CryptoException(ex.Message);
            }
        }

        /// <summary>
        /// Decrypts the specified data using Private key.
        /// </summary>
        public override byte[] Decrypt(byte[] cipherData, PrivateKey privateKey)
        {
            try
            {
                using (var cipher = new VirgilCipher())
                {
                    var data = cipher.DecryptWithKey(cipherData, privateKey.ReceiverId, privateKey.Value);
                    return data;
                }
            }
            catch (ApplicationException ex)
            {
                throw new CryptoException(ex.Message);
            }
        }

        /// <summary>
        /// Signs the specified data using Private key. 
        /// </summary>
        public override byte[] Sign(byte[] data, PrivateKey privateKey)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (privateKey == null)
                throw new ArgumentNullException(nameof(privateKey));

            try
            {
                using (var signer = new VirgilSigner())
                {
                    var signature = signer.Sign(data, privateKey.Value);
                    return signature;
                }
            }
            catch (ApplicationException ex)
            {
                throw new CryptoException(ex.Message);
            }
        }

        /// <summary>
        /// Verifies the specified signature using original data and publicKey's Public key.
        /// </summary>
        public override bool Verify(byte[] data, byte[] signature, PublicKey signerKey)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (signature == null)
                throw new ArgumentNullException(nameof(signature));

            try
            {
                using (var virgilSigner = new VirgilSigner())
                {
                    var isValid = virgilSigner.Verify(data, signature, signerKey.Value);
                    return isValid;
                }
            }
            catch (ApplicationException ex)
            {
                throw new CryptoException(ex.Message);
            }
        }

        /// <summary>
        /// Encrypts the specified stream using recipients Public keys.
        /// </summary>
        public override void Encrypt(Stream inputStream, Stream cipherStream, params PublicKey[] recipients)
        {
            try
            {
                using (var cipher = new VirgilChunkCipher())
                using (var source = new VirgilStreamDataSource(inputStream))
                using (var sink = new VirgilStreamDataSink(cipherStream))
                {
                    foreach (var publicKey in recipients)
                    {
                        cipher.AddKeyRecipient(publicKey.ReceiverId, publicKey.Value);
                    }

                    cipher.Encrypt(source, sink);
                }
            }
            catch (ApplicationException ex)
            {
                throw new CryptoException(ex.Message);
            }
        }

        /// <summary>
        /// Decrypts the specified stream using Private key.
        /// </summary>
        public override void Decrypt(Stream cipherStream, Stream outputStream, PrivateKey privateKey)
        {
            try
            {
                using (var cipher = new VirgilChunkCipher())
                using (var source = new VirgilStreamDataSource(cipherStream))
                using (var sink = new VirgilStreamDataSink(outputStream))
                {
                    cipher.DecryptWithKey(source, sink, privateKey.ReceiverId, privateKey.Value);
                }
            }
            catch (ApplicationException ex)
            {
                throw new CryptoException(ex.Message);
            }
        }

        /// <summary>
        /// Signs and encrypts the data.
        /// </summary>
        /// <param name="data">The data to encrypt.</param>
        /// <param name="privateKey">The Private key to sign the <param name="data"></param>.</param>
        /// <param name="recipients">The list of Public key recipients to encrypt the <param name="data"></param>.</param>
        /// <returns></returns>
        /// <exception cref="Virgil.SDK.Exceptions.CryptoException"></exception>
        public override byte[] SignThenEncrypt(byte[] data, PrivateKey privateKey, params PublicKey[] recipients)
        {
            try
            {
                using (var signer = new VirgilSigner())
                using (var cipher = new VirgilCipher())
                {
                    var signature = signer.Sign(data, privateKey.Value);
                    
                    var customData = cipher.CustomParams();
                    customData.SetData(this.CustomParamKeySignature, signature);

                    foreach (var publicKey in recipients)
                    {
                        cipher.AddKeyRecipient(publicKey.ReceiverId, publicKey.Value);
                    }

                    return cipher.Encrypt(data, true);
                }
            }
            catch (ApplicationException ex)
            {
                throw new CryptoException(ex.Message);
            }
        }

        /// <summary>
        /// Decrypts and verifies the data.
        /// </summary>
        /// <param name="cipherData">The cipher data.</param>
        /// <param name="privateKey">The Private key to decrypt.</param>
        /// <param name="publicKey">The Public key to verify.</param>
        /// <returns>The decrypted data</returns>
        /// <exception cref="Virgil.SDK.Exceptions.SignatureIsNotValidException"></exception>
        /// <exception cref="Virgil.SDK.Exceptions.CryptoException"></exception>
        public override byte[] DecryptThenVerify(byte[] cipherData, PrivateKey privateKey, PublicKey publicKey)
        {
            try
            {
                using (var signer = new VirgilSigner())
                using (var cipher = new VirgilCipher())
                {
                    var decryptedData = cipher.DecryptWithKey(cipherData, privateKey.ReceiverId, privateKey.Value);
                    var signature = cipher.CustomParams().GetData(this.CustomParamKeySignature);

                    var isValid = signer.Verify(decryptedData, signature, publicKey.Value);
                    if (!isValid)
                        throw new SignatureIsNotValidException();

                    return decryptedData;
                }
            }
            catch (ApplicationException ex)
            {
                throw new CryptoException(ex.Message);
            }
        }

        /// <summary>
        /// Signs the specified stream using Private key. 
        /// </summary>
        public override byte[] Sign(Stream inputStream, PrivateKey privateKey)
        {
            try
            {
                using (var signer = new VirgilStreamSigner())
                using (var source = new VirgilStreamDataSource(inputStream))
                {
                    var signature = signer.Sign(source, privateKey.Value);
                    return signature;
                }
            }
            catch (ApplicationException ex)
            {
                throw new CryptoException(ex.Message);
            }
        }

        /// <summary>
        /// Calculates the fingerprint.
        /// </summary>
        public override Fingerprint CalculateFingerprint(byte[] content)
        {
            if (content == null)
                throw new ArgumentNullException(nameof(content));

            try
            {
                using (var sha256 = new VirgilHash(VirgilHash.Algorithm.SHA256))
                {
                    var hash = sha256.Hash(content);
                    return new Fingerprint(hash);
                }
            }
            catch (ApplicationException ex)
            {
                throw new CryptoException(ex.Message);
            }
        }

        /// <summary>
        /// Computes the hash of specified data.
        /// </summary>
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

            try
            {
                using (hasher)
                {
                    return hasher.Hash(data);
                }
            }
            catch (ApplicationException ex)
            {
                throw new CryptoException(ex.Message);
            }
        }

        /// <summary>
        /// Verifies the specified signature using original stream and signer's Public key.
        /// </summary>
        public override bool Verify(Stream inputStream, byte[] signature, PublicKey publicKey)
        {
            if (signature == null)
                throw new ArgumentNullException(nameof(signature));

            try
            {
                using (var streamSigner = new VirgilStreamSigner())
                {
                    var source = new VirgilStreamDataSource(inputStream);
                    var isValid = streamSigner.Verify(source, signature, publicKey.Value);
                    return isValid;
                }
            }
            catch (ApplicationException ex)
            {
                throw new CryptoException(ex.Message);
            }
        }

        private byte[] ComputePublicKeyHash(byte[] publicKey)
        {
            var publicKeyDER = VirgilKeyPair.PublicKeyToDER(publicKey);
            return this.ComputeHash(publicKeyDER, HashAlgorithm.SHA256);
        }
    }
}