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
namespace Virgil.SDK.Web.Authorization
{
    /// <summary>
    /// <see cref="TokenContext"/> provides payload for 
    /// <see cref="CallbackJwtProvider.GetTokenAsync(TokenContext)"/>
    /// </summary>
    public class TokenContext
    {
        /// <summary>
        ///Operation for which token is needed.
        /// </summary>
        public readonly string Operation;

        /// <summary>
        /// Identity that should be used in access token.
        /// </summary>
        public readonly string Identity;

        /// <summary>
        /// You can set up token cache in 
        /// <see cref="CallbackJwtProvider.ObtainAccessTokenFunction"/> and reset cached token if True.
        /// </summary>
        public readonly bool ForceReload;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenContext"/>
        /// </summary>
<<<<<<< HEAD:SDK/Source/Virgil.SDK.Contracts/Storage/IKeyStorage.cs
        /// <param name="keyName">The alias name.</param>
        /// <exception cref="KeyEntryNotFoundException"></exception>
        void Delete(string keyName);

        /// <summary>
        /// Returns the list of keynames
        /// </summary>
        string[] Names();
=======
        /// <param name="identity">Identity to use in token</param>
        /// <param name="operation">Operation for which token is needed</param>
        /// <param name="forceReload">If you set up token cache in
        ///  <see cref="CallbackJwtProvider.ObtainAccessTokenFunction"/>, 
        /// it should reset cached token and return new if TRUE.</param>
        public TokenContext(string identity, string operation, bool forceReload = false)
        {
            Operation = operation;
            Identity = identity;
            ForceReload = forceReload;
        }
>>>>>>> v5:SDK/Source/Virgil.SDK.Shared/Web/Authorization/TokenContext.cs
    }
}
