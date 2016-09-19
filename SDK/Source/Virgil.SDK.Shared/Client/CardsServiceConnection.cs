namespace Virgil.SDK.Client.Http
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;

    using Virgil.SDK.Exceptions;

    /// <summary>
    /// A connection for making HTTP requests against URI endpoints for public api services.
    /// </summary>
    /// <seealso cref="ConnectionBase" />
    /// <seealso cref="IConnection" />
    internal class CardsServiceConnection : ConnectionBase, IConnection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CardsServiceConnection" /> class.
        /// </summary>
        /// <param name="accessToken">Application token</param>
        /// <param name="baseAddress">The base address.</param>
        public CardsServiceConnection(string accessToken, Uri baseAddress) : base(accessToken, baseAddress)
        {
            this.Errors = new Dictionary<int, string>
            {
                [10000] = "Internal application error. You know, shit happens, so do internal server errors.Just take a deep breath and try harder.",
                [20300] = "The Virgil access token was not specified or is invalid",
                [20301] = "The Virgil authenticator service responded with an error",
                [20302] = "The Virgil access token validation has failed on the Virgil Authenticator service",
                [20303] = "The application was not found for the acsses token",
                [20400] = "Request sign is invalid",
                [20401] = "Request sign header is missing",
                [20500] = "The Virgil Card is not avalable in this application",
                [30000] = "JSON specified as a request is invalid",
                [30010] = "A data inconsistency error",
                [30100] = "Global Virgil Card identity type is invalid, because it can be only 'email' or 'application'",
                [30101] = "Virgil Card scope must be either 'global' or 'application'",
                [30102] = "Virgil Card id validation failed",
                [30103] = "Virgil Card data parameter cannot contain more than 16 entries",
                [30104] = "Virgil Card info parameters missing.Expected exactly two items with 'device' and 'device_id' keys",
                [30105] = "Virgil Card info parameters length validation failed.The length cannot be exceed 256 characters",
                [30106] = "Virgil Card data parameter must be a dictionary",
                [30107] = "A virgil_card object parameter was not found on create Virgil Card endpoint invocation",
                [30108] = "Virgil Card validation token is required for a global scope",
                [30109] = "Virgil Card validation token validation failed on the Virgil Identity service",
                [30110] = "Virgil Card validation token cannot be specified for a Virgil Card which is not an application or an email",
                [30111] = "Virgil Card search identities must be a list of non-empty strings",
                [30112] = "Virgil Card is_confirmed must be a boolean",
                [30113] = "Virgil Card identity type is invalid for a global scope.Expected either 'emil' or 'application'",
                [30114] = "Segregated Virgil Card custom identity value must be a not empty string",
                [30115] = "Virgil Card identity email is invalid",
                [30116] = "Virgil Card identity application is invalid",
                [30117] = "Public key length is invalid. It goes from 16 to 2048 bytes",
                [30118] = "Public key must be base64-encoded string",
                [30119] = "Virgil Card data parameter must be a key/value list of strings",
                [30120] = "Virgil Card data parameters must be strings",
                [30121] = "Virgil Card custom data entry value length validation failed.It mustn't exceed 256 characters",
                [30122] = "Identity validation token is invalid",
                [30123] = "SCR signs list parameter is missing",
                [30124] = "SCR signs list parameter must be a list with two items for a create endpoint and a list with mo for a revoke endpoint",
                [30125] = "SCR sign item must contain only 'signer_card_id' and 'signed_digest' fields",
                [30126] = "SCR sign item signer card id is irrelevant and doesn't match Virgil Card id or Application Id",
                [30127] = "SCR sign item signed digest is invalid for the Virgil Card public key",
                [30128] = "SCR sign item signed digest is invalid for the application",
                [30129] = "SCR signs list must contain exactly two items(for an application and a Virgil Card itself)",
                [30131] = "Virgil Card id specified in request body must match one passed in the URL",
                [30132] = "Its necessary to pass either virgil card holder sign or an apllication sign into the request body",
                [30133] = "SCR sign item signed digest is invalid"
            };
        }

        /// <summary>
        /// Handles public keys service exception responses
        /// </summary>
        /// <param name="message">The http response message.</param>
        protected override void ExceptionHandler(HttpResponseMessage message)
        {
            this.ThrowException(message, (code, msg) => new VirgilClientException(code, msg));
        }
    }
}































































