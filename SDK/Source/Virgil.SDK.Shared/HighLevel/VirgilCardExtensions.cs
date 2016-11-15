#region "Copyright (C) 2016 Virgil Security Inc."
/**
 * Copyright (C) 2016 Virgil Security Inc.
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

namespace Virgil.SDK.HighLevel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    using Virgil.SDK.Cryptography;

    /// <summary>
    /// Provides useful extension methods for <see cref="VirgilCard"/> class.
    /// </summary>
    public static partial class VirgilCardExtensions
    {
        /// <summary>
        /// Encrypts the text.
        /// </summary>
        /// <param name="recipients">The list of <see cref="VirgilCard"/> recipients.</param>
        /// <param name="text">The text to encrypt.</param>
        /// <returns>The encrypted data.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static byte[] EncryptText(this IEnumerable<VirgilCard> recipients, string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException(Localization.ExceptionArgumentIsNullOrWhitespace, nameof(text));

            return Encrypt(recipients, Encoding.UTF8.GetBytes(text));
        }

        /// <summary>
        /// Encrypts the text.
        /// </summary>
        /// <param name="recipient">The <see cref="VirgilCard"/> recipient.</param>
        /// <param name="text">The text.</param>
        /// <returns>The encrypted data</returns>
        /// <exception cref="ArgumentException"></exception>
        public static byte[] EncryptText(this VirgilCard recipient, string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException(Localization.ExceptionArgumentIsNullOrWhitespace, nameof(text));
           
            return recipient.Encrypt(Encoding.UTF8.GetBytes(text));
        }

        /// <summary>
        /// Encrypts the data.
        /// </summary>
        /// <param name="recipients">The list of <see cref="VirgilCard"/> recipients.</param>
        /// <param name="data">The data to encrypt.</param>
        /// <returns>The encrypted data</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static byte[] Encrypt(this IEnumerable<VirgilCard> recipients, byte[] data)
        {
            if (recipients == null)
                throw new ArgumentNullException(nameof(recipients));

            var crypto = VirgilConfig.GetService<Crypto>();
            var publicKeys = recipients.Select(p => crypto.ImportPublicKey(p.PublicKey)).ToArray();
            
            var cipherdata = crypto.Encrypt(data, publicKeys);

            return cipherdata;
        }

        /// <summary>
        /// Verifies that a digital signature is valid for specified text.
        /// </summary>
        /// <param name="recipient">The <see cref="VirgilCard"/> recipient.</param>
        /// <param name="text">The text.</param>
        /// <param name="signature">The signature.</param>
        /// <returns><c>true</c> if the signature is valid; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static bool VerifyText(this VirgilCard recipient, string text, byte[] signature)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException(Localization.ExceptionArgumentIsNullOrWhitespace, nameof(text));
        
            return Verify(recipient, Encoding.UTF8.GetBytes(text), signature);
        }

        /// <summary>
        /// Verifies that a digital signature is valid for specified text.
        /// </summary>
        /// <param name="recipient">The <see cref="VirgilCard"/> recipient.</param>
        /// <param name="data">The text.</param>
        /// <param name="signature">The signature.</param>
        /// <returns><c>true</c> if the signature is valid; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static bool Verify(this VirgilCard recipient, byte[] data, byte[] signature)
        {
            if (recipient == null)
                throw new ArgumentNullException(nameof(recipient));
          
            var crypto = VirgilConfig.GetService<Crypto>();
            var publicKey = crypto.ImportPublicKey(recipient.PublicKey);
            var isValid = crypto.Verify(data, signature, publicKey);

            return isValid;
        }
    }
}