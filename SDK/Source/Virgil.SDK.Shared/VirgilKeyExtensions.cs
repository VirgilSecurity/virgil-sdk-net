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

namespace Virgil.SDK
{
    using System.Collections.Generic;

    /// <summary>
    /// Provides useful extension methods for <see cref="VirgilCard"/> class.
    /// </summary>
    public static class VirgilKeyExtensions
    {
        /// <summary>
        /// Signs a byte array data using current <see cref="VirgilKey"/> and then encrypt it 
        /// using multiple recipient's <see cref="VirgilCard"/>s.
        /// </summary>
        /// <param name="virgilKey">The <see cref="VirgilKey"/> used to sign the <paramref name="data"/>.</param>
        /// <param name="data">The data to be encrypted.</param>
        /// <param name="recipients">A list of recipient's <see cref="VirgilCard"/>s used to 
        /// encrypt the <paramref name="data"/>.</param>
        /// <returns>A new <see cref="VirgilBuffer"/> instance with encrypted data.</returns>
        public static VirgilBuffer SignThenEncrypt(this VirgilKey virgilKey, byte[] data, IEnumerable<VirgilCard> recipients)
        {
            return virgilKey.SignThenEncrypt(VirgilBuffer.From(data), recipients);
        }

        /// <summary>
        /// Signs a plaintext using current <see cref="VirgilKey"/> and then encrypt it 
        /// using multiple recipient's <see cref="VirgilCard"/>s.
        /// </summary>
        /// <param name="virgilKey">The <see cref="VirgilKey"/> used to sign the <paramref name="plaintext"/>.</param>
        /// <param name="plaintext">The plaintext to be encrypted.</param>
        /// <param name="recipients">A list of recipient's <see cref="VirgilCard"/>s used to 
        /// encrypt the <paramref name="plaintext"/>.</param>
        /// <returns>A new <see cref="VirgilBuffer"/> instance with encrypted data.</returns>
        public static VirgilBuffer SignThenEncrypt(this VirgilKey virgilKey, string plaintext, IEnumerable<VirgilCard> recipients)
        {
            return virgilKey.SignThenEncrypt(VirgilBuffer.From(plaintext), recipients);
        }

        /// <summary>
        /// Signs a byte array data using current <see cref="VirgilKey"/> and then encrypt it 
        /// using recipient's <see cref="VirgilCard"/>.
        /// </summary>
        /// <param name="virgilKey">The <see cref="VirgilKey"/> used to sign the <paramref name="data"/>.</param>
        /// <param name="data">The plaintext to be encrypted.</param>
        /// <param name="recipient">The recipient's <see cref="VirgilCard"/> used to 
        /// encrypt the <paramref name="data"/>.</param>
        /// <returns>A new <see cref="VirgilBuffer"/> instance with encrypted data.</returns>
        public static VirgilBuffer SignThenEncrypt(this VirgilKey virgilKey, byte[] data, VirgilCard recipient)
        {
            return virgilKey.SignThenEncrypt(VirgilBuffer.From(data), new[] { recipient });
        }

        /// <summary>
        /// Signs the plaintext using current <see cref="VirgilKey"/> and then encrypt it 
        /// using recipient's <see cref="VirgilCard"/>.
        /// </summary>
        /// <param name="virgilKey">The <see cref="VirgilKey"/> used to sign the <paramref name="plaintext"/>.</param>
        /// <param name="plaintext">The plaintext to be encrypted.</param>
        /// <param name="recipient">The recipient's <see cref="VirgilCard"/> used to 
        /// encrypt the <paramref name="plaintext"/>.</param>
        /// <returns>A new <see cref="VirgilBuffer"/> instance with encrypted data.</returns>
        public static VirgilBuffer SignThenEncrypt(this VirgilKey virgilKey, string plaintext, VirgilCard recipient)
        {
            return virgilKey.SignThenEncrypt(VirgilBuffer.From(plaintext), new []{ recipient });
        }

        /// <summary>
        /// Decrypts a ciphertext using current <see cref="VirgilKey"/> and verifies one 
        /// using specified <see cref="VirgilCard"/>.
        /// </summary>
        /// <param name="virgilKey">The <see cref="VirgilKey"/>, that represents a Private key.</param>
        /// <param name="cipherdata">The ciphertext in base64 encoded string.</param>
        /// <param name="signerCard">The signer's <see cref="VirgilCard"/>, that represents a 
        /// Public key and user/device information.</param>
        /// <returns>A new <see cref="VirgilBuffer"/> instance with decrypted data.</returns>
        public static VirgilBuffer DecryptThenVerify(this VirgilKey virgilKey, byte[] cipherdata, VirgilCard signerCard)
        {
            return virgilKey.DecryptThenVerify(new VirgilBuffer(cipherdata), signerCard);
        }

        /// <summary>
        /// Decrypts a ciphertext using current <see cref="VirgilKey"/> and verifies one 
        /// using specified <see cref="VirgilCard"/>.
        /// </summary>
        /// <param name="virgilKey">The <see cref="VirgilKey"/>, that represents a Private key.</param>
        /// <param name="ciphertext">The ciphertext in base64 encoded string.</param>
        /// <param name="signerCard">The signer's <see cref="VirgilCard"/>, that represents a 
        /// Public key and user/device information.</param>
        /// <returns>A new <see cref="VirgilBuffer"/> instance with decrypted data.</returns>
        public static VirgilBuffer DecryptThenVerify(this VirgilKey virgilKey, string ciphertext, VirgilCard signerCard)
        {
            return virgilKey.DecryptThenVerify(VirgilBuffer.From(ciphertext, StringEncoding.Base64), signerCard);
        }

        /// <summary>
        /// Signs a plaintext using current <see cref="VirgilKey"/>.
        /// </summary>
        /// <param name="virgilKey">The <see cref="VirgilKey"/> used to sign the <paramref name="plaintext"/></param>
        /// <param name="plaintext">The plaintext to be signed.</param>
        /// <returns>A new <see cref="VirgilBuffer"/> instance with generated signature.</returns>
        public static VirgilBuffer Sign(this VirgilKey virgilKey, string plaintext)
        {
            return virgilKey.Sign(VirgilBuffer.From(plaintext));
        }

        /// <summary>
        /// Signs a byte array data using current <see cref="VirgilKey"/>.
        /// </summary>
        /// <param name="virgilKey">The <see cref="VirgilKey"/> used to sign the <paramref name="data"/></param>
        /// <param name="data">The plaintext to be signed.</param>
        /// <returns>A new <see cref="VirgilBuffer"/> instance with generated signature.</returns>
        public static VirgilBuffer Sign(this VirgilKey virgilKey, byte[] data)
        {
            return virgilKey.Sign(VirgilBuffer.From(data));
        }
    }   
}