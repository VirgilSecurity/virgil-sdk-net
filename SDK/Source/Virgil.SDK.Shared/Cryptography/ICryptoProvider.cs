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
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="ICryptoProvider"/> interface provides a set of methods for dealing with low-level 
    /// cryptographic primitives and algorithms. 
     /// </summary>
    public interface ICryptoProvider
    {
        /// <summary>
        /// Generates a key object that represents a public and private key pair.
        /// </summary>
        /// <returns>
        /// A new <see cref="KeyPair" /> generated instance.
        /// </returns>
        KeyPair GenerateKeyPair();

        /// <summary>
        /// Generates a key object that represents a public and private key pair.
        /// </summary>
        /// <param name="details">The parameters.</param>
        KeyPair GenerateKeyPair(IKeyPairDetails details);

        /// <summary>
        /// Encrypts the <paramref name="data"/> using specified <paramref name="password"/>.
        /// </summary>
        /// <param name="data">The <paramref name="data"/> to be encrypted.</param>
        /// <param name="password">The password that uses to encrypt specified data.</param>
        /// <remarks>This method encrypts a data that is decrypted using the 
        /// <see><cref>ICryptoServiceProvider.PerformDecryption(byte[], string)</cref></see> method.</remarks>
        /// <returns>A byte array containing the result from performing the operation.</returns>
        byte[] Encrypt(byte[] data, string password);

        /// <summary>
        /// Encrypts the <paramref name="data"/> for the specified dictionary of recipients, 
        /// where Key is recipient ID and Value is Public Key.
        /// </summary>
        /// <param name="data">The data to be encrypted.</param>
        /// <param name="recipients">The dictionary of recipients Public Keys</param>
        /// <remarks>This method encrypts a data that is decrypted using the 
        /// <see><cref>ICryptoServiceProvider.PerformDecryption(byte[], string, byte[], string)</cref></see> method.</remarks>
        /// <returns>A byte array containing the result from performing the operation.</returns>
        byte[] Encrypt(byte[] data, IDictionary<string, byte[]> recipients);

        /// <summary>
        /// Decrypts the cipher data using recipient's <paramref name="password"/>.
        /// </summary>
        /// <param name="cipherData">The cipher data to be decrypted.</param>
        /// <param name="password">The password that was used to encrypt specified cipher data.</param>
        /// <remarks>This method decrypts a cipher data that is encrypted using the 
        /// <see><cref>ICryptoServiceProvider.Encrypt(byte[], string)</cref></see> method.</remarks>
        /// <returns>A byte array containing the result of decrypt operation.</returns>
        byte[] Decrypt(byte[] cipherData, string password);

        /// <summary>
        /// Decrypts the specified cipher data using recipient's identifier and <c>Private key</c>.
        /// </summary>
        /// <param name="cipherData">The cipher data to be decrypted.</param>
        /// <param name="recipientId">The unique ID, that identifies the recipient.</param>
        /// <param name="privateKey">A <see langword="byte"/> array that represents a <c>Private Key</c></param>
        /// <param name="privateKeyPassword">The <c>Private Key</c>'s password</param>
        /// <remarks>This method decrypts a data that is encrypted using the 
        /// <see><cref>ICryptoServiceProvider.Encrypt(byte[], IDictionary&lt;string, byte&gt;)</cref></see> method.</remarks>
        /// <returns>A byte array containing the result from performing the operation.</returns>
        byte[] Decrypt(byte[] cipherData, string recipientId, byte[] privateKey, string privateKeyPassword = null);

        /// <summary>
        /// Performs the signature generation operation with the signer's <c>Private Key</c> and the <paramref name="data"/> to be signed.
        /// </summary>
        /// <param name="data">The data to be signed.</param>
        /// <param name="privateKey">A byte array that represents a <c>Private Key</c></param>
        /// <param name="privateKeyPassword">The <c>Private Key</c>'s password</param>
        /// <returns>A byte array containing the result from performing the operation.</returns>
        byte[] Sign(byte[] data, byte[] privateKey, string privateKeyPassword = null);

        /// <summary>
        /// Performs the signature verification operation with the signer's <c>Public Key</c>.
        /// </summary>
        /// <param name="data">The <paramref name="data"/> that was signed with sender's <c>Private Key</c>.</param>
        /// <param name="signData">The signature to be verified.</param>
        /// <param name="publicKey">The sender's <c>Public Key</c>.</param>
        /// <returns>A value that indicates whether the specified signature is valid.</returns>
        bool Verify(byte[] data, byte[] signData, byte[] publicKey);
    }
}