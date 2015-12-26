namespace Virgil.SDK.Keys.Http
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Exceptions;
    using Newtonsoft.Json;

    public class IdentityConnection :  IConnection
    {
        public IdentityConnection(Uri baseAddress)
        {
            this.BaseAddress = baseAddress;
        }

        private void ExceptionHandler(HttpResponseMessage message)
        {
            // Http client downloads whole response unless specified header fetch
            var content = message.Content.ReadAsStringAsync().Result;

            int errorCode;
            string errorMessage;

            try
            {
                var errorResult = JsonConvert.DeserializeAnonymousType(content, new
                {
                    code = 0
                });

                errorCode = errorResult.code;
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
                case 40100:
                    errorMessage = "Identity type is invalid";
                    break;
                case 40110:
                    errorMessage = "Identity's ttl is invalid";
                    break;
                case 40120:
                    errorMessage = "Identity's ctl is invalid";
                    break;
                case 40130:
                    errorMessage = "Identity's token parameter is missing";
                    break;
                case 40140:
                    errorMessage = "Identity's token doesn't match parameters";
                    break;
                case 40150:
                    errorMessage = "Identity's token has expired";
                    break;
                case 40160:
                    errorMessage = "Identity's token cannot be decrypted";
                    break;
                case 40200:
                    errorMessage = "Email identity value validation failed";
                    break;
                case 40210:
                    errorMessage = "Identity's confirmation code is invalid";
                    break;
                case 41000:
                    errorMessage = "Identity entity was not found";
                    break;
                case 41010:
                    errorMessage = "Identity's confirmation period has expired";
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

            throw new IdentityServiceException(errorCode, errorMessage);
        }
        
        public Uri BaseAddress { get; }

        public async Task<IResponse> Send(IRequest request)
        {
            using (var httpClient = new HttpClient())
            {
                var nativeRequest = this.GetNativeRequest(request);
                var nativeResponse = await httpClient.SendAsync(nativeRequest);

                if (!nativeResponse.IsSuccessStatusCode)
                {
                    this.ExceptionHandler(nativeResponse);
                }

                var content = nativeResponse.Content.ReadAsStringAsync().Result;
                var headers = nativeResponse.Headers.ToDictionary(it => it.Key, it => string.Join(";", it.Value));

                //var knownKey = await this.provider.GetIdentitySerivcePublicKey();
                //this.verifier.VerifyResponse(nativeResponse, knownKey.PublicKey);

                return new Response
                {
                    Body = content,
                    Headers = headers,
                    StatusCode = (int) nativeResponse.StatusCode
                };
            }
        }

        private HttpRequestMessage GetNativeRequest(IRequest request)
        {
            var message = new HttpRequestMessage(request.Method.GetMethod(), new Uri(this.BaseAddress, request.Endpoint));

            if (request.Headers != null)
            {
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
    }
}