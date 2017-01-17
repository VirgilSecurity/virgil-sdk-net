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
    using Virgil.SDK.Cryptography;

    /// <summary>
    /// Provides credentials for application authentication using AppID and AppKey 
    /// retrieved from development deshboard.
    /// </summary>
    public class AppCredentials : Credentials
    {
		/// <summary>
		/// Gets or sets the application ID that uniquely identifies your application in our services, 
		/// and it is also used to identify the Virgil Card/Public key generated in a pair with <see cref="AppKey"/>.
		/// </summary>
		public string AppId { get; set; }
		
        /// <summary>
        /// Gets or sets the application key that is representing a Private key that is used to perform 
        /// creation and revocation of Virgil Cards (Public key) in Virgil services. Also the <see cref="AppKey"/> can 
        /// be used for cryptographic operations to take part in application logic. 
        /// </summary>
        public VirgilBuffer AppKey { get; set; }

        /// <summary>
        /// Gets or sets the application key password that is used to protect the <see cref="AppKey"/>.
        /// </summary>
        public string AppKeyPassword { get; set; }

        public override IPrivateKey GetAppKey(ICrypto crypto)
        {
            var authorityPrivateKey = crypto.ImportPrivateKey(this.AppKey.GetBytes(), this.AppKeyPassword);
            return authorityPrivateKey;
        }

		public override string GetAppId()
		{
			return this.AppId;
		}
    }
}