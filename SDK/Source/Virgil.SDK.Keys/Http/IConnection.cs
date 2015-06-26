namespace Virgil.SDK.Keys.Http
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    ///     A connection for making HTTP requests against URI endpoints.
    /// </summary>
    public interface IConnection
    {
        /// <summary>
        ///     Base address for the connection.
        /// </summary>
        Uri BaseAddress { get; }

        /// <summary>
        ///     Developer's application token.
        /// </summary>
        string AppToken { get; }

        /// <summary>
        ///     Sends an HTTP request to the API.
        /// </summary>
        /// <param name="request">The HTTP request details.</param>
        Task<IResponse> Send(IRequest request);
    }
}