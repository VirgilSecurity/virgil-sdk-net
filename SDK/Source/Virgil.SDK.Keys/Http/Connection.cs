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
   
    /// <summary>
    /// A connection for making HTTP requests against URI endpoints.
    /// </summary>
    public class Connection : IConnection
    {
        private const string AppTokenHeaderName = "X-VIRGIL-ACCESS-TOKEN";

        /// <summary>
        /// Initializes a new instance of the <see cref="Connection"/> class.
        /// </summary>
        /// <param name="appToken">Application token</param>
        /// <param name="baseAddress">The base address.</param>
        public Connection(string appToken, Uri baseAddress)
        {
            AppToken = appToken;
            BaseAddress = baseAddress;
        }

        /// <summary>
        /// Sends an HTTP request to the API.
        /// </summary>
        /// <param name="request">The HTTP request details.</param>
        /// <returns></returns>
        public async Task<IResponse> Send(IRequest request)
        {
            using (var httpClient = new HttpClient())
            {
                HttpRequestMessage nativeRequest = GetNativeRequest(request);
                var nativeResponse = await httpClient.SendAsync(nativeRequest);

                if (!nativeResponse.IsSuccessStatusCode)
                {
                    ExceptionHandler(nativeResponse);
                }

                string content = nativeResponse.Content.ReadAsStringAsync().Result;

                return new Response
                {
                    Body = content,
                    Headers = nativeResponse.Headers.ToDictionary(it => it.Key, it => it.Value.FirstOrDefault()),
                    StatusCode = nativeResponse.StatusCode
                };
            }
        }

        /// <summary>
        /// Application Token
        /// </summary>
        public string AppToken { get; }

        /// <summary>
        /// Base address for the connection.
        /// </summary>
        public Uri BaseAddress { get; private set; }

        private static HttpMethod GetMethod(RequestMethod requestMethod)
        {
            switch (requestMethod)
            {
                case RequestMethod.Get:
                    return HttpMethod.Get;
                case RequestMethod.Post:
                    return HttpMethod.Post;
                case RequestMethod.Put:
                    return HttpMethod.Put;
                case RequestMethod.Delete:
                    return HttpMethod.Delete;
                default:
                    throw new ArgumentOutOfRangeException(nameof(requestMethod));
            }
        }

        private HttpRequestMessage GetNativeRequest(IRequest request)
        {
            var message = new HttpRequestMessage(GetMethod(request.Method), new Uri(BaseAddress, request.Endpoint));

            if (request.Headers != null)
            {

                message.Headers.TryAddWithoutValidation(AppTokenHeaderName, AppToken);

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
        
        private static void ExceptionHandler(HttpResponseMessage nativeResponse)
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

            throw new VirgilException(errorCode, errorMessage);
        }
    }

    
}