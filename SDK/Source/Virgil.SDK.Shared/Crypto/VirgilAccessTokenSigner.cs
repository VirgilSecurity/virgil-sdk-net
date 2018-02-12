using Virgil.Crypto;
using Virgil.CryptoAPI;

namespace Virgil.SDK.Crypto
{
    /// <summary>
    /// The <see cref="VirgilAccessTokenSigner"/> implements <see cref="IAccessTokenSigner"/> interface and
    ///provides a cryptographic operations in applications, such as signature generation and verification in an access token.
    /// </summary>
    public class VirgilAccessTokenSigner : IAccessTokenSigner
    {
        private readonly string algorithm = "VEDS512";
        private readonly VirgilCrypto virgilCrypto;

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilAccessTokenSigner" /> class.
        /// </summary>
        public VirgilAccessTokenSigner()
        {
            virgilCrypto = new VirgilCrypto();
        }

        /// <summary>
        /// Generates the digital signature for the specified <paramref name="tokenBytes"/> using
        /// the specified <see cref="IPrivateKey"/>
        /// </summary>
        /// <param name="tokenBytes">The material representation bytes of access token 
        /// for which to compute the signature.</param>
        /// <param name="privateKey">The private key</param>
        /// <returns>The digital signature for the material representation bytes of access token.</returns>
        public byte[] GenerateTokenSignature(byte[] tokenBytes, IPrivateKey privateKey)
        {
            return virgilCrypto.GenerateSignature(tokenBytes, privateKey);
        }

        /// <summary>
        /// Represents used signature algorithm.
        /// </summary>
        /// <returns></returns>
        public string GetAlgorithm()
        {
            return algorithm;
        }

        /// <summary>
        /// Verifies that a digital signature is valid by checking the <paramref name="signature"/> and
        /// provided <see cref="IPublicKey"/> and <paramref name="tokenBytes"/>.
        /// </summary>
        /// <param name="tokenBytes">The material representation bytes of access token 
        /// for which the <paramref name="signature"/> has been generated.</param>
        /// <param name="signature">The digital signature for the <paramref name="tokenBytes"/></param>
        /// <param name="publicKey">The public key</param>
        /// <returns>True if signature is valid, False otherwise.</returns>
        public bool VerifyTokenSignature(byte[] signature, byte[] tokenBytes, IPublicKey publicKey)
        {
            return virgilCrypto.VerifySignature(signature, tokenBytes, publicKey);
        }
    }
}
