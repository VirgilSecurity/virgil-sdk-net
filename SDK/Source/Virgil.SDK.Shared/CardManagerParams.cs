﻿#region Copyright (C) Virgil Security Inc.
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

namespace Virgil.SDK
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Virgil.SDK.Web;
    using Virgil.SDK.Web.Authorization;
    using Virgil.CryptoAPI;
    using Verification;

    /// <summary>
    /// The <see cref="CardManagerParams"/> contains parameters 
    /// required for initializing <see cref="CardManager"/>
    /// </summary>
    public class CardManagerParams
    {
        /// <summary>
        /// an instance of <see cref="ICardCrypto"/> which provides cryptographic operations.
        /// </summary>
        public ICardCrypto CardCrypto { get; set; }

        /// <summary>
        /// an instance of <see cref="ICardVerifier"/> which provides card verification.
        /// </summary>
        public ICardVerifier Verifier { get; set; }

        /// <summary>
        /// CallBack which performs additional signatures for card before publishing.
        /// </summary>
        public Func<RawSignedModel, Task<RawSignedModel>> SignCallBack { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ApiUrl { get; set; }

        /// <summary>
        /// an instance of <see cref="IAccessTokenProvider"/> which provides token delivery.
        /// </summary>
        public IAccessTokenProvider AccessTokenProvider { get; set; }

        /// <summary>
        /// If true <see cref="CardManager"/> repeats once a request to the <see cref="CardClient"/> after
        ///  getting <see cref="UnauthorizedClientException"/>.
        /// </summary>
        public bool RetryOnUnauthorized { get; set; }
    }
}