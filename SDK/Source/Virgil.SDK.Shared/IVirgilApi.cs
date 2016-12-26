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
    /// <summary>
    /// The <see cref="IVirgilApi"/> interface defines a high-level API that provides easy access to 
    /// Virgil Security services and allows to perform cryptographic operations by using two domain entities 
    /// <see cref="VirgilKey"/> and <see cref="VirgilCard"/>. Where the <see cref="VirgilKey"/> is an entity
    /// that represents a user's Private key, and the <see cref="VirgilCard"/> is the entity that represents
    /// user's identity and a Public key.
    /// </summary>
    public interface IVirgilApi
    {
        /// <summary>
        /// Gets an instances of the class that provides a work with <see cref="VirgilKey"/> entities.
        /// </summary>
        IKeysManager Keys { get; }

        /// <summary>
        /// Gets an instances of the class that provides a work with <see cref="VirgilCard"/> entities.
        /// </summary>
        ICardsManager Cards { get; }

        /// <summary>
        /// Encrypts the specified buffer using <paramref name="recipients"/> Cards.
        /// </summary>
        /// <param name="buffer">The buffer to be encrypted.</param>
        /// <param name="recipients">The list of recipients Cards.</param>
        /// <returns>A new <see cref="VirgilBuffer"/> with encrypted data.</returns>
        VirgilBuffer Encrypt(VirgilBuffer buffer, params VirgilCard[] recipients);

        /// <summary>
        /// Decrypts the specified cipher buffer using receiver's <paramref name="receiverKey"/>.
        /// </summary>
        /// <param name="cipherBuffer">The cipher buffer to be decrypted.</param>
        /// <param name="receiverKey">The receiver's <see cref="VirgilKey"/>.</param>
        /// <returns>A new <see cref="VirgilBuffer"/> with decrypted data.</returns>
        VirgilBuffer Decrypt(VirgilBuffer cipherBuffer, VirgilKey receiverKey);

        /// <summary>
        /// Generates a diginat signature of specified <paramref name="buffer"/> using <see cref="signerKey"/>.
        /// </summary>
        /// <param name="buffer">The buffer data of which the signature would be generated.</param>
        /// <param name="signerKey">The signer's <see cref="VirgilKey"/>.</param>
        /// <returns>A new <see cref="VirgilBuffer"/> with generated signature.</returns>
        VirgilBuffer Sign(VirgilBuffer buffer, VirgilKey signerKey);

        /// <summary>
        /// Verifies the specified digital <paramref name="signature" /> using <paramref name="signerCard" /> and
        /// original <paramref name="buffer" /> data.
        /// </summary>
        /// <param name="buffer">The original buffer data for verification the <paramref name="signature" />.</param>
        /// <param name="signature">The digital signature, previously generated with signer's Key and original data.</param>
        /// <param name="signerCard">The signer's <see cref="VirgilCard" />, uses to verify the digital <paramref name="signature" />.</param>
        /// <returns>true, if signature is valid, otherwise, false</returns>
        bool Verify(VirgilBuffer buffer, VirgilBuffer signature, VirgilCard signerCard);

        /// <summary>
        /// Encrypts the specified buffer data for <paramref name="recipients"/> Cards and authenticate 
        /// it using <paramref name="signerKey"/>.
        /// </summary>
        /// <param name="buffer">The buffer data to be encrypted.</param>
        /// <param name="signerKey">The signer <see cref="VirgilKey"/>.</param>
        /// <param name="recipients">The recipients <see cref="VirgilCard"/>s.</param>
        /// <returns>A new <see cref="VirgilBuffer"/> with encrypted data.</returns>
        VirgilBuffer SignThenEncrypt(VirgilBuffer buffer, VirgilKey signerKey, params VirgilCard[] recipients);

        /// <summary>
        /// Decrypts the specified cipher buffer data using <paramref name="receiverKey"/> and verifies a
        /// signature using <paramref name="signerCard"/>. 
        /// </summary>
        /// <param name="cipherBuffer">The cipher buffer data to be decrypted.</param>
        /// <param name="receiverKey">The receiver's <see cref="VirgilKey"/>.</param>
        /// <param name="signerCard">The signer's <see cref="VirgilCard"/>.</param>
        /// <returns>A new <see cref="VirgilBuffer"/> with decrypted data.</returns>
        VirgilBuffer DecryptThenVerify(VirgilBuffer cipherBuffer, VirgilKey receiverKey, VirgilCard signerCard);
    }
}   