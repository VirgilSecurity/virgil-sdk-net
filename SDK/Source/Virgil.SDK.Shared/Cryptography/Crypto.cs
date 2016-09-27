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

    public abstract class Crypto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Crypto"/> class.
        /// </summary>
        protected internal Crypto()
        {
        }
        
        public abstract KeyPair GenerateKeys();  
        public abstract PrivateKey ImportPrivateKey(byte[] keyData, string password = null);  
        public abstract PublicKey ImportPublicKey(byte[] keyData);
        public abstract byte[] ExportPrivateKey(PrivateKey privateKey, string password = null);
        public abstract byte[] ExportPublicKey(PublicKey publicKey);

        public abstract byte[] Encrypt(byte[] data, params PublicKey[] recipients);
        public abstract void Encrypt(Stream inputStream, Stream outputStream, params PublicKey[] recipients);
        public abstract byte[] Decrypt(byte[] cipherData, PrivateKey privateKey);
        public abstract void Decrypt(Stream inputStream, Stream outputStream, PrivateKey privateKey);
        public abstract byte[] SignThenEncrypt(byte[] data, PrivateKey privateKey, params PublicKey[] recipients);
        public abstract byte[] DecryptThenVerify(byte[] cipherData, PrivateKey privateKey, PublicKey publicKey);

        public abstract bool Verify(byte[] data, byte[] signature, PublicKey signer);
        public abstract bool Verify(Stream inputStream, byte[] signature, PublicKey signer);
        public abstract bool VerifyFingerprint(string fingerprint, byte[] signature, PublicKey signer);
        public abstract byte[] Sign(byte[] data, PrivateKey privateKey);
        public abstract byte[] Sign(Stream inputStream, PrivateKey privateKey);
        public abstract byte[] SignFingerprint(string fingerprint, PrivateKey privateKey);

        public abstract string CalculateFingerprint(byte[] data);

        public abstract byte[] ComputeHash(byte[] data, HashAlgorithm algorithm);   
    }
}