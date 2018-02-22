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
    ///  The <see cref="IPrivateKeyExporter"/> provides interface 
    /// to export <see cref="IPrivateKey"/> into its material representation bytes and 
    /// import <see cref="IPrivateKey"/> from its material representation bytes.
    /// </summary>
    public interface IPrivateKeyExporter
    {
        /// <summary>
        /// Exports the provided <see cref="IPrivateKey"/> into material representation bytes.
        /// </summary>
        /// <param name="privateKey">The private key.</param>
        /// <returns>The private key material representation bytes.</returns>
        byte[] ExportPrivatekey(IPrivateKey privateKey);


        /// <summary>
        /// Imports the private key from its material representation.
        /// </summary>
        /// <param name="privateKeyBytes">The private key material representation bytes.</param>
        /// <returns>The instance of <see cref="IPrivateKey"/> imported 
        /// from <paramref name="privateKeyBytes"/>.</returns>
        IPrivateKey ImportPrivateKey(byte[] privateKeyBytes);
    }
}
