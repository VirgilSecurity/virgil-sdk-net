namespace Virgil.SDK.Client.Http
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Exceptions;
    using Newtonsoft.Json;

    /// <summary>
    /// </summary>
    internal abstract class ConnectionBase : IConnection
    {
        /// <summary>
        /// The error code to message mapping dictionary
        /// </summary>
        protected Dictionary<int, string> Errors = new Dictionary<int, string>();

        /// <summary>
        /// The access token header name
        /// </summary>
        protected const string AccessTokenHeaderName = "Authorization";

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionBase" /> class.
        /// </summary>
        /// <param name="accessToken">The application token.</param>
        /// <param name="baseAddress">The base address.</param>
        protected ConnectionBase(string accessToken, Uri baseAddress)
        {
            this.AccessToken = accessToken;
            this.BaseAddress = baseAddress;
        }

        /// <summary>
        /// Access Token
        /// </summary>
        public string AccessToken { get; protected set; }

        /// <summary>
        /// Base address for the connection.
        /// </summary>
        public Uri BaseAddress { get; protected set; }

        /// <summary>
        /// Sends an HTTP request to the API.
        /// </summary>
        /// <param name="request">The HTTP request details.</param>
        /// <param name="ignoreError">if set to <c>true</c> ignore error.</param>
        /// <returns>Response</returns>
        public virtual async Task<IResponse> Send(IRequest request, bool ignoreError = false)
        {
            using (var httpClient = new HttpClient())
            {
                var nativeRequest = this.GetNativeRequest(request);
                var nativeResponse = await httpClient.SendAsync(nativeRequest).ConfigureAwait(false);

                if (!ignoreError && !nativeResponse.IsSuccessStatusCode)
                {
                    this.ExceptionHandler(nativeResponse);
                }

                var content = nativeResponse.Content.ReadAsStringAsync().Result;

                var response = new Response
                {
                    Body = content,
                    Headers = nativeResponse.Headers.ToDictionary(it => it.Key, it => it.Value.FirstOrDefault()),
                    StatusCode = (int) nativeResponse.StatusCode
                };
                
                return response;
            }
        }

        /// <summary>
        ///     Produces native HTTP request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>HttpRequestMessage</returns>
        protected virtual HttpRequestMessage GetNativeRequest(IRequest request)
        {
            var message = new HttpRequestMessage(request.Method.GetMethod(), new Uri(this.BaseAddress, request.Endpoint));

            if (request.Headers != null)
            {
                if (this.AccessToken != null)
                {
                    message.Headers.TryAddWithoutValidation(AccessTokenHeaderName, $"VIRGIL {this.AccessToken}" );
                }

                foreach (var header in request.Headers)
                {
                    message.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }

            if (request.Method != RequestMethod.Get)
            {
                message.Content = new StringContent(request.Body, Encoding.UTF8, "application/json");
            }

            return message;
        }

        /// <summary>
        ///     Handles exception responses
        /// </summary>
        /// <param name="message">The http response message.</param>
        protected virtual void ExceptionHandler(HttpResponseMessage message)
        {
            this.ThrowException(message, (code, msg) => new VirgilServiceException(code, msg));
        }

        /// <summary>
        /// Parses service response to retrieve error code
        /// </summary>
        /// <param name="content">Http body of service response</param>
        /// <returns>Parsed error code</returns>
        protected int TryParseErrorCode(string content)
        {
            int errorCode;

            try
            {
                var errorResult = JsonConvert.DeserializeAnonymousType(content, new
                {
                    code = 0
                });

                errorCode = errorResult.code;
            }
            catch (JsonException)
            {
                try
                {
                    var errorResult = JsonConvert.DeserializeAnonymousType(content, new
                    {
                        error = new
                        {
                            code = 0
                        }
                    });

                    errorCode = errorResult.error.code;

                }
                catch
                {
                    errorCode = 0;
                }
            }
            catch
            {
                errorCode = 0;
            }

            return errorCode;
        }

        /// <summary>
        /// Parses service http response and throws apropriate exception
        /// </summary>
        /// <param name="message">Message received from service</param>
        /// <param name="exception">Exception factory</param>
        /// <typeparam name="T">Virgil exception child class</typeparam>
        protected void ThrowException<T>(HttpResponseMessage message, Func<int, string, T> exception) where T : VirgilServiceException
        {
            int errorCode = this.TryParseErrorCode(message.Content.ReadAsStringAsync().Result);
            string errorMessage;

            if (this.Errors.TryGetValue(errorCode, out errorMessage))
                throw exception(errorCode, errorMessage);

            if (errorCode == 0)
            {
                switch (message.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                        errorMessage = "Request error";
                        break;
                    case HttpStatusCode.Unauthorized:
                        errorMessage = "Authorization error";
                        break;
                    case HttpStatusCode.NotFound:
                        errorMessage = "Entity not found";
                        break;
                    case HttpStatusCode.MethodNotAllowed:
                        errorMessage = "Method not allowed";
                        break;
                    case HttpStatusCode.InternalServerError:
                        errorMessage = "Internal Server error";
                        break;
                    default:
                        errorMessage = $"Undefined exception: {errorCode}; Http status: {message.StatusCode}";
                        break;
                }
            }
            else
            {
                errorMessage = $"Undefined exception: {errorCode}; Http status: {message.StatusCode}";
            }

            throw exception(errorCode, errorMessage);
        }
    }
}