#region Copyright (C) 2016 Virgil Security Inc.
// Copyright (C) 2016 Virgil Security Inc.
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
    using System;
    using System.Text;

    public static class CryptoExtensions
    {
        public static string EncryptText(this Crypto crypto, string plaintext, params PublicKey[] recipients)
        {
            var data = Encoding.UTF8.GetBytes(plaintext);
            var ciphertext = Convert.ToBase64String(crypto.Encrypt(data, recipients));

            return ciphertext;
        }

        public static string DecryptText(this Crypto crypto, string ciphertext, PrivateKey privateKey)
        {
            var cipherdata = Convert.FromBase64String(ciphertext);
            var plaintext = Encoding.UTF8.GetString(crypto.Decrypt(cipherdata, privateKey));

            return plaintext;
        }

        public static string SignText(this Crypto crypto, string text, PrivateKey privateKey)
        {
            var data = Encoding.UTF8.GetBytes(text);
            var signature = Convert.ToBase64String(crypto.Sign(data, privateKey));

            return signature;
        }

        public static bool VerifyText(this Crypto crypto, string text, string signature, PublicKey signer)
        {
            var data = Encoding.UTF8.GetBytes(text);
            var signatureData = Convert.FromBase64String(signature);

            return crypto.Verify(data, signatureData, signer);
        }
    }
}   