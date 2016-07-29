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
    using System.Threading.Tasks;

    /// <summary>
    /// The <see cref="VirgilKey"/> object represents an opaque reference to keying material 
    /// that is managed by the user agent.
    /// </summary>
    public sealed class VirgilKey
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="VirgilKey"/> class from being created.
        /// </summary>
        private VirgilKey()
        {
        }

        /// <summary>
        /// Creates an instance of <see cref="VirgilKey"/> object that represents a new named key, 
        /// using default key storage provider.
        /// </summary>
        /// <param name="keyName">The name of the key.</param>
        /// <returns>An instance of <see cref="VirgilKey"/> that represent a newly created key.</returns>
        public static VirgilKey Create(string keyName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates an instance of <see cref="VirgilKey"/> object by specified Public/Private key, 
        /// using default key storage provider.
        /// </summary>
        /// <param name="keyName">The name of the key.</param>
        /// <param name="publicKey">The byte array that represents a Public Key in PEM format.</param>
        /// <param name="privateKey">The byte array that represents a Private Key in PEM format.</param>
        /// <returns>An instance of <see cref="VirgilKey"/> that represent a newly created key.</returns>
        public static VirgilKey Create(string keyName, byte[] publicKey, byte[] privateKey)
        {
            throw new NotImplementedException();
        }

        public static VirgilKey Import(VirgilBuffer keyData)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks 
        /// </summary>
        /// <param name="keyName">Name of the key.</param>
        /// <returns></returns>
        public static bool Exists(string keyName)
        {
            throw new NotImplementedException();
        }

        public static VirgilKey Load(string keyName)
        {
            throw new NotImplementedException();
        }

        public VirgilBuffer Export()
        {
            throw new NotImplementedException();
        }

        public VirgilBuffer ImportForm()
        {
            throw new NotImplementedException();
        }

        public VirgilBuffer Sign(VirgilBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public VirgilBuffer Decrypt(VirgilBuffer ciphertext)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Revokes associated <see cref="VirgilCard"/> from Virgil Cards service.
        /// </summary>
        public Task RevokeAssociatedVirgilCard()
        {
            throw new NotImplementedException();
        }

        public VirgilCardRequest BuildCardRequest(string alice, string name, bool isGlobal)
        {
            throw new NotImplementedException();
        }
    }
}