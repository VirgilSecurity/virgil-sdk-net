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

            nativeRequest.Headers.TryAddWithoutValidation("x-virgil-app-token", AppToken);

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
                    throw new ArgumentOutOfRangeException("requestMethod");
            }
        }

        private HttpRequestMessage GetNativeRequest(IRequest request)
        {
            var message = new HttpRequestMessage(GetMethod(request.Method), BaseAddress + request.Endpoint);

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
                    errorMessage = "Internal application error";
                    break;
                case 10001:
                    errorMessage = "Application kernel error";
                    break;
                case 10010:
                    errorMessage = "Internal application error";
                    break;
                case 10011:
                    errorMessage = "Internal application error";
                    break;
                case 10012:
                    errorMessage = "Internal application error";
                    break;
                case 10100:
                    errorMessage = "JSON specified as a request body is invalid";
                    break;
                case 10200:
                    errorMessage = "Guid specified is expired already";
                    break;
                case 10201:
                    errorMessage = "The Guid specified is invalid";
                    break;
                case 10202:
                    errorMessage = "The Authorization header was not specified";
                    break;
                case 10203:
                    errorMessage = "Certificate header not specified or incorrect";
                    break;
                case 10204:
                    errorMessage = "The signed digest specified is incorrect";
                    break;
                case 20000:
                    errorMessage = "Account object not found for id specified";
                    break;
                case 20100:
                    errorMessage = "Public key object not found for id specified";
                    break;
                case 20101:
                    errorMessage = "Public key invalid";
                    break;
                case 20102:
                    errorMessage = "Public key not specified";
                    break;
                case 20103:
                    errorMessage = "Public key must be base64-encoded string";
                    break;
                case 20200:
                    errorMessage = "UserData object not found for id specified";
                    break;
                case 20201:
                    errorMessage = "UserData type specified is invalid";
                    break;
                case 20202:
                    errorMessage = "UserData type specified for user identity is invalid";
                    break;
                case 20203:
                    errorMessage = "Domain specified for domain identity is invalid";
                    break;
                case 20204:
                    errorMessage = "Email specified for email identity is invalid";
                    break;
                case 20205:
                    errorMessage = "Phone specified for phone identity is invalid";
                    break;
                case 20206:
                    errorMessage = "Fax specified for fax identity is invalid";
                    break;
                case 20207:
                    errorMessage = "Application specified for application identity is invalid";
                    break;
                case 20208:
                    errorMessage = "Mac address specified for mac address identity is invalid";
                    break;
                case 20210:
                    errorMessage = "UserData integrity constraint violation";
                    break;
                case 20211:
                    errorMessage = "UserData confirmation entity not found by code specified";
                    break;
                case 20212:
                    errorMessage = "UserData confirmation code invalid";
                    break;
                case 20213:
                    errorMessage = "UserData was already confirmed and does not need further confirmation";
                    break;
                case 20214:
                    errorMessage = "UserData class specified is invalid";
                    break;
                case 20300:
                    errorMessage = "User info data validation failed. Name is invalid";
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
                            errorMessage = "Undefined exception: " + errorCode + "; Http status: " +
                                           nativeResponse.StatusCode;
                            break;
                    }
                }
                    break;

                default:
                    errorMessage = "Undefined exception: " + errorCode + "; Http status: " + nativeResponse.StatusCode;
                    break;
            }

            throw new PkiWebException(errorCode, errorMessage, nativeResponse.StatusCode, content);
        }
    }
}