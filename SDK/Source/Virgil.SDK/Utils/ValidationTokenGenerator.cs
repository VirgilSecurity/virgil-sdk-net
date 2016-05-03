namespace Virgil.SDK.Utils
{
    using System;
    using System.Text;

    using Virgil.Crypto;

    /// <summary>
    /// Provides a helper methods to generate validation token based on application's private key.  
    /// </summary>
    public class ValidationTokenGenerator
    {
        /// <summary>
        /// Generates the validation token based on application's private key. 
        /// </summary>
        /// <param name="identityValue">The identity value.</param>
        /// <param name="identityType">The type of the identity.</param>
        /// <param name="privateKey">The application's private key.</param>
        /// <param name="privateKeyPassword">The private key password.</param>
        /// <returns></returns>
        public static string Generate(string identityValue, string identityType, byte[] privateKey, string privateKeyPassword = null)
        {
            var id = Guid.NewGuid();
            return Generate(id, identityValue, identityType, privateKey, privateKeyPassword);
        }

        /// <summary>
        /// Generates the validation token based on application's private key. 
        /// </summary>
        /// <param name="identityValue">The identity value.</param>
        /// <param name="identityType">The type of the identity.</param>
        /// <param name="privateKey">The application's private key.</param>
        /// <param name="privateKeyPassword">The private key password.</param>
        /// <returns></returns>
        internal static string Generate(Guid id, string identityValue, string identityType, byte[] privateKey, string privateKeyPassword = null)
        {
            var signature = CryptoHelper.Sign(id + identityType + identityValue, privateKey, privateKeyPassword);
            var validationTokenBytes = Encoding.UTF8.GetBytes($"{id}.{signature}");
            var validationToken = Convert.ToBase64String(validationTokenBytes);

            return validationToken;
        }
    }
}