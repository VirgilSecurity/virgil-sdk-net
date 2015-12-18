namespace Virgil.SDK.Keys.Http
{
    using System;
    using System.Net;
    using System.Net.Http;
    using Virgil.SDK.Keys.Exceptions;

    using Newtonsoft.Json;


    /// <summary>
    /// A connection for making HTTP requests against URI endpoints for public api services.
    /// </summary>
    /// <seealso cref="Virgil.SDK.Keys.Http.ConnectionBase" />
    /// <seealso cref="Virgil.SDK.Keys.Http.IConnection" />
    public class PublicServicesConnection : ConnectionBase, IConnection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublicServicesConnection"/> class.
        /// </summary>
        /// <param name="appToken">Application token</param>
        /// <param name="baseAddress">The base address.</param>
        public PublicServicesConnection(string appToken, Uri baseAddress) : base(appToken, baseAddress)
        {
            
        }

        /// <summary>
        /// Handles public keys service exception resposnses
        /// </summary>
        /// <param name="message">The http response message.</param>
        protected override void ExceptionHandler(HttpResponseMessage message)
        {
            // Http client downloads whole response unless specified header fetch
            string content = message.Content.ReadAsStringAsync().Result;

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
                    errorMessage = "Internal application error";
                    break;
                case 10100:
                    errorMessage = "JSON specified as a request body is invalid";
                    break;
                case 20100:
                    errorMessage = "The request UUID header was used already";
                    break;
                case 20101:
                    errorMessage = "The request UUID header is invalid";
                    break;
                case 20200:
                    errorMessage = "The request sing header not found";
                    break;
                case 20201:
                    errorMessage = "The Public Key UUID header not specified or incorrect";
                    break;
                case 20202:
                    errorMessage = "The request sign header is invalid";
                    break;
                case 20203:
                    errorMessage = "Public Key value is required in request body";
                    break;
                case 20204:
                    errorMessage = "Public Key value in request body must be base64 encoded value";
                    break;
                case 20205:
                    errorMessage = "Public Key UUIDs in URL part and X-VIRGIL-REQUEST-SIGN-VIRGIL-CARD-ID header must match";
                    break;
                case 20206:
                    errorMessage = "The public key id in the request body is invalid ";
                    break;
                case 20207:
                    errorMessage = "Public Key UUIDs in Request and X-VIRGIL-REQUEST-SIGN-VIRGIL-CARD-ID header must match";
                    break;
                case 20300:
                    errorMessage = "The Virgil application token was not specified or invalid";
                    break;
                case 20301:
                    errorMessage = "The Virgil statistics application error";
                    break;
                case 30001:
                    errorMessage = "The entity not found by specified UUID";
                    break;
                case 30100:
                    errorMessage = "Public Key object not found by specified UUID";
                    break;
                case 30101:
                    errorMessage = "Public key length invalid";
                    break;
                case 30102:
                    errorMessage = "Public key must be base64-encoded string";
                    break;
                case 30200:
                    errorMessage = "Identity object is not found for id specified";
                    break;
                case 30201:
                    errorMessage = "Identity type is invalid. Valid types are: 'email', 'application'";
                    break;
                case 30202:
                    errorMessage = "Email value specified for the email identity is invalid";
                    break;
                case 30203:
                    errorMessage = "Cannot create unconfirmed application identity";
                    break;
                case 30300:
                    errorMessage = "Virgil Card object not found for id specified";
                    break;
                case 30400:
                    errorMessage = "Sign object not found for id specified";
                    break;
                case 30402:
                    errorMessage = "The signed digest value is invalid";
                    break;
                case 30403:
                    errorMessage = "Sign Signed digest must be base64 encoded string";
                    break;
                case 30404:
                    errorMessage = "Cannot save the Sign because it exists already";
                    break;
                case 31000:
                    errorMessage = "Value search parameter is mandatory";
                    break;

                case 0:
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
                    break;

                default:
                    errorMessage = $"Undefined exception: {errorCode}; Http status: {message.StatusCode}";
                    break;
            }

            throw new VirgilPublicKeysException(errorCode, errorMessage);
        }
    }
}