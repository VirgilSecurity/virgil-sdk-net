#region Copyright (C) Virgil Security Inc.
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

namespace Virgil.SDK
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Virgil.SDK.Web.Authorization;

    /// <summary>
    /// The <see cref="GeneratorJwtProvider"/> class provides an opportunity to  
    /// generate <see cref="IAccessToken" /> using provided <see cref="Web.Authorization.JwtGenerator" />. 
    /// </summary>
    public class GeneratorJwtProvider : IAccessTokenProvider
    {
        public string DefaultIdentity { get; private set; }
        public Dictionary<object, object> AdditionalData;
        public JwtGenerator JwtGenerator { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneratorJwtProvider" /> class.
        /// </summary>
        /// <param name="jwtGenerator">will generate new JWT.</param>
        /// <param name="defaultIdentity">identity which will be used in token
        ///  generation by default.</param>
        /// <param name="additionalData">dictionary with additional data which will
        ///  be used in token generation.</param>
        public GeneratorJwtProvider(
            JwtGenerator jwtGenerator, 
            string defaultIdentity, 
            Dictionary<object, object> additionalData = null)
        {
            if (string.IsNullOrWhiteSpace(defaultIdentity))
            {
                throw new ArgumentException(nameof(defaultIdentity));
            }
            this.DefaultIdentity = defaultIdentity;
            this.JwtGenerator = jwtGenerator ?? throw new ArgumentNullException(nameof(jwtGenerator));
            this.AdditionalData = additionalData;
        }

        /// <summary>
        /// Generates new JWT using specified identity from content or default identity and additional data.
        /// </summary>
        /// <param name="context">includes an identity to generate with.</param>
        /// <returns>a new instanse of <see cref="IAccessToken"/>.</returns>
        public Task<IAccessToken> GetTokenAsync(TokenContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            var token = this.JwtGenerator.GenerateToken(context.Identity ?? this.DefaultIdentity, AdditionalData);
            return Task.FromResult((IAccessToken)token);
        }
    }
}
