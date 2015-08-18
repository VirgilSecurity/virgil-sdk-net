namespace Virgil.SDK.Keys.Http
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    using Virgil.SDK.Keys.Exceptions;

    using Newtonsoft.Json;

    public class Connection : IConnection
    {
        public Connection(string appToken, Uri baseAddress)
        {
            AppToken = appToken;
            BaseAddress = baseAddress;
        }

        public async Task<IResponse> Send(IRequest request)
        {
            var httpClient = new HttpClient();

            HttpRequestMessage nativeRequest = GetNativeRequest(request);
            nativeRequest.Headers.TryAddWithoutValidation("X-VIRGIL-APPLICATION-TOKEN", AppToken);

            HttpResponseMessage nativeResponse = await httpClient.SendAsync(nativeRequest);

            if (!nativeResponse.IsSuccessStatusCode)
            {
                await ExceptionHandler(nativeResponse);
            }

            string content = await nativeResponse.Content.ReadAsStringAsync();

            return new Response
            {
                Body = content,
                Headers = nativeResponse.Headers.ToDictionary(it => it.Key, it => it.Value.FirstOrDefault()),
                StatusCode = nativeResponse.StatusCode
            };
        }

        public Uri BaseAddress { get; private set; }

        public string AppToken { get; private set; }

        private static HttpMethod GetMethod(RequestMethod requestMethod)
        {
            switch (requestMethod)
            {
                case RequestMethod.Get:
                    return HttpMethod.Get;
                    break;
                case RequestMethod.Post:
                    return HttpMethod.Post;
                    break;
                case RequestMethod.Put:
                    return HttpMethod.Put;
                    break;
                case RequestMethod.Delete:
                    return HttpMethod.Delete;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(requestMethod));
            }
        }

        private HttpRequestMessage GetNativeRequest(IRequest request)
        {
            var message = new HttpRequestMessage(GetMethod(request.Method), new Uri(BaseAddress, request.Endpoint));

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

        private static async Task ExceptionHandler(HttpResponseMessage nativeResponse)
        {
            string content = await nativeResponse.Content.ReadAsStringAsync();

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
                    errorMessage = "Internal error ";
                    break;

                case 10001:
                    errorMessage = "The error happened in one of system modules";
                    break;

                case 10010:
                    errorMessage = "controller alias to full controller name";
                    break;

                case 10011:
                    errorMessage = "Action method was not found during transforming the endpoint handler to Route-friendly alias";
                    break; 

                case 10012:
                    errorMessage = "The action requested is not allowed";
                    break;

                case 10013:
                    errorMessage = "The api version requested is not supported";
                    break;

                case 10100:
                    errorMessage = "JSON specified as a request is invalid";
                    break;

                case 10200:
                    errorMessage = "The UUID specified was used already";
                    break;

                case 10201:
                    errorMessage = "The UUID specified is invalid";
                    break;

                case 10202:
                    errorMessage = "The Authorization header not specified";
                    break;

                case 10203:
                    errorMessage = "The Public Key header not specified or incorrect";
                    break;

                case 10204:
                    errorMessage = "The signed digest specified is incorrect";
                    break;

                case 10205:
                    errorMessage = "The Virgil application token not specified or invalid";
                    break;

                case 10206:
                    errorMessage = "The Virgil statistics application error";
                    break;

                case 10207:
                    errorMessage = "The Public Key specified in header was not confirmed yet";
                    break;

                case 10208:
                    errorMessage = "Public Key value required in request body";
                    break;

                case 20000:
                    errorMessage = "Account object not found for id specified";
                    break;

                case 20100:
                    errorMessage = "Public Key object not found for id specified";
                    break;

                case 20101:
                    errorMessage = "Public key length invalid";
                    break;

                case 20102:
                    errorMessage = "Public key not specified";
                    break;

                case 20103:
                    errorMessage = "Public key must be base64-encoded string";
                    break;

                case 20104:
                    errorMessage = "Public key must contain confirmed UserData entities";
                    break;

                case 20105:
                    errorMessage = "Public key must contain at least one UserID entry";
                    break;

                case 20106:
                    errorMessage = "Public key must have a link to account";
                    break;

                case 20107:
                    errorMessage = "There is UDID registered for current application already";
                    break;

                case 20108:
                    errorMessage = "UDIDs specified are registered for several accounts";
                    break;

                case 20109:
                    errorMessage = "Public key is not confirmed";
                    break;

                case 20110:
                    errorMessage = "Public key is not found for any application";
                    break;

                case 20111:
                    errorMessage = "Public key is found for another application";
                    break;

                case 20200:
                    errorMessage = "User Data object not found for id specified";
                    break;

                case 20201:
                    errorMessage = "User Data type specified is invalid";
                    break;

                case 20202:
                    errorMessage = "User Data type specified as user identity is invalid";
                    break;

                case 20203:
                    errorMessage = "Domain value specified for the domain identity is invalid";
                    break;

                case 20204:
                    errorMessage = "Email value specified for the email identity is invalid";
                    break;

                case 20205:
                    errorMessage = "Phone value specified for the phone identity is invalid";
                    break;

                case 20206:
                    errorMessage = "Fax value specified for the fax identity is invalid";
                    break;

                case 20207:
                    errorMessage = "Application value specified for the application identity is invalid";
                    break;

                case 20208:
                    errorMessage = "Mac address value specified for the mac address identity is invalid";
                    break;

                case 20210:
                    errorMessage = "User Data integrity constraint violation";
                    break;

                case 20211:
                    errorMessage = "User Data confirmation entity not found";
                    break;

                case 20212:
                    errorMessage = "User Data confirmation token invalid";
                    break;

                case 20213:
                    errorMessage = "User Data was already confirmed and does not need further confirmation";
                    break;

                case 20214:
                    errorMessage = "User Data class specified is invalid";
                    break;

                case 20215:
                    errorMessage = "Domain value specified for the domain identity is invalid";
                    break;

                case 20216:
                    errorMessage = "This user id had been confirmed earlier";
                    break;

                case 20217:
                    errorMessage = "The user data is not confirmed yet";
                    break;

                case 20218:
                    errorMessage = "The user data value is required";
                    break;

                case 20300:
                    errorMessage = "User info data validation failed";
                    break;

                case 20400:
                    errorMessage = "Sign digest parameter validation failed";
                    break;

                case 20401:
                    errorMessage = "Sign hash parameter validation failed";
                    break;

                case 0:
                    {
                        switch (nativeResponse.StatusCode)
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
                                errorMessage = $"Undefined exception: {errorCode}; Http status: {nativeResponse.StatusCode}";
                                break;
                        }
                    }
                    break;

                default:
                    errorMessage = $"Undefined exception: {errorCode}; Http status: {nativeResponse.StatusCode}";
                    break;
            }

            if (errorCode > 0 && errorCode < 10200)
            {
                throw new KeysServiceServerException(errorCode, errorMessage, nativeResponse.StatusCode, content);
            }
            if (errorCode >= 10200 && errorCode < 20000)
            {
                throw new KeysServiceAuthException(errorCode, errorMessage, nativeResponse.StatusCode, content);
            }
            if (errorCode >= 20000)
            {
                throw new KeysServiceRequestException(errorCode, errorMessage, nativeResponse.StatusCode, content);
            }

            throw new KeysServiceException(errorCode, errorMessage, nativeResponse.StatusCode, content);
        }
    }
}