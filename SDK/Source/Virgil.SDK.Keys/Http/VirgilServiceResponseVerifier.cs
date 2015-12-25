namespace Virgil.SDK.Keys.Http
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using Crypto;
    using Domain;
    using Exceptions;
    
    public class VirgilServiceResponseVerifier : IVirgilServiceResponseVerifier
    {
        const string SIGN_ID_HEADER = "X-VIRGIL-RESPONSE-ID";
        const string SIGN_HEADER = "X-VIRGIL-RESPONSE-SIGN";
        
        /// <summary>
        /// Verifies the virgil service response sign.
        /// </summary>
        /// <param name="nativeResponse">Http response being verified.</param>
        /// <param name="publicKey">The public key of the service.</param>
        /// <exception cref="ServiceSignVerificationException">
        /// Request sign verification error
        /// </exception>
        /// <remarks>
        /// Throws exception if signature is missing or invalid.
        /// </remarks>
        public void VerifyResponse(HttpResponseMessage nativeResponse, byte[] publicKey)
        {
            var headers = nativeResponse.Headers.ToDictionary(it => it.Key, it => string.Join(";", it.Value));
            var content = nativeResponse.Content.ReadAsStringAsync().Result;

            var signId = headers.FirstOrDefault(it => it.Key == SIGN_ID_HEADER).Value;
            var signBase64 = headers.FirstOrDefault(it => it.Key == SIGN_HEADER).Value;

            if (string.IsNullOrWhiteSpace(signId))
            {
                throw new ServiceSignVerificationException($"{SIGN_ID_HEADER} header was not found in service response");
            }

            if (string.IsNullOrWhiteSpace(signBase64))
            {
                throw new ServiceSignVerificationException($"{SIGN_HEADER} header was not found in service response");
            }

            byte[] sign;

            try
            {
                sign = Convert.FromBase64String(signBase64);
            }
            catch (FormatException)
            {
                throw new ServiceSignVerificationException($"{SIGN_HEADER} header is not a base 64 string");
            }

            var signed = (signId + content).GetBytes(Encoding.UTF8);

            using (var signer = new VirgilSigner())
            {
                var verify = signer.Verify(signed, sign, publicKey);

                if (!verify)
                {
                    throw new ServiceSignVerificationException("Request sign verification error");
                }
            }
        }

        public void VerifyResponse(IResponse nativeResponse, byte[] publicKey)
        {
            var headers = nativeResponse.Headers;
            var content = nativeResponse.Body;

            var signId = headers.FirstOrDefault(it => it.Key == SIGN_ID_HEADER).Value;
            var signBase64 = headers.FirstOrDefault(it => it.Key == SIGN_HEADER).Value;

            if (string.IsNullOrWhiteSpace(signId))
            {
                throw new ServiceSignVerificationException($"{SIGN_ID_HEADER} header was not found in service response");
            }

            if (string.IsNullOrWhiteSpace(signBase64))
            {
                throw new ServiceSignVerificationException($"{SIGN_HEADER} header was not found in service response");
            }

            byte[] sign;

            try
            {
                sign = Convert.FromBase64String(signBase64);
            }
            catch (FormatException)
            {
                throw new ServiceSignVerificationException($"{SIGN_HEADER} header is not a base 64 string");
            }

            var signed = (signId + content).GetBytes(Encoding.UTF8);

            using (var signer = new VirgilSigner())
            {
                var verify = signer.Verify(signed, sign, publicKey);

                if (!verify)
                {
                    throw new ServiceSignVerificationException("Request sign verification error");
                }
            }
        }
    }
}