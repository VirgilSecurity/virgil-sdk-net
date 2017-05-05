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

namespace Virgil.SDK.Cryptography
{
    using System.IO;

    /// <summary>
    /// The <see cref="Crypto"/> interface provides a set of methods for dealing 
    /// with low-level cryptographic primitives and algorithms.
    /// </summary>
    public interface ICrypto
    {
        Fingerprint CalculateFingerprint(byte[] data);
        byte[] ComputeHash(byte[] data, HashAlgorithm algorithm);
        byte[] Decrypt(byte[] cipherData, IPrivateKey privateKey);
        void Decrypt(Stream inputStream, Stream outputStream, IPrivateKey privateKey);
        byte[] DecryptThenVerify(byte[] cipherData, IPrivateKey privateKey, params IPublicKey[] recipients);
        byte[] Encrypt(byte[] data, params IPublicKey[] recipients);
        void Encrypt(Stream inputStream, Stream outputStream, params IPublicKey[] recipients);
        byte[] ExportPrivateKey(IPrivateKey privateKey, string password = null);
        byte[] ExportPublicKey(IPublicKey publicKey);
        IPublicKey ExtractPublicKey(IPrivateKey privateKey);
        KeyPair GenerateKeys();
        IPrivateKey ImportPrivateKey(byte[] keyData, string password = null);
        IPublicKey ImportPublicKey(byte[] keyData);
        byte[] Sign(Stream inputStream, IPrivateKey privateKey);
        byte[] Sign(byte[] data, IPrivateKey privateKey);
        byte[] SignThenEncrypt(byte[] data, IPrivateKey privateKey, params IPublicKey[] recipients);
        bool Verify(Stream inputStream, byte[] signature, IPublicKey signerKey);
        bool Verify(byte[] data, byte[] signature, IPublicKey signerKey);
    }
}