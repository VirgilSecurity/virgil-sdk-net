namespace Virgil.SDK.Keys.Http
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using Exceptions;

    /// <summary>
    /// A connection for making HTTP requests against URI endpoints for identity api services.
    /// </summary>
    /// <seealso cref="Virgil.SDK.Keys.Http.ConnectionBase" />
    /// <seealso cref="Virgil.SDK.Keys.Http.IConnection" />
    public class IdentityConnection :  ConnectionBase, IConnection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityConnection"/> class.
        /// </summary>
        /// <param name="baseAddress">The base address.</param>
        public IdentityConnection(Uri baseAddress) : base(null, baseAddress)
        {
            this.Errors = new Dictionary<int, string>
            {
                [10000] = "Internal application error",
                [10100] = "JSON specified as a request body is invalid",
                [40100] = "Identity type is invalid",
                [40110] = "Identity's ttl is invalid",
                [40120] = "Identity's ctl is invalid",
                [40130] = "Identity's token parameter is missing",
                [40140] = "Identity's token doesn't match parameters",
                [40150] = "Identity's token has expired",
                [40160] = "Identity's token cannot be decrypted",
                [40200] = "Email identity value validation failed",
                [40210] = "Identity's confirmation code is invalid",
                [41000] = "Identity entity was not found",
                [41010] = "Identity's confirmation period has expired",
            };
        }

        /// <summary>
        /// Handles exception resposnses
        /// </summary>
        /// <param name="message">The http response message.</param>
        protected override void ExceptionHandler(HttpResponseMessage message)
        {
            this.ThrowException(message, (code, msg) => new IdentityServiceException(code, msg));
        }
    }
}