namespace Virgil.SDK.Domain
{
    using System.Text;

    internal static class BytesExtensions
    {
        /// <summary>
        /// Gets the byte representation of string in specified encoding.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="encoding">The encoding. Optional. UTF8 is used by default</param>
        /// <returns>Byte array</returns>
        public static byte[] GetBytes(this string source, Encoding encoding = null)
        {
            return (encoding ?? Encoding.UTF8).GetBytes(source);
        }

        /// <summary>
        /// Gets the string of byte array representation.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="encoding">The encoding. Optional. UTF8 is used by default</param>
        /// <returns>String representation</returns>
        public static string GetString(this byte[] source, Encoding encoding = null)
        {
            return (encoding ?? Encoding.UTF8).GetString(source, 0, source.Length);
        }
    }
}