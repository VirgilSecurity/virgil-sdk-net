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

namespace Virgil.SDK
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    using Virgil.SDK.Cryptography;

    public static partial class VirgilCardExtensions
    {
        public static byte[] EncryptText(this IEnumerable<VirgilCard> cards, string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            return Encrypt(cards, Encoding.UTF8.GetBytes(text));
        }

        public static byte[] EncryptText(this VirgilCard card, string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException(Localization.ExceptionArgumentIsNullOrWhitespace, nameof(text));
            }

            return card.Encrypt(Encoding.UTF8.GetBytes(text));
        }

        public static byte[] Encrypt(this IEnumerable<VirgilCard> cards, byte[] data)
        {
            if (cards == null)
            {
                throw new ArgumentNullException(nameof(cards));
            }

            var recipients = cards.ToList();

            var crypto = VirgilConfig.GetService<Crypto>();
            var publicKeys = recipients.Select(it => it.PublicKey).ToArray();

            var cipherdata = crypto.Encrypt(data, publicKeys);

            return cipherdata;
        }

        public static bool VerifyText(this VirgilCard card, string text, byte[] signature)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException(Localization.ExceptionArgumentIsNullOrWhitespace, nameof(text));
            }

            return Verify(card, Encoding.UTF8.GetBytes(text), signature);
        }

        public static bool Verify(this VirgilCard card, byte[] data, byte[] signature)
        {
            if (card == null)
            {
                throw new ArgumentNullException(nameof(card));
            }

            var encryptor = VirgilConfig.GetService<Crypto>();
            var isValid = encryptor.Verify(data, signature, card.PublicKey);

            return isValid;
        }
    }
}