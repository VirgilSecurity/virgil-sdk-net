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

namespace Virgil.SDK.Client.Connection
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;

    /// <summary>
    /// A connection for making HTTP requests against URI endpoints for RA api service.
    /// </summary>
    /// <seealso cref="Connection" />
    /// <seealso cref="IConnection" />
    internal class RAServiceConnection : CardsServiceConnection, IConnection
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RAServiceConnection" /> class.
		/// </summary>
		/// <param name="accessToken">Application token</param>
		/// <param name="baseAddress">The base address.</param>
		public RAServiceConnection(string accessToken, Uri baseAddress) : base(accessToken, baseAddress)
		{
            var raErrors = new Dictionary<int, string>
			{
				[30300] = "Development Portal sign was not found inside the meta.signs property",
				[30301] = "Development Portal sign is invalid",
				[30302] = "Identity Validation Token is invalid or has expired",
				[30303] = "Provided Virgil Card was not found or invalid"
			};
            
            foreach(var err in raErrors)
            {
                this.Errors.Add(err.Key, err.Value);
            }
        }

        /// <summary>
        /// Handles public keys service exception responses
        /// </summary>
        /// <param name="message">The http response message.</param>
        protected override void ExceptionHandler(HttpResponseMessage message)
        {
            this.ThrowException(message, (code, msg) => new VirgilClientException(code, msg));
        }
    }
}
