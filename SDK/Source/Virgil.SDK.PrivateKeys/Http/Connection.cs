namespace Virgil.SDK.PrivateKeys.Http
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using Virgil.SDK.PrivateKeys.Exceptions;
    using Virgil.SDK.PrivateKeys.TransferObject;

    /// <summary>
    /// A connection for making HTTP requests against URI endpoints.
    /// </summary>
    public class Connection : IConnection
    {
        private string authToken;
        private const string defaultPath = "https://keys-private.virgilsecurity.com";

        /// <summary>
        /// Initializes a new instance of the <see cref="Connection"/> class.
        /// </summary>
        public Connection() : this(new Uri(defaultPath))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Connection"/> class.
        /// </summary>
        /// <param name="baseAddress">The base address of Private Keys API.</param>
        public Connection(Uri baseAddress) : this(null, baseAddress)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Connection"/> class.
        /// </summary>
        /// <param name="credentials">The user credentials.</param>
        /// <param name="baseAddress">The base address of Private Keys API.</param>
        public Connection(Credentials credentials, Uri baseAddress)
        {
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
            var result = await this.SendInternal(Request.Post("authentication/get-token", content));
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

            string errorMessage;
            var errorCode = this.ExtractErrorCodeFromResponseBody(responseBody);

            switch (errorCode)
            {
                case 10001: errorMessage = "Internal application error. Route was not found."; break;
                case 10002: errorMessage = "Internal application error. Route not allowed."; break;

                case 20001: errorMessage = "Athentication password validation failed"; break;
                case 20002: errorMessage = "Athentication user data validation failed"; break;
                case 20003:	errorMessage = "Athentication account was not found by provided user data"; break;
                case 20004:	errorMessage = "Athentication token validation failed"; break;
                case 20005:	errorMessage = "Athentication token not found"; break;
                case 20006:	errorMessage = "Athentication token has expired"; break;

                case 30001:	errorMessage = "Signed validation failed"; break;

                case 40001:	errorMessage = "Account validation failed"; break;
                case 40002:	errorMessage = "Account was not found"; break;
                case 40003:	errorMessage = "Account already exists"; break;
                case 40004:	errorMessage = "Account password was not specified"; break;
                case 40005:	errorMessage = "Account password validation failed"; break;
                case 40006:	errorMessage = "Account was not found in PKI service"; break;
                case 40007:	errorMessage = "Account type validation failed"; break;

                case 50001:	errorMessage = "Public Key validation failed"; break;
                case 50002:	errorMessage = "Public Key was not found"; break;
                case 50003:	errorMessage = "Public Key already exists"; break;
                case 50004:	errorMessage = "Public Key private key validation failed"; break;
                case 50005:	errorMessage = "Public Key private key base64 validation failed"; break;

                case 60001:	errorMessage = "Token was not found in request"; break;
                case 60002:	errorMessage = "User Data validation failed"; break;
                case 60003:	errorMessage = "Account was not found by user data"; break;
                case 60004: errorMessage = "Verification token ash expired"; break;

                default: 
                    errorMessage = "An unknown error has occurred"; 
                    break;
            }

            throw new PrivateKeysServiceException(errorCode, errorMessage, nativeResponse.StatusCode, responseBody);
        }

        /// <summary>
        /// Extracts the error result from response body.
        /// </summary>
        /// <param name="responseBody">The HTTP response body.</param>
        private int ExtractErrorCodeFromResponseBody(string responseBody)
        {
            try
            {
                var errorResult = JsonConvert.DeserializeAnonymousType(responseBody, new
                {
                    error = new
                    {
                        code = 0
                    }
                });

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
                    throw new ArgumentOutOfRangeException("requestMethod");
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

            if (!String.IsNullOrEmpty(this.authToken))
            {
                message.Headers.TryAddWithoutValidation("X-AUTH-TOKEN", this.authToken);
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