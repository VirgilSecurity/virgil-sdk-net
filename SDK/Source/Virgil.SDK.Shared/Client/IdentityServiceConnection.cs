namespace Virgil.SDK.Client
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using Exceptions;

    using Virgil.SDK.Client.Http;

    /// <summary>
    /// A connection for making HTTP requests against URI endpoints for identity api services.
    /// </summary>
    /// <seealso cref="ConnectionBase" />
    /// <seealso cref="IConnection" />
    internal class IdentityServiceConnection :  ConnectionBase, IConnection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityServiceConnection"/> class.
        /// </summary>
        /// <param name="accessToken">Application access token</param>
        /// <param name="baseAddress">The base address.</param>
        public IdentityServiceConnection(string accessToken, Uri baseAddress) : base(accessToken, baseAddress)
        {
            this.Errors = new Dictionary<int, string>
            {
                [10000] = "Internal application error",
                [40000] = "JSON specified as a request body is invalid",
                [40100] = "Identity type is invalid",
                [40110] = "Identity's ttl is invalid",
                [40120] = "Identity's ctl is invalid",
                [40130] = "Identity's token parameter is missing",
                [40140] = "Identity's token doesn't match parameters",
                [40150] = "Identity's token has expired",
                [40160] = "Identity's token cannot be decrypted",
                [40170] = "Identity's token parameter is invalid",
                [40180] = "Identity is not unconfirmed",
                [40190] = "Hash to be signed parameter is invalid",
                [40200] = "Email identity value validation failed",
                [40210] = "Identity's confirmation code is invalid",
                [40300] = "Application value is invalid",
                [40310] = "Application's signed message is invalid",
                [41000] = "Identity entity was not found",
                [41010] = "Identity's confirmation period has expired"
            };
        }

        /// <summary>
        /// Handles exception responses
        /// </summary>
        /// <param name="message">The http response message.</param>
        protected override void ExceptionHandler(HttpResponseMessage message)
        {
            this.ThrowException(message, (code, msg) => new IdentityServiceServiceException(code, msg));
        }
    }
}