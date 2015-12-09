namespace Virgil.SDK.Keys.Http
{
    using System;
    using System.Net;
    using System.Net.Http;
    using Exceptions;
    using Newtonsoft.Json;

    public class PrivateKeysConnection : ConnectionBase, IConnection
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateKeysConnection"/> class.
        /// </summary>
        /// <param name="appToken">Application token</param>
        /// <param name="baseAddress">The base address.</param>
        public PrivateKeysConnection(string appToken, Uri baseAddress) : base(appToken, baseAddress)
        {
        }
        
        protected override void ExceptionHandler(HttpResponseMessage nativeResponse)
        {
            // Http client downloads whole response unless specified header fetch
            string content = nativeResponse.Content.ReadAsStringAsync().Result;

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
                case 10001:
                    errorMessage = "Controller was not found.";
                    break;
                case 10002:
                    errorMessage = "Action was not found.";
                    break;
                case 20010:
                    errorMessage = "Request wrongly encoded.";
                    break;
                case 20020:
                    errorMessage = "Request JSON invalid.";
                    break;
                case 20003:
                    errorMessage = "Request 'response_password' parameter invalid.";
                    break;
                case 30010:
                    errorMessage = "Private Key not specified.";
                    break;
                case 30020:
                    errorMessage = "Private Key not base64 encoded.";
                    break;
                case 40000:
                    errorMessage = "Public Key ID not specified.";
                    break;
                case 40010:
                    errorMessage = "Public Key ID has incorrect format.";
                    break;
                case 40020:
                    errorMessage = "Public Key ID not found.";
                    break;
                case 40030:
                    errorMessage = "Public Key ID already exists.";
                    break;
                case 40040:
                    errorMessage = "Public Key ID not found in Public Key service.";
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

                case 0:
                {
                    switch (nativeResponse.StatusCode)
                    {
                        case HttpStatusCode.BadRequest:
                            errorMessage = "Request error";
                            break;
                        case HttpStatusCode.InternalServerError:
                            errorMessage = "Internal Server error";
                            break;
                        default:
                            errorMessage = $"Undefined exception: {errorCode}; Http status: {nativeResponse.StatusCode}";
                            break;
                    }
                }
                    break;

                default:
                    errorMessage = $"Undefined exception: {errorCode}; Http status: {nativeResponse.StatusCode}";
                    break;
            }

            throw new VirgilException(errorCode, errorMessage);
        }
    }
}