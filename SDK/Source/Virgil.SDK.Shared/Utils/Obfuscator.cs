namespace Virgil.SDK.Utils
{
    using System;
    using System.Text;

    using Virgil.Crypto.Foundation;

    /// <summary>
    /// Provides a helper methods to obfuscate the data.
    /// </summary>
    public class Obfuscator
    {
        /// <summary>
        /// Derives the obfuscated data from incoming parameters using PBKDF function.
        /// </summary>
        /// <param name="value">The string value to be hashed.</param>
        /// <param name="algorithm">The hash algorithm <see cref="VirgilPBKDF.Hash"/> type.</param>
        /// <param name="iterations">The count of iterations.</param>
        /// <param name="salt">The hash salt.</param>
        public static string PBKDF(string value, string salt, VirgilPBKDF.Hash algorithm = VirgilPBKDF.Hash.SHA384, uint iterations = 2048)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            var hashBytes = PBKDF(bytes, salt, algorithm, iterations);

            return Convert.ToBase64String(hashBytes);
        }

        /// <summary>
        /// Derives the obfuscated data from incoming parameters using PBKDF function.
        /// </summary>
        /// <param name="bytes">The  value to be hashed.</param>
        /// <param name="algorithm">The hash algorithm <see cref="VirgilPBKDF.Hash"/> type.</param>
        /// <param name="iterations">The count of iterations.</param>
        /// <param name="salt">The hash salt.</param>
        public static byte[] PBKDF(byte[] bytes, string salt, VirgilPBKDF.Hash algorithm = VirgilPBKDF.Hash.SHA384, uint iterations = 2048)
        {
            var saltData = Encoding.UTF8.GetBytes(salt);
            using (var pbkdf = new VirgilPBKDF(saltData, iterations))
            {
                pbkdf.SetHash(algorithm);
                var valueData = bytes;

                var hash = pbkdf.Derive(valueData);
                return hash;
            }
        }
    }
}