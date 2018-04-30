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

namespace Virgil.SDK.Common
{
    using System;
    using System.Text;
    using System.Linq;

    public class Bytes
    {
        /// <summary>
        /// Combines several byte arrays into one.
        /// </summary>
        public static byte[] Combine(params byte[][] arrays)
        {
            var rv = new byte[arrays.Sum(a => a.Length)];
            var offset = 0;
            foreach (var array in arrays)
            {
                Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                offset += array.Length;
            }
            return rv;
        }

        /// <summary>
        /// Decodes the current <paramref name="inputBytes"/> to a string according to the specified
        /// character encoding in <paramref name="encoding" />.
        /// </summary>
        /// <param name="inputBytes"></param>
        /// <param name="encoding">The character encoding to decode to.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(byte[] inputBytes, StringEncoding encoding = StringEncoding.UTF8)
        {
            if (inputBytes == null)
            {
                throw new ArgumentNullException(nameof(inputBytes));
            }
            switch (encoding)
            {
                case StringEncoding.BASE64:
                    return Convert.ToBase64String(inputBytes);
                case StringEncoding.HEX:
                    var hex = BitConverter.ToString(inputBytes);
                    return hex.Replace("-", "").ToLower();
                case StringEncoding.UTF8:
                    return Encoding.UTF8.GetString(inputBytes, 0, inputBytes.Length);
                default:
                    throw new ArgumentOutOfRangeException(nameof(encoding), encoding, null);
            }
        }
        
        /// <summary>
        /// Creates a new <see cref="Buffer"/> containing the given string. If provided, the encoding parameter 
        /// identifies the character encoding of string.
        /// </summary>
        /// <param name="str">String to encode.</param>
        /// <param name="encoding">The encoding of string.</param>
        /// <returns></returns>
        public static byte[] FromString(string str, StringEncoding encoding = StringEncoding.UTF8)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            switch (encoding)
            {
                case StringEncoding.BASE64:
                    return Convert.FromBase64String(str);
                case StringEncoding.HEX:
                    return FromHEXString(str);
                case StringEncoding.UTF8:
                    return Encoding.UTF8.GetBytes(str);
                default:
                    throw new ArgumentOutOfRangeException(nameof(encoding), encoding, null);
            }
        }
        
        /// <summary>
        /// Get bytes from specified string, which encodes binary 
        /// data as hexadecimal digits.
        /// </summary>
        private static byte[] FromHEXString(string str)
        {
            var numberChars = str.Length;
            var bytes = new byte[numberChars / 2];

            for (var i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(str.Substring(i, 2), 16);
            }

            return bytes;
        }
    }
}
