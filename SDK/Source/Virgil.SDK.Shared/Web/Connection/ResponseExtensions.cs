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

namespace Virgil.SDK.Web.Connection
{
    using Virgil.SDK.Common;
    
    public static class ResponseExtensions
    {
        public static IResponse HandleError(this IResponse response, IJsonSerializer serializer)
        {
            string errorMessage;

            switch (response.StatusCode)
            {
                case 200: // OK
                case 201: // Created
                case 202: // Accepted
                case 203: // Non-Authoritative Information
                case 204: // No Content

                    // request sent successfully
                    return response;

                case 400: errorMessage = "Request Error"; break;
                case 401: errorMessage = "Authorization Error"; break;
                case 404: errorMessage = "Entity Not Found"; break;
                case 405: errorMessage = "Method Not Allowed"; break;
                case 500: errorMessage = "Internal Server Error"; break;

                default:
                    errorMessage = $"Undefined Exception (Http Status Code: {response.StatusCode})";
                    break;
            }

            var errorCode = 0;
            if (!string.IsNullOrWhiteSpace(response.Body)){
                var error = serializer.Deserialize<ServiceError>(response.Body);
                errorCode = error?.ErrorCode ?? 0;
                if (error != null && error.Message != null)
                {
                    errorMessage += $": {error.Message}";
                }
            }

            if (response.StatusCode == 401)
            {
                throw new UnauthorizedClientException(errorCode, errorMessage);
            }

            throw new ClientException(errorCode, errorMessage);
        }

        public static TResult Parse<TResult>(this IResponse response, IJsonSerializer serializer)
        {
            return serializer.Deserialize<TResult>(response.Body);
        }
    }
}
