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
    /// The <see cref="ICryptoServiceProvider"/> interface that represents cryptographic operations and key storage.
    /// </summary>
    public interface ICryptoServiceProvider
    {
        /// <summary>
        /// Encrypts data for the specified list of recipients.
        /// </summary>
        /// <param name="data">The data to be encrypted.</param>
        /// <param name="recipients">The recipients.</param>
        /// <returns>The encrypted data.</returns>
        byte[] Encrypt(byte[] data, IEnumerable<PublicKey> recipients);

        /// <summary>
        /// Decrypts data that was previously encrypted <see cref="Encrypt(byte[], IEnumerable{PublicKey})"/> method.
        /// </summary>
        /// <param name="cipherdata">The data to decrypt.</param>
        /// <returns>The decrypted data.</returns>
        byte[] Decrypt(byte[] cipherdata);

        /// <summary>
        /// Computes the hash value of the specified byte array, and signs the resulting hash value.
        /// </summary>
        /// <param name="data">The input data for which to compute the hash.</param>
        /// <returns>The signature for the specified data.</returns>
        byte[] Sign(byte[] data);

        /// <summary>
        /// Verifies that a digital signature is valid by calculating the hash value of the specified data, 
        /// and comparing it to the provided signature.
        /// </summary>
        /// <param name="data">The signed data.</param>
        /// <param name="signature">The signature data to be verified.</param>
        /// <param name="publicKey">The public key.</param>
        /// <returns>true if the signature is valid; otherwise, false.</returns>
        bool Verify(byte[] data, byte[] signature, PublicKey publicKey);
    }
}   