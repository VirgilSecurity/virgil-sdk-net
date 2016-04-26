namespace Virgil.SDK.Cryptography
{
    using System;
    using System.Text;

    using Virgil.Crypto.Foundation;

    /// <summary>
    /// Provides a helper methods to compute the hash.
    /// </summary>
    public class VirgilHasher
    {
        /// <summary>
        /// Computes the hash value with specified parameters.
        /// </summary>
        /// <param name="value">The string value to be hashed.</param>
        /// <param name="algorithm">The hash algorithm <see cref="VirgilPBKDF.Hash"/> type.</param>
        /// <param name="iterations">The count of iterations.</param>
        /// <param name="salt">The hash salt.</param>
        public static string ComputeHash(string value, string salt, VirgilPBKDF.Hash algorithm = VirgilPBKDF.Hash.SHA384, uint iterations = 2048)
        {
            var saltData = Encoding.UTF8.GetBytes(salt);
            using (var pbkdf = new VirgilPBKDF(saltData, iterations))
            {
                pbkdf.SetHash(algorithm);
                var valueData = Encoding.UTF8.GetBytes(value);

                uint outCount = 64;

                switch (algorithm)
                {
                    case VirgilPBKDF.Hash.SHA1:   outCount = 20; break;
                    case VirgilPBKDF.Hash.SHA224: outCount = 28; break;
                    case VirgilPBKDF.Hash.SHA256: outCount = 32; break;
                    case VirgilPBKDF.Hash.SHA384: outCount = 48; break;
                    case VirgilPBKDF.Hash.SHA512: outCount = 64; break;
                }

                var hash = pbkdf.Derive(valueData, outCount);
                return Convert.ToBase64String(hash);
            }
        }
    }
}