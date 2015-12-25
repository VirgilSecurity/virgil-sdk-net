namespace Virgil.SDK.Keys.Http
{
    using System;
    using System.Net;
    using System.Net.Http;
    using Exceptions;
    using Newtonsoft.Json;

    /// <summary>
    ///     A connection for making HTTP requests against URI endpoints for public keys service.
    /// </summary>
    /// <seealso cref="Virgil.SDK.Keys.Http.ConnectionBase" />
    /// <seealso cref="Virgil.SDK.Keys.Http.IConnection" />
    public class PrivateKeysConnection : ConnectionBase, IConnection
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PrivateKeysConnection" /> class.
        /// </summary>
        /// <param name="accessToken">Application token</param>
        /// <param name="baseAddress">The base address.</param>
        public PrivateKeysConnection(string accessToken, Uri baseAddress) : base(accessToken, baseAddress)
        {
        }

        /// <summary>
        ///     Handles private keys service exception resposnses
        /// </summary>
        /// <param name="message">The http response message.</param>
        protected override void ExceptionHandler(HttpResponseMessage message)
        {
            // Http client downloads whole response unless specified header fetch
            var content = message.Content.ReadAsStringAsync().Result;

            int errorCode;
            string errorMessage;

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
            catch (Exception)
            {
                errorCode = 0;
            }

            switch (errorCode)
            {
                case 10000:
                    errorMessage = "Internal application error.";
                    break;
                case 10010:
                    errorMessage = "Controller was not found.";
                    break;
                case 10020:
                    errorMessage = "Action was not found.";
                    break;
                case 20000:
                    errorMessage = "Request wrongly encoded.";
                    break;
                case 20010:
                    errorMessage = "Request JSON invalid.";
                    break;
                case 20020:
                    errorMessage = "Request 'response_password' parameter invalid.";
                    break;
                case 30010:
                    errorMessage = "Private Key not specified.";
                    break;
                case 30020:
                    errorMessage = "Private Key not base64 encoded.";
                    break;
                case 40000:
                    errorMessage = "Virgil Card ID not specified.";
                    break;
                case 40010:
                    errorMessage = "Virgil Card ID has incorrect format.";
                    break;
                case 40020:
                    errorMessage = "Virgil Card ID not found.";
                    break;
                case 40030:
                    errorMessage = "Virgil Card ID already exists.";
                    break;
                case 40040:
                    errorMessage = "Virgil Card ID not found in Public Key service.";
                    break;
                case 40050:
                    errorMessage = "Virgil Card ID not found for provided Identity.";
                    break;
                case 50000:
                    errorMessage = "Request Sign UUID not specified.";
                    break;
                case 50010:
                    errorMessage = "Request Sign UUID has wrong format.";
                    break;
                case 50020:
                    errorMessage = "Request Sign UUID already exists.";
                    break;
                case 50030:
                    errorMessage = "Request Sign is incorrect.";
                    break;
                case 60000:
                    errorMessage = "Identity not specified.";
                    break;
                case 60010:
                    errorMessage = "Identity Type not specified.";
                    break;
                case 60020:
                    errorMessage = "Identity Value not specified.";
                    break;
                case 60030:
                    errorMessage = "Identity Token not specified.";
                    break;
                case 90000:
                    errorMessage = "Identity validation under Identity service failed.";
                    break;
                case 90010:
                    errorMessage = "Access Token validation under Stats service failed.";
                    break;

                case 0:
                {
                    switch (message.StatusCode)
                    {
                        case HttpStatusCode.BadRequest:
                            errorMessage = "Request error";
                            break;
                        case HttpStatusCode.InternalServerError:
                            errorMessage = "Internal Server error";
                            break;
                        default:
                            errorMessage = $"Undefined exception: {errorCode}; Http status: {message.StatusCode}";
                            break;
                    }
                }
                    break;

                default:
                    errorMessage = $"Undefined exception: {errorCode}; Http status: {message.StatusCode}";
                    break;
            }

            throw new VirgilPrivateKeysException(errorCode, errorMessage);
        }
    }
}