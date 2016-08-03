#region "Copyright (C) 2015 Virgil Security Inc."
// Copyright (C) 2015 Virgil Security Inc.
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
//   contributors may be used to endorse or promote products derived from
//   this software without specific prior written permission.
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
    using Virgil.SDK.Cryptography;

    /// <summary>
    /// The default implementation of <see cref="ICryptoKeyContainer"/> interface which provides a 
    /// cryptographic operations and key storage.
    /// </summary>
    public class VirgilKeyContainer : ICryptoKeyContainer
    {
        /// <summary>
        /// Initializes an existing container.
        /// </summary>
        /// <param name="details">The <see cref="VirgilKey"/> details.</param>
        public void InitializeExisting(IKeyPairDetails details)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Initializes a new container for <see cref="VirgilKey"/>. 
        /// The <see cref="KeyPair"/> will be generated and storage in protected storage.
        /// </summary>
        /// <param name="details">The <see cref="VirgilKey"/> details.</param>
        public void InitializeNew(IKeyPairDetails details)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Performs the decryption for specified <paramref name="cipherdata" />.
        /// </summary>
        /// <param name="cipherdata">The encrypted data to be decrypted.</param>
        /// <returns>A byte array containing the result from decrypt operation.</returns>
        public byte[] PerformDecryption(byte[] cipherdata)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Performs the signature generation for specified <paramref name="data" />.
        /// </summary>
        /// <param name="data">The data to be signed.</param>
        /// <returns>A byte array containing the result from sign operation.</returns>
        public byte[] PerformSignatureGeneration(byte[] data)   
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Release resources and delete the key from the storage.
        /// </summary>
        public void Remove()
        {
            throw new System.NotImplementedException();
        }
    }
}