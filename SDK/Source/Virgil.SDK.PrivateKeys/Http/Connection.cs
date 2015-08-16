namespace Virgil.SDK.PrivateKeys.Http
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    using Virgil.SDK.PrivateKeys.Exceptions;
    using Virgil.SDK.PrivateKeys.TransferObject;

    /// <summary>
    /// A connection for making HTTP requests against URI endpoints.
    /// </summary>
    public class Connection : IConnection
    {
        private readonly string appToken;
        private string authToken;
        private const string defaultPath = "https://keys-private.virgilsecurity.com";

        private const string ApplicationHeader = "X-VIRGIL-APPLICATION-TOKEN";
        private const string AuthenticationHeader = "X-VIRGIL-AUTHENTICATION";

        /// <summary>
        /// Initializes a new instance of the <see cref="Connection"/> class.
        /// </summary>
        public Connection(string appToken) : this(appToken, new Uri(defaultPath))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Connection"/> class.
        /// </summary>
        /// <param name="appToken">Application Token</param>
        /// <param name="baseAddress">The base address of Private Keys API.</param>
        public Connection(string appToken, Uri baseAddress) : this(appToken, null, baseAddress)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Connection"/> class.
        /// </summary>
        /// <param name="appToken">Application Token</param>
        /// <param name="credentials">The user credentials.</param>
        /// <param name="baseAddress">The base address of Private Keys API.</param>
        public Connection(string appToken, Credentials credentials, Uri baseAddress)
        {
            this.appToken = appToken;
            this.authToken = String.Empty;

            this.Credentials = credentials;
            this.BaseAddress = baseAddress;
        }

        /// <summary>
        /// Base address for the connection.
        /// </summary>
        public Uri BaseAddress { get; private set; }

        /// <summary>
        /// Gets the account credentials.
        /// </summary>
        public Credentials Credentials { get; private set; }

        /// <summary>
        /// Sets the Private Keys account credentials.
        /// </summary>
        public void SetCredentials(Credentials credentials)
        {
            this.Credentials = credentials;
        }

        /// <summary>
        /// Sends an HTTP request to the API.
        /// </summary>
        /// <param name="request">The HTTP request details.</param>
        /// <exception cref="Virgil.SDK.PrivateKeys.Exceptions.PrivateKeysServiceException"></exception>
        public async Task<IResponse> Send(IRequest request)
        {
            try
            {
                // try to get the authentication session token, if this is the first request 
                // and credentials has been set.

                if (String.IsNullOrEmpty(this.authToken) && this.Credentials != null)
                {
                    await this.Authenticate();
                }

                return await this.SendInternal(request);
            }
            catch (PrivateKeysServiceException exception)
            {
                if (exception.ErrorCode != 20006)
                {
                    throw;
                }
            }

            // try to get the new authentication session token, if the 
            // old one has been expired.

            await this.Authenticate();

            // resend the previous service request.

            return await this.SendInternal(request);
        }

        /// <summary>
        /// Authenticates this session with current credentials.
        /// </summary>
        public async Task Authenticate()
        {
            var body = new
            {
                password = this.Credentials.Password,
                user_data = new
                {
                    @class = "user_id",
                    type = "email",
                    value = this.Credentials.UserName
                }
            };

            var content = JsonConvert.SerializeObject(body);

            var request = new Request
            {
                Endpoint = "authentication/get-token",
                Method = RequestMethod.Post,
                Body = content
            };

            var result = await this.SendInternal(request);
            var authenticationResult = JsonConvert.DeserializeObject<AuthenticationResult>(result.Body);

            this.authToken = authenticationResult.AuthToken;
        }

        /// <summary>
        /// Sends an HTTP request to the API.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        /// <exception cref="Virgil.SDK.PrivateKeys.Exceptions.PrivateKeysServiceException"></exception>
        private async Task<IResponse> SendInternal(IRequest request)
        {
            var httpClient = new HttpClient();
            
            var nativeRequest = this.GetNativeRequest(request);
            var nativeResponse = await httpClient.SendAsync(nativeRequest);
            var responseBody = await nativeResponse.Content.ReadAsStringAsync();

            if (nativeResponse.IsSuccessStatusCode)
            {
                var response = new Response
                {
                    Body = responseBody,
                    Headers = nativeResponse.Headers.ToDictionary(it => it.Key, it => it.Value.FirstOrDefault()),
                    StatusCode = nativeResponse.StatusCode
                };

                return response;
            }

            throw GetServiceException(responseBody, nativeResponse);
        }

        private PrivateKeysServiceException GetServiceException(string responseBody, HttpResponseMessage nativeResponse)
        {
            var errorCode = this.ExtractErrorCodeFromResponseBody(responseBody);
            PrivateKeysServiceException exception;

            switch (errorCode.ToString()[0])
            {
                case '2': exception = AuthenticationException.Create(errorCode, nativeResponse.StatusCode, responseBody);break;
                case '3': exception = new RequestSignIsNotValidException(errorCode, responseBody);break;
                case '4': exception = ContainerOperationException.Create(errorCode, nativeResponse.StatusCode, responseBody);break;
                case '5': exception = PrivateKeyOperationException.Create(errorCode, nativeResponse.StatusCode, responseBody); break;
                case '6': exception = VerificationException.Create(errorCode, nativeResponse.StatusCode, responseBody);break;
                case '7': exception = new ApplicationTokenInvalidExcepton(errorCode, responseBody); break;
                default:
                    exception = new PrivateKeysServiceException(errorCode, Localization.ExceptionUnrecognizedError, nativeResponse.StatusCode, responseBody);
                    break;
            }
            
            return exception;
        }

        /// <summary>
        /// Extracts the error result from response body.
        /// </summary>
        /// <param name="responseBody">The HTTP response body.</param>
        private int ExtractErrorCodeFromResponseBody(string responseBody)
        {
            try
            {
                var errorResult = JsonConvert.DeserializeAnonymousType(responseBody, new { error = new { code = 0 } });
                return errorResult.error.code;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets the method.
        /// </summary>
        /// <param name="requestMethod">The request method.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">requestMethod</exception>
        private static HttpMethod GetMethod(RequestMethod requestMethod)
        {
            switch (requestMethod)
            {
                case RequestMethod.Get: return HttpMethod.Get;
                case RequestMethod.Post: return HttpMethod.Post;
                case RequestMethod.Put: return HttpMethod.Put;
                case RequestMethod.Delete: return HttpMethod.Delete;
                default:
                    throw new ArgumentOutOfRangeException(nameof(requestMethod));
            }
        }

        /// <summary>
        /// Gets the native request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        private HttpRequestMessage GetNativeRequest(IRequest request)
        {
            var message = new HttpRequestMessage(GetMethod(request.Method), BaseAddress + request.Endpoint);

            message.Headers.TryAddWithoutValidation(ApplicationHeader, this.appToken);

            if (!String.IsNullOrEmpty(this.authToken))
            {
                message.Headers.TryAddWithoutValidation(AuthenticationHeader, this.authToken);
            }

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