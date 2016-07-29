#region "Copyright (C) 2015 Virgil Security Inc."
/**
 * Copyright (C) 2015 Virgil Security Inc.
 *
 * Lead Maintainer: Virgil Security Inc. <support@virgilsecurity.com>
 *
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are
 * met:
 *
 *     (1) Redistributions of source code must retain the above copyright
 *     notice, this list of conditions and the following disclaimer.
 *
 *     (2) Redistributions in binary form must reproduce the above copyright
 *     notice, this list of conditions and the following disclaimer in
 *     the documentation and/or other materials provided with the
 *     distribution.
 *
 *     (3) Neither the name of the copyright holder nor the names of its
 *     contributors may be used to endorse or promote products derived from
 *     this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE AUTHOR ''AS IS'' AND ANY EXPRESS OR
 * IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT,
 * INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
 * HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
 * STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING
 * IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
 * POSSIBILITY OF SUCH DAMAGE.
 */
#endregion

namespace Virgil.SDK.Cryptography
{
    using System.Collections.Generic;

    /// <summary>
    /// The Virgil Cryptography high level API that provides a cryptographic operations in applications, such as
    /// signature generation and verification, and encryption and decryption.
    /// </summary>
    public sealed class VirgilCrypto
    {
        private static VirgilCrypto crypto;
        private readonly ICryptoProvider provider;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="crypto"/> class.
        /// </summary>
        private VirgilCrypto(ICryptoProvider provider)
        {
            this.provider = provider;
        }

        public static KeyPair GenerateKeypair()
        {
            return crypto.provider.GenerateKeypair();
        }

        public static KeyPair GenerateKeypair(KeyPairType type)
        {
            return crypto.provider.GenerateKeypair(type);
        }

        public static byte[] Encrypt(byte[] data, string password)
        {
            return crypto.provider.Encrypt(data, password);
        }

        public static byte[] Encrypt(byte[] data, IDictionary<string, byte[]> recipients)
        {
            return crypto.provider.Encrypt(data, recipients);
        }

        public static byte[] Decrypt(byte[] cipherData, string password)
        {
            return crypto.provider.Decrypt(cipherData, password);
        }

        public static byte[] Decrypt(byte[] cipherData, string recipientId, byte[] privateKey, string privateKeyPassword = null)
        {
            return crypto.provider.Decrypt(cipherData, recipientId, privateKey, privateKeyPassword);
        }

        public static byte[] Sign(byte[] data, byte[] privateKey, string privateKeyPassword)
        {
            return crypto.provider.Sign(data, privateKey, privateKeyPassword);
        }

        public static bool Verify(byte[] data, byte[] signData, byte[] publicKey)
        {
            return crypto.provider.Verify(data, signData, publicKey);
        }

        public static void SetCryptoProvider(ICryptoProvider provider)
        {
            crypto = new VirgilCrypto(provider);
        }

        public static void SetDefaultCryptoProvider()
        {
            crypto = new VirgilCrypto(new VirgilCryptoProvider());
        }
    }
}