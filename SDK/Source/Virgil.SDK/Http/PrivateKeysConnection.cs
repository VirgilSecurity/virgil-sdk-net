namespace Virgil.SDK.Http
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using Virgil.SDK.Exceptions;

    /// <summary>
    ///     A connection for making HTTP requests against URI endpoints for public keys service.
    /// </summary>
    /// <seealso cref="ConnectionBase" />
    /// <seealso cref="IConnection" />
    public class PrivateKeysConnection : ConnectionBase, IConnection
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PrivateKeysConnection" /> class.
        /// </summary>
        /// <param name="accessToken">Application token</param>
        /// <param name="baseAddress">The base address.</param>
        public PrivateKeysConnection(string accessToken, Uri baseAddress) : base(accessToken, baseAddress)
        {
            this.Errors = new Dictionary<int, string>
            {
                [10000] = "Internal application error.",
                [10010] = "Controller was not found.",
                [10020] = "Action was not found.",
                [20000] = "Request wrongly encoded.",
                [20010] = "Request JSON invalid.",
                [20020] = "Request 'response_password' parameter invalid.",
                [30010] = "Private Key not specified.",
                [30020] = "Private Key not base64 encoded.",
                [40000] = "Virgil Card ID not specified.",
                [40010] = "Virgil Card ID has incorrect format.",
                [40020] = "Virgil Card ID not found.",
                [40030] = "Virgil Card ID already exists.",
                [40040] = "Virgil Card ID not found in Public Key service.",
                [40050] = "Virgil Card ID not found for provided Identity",
                [50000] = "Request Sign UUID not specified.",
                [50010] = "Request Sign UUID has wrong format.",
                [50020] = "Request Sign UUID already exists.",
                [50030] = "Request Sign is incorrect.",
                [60000] = "Identity not specified.",
                [60010] = "Identity Type not specified.",
                [60020] = "Identity Value not specified.",
                [60030] = "Identity Token not specified.",
                [90000] = "Identity validation under RA service failed.",
                [90010] = "Access Token validation under Stats service failed.",
            };
        }

        /// <summary>
        ///     Handles private keys service exception resposnses
        /// </summary>
        /// <param name="message">The http response message.</param>
        protected override void ExceptionHandler(HttpResponseMessage message)
        {
            this.ThrowException(message, (code, msg) => new VirgilPrivateServicesException(code, msg));
        }
    }
}