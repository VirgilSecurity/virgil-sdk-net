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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Provides useful extension methods for <see cref="VirgilCard"/> class.
    /// </summary>
    public static class VirgilCardExtensions
    {
        /// <summary>
        /// Encrypts the specified buffer data for list of recipients Cards.
        /// </summary>
        /// <param name="task">The list of <see cref="VirgilCard"/> recipients.</param>
        /// <param name="buffer">The buffer data to be encrypted.</param>
        /// <returns>A new <see cref="VirgilBuffer"/> with encrypted data.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Task<VirgilBuffer> Encrypt(this Task<IEnumerable<VirgilCard>> task, VirgilBuffer buffer)
        {
            return task.ContinueWith(t => t.Result.Encrypt(buffer));
        }

        /// <summary>
        /// Encrypts the specified plaintext for list of Cards recipients.
        /// </summary>
        /// <param name="task">The list of <see cref="VirgilCard"/> recipients.</param>
        /// <param name="plaintext">The plaintext to be encrypted.</param>
        /// <returns>A new <see cref="VirgilBuffer"/> with encrypted data.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Task<VirgilBuffer> Encrypt(this Task<IEnumerable<VirgilCard>> task, string plaintext)
        {
            return Encrypt(task, VirgilBuffer.From(plaintext));
        }

        /// <summary>
        /// Encrypts the specified buffer data for list of recipients Cards.
        /// </summary>
        /// <param name="task">The list of <see cref="VirgilCard"/> recipients.</param>
        /// <param name="data">The byte array data to be encrypted.</param>
        /// <returns>A new <see cref="VirgilBuffer"/> with encrypted data.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Task<VirgilBuffer> Encrypt(this Task<IEnumerable<VirgilCard>> task, byte[] data)
        {
            return Encrypt(task, VirgilBuffer.From(data));
        }

        public static Task<VirgilBuffer> Encrypt(this Task<VirgilCard> task, VirgilBuffer buffer)
        {
            return task.ContinueWith(t => t.Result.Encrypt(buffer));
        }

        public static Task<VirgilBuffer> Encrypt(this Task<VirgilCard> task, string plaintext)
        {
            return Encrypt(task, VirgilBuffer.From(plaintext));
        }

        public static Task<VirgilBuffer> Encrypt(this Task<VirgilCard> task, byte[] data)
        {
            return Encrypt(task, VirgilBuffer.From(data));
        }
       
        /// <summary>
        /// Encrypts the specified text for list of <paramref name="recipients"/> Cards.
        /// </summary>
        /// <param name="recipients">The list of <see cref="VirgilCard"/> recipients.</param>
        /// <param name="plaintext">The buffer data to be encrypted.</param>
        /// <returns>A new <see cref="VirgilBuffer"/> with encrypted data.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static VirgilBuffer Encrypt(this IEnumerable<VirgilCard> recipients, string plaintext)
        {
            if (string.IsNullOrWhiteSpace(plaintext))
                throw new ArgumentException(Localization.ExceptionArgumentIsNullOrWhitespace, nameof(plaintext));

            return Encrypt(recipients, VirgilBuffer.From(plaintext));
        }

        /// <summary>
        /// Encrypts the specified bytes for list of <paramref name="recipients"/> Cards.
        /// </summary>
        /// <param name="recipients">The list of <see cref="VirgilCard"/> recipients.</param>
        /// <param name="bytes">The buffer data to be encrypted.</param>
        /// <returns>A new <see cref="VirgilBuffer"/> with encrypted data.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static VirgilBuffer Encrypt(this IEnumerable<VirgilCard> recipients, byte[] bytes)
        {
            if (recipients == null)
                throw new ArgumentNullException(nameof(recipients));

            return Encrypt(recipients, new VirgilBuffer(bytes));
        }

        /// <summary>
        /// Encrypts the specified buffer data for list of <paramref name="recipients"/> Cards.
        /// </summary>
        /// <param name="recipients">The list of <see cref="VirgilCard"/> recipients.</param>
        /// <param name="buffer">The buffer data to be encrypted.</param>
        /// <returns>A new <see cref="VirgilBuffer"/> with encrypted data.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static VirgilBuffer Encrypt(this IEnumerable<VirgilCard> recipients, VirgilBuffer buffer)
        {
            if (recipients == null)
                throw new ArgumentNullException(nameof(recipients));

            var virgilCards = recipients.ToList();
            return virgilCards.First().Encrypt(buffer, virgilCards);
        }

        /// <summary>
        /// Encrypts the specified text for <paramref name="recipient"/> Card.
        /// </summary>
        /// <param name="recipient">The list of <see cref="VirgilCard"/> recipients.</param>
        /// <param name="plaintext">The plaintext to be encrypted.</param>
        /// <returns>A new <see cref="VirgilBuffer"/> with encrypted data.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static VirgilBuffer Encrypt(this VirgilCard recipient, string plaintext)
        {
            if (string.IsNullOrWhiteSpace(plaintext))
                throw new ArgumentException(Localization.ExceptionArgumentIsNullOrWhitespace, nameof(plaintext));

            return recipient.Encrypt(VirgilBuffer.From(plaintext));
        }

        /// <summary>
        /// Encrypts the specified text for <paramref name="recipient"/> Card.
        /// </summary>
        /// <param name="recipient">The list of <see cref="VirgilCard"/> recipients.</param>
        /// <param name="bytes">The byte array to be encrypted.</param>
        /// <returns>A new <see cref="VirgilBuffer"/> with encrypted data.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static VirgilBuffer Encrypt(this VirgilCard recipient, byte[] bytes)
        {
            return recipient.Encrypt(new VirgilBuffer(bytes));
        }

        /// <summary>
        /// Verifies that a digital signature is valid for specified text.
        /// </summary>
        /// <param name="recipient">The <see cref="VirgilCard"/> recipient.</param>
        /// <param name="text">The text.</param>
        /// <param name="signature">The signature.</param>
        /// <returns><c>true</c> if the signature is valid; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static bool Verify(this VirgilCard recipient, string text, VirgilBuffer signature)
        {
            return Verify(recipient, VirgilBuffer.From(text), signature);
        }

        /// <summary>
        /// Verifies that a digital signature is valid for specified text.
        /// </summary>
        /// <param name="recipient">The <see cref="VirgilCard"/> recipient.</param>
        /// <param name="data">The data to be signed.</param>
        /// <param name="signature">The signature.</param>
        /// <returns><c>true</c> if the signature is valid; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static bool Verify(this VirgilCard recipient, byte[] data, VirgilBuffer signature)
        {
            return Verify(recipient, VirgilBuffer.From(data), signature);
        }

        /// <summary>
        /// Verifies that a digital signature is valid for specified text.
        /// </summary>
        /// <param name="recipient">The <see cref="VirgilCard"/> recipient.</param>
        /// <param name="buffer">The text.</param>
        /// <param name="signature">The signature.</param>
        /// <returns><c>true</c> if the signature is valid; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static bool Verify(this VirgilCard recipient, VirgilBuffer buffer, VirgilBuffer signature)
        {
            return recipient.Verify(buffer, signature);
        }
    }
}