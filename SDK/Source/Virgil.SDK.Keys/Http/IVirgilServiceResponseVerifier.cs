namespace Virgil.SDK.Keys.Http
{
    using System.Collections.Generic;
    using System.Net.Http;
    using Exceptions;

    /// <summary>
    /// Verifies Virgil Service response
    /// </summary>
    public interface IVirgilServiceResponseVerifier
    {
        /// <summary>
        /// Verifies the virgil service response sign.
        /// </summary>
        /// <param name="nativeResponse">Http response being verified.</param>
        /// <param name="publicKey">The public key of the service.</param>
        /// <remarks>Throws exception if signature is missing or invalid.</remarks>
        /// <exception cref="ServiceSignVerificationException" />
        void VerifyResponse(HttpResponseMessage nativeResponse, byte[] publicKey);

        /// <summary>
        /// Verifies the virgil service response sign.
        /// </summary>
        /// <param name="nativeResponse">The response being verified.</param>
        /// <param name="publicKey">The public key of the service.</param>
        /// <remarks>Throws exception if signature is missing or invalid.</remarks>
        /// <exception cref="ServiceSignVerificationException" />
        void VerifyResponse(IResponse nativeResponse, byte[] publicKey);
    }
}