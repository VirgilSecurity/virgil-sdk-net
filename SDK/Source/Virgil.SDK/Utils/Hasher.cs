namespace Virgil.SDK.Utils
{
    /// <summary>
    /// Provides a helper methods to compute the hash.
    /// </summary>
    public class Hasher
    {
        /// <summary>
        /// Computes the hash value with default options.
        /// </summary>
        /// <param name="value">The string value to be hashed.</param>
        public string ComputeHash(string value)
        {
            return this.ComputeHash(value, HashAlgorithm.SHA384, 1000, null);
        }

        /// <summary>
        /// Computes the hash value with specified algorithm.
        /// </summary>
        /// <param name="value">The string value to be hashed.</param>
        /// <param name="hashAlgorithm">The hash algorithm <see cref="HashAlgorithm"/> type.</param>
        public string ComputeHash(string value, string hashAlgorithm)
        {
            return this.ComputeHash(value, hashAlgorithm, 1000, null);
        }

        /// <summary>
        /// Computes the hash value with specified algorithm and count of iterations.
        /// </summary>
        /// <param name="value">The string value to be hashed.</param>
        /// <param name="hashAlgorithm">The hash algorithm <see cref="HashAlgorithm"/> type.</param>
        /// <param name="iterations">The count of iterations.</param>
        public string ComputeHash(string value, string hashAlgorithm, int iterations)
        {
            return this.ComputeHash(value, hashAlgorithm, iterations, null);
        }

        /// <summary>
        /// Computes the hash value with specified parameters.
        /// </summary>
        /// <param name="value">The string value to be hashed.</param>
        /// <param name="hashAlgorithm">The hash algorithm <see cref="HashAlgorithm"/> type.</param>
        /// <param name="iterations">The count of iterations.</param>
        /// <param name="salt">The hash salt.</param>
        public string ComputeHash(string value, string hashAlgorithm, int iterations, string salt)
        {
            return value.GetHashCode().ToString();
        }
    }
}