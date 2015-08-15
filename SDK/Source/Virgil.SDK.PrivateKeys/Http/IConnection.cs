using Virgil.SDK.PrivateKeys.TransferObject;

namespace Virgil.SDK.PrivateKeys.Http
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// A connection for making HTTP requests against URI endpoints.
    /// </summary>
    public interface IConnection
    {
        /// <summary>
        /// Base address for the connection.
        /// </summary>
        Uri BaseAddress { get; }

        /// <summary>
        /// Sets the Private Keys account credentials.
        /// </summary>
        void SetCredentials(Credentials credentials);

        /// <summary>
        /// Sends an authentication request to grap session token.
        /// </summary>
        Task Authenticate();

        /// <summary>
        /// Sends an HTTP request to the API.
        /// </summary>
        /// <param name="request">The HTTP request details.</param>
        Task<IResponse> Send(IRequest request);
    }
}