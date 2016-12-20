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
    using System.Text;

    /// <summary>
    /// The <see cref="VirgilBuffer"/> class provides a list of methods that 
    /// simplify the work with an array of bytes. 
    /// </summary>
    public class VirgilBuffer
    {
        private readonly byte[] bytes;

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilBuffer"/> class.
        /// </summary>
        /// <param name="bytes">The array of bytes.</param>
        public VirgilBuffer(byte[] bytes)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            if (bytes.Length == 0)
                throw new ArgumentException(@"Argument is empty collection", nameof(bytes));

            this.bytes = bytes;
        }

        /// <summary>
        /// Converts all the bytes in current buffer to its equivalent string representation that 
        /// is encoded with base-64 digits.
        /// </summary>
        /// <returns>The string representation of current buffer bytes.</returns>
        public string ToBase64String()
        {
            return Convert.ToBase64String(this.bytes);
        }

        /// <summary>
        /// Decodes all the bytes in current buffer into a string.
        /// </summary>
        /// <returns>A string that contains the results of decoding the specified sequence of bytes.</returns>
        public string ToUTF8String()
        {
            return Encoding.UTF8.GetString(this.bytes);
        }

        /// <summary>
        /// Converts the numeric value of each element of a current buffer bytes to its 
        /// equivalent hexadecimal string representation.
        /// </summary>
        /// <returns>The string representation of current buffer bytes</returns>
        public string ToHEXString()
        {
            var hex = BitConverter.ToString(this.bytes);
            return hex.Replace("-", "").ToLower();
        }

        /// <summary>
        /// Gets an array of bytes.
        /// </summary>
        /// <returns>A byte array</returns>
        public byte[] GetBytes()
        {
            return this.bytes;
        }

        /// <summary>
        /// Initializes a new buffer from specified string, which encodes binary data as base-64 digits.
        /// </summary>
        /// <returns>A new instance of <see cref="VirgilBuffer"/> class.</returns>
        public static VirgilBuffer FromBase64String(string str)
        {
            return new VirgilBuffer(Convert.FromBase64String(str));
        }

        /// <summary>
        /// Initializes a new buffer from specified string, which encodes binary data as utf-8.
        /// </summary>
        /// <returns>A new instance of <see cref="VirgilBuffer"/> class.</returns>
        public static VirgilBuffer FromUTF8String(string str)
        {
            return new VirgilBuffer(Encoding.UTF8.GetBytes(str));
        }

        /// <summary>
        /// Initializes a new buffer from specified string, which encodes binary data as hexadecimal digits.
        /// </summary>
        /// <returns>A new instance of <see cref="VirgilBuffer"/> class.</returns>
        public static VirgilBuffer FromHEXString(string str)
        {
            var numberChars = str.Length;
            var bytes = new byte[numberChars / 2];

            for (var i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(str.Substring(i, 2), 16);
            }

            return new VirgilBuffer(bytes);
        }
    }
}