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
    using System.Linq;
    using Virgil.Crypto;
    using Virgil.Crypto.Foundation;

    using Virgil.SDK.Exceptions;

    /// <summary>
    /// The <see cref="VirgilCrypto"/> class provides a cryptographic operations in applications, such as hashing, 
    /// signature generation and verification, and encryption and decryption.
    /// </summary>
    public sealed class VirgilCrypto : Crypto
    {
        private readonly KeyPairType defaultKeyPairType;
        private readonly byte[] CustomParamKeySignature = Encoding.UTF8.GetBytes("VIRGIL-DATA-SIGNATURE");
        private readonly byte[] CustomParamKeySignerId = Encoding.UTF8.GetBytes("VIRGIL-DATA-SIGNER-ID");

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilCrypto" /> class.
        /// </summary>
        /// <param name="defaultKeyPairType">Default type of the key pair.</param>
        public VirgilCrypto(KeyPairType defaultKeyPairType)
        {
            this.defaultKeyPairType = defaultKeyPairType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilCrypto" /> class.
        /// </summary>
        public VirgilCrypto()
        {
            this.defaultKeyPairType = KeyPairType.Default;
        }

        /// <summary>
        /// Generates asymmetric key pair that is comprised of both public and private keys by specified type.
        /// </summary>
        /// <param name="keyPairType">type of the generated keys.
        ///   The possible values can be found in <see cref="KeyPairType"/>.</param>
        /// <returns>Generated key pair with type EC_SECP256R1.</returns>
        /// <example>
        ///     <code>
        ///         var crypto = new VirgilCrypto();
        ///         var keyPair = crypto.GenerateKeys(KeyPairType.EC_SECP256R1);
        ///     </code>
        /// </example>
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
            catch (Exception ex)
            {
                throw new CryptoException(ex.Message);
            }
        }

        /// <summary>
        /// Generates recommended asymmetric key pair that is comprised of both Public and Private keys.
        /// </summary>
        /// <returns>Generated key pair.</returns>
        /// <example>
        ///     <code>
        ///         var crypto = new VirgilCrypto();
        ///         var keyPair = crypto.GenerateKeys();
        ///     </code>
        /// </example>
        public override KeyPair GenerateKeys()
        {
            return this.GenerateKeys(this.defaultKeyPairType);
        }

        /// <summary>
        /// Imports the Private key from material representation.
        /// </summary>
        /// <example>
        ///     <code>
        ///         var crypto = new VirgilCrypto();
        ///         var publicKey = crypto.ImportPrivateKey(exportedPrivateKey);
        ///     </code>
        /// </example>
        /// How to get exportedPrivateKey <see cref="ExportPrivateKey(IPrivateKey privateKey, string password = null)"/>
        public override IPrivateKey ImportPrivateKey(byte[] keyData, string password = null)
        {
            if (keyData == null)
                throw new ArgumentNullException(nameof(keyData));

            try
            {
                var privateKeyBytes = string.IsNullOrEmpty(password)
                    ? keyData
                    : VirgilKeyPair.DecryptPrivateKey(keyData, Encoding.UTF8.GetBytes(password));

                var publicKey = VirgilKeyPair.ExtractPublicKey(privateKeyBytes, new byte[] { });
                var privateKey = new PrivateKey
                {
                    ReceiverId = this.ComputePublicKeyHash(publicKey),
                    Value = VirgilKeyPair.PrivateKeyToDER(privateKeyBytes)
                };

                return privateKey;
            }
            catch (Exception ex)
            {
                throw new CryptoException(ex.Message);
            }
        }

        /// <summary>
        /// Imports the Public key from material representation.
        /// </summary>
        /// <param name="keyData">public key material representation bytes.</param>
        /// <returns>Imported public key.</returns>
        /// <example>
        ///     <code>
        ///         var crypto = new VirgilCrypto();
        ///         var publicKey = crypto.ImportPublicKey(exportedPublicKey);
        ///     </code>
        /// </example>
        /// How to get exportedPublicKey <see cref="ExportPublicKey(IPublicKey)"/>    
        public override IPublicKey ImportPublicKey(byte[] keyData)
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
            catch (Exception ex)
            {
                throw new CryptoException(ex.Message);
            }
        }

        /// <summary>
        /// Exports the Private key into material representation.
        /// </summary>
        public override byte[] ExportPrivateKey(IPrivateKey privateKey, string password = null)
        {
            try
            {
                if (string.IsNullOrEmpty(password))
                {
                    return VirgilKeyPair.PrivateKeyToDER(privateKey.Get().Value);
                }

                var passwordBytes = Encoding.UTF8.GetBytes(password);
                var encryptedKey = VirgilKeyPair.EncryptPrivateKey(privateKey.Get().Value, passwordBytes);

                return VirgilKeyPair.PrivateKeyToDER(encryptedKey, passwordBytes);
            }
            catch (Exception ex)
            {
                throw new CryptoException(ex.Message);
            }
        }



        /// <summary>
        /// Exports the Public key into material representation.
        /// </summary>
        /// <param name="publicKey">public key for export.</param>
        /// <returns>Key material representation bytes.</returns>
        /// <example>
        ///     <code>
        ///         var crypto = new VirgilCrypto();
        ///         var keyPair = crypto.GenerateKeys();
        ///         var exportedPublicKey = crypto.ExportPublicKey(keyPair.PublicKey);
        ///     </code>
        /// </example>
        public override byte[] ExportPublicKey(IPublicKey publicKey)
        {
            try
            {
                return VirgilKeyPair.PublicKeyToDER(publicKey.Get().Value);
            }
            catch (Exception ex)
            {
                throw new CryptoException(ex.Message);
            }
        }

        /// <summary>
        /// Extracts the Public key from Private key.
        /// </summary>
        public override IPublicKey ExtractPublicKey(IPrivateKey privateKey)
        {
            try
            {
                var publicKeyData = VirgilKeyPair.ExtractPublicKey(privateKey.Get().Value, new byte[] { });

                var publicKey = new PublicKey
                {
                    ReceiverId = privateKey.Get().ReceiverId,
                    Value = VirgilKeyPair.PublicKeyToDER(publicKeyData)
                };

                return publicKey;
            }
            catch (Exception ex)
            {
                throw new CryptoException(ex.Message);
            }
        }



        /// <summary>
        /// Encrypts the specified data using recipients Public keys.
        /// </summary>
        /// <param name="data">raw data bytes for encryption.</param>
        /// <param name="recipients"> list of recipients' public keys.</param>
        /// <returns>Encrypted bytes.</returns>
        /// <example>
        ///     <code>
        ///         var crypto = new VirgilCrypto();
        ///         var keyPair = crypto.GenerateKeys();
        ///         var data = Encoding.UTF8.GetBytes("Encrypt me!");
        ///         var encryptedData = crypto.Encrypt(data, keyPair.PublicKey);
        ///     </code>
        /// </example>
        public override byte[] Encrypt(byte[] data, params IPublicKey[] recipients)
        {
            try
            {
                using (var cipher = new VirgilCipher())
                {
                    foreach (var publicKey in recipients)
                    {
                        cipher.AddKeyRecipient(publicKey.Get().ReceiverId, publicKey.Get().Value);
                    }

                    var encryptedData = cipher.Encrypt(data, true);
                    return encryptedData;
                }
            }
            catch (Exception ex)
            {
                throw new CryptoException(ex.Message);
            }
        }


        /// <summary>
        /// Decrypts the specified data using Private key.
        /// </summary>
        /// <param name="cipherData">encrypted data bytes for decryption.</param>
        /// <param name="privateKey">private key for decryption.</param>
        /// <returns>Decrypted data bytes.</returns>
        /// <example>
        ///     <code>
        ///         var crypto = new VirgilCrypto();
        ///         var keyPair = crypto.GenerateKeys();
        ///         var plainData = crypto.Decrypt(encryptedData, keyPair.PrivateKey);
        ///     </code>
        /// </example>
        /// How to get encryptedData <see cref="Encrypt(byte[] data, params IPublicKey[] recipients)"/>
        public override byte[] Decrypt(byte[] cipherData, IPrivateKey privateKey)
        {
            try
            {
                using (var cipher = new VirgilCipher())
                {
                    var data = cipher.DecryptWithKey(cipherData, privateKey.Get().ReceiverId, privateKey.Get().Value);
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw new CryptoException(ex.Message);
            }
        }


        /// <summary>
        /// Signs the specified data using Private key. 
        /// </summary>
        /// <param name="data">raw data bytes for signing.</param>
        /// <param name="privateKey">private key for signing.</param>
        /// <returns>Signature data.</returns>
        /// <example>
        ///     <code>
        ///         var crypto = new VirgilCrypto();
        ///         var keyPair = crypto.GenerateKeys();
        ///         var data = Encoding.UTF8.GetBytes("Hello Bob!");
        ///         var sugnature = crypto.Sign(data, keyPair.PrivateKey);
        ///     </code>
        /// </example>
        public override byte[] Sign(byte[] data, IPrivateKey privateKey)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (privateKey == null)
                throw new ArgumentNullException(nameof(privateKey));

            try
            {
                using (var signer = new VirgilSigner())
                {
                    var signature = signer.Sign(data, privateKey.Get().Value);
                    return signature;
                }
            }
            catch (Exception ex)
            {
                throw new CryptoException(ex.Message);
            }
        }


        /// <summary>
        /// Verifies the specified signature using original data and signer's Public key.
        /// </summary>
        /// <param name="data"> original data bytes for verification.</param>
        /// <param name="signature">signature bytes for verification.</param>
        /// <param name="signerKey"> signer public key for verification.</param>
        /// <returns>True if signature is valid, False otherwise.</returns>
        /// <example>
        ///     <code>
        ///         var crypto = new VirgilCrypto();
        ///         var publicKey = crypto.ImportPublicKey(exportedPublicKey);
        ///         var data = Encoding.UTF8.GetBytes("Hello Bob!");
        ///         crypto.Verify(data, signature, publicKey)
        ///     </code>
        /// </example>
        /// How to get signature <see cref="Sign(byte[] data, IPrivateKey privateKey)"/>
        /// How to get exportedPublicKey <see cref="ExportPublicKey(IPublicKey)"/>     
        public override bool Verify(byte[] data, byte[] signature, IPublicKey signerKey)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (signature == null)
                throw new ArgumentNullException(nameof(signature));

            try
            {
                using (var virgilSigner = new VirgilSigner())
                {
                    var isValid = virgilSigner.Verify(data, signature, signerKey.Get().Value);
                    return isValid;
                }
            }
            catch (Exception ex)
            {
                throw new CryptoException(ex.Message);
            }
        }

        /// <summary>
        /// Encrypts the specified stream using recipients Public keys.
        /// </summary>
        /// <param name="inputStream">readable stream containing input bytes.</param>
        /// <param name="cipherStream">writable stream for output.</param>
        /// <param name="recipients"> list of recipients' public keys.</param>
        /// <returns>Encrypted bytes.</returns>
        /// <example>
        ///     <code>
        ///         var crypto = new VirgilCrypto();
        ///         var aliceKeyPair = crypto.GenerateKeys();
        ///         var bobKeyPair = crypto.GenerateKeys();
        ///         using (var inputStream = new FileStream("[YOUR_FILE_PATH_HERE]", 
        ///         FileMode.Open, FileAccess.Read))
        ///         {
        ///             using (var cipherStream = new FileStream("[YOUR_CIPHER_FILE_PATH_HERE]", 
        ///             FileMode.Create, FileAccess.Write))
        ///             {
        ///                crypto.Encrypt(inputStream, cipherStream, aliceKeyPair.PublicKey, bobKeyPair.PublicKey);
        ///             }
        ///          }
        ///     </code>
        /// </example>
        public override void Encrypt(Stream inputStream, Stream cipherStream, params IPublicKey[] recipients)
        {
            try
            {
                using (var cipher = new VirgilChunkCipher())
                using (var source = new VirgilStreamDataSource(inputStream))
                using (var sink = new VirgilStreamDataSink(cipherStream))
                {
                    foreach (var publicKey in recipients)
                    {
                        cipher.AddKeyRecipient(publicKey.Get().ReceiverId, publicKey.Get().Value);
                    }

                    cipher.Encrypt(source, sink);
                }
            }
            catch (Exception ex)
            {
                throw new CryptoException(ex.Message);
            }
        }


        /// <summary>
        /// Decrypts the specified stream using Private key.
        /// <param name="cipherStream">readable stream containing encrypted data.</param>
        /// <param name="outputStream">writable stream for output.</param>
        /// <param name="privateKey">private key for decryption.</param>
        /// </summary>
        /// <example>
        ///     <code>
        ///         var crypto = new VirgilCrypto();
        ///         var alicePrivateKey = crypto.ImportPrivateKey(exportedPrivateKey);
        ///         using (var encryptedStream = new FileStream("[YOUR_CIPHER_FILE_PATH_HERE]", 
        ///                 FileMode.Open, FileAccess.Read))
        ///         {
        ///             using (var decryptedStream = new FileStream("[YOUR_DECRYPTED_FILE_PATH_HERE]", 
        ///                     FileMode.Create, FileAccess.Write))
        ///             {
        ///                 crypto.Decrypt(encryptedStream, decryptedStream, alicePrivateKey);
        ///             }
        ///          }
        ///     </code>
        /// </example>
        /// <remarks>How to get encryptedStream <see cref="Encrypt(Stream, Stream, IPublicKey[])"/></remarks>
        /// <remarks>How to get exportedPrivateKey <see cref="ExportPrivateKey(IPrivateKey, string)"/> </remarks>
        public override void Decrypt(Stream cipherStream, Stream outputStream, IPrivateKey privateKey)
        {
            try
            {
                using (var cipher = new VirgilChunkCipher())
                using (var source = new VirgilStreamDataSource(cipherStream))
                using (var sink = new VirgilStreamDataSink(outputStream))
                {
                    cipher.DecryptWithKey(source, sink, privateKey.Get().ReceiverId, privateKey.Get().Value);
                }
            }
            catch (Exception ex)
            {
                throw new CryptoException(ex.Message);
            }
        }


        /// <summary>
        /// Signs and encrypts the data.
        /// </summary>
        /// <param name="data">The data to encrypt.</param>
        /// <param name="privateKey">The Private key to sign the data.</param>
        /// <param name="recipients">The list of Public key recipients to encrypt the data.</param>
        /// <returns>Signed and encrypted data bytes.</returns>
        /// <exception cref="Virgil.SDK.Exceptions.CryptoException"></exception>
        /// <example>
        ///   <code>
        ///     var crypto = new VirgilCrypto();
        /// 
        ///     var alice = crypto.GenerateKeys();
        ///     var bob = crypto.GenerateKeys();
        ///     var originalData = Encoding.UTF8.GetBytes("Hello Bob, How are you?");
        ///     // The data to be signed with Alice's Private key and then encrypted for Bob.
        ///     var cipherData = crypto.SignThenEncrypt(originalData, alice.PrivateKey, bob.PublicKey);
        ///   </code>
        /// </example>
        public override byte[] SignThenEncrypt(byte[] data, IPrivateKey privateKey, params IPublicKey[] recipients)
        {
            try
            {
                using (var signer = new VirgilSigner())
                using (var cipher = new VirgilCipher())
                {
                    var signature = signer.Sign(data, privateKey.Get().Value);
                    
                    var customData = cipher.CustomParams();
                    customData.SetData(this.CustomParamKeySignature, signature);

                    var publicKey = this.ExtractPublicKey(privateKey);

                    customData.SetData(this.CustomParamKeySignerId, publicKey.Get().ReceiverId);
                    
                    foreach (var recipientPublicKey in recipients)
                    {
                        cipher.AddKeyRecipient(recipientPublicKey.Get().ReceiverId, recipientPublicKey.Get().Value);
                    }

                    return cipher.Encrypt(data, true);
                }
            }
            catch (Exception ex)
            {
                throw new CryptoException(ex.Message);
            }
        }

        /// <summary>
        /// Decrypts and verifies the data.
        /// </summary>
        /// <param name="cipherData">The cipher data.</param>
        /// <param name="privateKey">The Private key to decrypt.</param>
        /// <param name="publicKeys"> The list of trusted public keys for verification, 
        /// which can contain signer's public key.</param>
        /// <returns>The decrypted data</returns>
        /// <exception cref="Virgil.SDK.Exceptions.SignatureIsNotValidException"></exception>
        /// <exception cref="Virgil.SDK.Exceptions.CryptoException"></exception>
        /// <example>
        ///     <code>
        ///         var crypto = new VirgilCrypto();
        ///         var decryptedData = crypto.DecryptThenVerify(cipherData, bob.PrivateKey, alice.PublicKey);
        ///     </code>
        /// </example>
        /// How to get cipherData as well as Alice's and Bob's key pairs.
        /// <see cref="SignThenEncrypt(byte[] data, IPrivateKey privateKey, params IPublicKey[] recipients)"/>
        public override byte[] DecryptThenVerify(byte[] cipherData, IPrivateKey privateKey, params IPublicKey[] publicKeys)
        {
            try
            {
                using (var signer = new VirgilSigner())
                using (var cipher = new VirgilCipher())
                {
                    var decryptedData = cipher.DecryptWithKey(cipherData, privateKey.Get().ReceiverId, privateKey.Get().Value);
                    var signature = cipher.CustomParams().GetData(this.CustomParamKeySignature);

                    var signerPublicKey = publicKeys.FirstOrDefault();
                    if (publicKeys.Length > 1)
                    {
                        var signerId = cipher.CustomParams().GetData(this.CustomParamKeySignerId);
                        signerPublicKey = publicKeys.Single(publicKey => publicKey.Get().ReceiverId.SequenceEqual(signerId));
                    }
                
                    var isValid = signer.Verify(decryptedData, signature, signerPublicKey.Get().Value);
                    if (!isValid)
                        throw new SignatureIsNotValidException();

                    return decryptedData;
                }
            }
            catch (Exception ex)
            {
                throw new CryptoException(ex.Message);
            }
        }




        /// <summary>
        /// Signs the specified stream using Private key. 
        /// </summary>
        /// <param name="inputStream">readable stream containing input data.</param>
        /// <param name="privateKey">private key for signing.</param>
        /// <returns>Signature data.</returns>
        /// <example>
        ///     <code>
        ///         var crypto = new VirgilCrypto();
        ///         var keyPair = crypto.GenerateKeys();
        ///         using (var inputStream = new FileStream("[YOUR_FILE_PATH_HERE]", FileMode.Open, FileAccess.Read))
        ///         {
        ///             signature = crypto.Sign(inputStream, keyPair.PrivateKey);
        ///         }
        ///     </code>
        /// </example>
        public override byte[] Sign(Stream inputStream, IPrivateKey privateKey)
        {
            try
            {
                using (var signer = new VirgilStreamSigner())
                using (var source = new VirgilStreamDataSource(inputStream))
                {
                    var signature = signer.Sign(source, privateKey.Get().Value);
                    return signature;
                }
            }
            catch (Exception ex)
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
            catch (Exception ex)
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

            var virgilHashAlg = (VirgilHash.Algorithm)algorithm;
            var hasher = new VirgilHash(virgilHashAlg);
            
            try
            {
                using (hasher)
                {
                    return hasher.Hash(data);
                }
            }
            catch (Exception ex)
            {
                throw new CryptoException(ex.Message);
            }
        }

        /// <summary>
        /// Verifies the specified signature using original stream and signer's Public key.
        /// </summary>
        /// <param name="inputStream">readable stream containing input data.</param>
        /// <param name="publicKey">signer public key for verification.</param>
        /// <param name="signature">signature bytes for verification.</param>
        /// <returns>True if signature is valid, False otherwise.</returns>
        /// <example>
        /// <code>
        ///    var publicKey = crypto.ImportPublicKey(exportedPublicKey);
        ///    using (var inputStream = new FileStream("[YOUR_FILE_PATH_HERE]", FileMode.Open, FileAccess.Read))
        ///    {
        ///       crypto.Verify(inputStream, signature, publicKey);
        ///    }
        /// </code>
        /// </example>
        /// How to get exportedPublicKey <see cref="ExportPublicKey(IPublicKey)"/>     
        public override bool Verify(Stream inputStream, byte[] signature, IPublicKey publicKey)
        {
            if (signature == null)
                throw new ArgumentNullException(nameof(signature));

            try
            {
                using (var streamSigner = new VirgilStreamSigner())
                {
                    var source = new VirgilStreamDataSource(inputStream);
                    var isValid = streamSigner.Verify(source, signature, publicKey.Get().Value);
                    return isValid;
                }
            }
            catch (Exception ex)
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