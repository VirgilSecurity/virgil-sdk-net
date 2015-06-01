namespace Virgil.PKI.Http
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// A connection for making HTTP requests against URI endpoints.
    /// </summary>
    public interface IConnection
    {
        Task<IResponse> Send(IRequest request);

        /// <summary>
        /// Base address for the connection.
        /// </summary>
        Uri BaseAddress { get; }
    }
}