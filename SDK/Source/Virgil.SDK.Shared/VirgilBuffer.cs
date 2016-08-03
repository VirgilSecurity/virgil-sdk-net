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
    using System.Text;

    /// <summary>
    /// The <see cref="VirgilBuffer"/> class provide methods to convert data between types.
    /// </summary>
    public class  VirgilBuffer
    {
        private readonly byte[] bytes;

        /// <summary>
        /// Prevents a default instance of the <see cref="VirgilBuffer"/> class from being created.
        /// </summary>
        private VirgilBuffer(byte[] bytes)
        {
            this.bytes = bytes;
        }

        /// <summary>
        /// Converts the buffer to its equivalent string representation that is encoded with base-64 digits.
        /// </summary>
        public string ToBase64String()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts the buffer to its equivalent string representation.
        /// </summary>
        public override string ToString()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts the buffer to its hexadecimal string representation.
        /// </summary>
        public string ToHexString()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts the buffer to byte array.
        /// </summary>
        public byte[] ToBytes()
        {
            return this.bytes;
        }

        /// <summary>
        /// Creates an instance of <see cref="VirgilBuffer"/> from specified string.
        /// </summary>
        public static VirgilBuffer FromString(string plaintext)
        {
            var bytes = Encoding.UTF8.GetBytes(plaintext);
            return new VirgilBuffer(bytes);
        }

        /// <summary>
        /// Creates an instance of <see cref="VirgilBuffer"/> from specified string is encoded with base-64 digits.
        /// </summary>
        public static VirgilBuffer FromBase64String(string base64String)
        {
            var bytes = Convert.FromBase64String(base64String);
            return new VirgilBuffer(bytes);
        }

        /// <summary>
        /// Creates an instance of <see cref="VirgilBuffer"/> from specified string in hexadecimal representation.
        /// </summary>
        public static VirgilBuffer FromHexString(string hexString)
        {
            var length = hexString.Length;
            var bytes = new byte[length / 2];

            for (var index = 0; index < length; index += 2)
            {
                bytes[index / 2] = Convert.ToByte(hexString.Substring(index, 2), 16);
            }

            return new VirgilBuffer(bytes);
        }

        /// <summary>
        /// Creates an instance of <see cref="VirgilBuffer"/> from specified byte array.
        /// </summary>
        public static VirgilBuffer FromBytes(byte[] bytes)
        {
            return new VirgilBuffer(bytes);
        }

        /// <summary>
        /// Opens a binary file, reads the contents of the file into a <see cref="VirgilBuffer"/>, and then closes the file.
        /// </summary>
        /// <param name="path">The file to open for reading.</param>
        /// <returns>
        /// A <see cref="VirgilBuffer"/> containing the contents of the file.
        /// </returns>
        public static VirgilBuffer FromFile(string path)
        {
            var fileBytes = System.IO.File.ReadAllBytes(path);
            var buffer = new VirgilBuffer(fileBytes);

            return buffer;
        }
    }
}