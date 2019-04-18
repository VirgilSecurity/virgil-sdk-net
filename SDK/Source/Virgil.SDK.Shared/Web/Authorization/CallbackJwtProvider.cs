﻿#region Copyright (C) Virgil Security Inc.
// Copyright (C) 2015-2019 Virgil Security Inc.
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

namespace Virgil.SDK.Web.Authorization
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// The <see cref="CallbackJwtProvider"/> class provides an opportunity to  
    /// get access token using callback mechanism.
    /// </summary>
    public class CallbackJwtProvider : IAccessTokenProvider
    {
        /// <summary>
        ///  Callback, that takes an instance of
        /// <see cref="TokenContext"/> and returns string representation of 
        /// generated instance of <see cref="IAccessToken"/>>.
        /// </summary>
        public readonly Func<TokenContext, Task<string>> ObtainAccessTokenFunction;

        /// <summary>
        /// Initializes a new instance of the <see cref="CallbackJwtProvider" /> class.
        /// </summary>
        /// <param name="obtainTokenFunc"> async function, that takes an instance of 
        /// <see cref="TokenContext"/> and returns string representation of 
        /// generated instance of <see cref="IAccessToken"/>>.</param>
        public CallbackJwtProvider(Func<TokenContext, Task<string>> obtainTokenFunc)
        {
            this.ObtainAccessTokenFunction = obtainTokenFunc 
                ?? throw new ArgumentNullException(nameof(obtainTokenFunc));
        }

        /// <summary>
        /// Gets access token.
        /// </summary>
        /// <param name="context">The instance of <see cref="TokenContext"/>. </param>
        /// <returns>The instance of <see cref="IAccessToken"/>.</returns>
        public async Task<IAccessToken> GetTokenAsync(TokenContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var jwt = await this.ObtainAccessTokenFunction.Invoke(context);
            return new Jwt(jwt);
        }
    }
}
