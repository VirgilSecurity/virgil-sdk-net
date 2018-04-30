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

namespace Virgil.SDK.Web.Connection
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    public class ServiceConnection : IConnection
    {
        public ServiceConnection(string baseURL)
        {
            this.BaseURL = new Uri(baseURL);
        }

        /// <summary>
        // Base URL for API requests. Defaults to the public Virgil API, but 
        // can be set to a domain endpoint to use with Virgil Enterprise. 
        /// </summary>
        /// <remarks>
        /// BaseURL should always be specified with a trailing slash.
        /// </remarks>
        public Uri BaseURL { get; private set; }
        
        /// <summary>
        /// Sends an HTTP request to the API.
        /// </summary>
        /// <param name="request">The HTTP request details.</param>
        /// <param name="token">The access token.</param>
        /// <returns>Response</returns>
        public virtual async Task<IResponse> SendAsync(IRequest request)
        {
            using (var httpClient = new HttpClient())
            {
                var nativeRequest = this.GetNativeRequest(request);
                var nativeResponse = await httpClient.SendAsync(nativeRequest).ConfigureAwait(false);
               
                var content = nativeResponse.Content.ReadAsStringAsync().Result;
                var response = new HttpResponse
                {
                    Body = content,
                    Headers = nativeResponse.Headers.ToDictionary(it => it.Key, it => it.Value.FirstOrDefault()),
                    StatusCode = (int)nativeResponse.StatusCode
                };

                return response;
            }
        }

        /// <summary>
        /// Produces native HTTP request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="token">The access token.</param>
        /// <returns>HttpRequestMessage</returns>
        protected virtual HttpRequestMessage GetNativeRequest(IRequest request)
        {
            var message = new HttpRequestMessage(request.Method.GetMethod(),
                new Uri(this.BaseURL, request.Endpoint));

            if (request.Headers != null)
            {
                foreach (var header in request.Headers)
                {
                    message.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }

            if (request.Method != HttpRequestMethod.Get)
            {
                message.Content = new StringContent(request.Body, Encoding.UTF8, "application/json");
            }
            return message;
        }
    }
}