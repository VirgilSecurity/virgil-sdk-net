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

namespace Virgil.CryptoAPI
{
    /// <summary>
    ///  The <see cref="IAccessTokenSigner"/> provides interface 
    /// to sign access token and verify its signature using <see cref="IPrivateKey"/> and <see cref="IPublicKey"/>.
    /// </summary>
    public interface IAccessTokenSigner
    {
        /// <summary>
        /// Represents used signature algorithm.
        /// </summary>
        /// <returns></returns>
        string GetAlgorithm();

        /// <summary>
        /// Generates the digital signature for the specified <paramref name="tokenBytes"/> using
        /// the specified <see cref="IPrivateKey"/>
        /// </summary>
        /// <param name="tokenBytes">The material representation bytes of access token 
        /// for which to compute the signature.</param>
        /// <param name="privateKey">The private key</param>
        /// <returns>The digital signature for the material representation bytes of access token.</returns>
        byte[] GenerateTokenSignature(byte[] tokenBytes, IPrivateKey privateKey);

        /// <summary>
        /// Verifies that a digital signature is valid by checking the <paramref name="signature"/> and
        /// provided <see cref="IPublicKey"/> and <paramref name="tokenBytes"/>.
        /// </summary>
        /// <param name="tokenBytes">The material representation bytes of access token 
        /// for which the <paramref name="signature"/> has been generated.</param>
        /// <param name="signature">The digital signature for the <paramref name="tokenBytes"/></param>
        /// <param name="publicKey">The public key</param>
        /// <returns>True if signature is valid, False otherwise.</returns>
        bool VerifyTokenSignature(byte[] signature, byte[] tokenBytes, IPublicKey publicKey);
    }
}