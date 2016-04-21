namespace Virgil.SDK.Utils
{
    using Virgil.Crypto;
    using Virgil.SDK.Models;

    /// <summary>
    /// Provides a helper methods to generate validation token based on application's private key.  
    /// </summary>
    public class IdentitySigner
    {
        /// <summary>
        /// Signs the specified identity with application's private key.
        /// </summary>
        public static string Sign(IdentityModel identity, byte[] privateKey, string privateKeyPassword = null)
        {
            var identitySignature = CryptoHelper.Sign(identity.Type + identity.Value, privateKey, privateKeyPassword);
            return identitySignature;
        }
    }
}