#region Copyright (C) Virgil Security Inc.
// Copyright (C) 2015-2017 Virgil Security Inc.
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

namespace Virgil.SDK.Client.Connection
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;

    /// <summary>
    /// A connection for making HTTP requests against URI endpoints for identity api services.
    /// </summary>
    /// <seealso cref="Connection" />
    /// <seealso cref="IConnection" />
    public class IdentityServiceConnection : Connection, IConnection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityServiceConnection"/> class.
        /// </summary>
        /// <param name="baseAddress">The base address.</param>
        public IdentityServiceConnection(Uri baseAddress) : base(null, baseAddress)
        {
            this.Errors = new Dictionary<int, string>
            {
                [10000] = "Internal application error",
                [40000] = "JSON specified as a request body is invalid",
                [40100] = "Identity type is invalid",
                [40110] = "Identity's ttl is invalid",
                [40120] = "Identity's ctl is invalid",
                [40130] = "Identity's token parameter is missing",
                [40140] = "Identity's token doesn't match parameters",
                [40150] = "Identity's token has expired",
                [40160] = "Identity's token cannot be decrypted",
                [40170] = "Identity's token parameter is invalid",
                [40180] = "Identity is not unconfirmed",
                [40190] = "Hash to be signed parameter is invalid",
                [40200] = "Email identity value validation failed",
                [40210] = "Identity's confirmation code is invalid",
                [40300] = "Application value is invalid",
                [40310] = "Application's signed message is invalid",
                [41000] = "Identity entity was not found",
                [41010] = "Identity's confirmation period has expired"
            };
        }

        /// <summary>
        /// Handles exception responses
        /// </summary>
        /// <param name="message">The http response message.</param>
        protected override void ExceptionHandler(HttpResponseMessage message)
        {
            this.ThrowException(message, (code, msg) => new IdentityServiceException(code, msg));
        }
    }
}