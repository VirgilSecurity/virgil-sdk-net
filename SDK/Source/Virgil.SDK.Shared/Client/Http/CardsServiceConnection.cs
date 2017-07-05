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

namespace Virgil.SDK.Client.Http
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;

    using Virgil.SDK.Exceptions;

    /// <summary>
    /// A connection for making HTTP requests against URI endpoints for public api services.
    /// </summary>
    /// <seealso cref="ConnectionBase" />
    /// <seealso cref="IConnection" />
    internal class CardsServiceConnection : ConnectionBase, IConnection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CardsServiceConnection" /> class.
        /// </summary>
        /// <param name="accessToken">Application token</param>
        /// <param name="baseAddress">The base address.</param>
        public CardsServiceConnection(string accessToken, Uri baseAddress) : base(accessToken, baseAddress)
        {
            this.Errors = new Dictionary<int, string>
            {
                [10000] = "Internal application error. You know, shit happens, so do internal server errors.Just take a deep breath and try harder.",
                [20300] = "The Virgil access token was not specified or is invalid",
                [20301] = "The Virgil authenticator service responded with an error",
                [20302] = "The Virgil access token validation has failed on the Virgil Authenticator service",
                [20303] = "The application was not found for the acsses token",
                [20400] = "Request sign is invalid",
                [20401] = "Request sign header is missing",
                [20500] = "The Virgil Card is not available in this application",
                [30000] = "JSON specified as a request is invalid",
                [30010] = "A data inconsistency error",
                [30100] = "Global Virgil Card identity type is invalid, because it can be only an 'email'",
                [30101] = "Virgil Card scope must be either 'global' or 'application'",
                [30102] = "Virgil Card id validation failed",
                [30103] = "Virgil Card data parameter cannot contain more than 16 entries",
                [30104] = "Virgil Card info parameter cannot be empty if specified and must contain 'device' and/or 'device_name' key",
                [30105] = "Virgil Card info parameters length validation failed.The length cannot exceed 256 characters",
                [30106] = "Virgil Card data parameter must be an associative array(https://en.wikipedia.org/wiki/Associative_array)",
                [30107] = "A CSR parameter (content_snapshot) parameter is missing or is incorrect",
                [30111] = "Virgil Card identities passed to search endpoint must be a list of non-empty strings",
                [30113] = "Virgil Card identity type is invalid",
                [30114] = "Segregated Virgil Card custom identity value must be a not empty string",
                [30115] = "Virgil Card identity email is invalid",
                [30116] = "Virgil Card identity application is invalid",
                [30117] = "Public key length is invalid.It goes from 16 to 2048 bytes",
                [30118] = "Public key must be base64-encoded string",
                [30119] = "Virgil Card data parameter must be a key/value list of strings",
                [30120] = "Virgil Card data parameters must be strings",
                [30121] = "Virgil Card custom data entry value length validation failed.It mustn't exceed 256 characters",
                [30122] = "Identity validation token is invalid",
                [30123] = "SCR signs list parameter is missing or is invalid",
                [30126] = "SCR sign item signer card id is irrelevant and doesn't match Virgil Card id or Application Id",
                [30127] = "SCR sign item signed digest is invalid for the Virgil Card public key",
                [30128] = "SCR sign item signed digest is invalid for the application",
                [30131] = "Virgil Card id specified in the request body must match with the one passed in the URL",
                [30134] = "Virgil Card data parameters key must be aplphanumerical",
                [30135] = "Virgil Card validation token must be an object with value parameter",
                [30136] = "SCR sign item signed digest is invalid for the virgil identity service",
                [30137] = "Global Virigl Card cannot be created unconfirmed(which means that Virgil Identity service sign is mandatory)",
                [30138] = "Virigl Card with the same fingerprint exists already",
                [30139] = "Virigl Card revocation reason isn't specified or is invalid",
                [30140] = "SCR sign validation failed",
                [30141] = "SCR one of signers Virgil Cards is not found",
                [30142] = "SCR sign item is invalid or missing for the CardsClient",
                [30143] = "SCR sign item is invalid or missing for the Virgil Registration Authority service",
                [30200] = "Virgil Card relation sign is invalid",
                [30201] = "Virgil Card relation sign by the source Virgil Card was not found",
                [30202] = "Related Virgil content snapshot parameter was not found",
                [30203] = "The relation with this Virgil Card exists already",
                [30204] = "The related Virgil Card was not found for the provided CSR",
                [30205] = "The Virgil Card relation doesn't exist"
            };
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
































































