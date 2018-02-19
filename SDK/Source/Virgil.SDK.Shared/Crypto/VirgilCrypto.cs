
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

using System.Linq;
using Virgil.Crypto.Foundation;

namespace Virgil.Crypto
{
    using System;
    using System.IO;
    using System.Text;
    using Virgil.CryptoAPI;

    /// <summary>
    /// The <see cref="VirgilCardCrypto"/> class provides a cryptographic operations in applications, such as hashing, 
    /// signature generation and verification, and encryption and decryption.
    /// </summary>
    public sealed class VirgilCrypto
    {
        private readonly KeyPairType defaultKeyPairType;
        private readonly byte[] CustomParamKeySignature = Encoding.UTF8.GetBytes("VIRGIL-DATA-SIGNATURE");
        private readonly byte[] CustomParamKeySignerId = Encoding.UTF8.GetBytes("VIRGIL-DATA-SIGNER-ID");
        public bool UseSHA256Fingerprints { get; set; }
       
        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilCardCrypto" /> class.
        /// </summary>
        /// <param name="defaultKeyPairType">Default type of the key pair.</param>
        public VirgilCrypto(KeyPairType defaultKeyPairType)
        {
            this.defaultKeyPairType = defaultKeyPairType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilCardCrypto" /> class.
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
        /// <returns>Generated key pair with the specified type.</returns>
        /// <example>
        /// Generated key pair with type EC_SECP256R1.
        ///     <code>
        ///         var crypto = new VirgilCrypto();
        ///         var keyPair = crypto.GenerateKeys(KeyPairType.EC_SECP256R1);
        ///     </code>
        /// </example>
        public KeyPair GenerateKeys(KeyPairType keyPairType)
        {
            try
            {
                using (VirgilKeyPair keyPair = VirgilKeyPair.Generate(VirgilCryptoExtentions.ToVirgilKeyPairType(keyPairType)))
                {
                    byte[] keyPairId = this.ComputePublicKeyHash(keyPair.PublicKey());
                    PrivateKey privateKey = new PrivateKey();
                    privateKey.Id = keyPairId;
                    privateKey.RawKey = VirgilKeyPair.PrivateKeyToDER(keyPair.PrivateKey());

                    PublicKey publicKey = new PublicKey();
                    publicKey.Id = keyPairId;
                    publicKey.RawKey = VirgilKeyPair.PublicKeyToDER(keyPair.PublicKey());

                    return new KeyPair(publicKey, privateKey);
                }
            }
            catch (Exception ex)
            {
                throw new VirgilCryptoException(ex.Message);
            }
        }

        /// <summary>
        /// Generates recommended asymmetric key pair that is comprised of both Public and Private keys.
        /// </summary>
        /// <returns>Generated key pair.</returns>
        /// <example>
        /// Generated key pair with the default type FAST_EC_ED25519.
        ///     <code>
        ///         var crypto = new VirgilCrypto();
        ///         var keyPair = crypto.GenerateKeys();
        ///     </code>
        /// </example>
        public KeyPair GenerateKeys()
        {
            return this.GenerateKeys(this.defaultKeyPairType);
        }

        /// <summary>
        /// Imports the Private key from material representation.
        /// </summary>
        /// <param name="keyBytes">private key material representation bytes.</param>
        /// <param name="password">the password that was used during 
        /// <see cref="ExportPrivateKey(IPrivateKey, string)"/>.</param>
        /// <returns>Imported private key.</returns>
        /// <example>
        ///     <code>
        ///         var crypto = new VirgilCardCrypto();
        ///         var privateKey = crypto.ImportPrivateKey(exportedPrivateKey, "my_password");
        ///     </code>
        /// </example>
        /// <remarks>How to get exportedPrivateKey <see cref="ExportPrivateKey(IPrivateKey, string)"/></remarks>    
        public IPrivateKey ImportPrivateKey(byte[] keyBytes, string password)
        {
            if (keyBytes == null)
                throw new ArgumentNullException("keyBytes");

            try
            {
                byte[] privateKeyBytes = string.IsNullOrEmpty(password)
                    ? keyBytes
                    : VirgilKeyPair.DecryptPrivateKey(keyBytes, Encoding.UTF8.GetBytes(password));

                byte[] publicKey = VirgilKeyPair.ExtractPublicKey(privateKeyBytes, new byte[] { });
                PrivateKey privateKey = new PrivateKey();
                privateKey.Id = this.ComputePublicKeyHash(publicKey);
                privateKey.RawKey = VirgilKeyPair.PrivateKeyToDER(privateKeyBytes);

                return privateKey;
            }
            catch (Exception ex)
            {
                throw new VirgilCryptoException(ex.Message);
            }
        }

        /// <summary>
        /// Imports the Private key from material representation.
        /// </summary>
        /// <param name="keyBytes">private key material representation bytes.</param>
        /// <returns>Imported private key.</returns>
        /// <example>
        ///     <code>
        ///         var crypto = new VirgilCardCrypto();
        ///         var privateKey = crypto.ImportPrivateKey(exportedPrivateKey);
        ///     </code>
        /// </example>
        /// <remarks>How to get exportedPrivateKey <see cref="ExportPrivateKey(IPrivateKey)"/>.</remarks>
        public IPrivateKey ImportPrivateKey(byte[] keyBytes)
        {
            return ImportPrivateKey(keyBytes, null);
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
        /// <remarks>How to get exportedPublicKey <see cref="ExportPublicKey(IPublicKey)"/>.</remarks>
        public IPublicKey ImportPublicKey(byte[] keyData)
        {
            try
            {
                PublicKey publicKey = new PublicKey();
                publicKey.Id = this.ComputePublicKeyHash(keyData);
                publicKey.RawKey = VirgilKeyPair.PublicKeyToDER(keyData);

                return publicKey;
            }
            catch (Exception ex)
            {
                throw new VirgilCryptoException(ex.Message);
            }
        }

        /// <summary>
        /// Exports the Private key into material representation.
        /// </summary>
        /// <param name="privateKey">private key for export.</param>
        /// <param name="password">password that is used for encryption of private key raw bytes.</param>
        /// <returns>Private key material representation bytes.</returns>
        /// <example>
        ///     <code>
        ///         var crypto = new VirgilCrypto();
        ///         var keyPair = crypto.GenerateKeys();
        ///         var crypto = new VirgilCrypto();
        ///         var exportedPrivateKey = crypto.ExportPrivateKey(keyPair.PrivateKey, "my_password");
        ///     </code>
        /// </example>
        /// <remarks>How to import private key <see cref="ImportPrivateKey(byte[], string)"/>.</remarks>
        /// <remarks>How to get generate keys <see cref="VirgilCrypto.GenerateKeys()"/>.</remarks>
        public byte[] ExportPrivateKey(IPrivateKey privateKey, string password)
        {
            if (privateKey == null)
                throw new ArgumentNullException("privateKey");
            try
            {
                if (string.IsNullOrEmpty(password))
                {
                    return VirgilKeyPair.PrivateKeyToDER(VirgilCryptoExtentions.Get(privateKey).RawKey);
                }

                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] encryptedKey = VirgilKeyPair.EncryptPrivateKey(VirgilCryptoExtentions.Get(privateKey).RawKey,
                    passwordBytes);

                return VirgilKeyPair.PrivateKeyToDER(encryptedKey, passwordBytes);
            }
            catch (Exception ex)
            {
                throw new VirgilCryptoException(ex.Message);
            }
        }


        /// <summary>
        /// Exports the Private key into material representation.
        /// </summary>
        /// <param name="privateKey">private key for export.</param>
        /// <returns>Private key material representation bytes.</returns>
        /// <example>
        ///     <code>
        ///         var crypto = new VirgilCrypto();
        ///         var keyPair = crypto.GenerateKeys();
        ///         var crypto = new VirgilCrypto();
        ///         var exportedPrivateKey = crypto.ExportPrivateKey(keyPair.PrivateKey);
        ///     </code>
        /// </example>
        /// <remarks>How to import private key <see cref="ImportPrivateKey(byte[])"/>.</remarks>
        /// <remarks>How to get generate keys <see cref="VirgilCrypto.GenerateKeys()"/>.</remarks>
        public byte[] ExportPrivateKey(IPrivateKey privateKey)
        {
            return ExportPrivateKey(privateKey, null);
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
        /// <remarks>How to import public key <see cref="ImportPublicKey(byte[])"/>.</remarks>
        /// <remarks>How to get generate keys <see cref="GenerateKeys()"/>.</remarks>   
        public byte[] ExportPublicKey(IPublicKey publicKey)
        {
            try
            {
                return VirgilKeyPair.PublicKeyToDER(VirgilCryptoExtentions.Get(publicKey).RawKey);
            }
            catch (Exception ex)
            {
                throw new VirgilCryptoException(ex.Message);
            }
        }

        /// <summary>
        /// Extracts the Public key from the specified <see cref="IPrivateKey"/>
        /// </summary>
        /// <param name="privateKey"> The private key.</param>
        /// <returns>The instance of <see cref="IPublicKey"/></returns>
        public IPublicKey ExtractPublicKey(IPrivateKey privateKey)
        {
            try
            {
                byte[] publicKeyData = VirgilKeyPair.ExtractPublicKey(
                    VirgilCryptoExtentions.Get(privateKey).RawKey, new byte[] { });

                PublicKey publicKey = new PublicKey();
                publicKey.Id = VirgilCryptoExtentions.Get(privateKey).Id;
                publicKey.RawKey = VirgilKeyPair.PublicKeyToDER(publicKeyData);

                return publicKey;
            }
            catch (Exception ex)
            {
                throw new VirgilCryptoException(ex.Message);
            }
        }


        /// <summary>
        /// Encrypts the specified data using the specified recipients Public keys.
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
        /// <remarks>How to decrypt data <see cref="Decrypt(byte[], Virgil.CryptoAPI.IPrivateKey)"/>.</remarks>
        public byte[] Encrypt(byte[] data, params IPublicKey[] recipients)
        {
            try
            {
                using (VirgilCipher cipher = new VirgilCipher())
                {
                    foreach (IPublicKey publicKey in recipients)
                    {
                        cipher.AddKeyRecipient(VirgilCryptoExtentions.Get(publicKey).Id,
                            VirgilCryptoExtentions.Get(publicKey).RawKey);
                    }

                    byte[] encryptedData = cipher.Encrypt(data, true);
                    return encryptedData;
                }
            }
            catch (Exception ex)
            {
                throw new VirgilCryptoException(ex.Message);
            }
        }


        /// <summary>
        /// Decrypts the specified data using the specified Private key.
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
        /// <remarks>How to get encryptedData <see cref="Encrypt(byte[], IPublicKey[])"/>.</remarks>
        public byte[] Decrypt(byte[] cipherData, IPrivateKey privateKey)
        {
            try
            {
                using (VirgilCipher cipher = new VirgilCipher())
                {
                    byte[] data = cipher.DecryptWithKey(cipherData,
                        VirgilCryptoExtentions.Get(privateKey).Id,
                        VirgilCryptoExtentions.Get(privateKey).RawKey);
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw new VirgilCryptoException(ex.Message);
            }
        }


        /// <summary>
        /// Signs the specified data using the specified Private key. 
        /// </summary>
        /// <param name="data">raw data bytes for signing.</param>
        /// <param name="privateKey">private key for signing.</param>
        /// <returns>Signature data.</returns>
        /// <example>
        ///     <code>
        ///         var crypto = new VirgilCrypto();
        ///         var keyPair = crypto.GenerateKeys();
        ///         var data = Encoding.UTF8.GetBytes("Hello Bob!");
        ///         var signature = crypto.GenerateSignature(data, keyPair.PrivateKey);
        ///     </code>
        /// </example>
        /// <remarks>How to verify signature <see cref="VerifySignature(byte[], byte[], IPublicKey)"/>.</remarks>
        public byte[] GenerateSignature(byte[] data, IPrivateKey privateKey)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            if (privateKey == null)
                throw new ArgumentNullException("privateKey");

            try
            {
                using (VirgilSigner signer = new VirgilSigner(VirgilHash.Algorithm.SHA512))
                {
                    byte[] signature = signer.Sign(data, VirgilCryptoExtentions.Get(privateKey).RawKey);
                    return signature;
                }
            }
            catch (Exception ex)
            {
                throw new VirgilCryptoException(ex.Message);
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
        ///         var isSignatureVerified = crypto.VerifySignature(signature, data, publicKey);
        ///     </code>
        /// </example>
        /// <remarks>How to generate signature <see cref="GenerateSignature(byte[], IPrivateKey)"/>.</remarks>
        /// <remarks>How to get exportedPublicKey <see cref="ExportPublicKey(IPublicKey)"/>.</remarks>     
        public bool VerifySignature(byte[] signature, byte[] data, IPublicKey signerKey)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            if (signature == null)
                throw new ArgumentNullException("signature");

            try
            {
                using (VirgilSigner virgilSigner = new VirgilSigner(VirgilHash.Algorithm.SHA512))
                {
                    bool isValid = virgilSigner.Verify(data, signature, VirgilCryptoExtentions.Get(signerKey).RawKey);
                    return isValid;
                }
            }
            catch (Exception ex)
            {
                throw new VirgilCryptoException(ex.Message);
            }
        }

        /// <summary>
        /// Encrypts the specified stream <see cref="inputStream"/> using the specified recipients Public keys
        /// and writes to specified output stream <see cref="cipherStream"/>.
        /// </summary>
        /// <param name="inputStream">readable stream containing input bytes.</param>
        /// <param name="cipherStream">writable stream for output.</param>
        /// <param name="recipients"> list of recipients' public keys.</param>
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
        public void Encrypt(Stream inputStream, Stream cipherStream, params IPublicKey[] recipients)
        {
            try
            {
                using (VirgilChunkCipher cipher = new VirgilChunkCipher())
                using (VirgilStreamDataSource source = new VirgilStreamDataSource(inputStream))
                using (VirgilStreamDataSink sink = new VirgilStreamDataSink(cipherStream))
                {
                    foreach (IPublicKey publicKey in recipients)
                    {
                        cipher.AddKeyRecipient(VirgilCryptoExtentions.Get(publicKey).Id,
                            VirgilCryptoExtentions.Get(publicKey).RawKey);
                    }

                    cipher.Encrypt(source, sink);
                }
            }
            catch (Exception ex)
            {
                throw new VirgilCryptoException(ex.Message);
            }
        }


        /// <summary>
        /// Decrypts the specified stream <see cref="cipherStream"/> using the specified Private key
        /// and writes to specified output stream <see cref="outputStream"/>
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
        /// <remarks>How to get exportedPrivateKey <see cref="ExportPrivateKey(IPrivateKey, string)"/></remarks>   
        public void Decrypt(Stream cipherStream, Stream outputStream, IPrivateKey privateKey)
        {
            try
            {
                using (VirgilChunkCipher cipher = new VirgilChunkCipher())
                using (VirgilStreamDataSource source = new VirgilStreamDataSource(cipherStream))
                using (VirgilStreamDataSink sink = new VirgilStreamDataSink(outputStream))
                {
                    cipher.DecryptWithKey(source, sink, VirgilCryptoExtentions.Get(privateKey).Id,
                        VirgilCryptoExtentions.Get(privateKey).RawKey);
                }
            }
            catch (Exception ex)
            {
                throw new VirgilCryptoException(ex.Message);
            }
        }


        /// <summary>
        /// Signs and encrypts the specified data.
        /// </summary>
        /// <param name="data">The data to encrypt.</param>
        /// <param name="privateKey">The Private key to sign the data.</param>
        /// <param name="recipients">The list of Public key recipients to encrypt the data.</param>
        /// <returns>Signed and encrypted data bytes.</returns>
        /// <exception cref="Virgil.Crypto.VirgilCryptoException"></exception>
        /// <example>
        ///   <code>
        ///     var crypto = new VirgilCrypto();
        ///     var alice = crypto.GenerateKeys();
        ///     var bob = crypto.GenerateKeys();
        ///     var originalData = Encoding.UTF8.GetBytes("Hello Bob, How are you?");
        ///     // The data to be signed with Alice's Private key and then encrypted for Bob.
        ///     var cipherData = crypto.SignThenEncrypt(originalData, alice.PrivateKey, bob.PublicKey);
        ///   </code>
        /// </example>
        public byte[] SignThenEncrypt(byte[] data, IPrivateKey privateKey, params IPublicKey[] recipients)
        {
            try
            {
                using (VirgilSigner signer = new VirgilSigner(VirgilHash.Algorithm.SHA512))
                using (VirgilCipher cipher = new VirgilCipher())
                {
                    byte[] signature = signer.Sign(data, VirgilCryptoExtentions.Get(privateKey).RawKey);

                    VirgilCustomParams customData = cipher.CustomParams();
                    customData.SetData(this.CustomParamKeySignature, signature);

                    IPublicKey publicKey = this.ExtractPublicKey(privateKey);

                    customData.SetData(this.CustomParamKeySignerId, VirgilCryptoExtentions.Get(publicKey).Id);

                    foreach (IPublicKey recipientPublicKey in recipients)
                    {
                        cipher.AddKeyRecipient(VirgilCryptoExtentions.Get(recipientPublicKey).Id,
                            VirgilCryptoExtentions.Get(recipientPublicKey).RawKey);
                    }

                    return cipher.Encrypt(data, true);
                }
            }
            catch (Exception ex)
            {
                throw new VirgilCryptoException(ex.Message);
            }
        }

        /// <summary>
        /// Decrypts and verifies the specified data.
        /// </summary>
        /// <param name="cipherData">The cipher data.</param>
        /// <param name="privateKey">The Private key to decrypt.</param>
        /// <param name="publicKeys"> The list of trusted public keys for verification, 
        /// which can contain signer's public key.</param>
        /// <returns>The decrypted and verified data</returns>
        /// <exception cref="VirgilCryptoException"></exception>
        /// <example>
        ///     <code>
        ///         var crypto = new VirgilCrypto();
        ///         var decryptedData = crypto.DecryptThenVerify(cipherData, bob.PrivateKey, alice.PublicKey);
        ///     </code>
        /// </example>
        /// <remarks>How to get cipherData as well as Alice's and Bob's key pairs
        /// <see cref="SignThenEncrypt(byte[], IPrivateKey, IPublicKey[])"/>.</remarks>
        public byte[] DecryptThenVerify(byte[] cipherData, IPrivateKey privateKey, params IPublicKey[] publicKeys)
        {
            try
            {
                using (VirgilSigner signer = new VirgilSigner(VirgilHash.Algorithm.SHA512))
                using (VirgilCipher cipher = new VirgilCipher())
                {
                    byte[] decryptedData =
                        cipher.DecryptWithKey(cipherData, VirgilCryptoExtentions.Get(privateKey).Id,
                        VirgilCryptoExtentions.Get(privateKey).RawKey);
                    byte[] signature = cipher.CustomParams().GetData(this.CustomParamKeySignature);

                    IPublicKey signerPublicKey = (publicKeys.Length > 0) ? publicKeys[0] : null;
                    if (publicKeys.Length > 1)
                    {
                        byte[] signerId = cipher.CustomParams().GetData(this.CustomParamKeySignerId);
                        signerPublicKey = FindPublicKeyBySignerId(publicKeys, signerId);
                    }

                    bool isValid = signer.Verify(decryptedData, signature, VirgilCryptoExtentions.Get(signerPublicKey).RawKey);
                    if (!isValid)
                        throw new VirgilCryptoException("Signature is not valid.");

                    return decryptedData;
                }
            }
            catch (Exception ex)
            {
                throw new VirgilCryptoException(ex.Message);
            }
        }


        /// <summary>
        /// Signs the specified stream using the specified Private key. 
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
        ///             signature = crypto.GenerateSignature(inputStream, keyPair.PrivateKey);
        ///         }
        ///     </code>
        /// </example>
        /// <remarks>How to verify signature <see cref="VerifySignature(byte[], Stream, IPublicKey)"/>.</remarks>
        public byte[] GenerateSignature(Stream inputStream, IPrivateKey privateKey)
        {
            try
            {
                using (VirgilStreamSigner signer = new VirgilStreamSigner(VirgilHash.Algorithm.SHA512))
                using (VirgilStreamDataSource source = new VirgilStreamDataSource(inputStream))
                {
                    byte[] signature = signer.Sign(source, VirgilCryptoExtentions.Get(privateKey).RawKey);
                    return signature;
                }
            }
            catch (Exception ex)
            {
                throw new VirgilCryptoException(ex.Message);
            }
        }

        /// <summary>
        /// Calculates the fingerprint.
        /// </summary>
        /// <param name="payload"> original data bytes to be hashed.</param>
        /// <returns>The hash value.</returns>
        public byte[] GenerateHash(byte[] payload)
        {
            if (payload == null)
                throw new ArgumentNullException("payload");

            try
            {

                using (VirgilHash sha = new VirgilHash(
                    UseSHA256Fingerprints ? VirgilHash.Algorithm.SHA256 : VirgilHash.Algorithm.SHA512))
                {
                    byte[] hash = sha.Hash(payload);
                    return hash;
                }
            }
            catch (Exception ex)
            {
                throw new VirgilCryptoException(ex.Message);
            }
        }


        /// <summary>
        /// Computes the hash of the specified data and the specified <see cref="HashAlgorithm"/>.
        /// </summary>
        /// <param name="data"> original data bytes to be hashed.</param>
        /// <param name="algorithm"> the hash algorithm.</param>
        /// <returns>The value returned by execution of the hashing algorithm.</returns>
        public byte[] GenerateHash(byte[] data, HashAlgorithm algorithm)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            VirgilHash.Algorithm virgilHashAlg = (VirgilHash.Algorithm)algorithm;
            VirgilHash hasher = new VirgilHash(virgilHashAlg);

            try
            {
                using (hasher)
                {
                    return hasher.Hash(data);
                }
            }
            catch (Exception ex)
            {
                throw new VirgilCryptoException(ex.Message);
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
        /// <remarks>How to get exportedPublicKey <see cref="ExportPublicKey(IPublicKey)"/>.</remarks>
        /// <remarks>How to genrate signature <see cref="GenerateSignature(Stream, IPrivateKey)"/>.</remarks>
        public bool VerifySignature(byte[] signature, Stream inputStream, IPublicKey publicKey)
        {
            if (signature == null)
                throw new ArgumentNullException("signature");

            try
            {
                using (VirgilStreamSigner streamSigner = new VirgilStreamSigner(VirgilHash.Algorithm.SHA512))
                {
                    VirgilStreamDataSource source = new VirgilStreamDataSource(inputStream);
                    bool isValid = streamSigner.Verify(source, signature, VirgilCryptoExtentions.Get(publicKey).RawKey);
                    return isValid;
                }
            }
            catch (Exception ex)
            {
                throw new VirgilCryptoException(ex.Message);
            }
        }

        private byte[] ComputePublicKeyHash(byte[] publicKey)
        {
            byte[] publicKeyDER = VirgilKeyPair.PublicKeyToDER(publicKey);
            var hash = UseSHA256Fingerprints
                ? this.GenerateHash(publicKeyDER, HashAlgorithm.SHA256)
                : this.GenerateHash(publicKeyDER, HashAlgorithm.SHA512).Take(8).ToArray();
            return hash;
        }

        private IPublicKey FindPublicKeyBySignerId(IPublicKey[] publicKeys, byte[] signerId)
        {
            foreach (IPublicKey publicKey in publicKeys)
            {
                if (ByteSequencesEqual(VirgilCryptoExtentions.Get(publicKey).Id, signerId))
                {
                    return publicKey;
                }
            }
            return null;
        }

        private bool ByteSequencesEqual(byte[] sequence1, byte[] sequence2)
        {
            if (sequence1.Length != sequence2.Length)
            {
                return false;
            }
            for (int i = 0; i < sequence1.Length; i++)
            {
                if (sequence1[i] != sequence2[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
