namespace Virgil.SDK.Domain
{
    using System;

    public class VirgilBuffer
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="VirgilBuffer"/> class from being created.
        /// </summary>
        private VirgilBuffer()
        {
        }

        /// <summary>
        /// Converts to string representation that is encoded with base-64 digits.
        /// </summary>
        public string ToBase64()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts to string representation that is encoded as UTF8.
        /// </summary>
        public string ToUTF8()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts to byte array.
        /// </summary>
        public byte[] ToBytes()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts the specified string, which encodes binary data as base-64 digits, to <see cref="VirgilBuffer"/>.
        /// </summary>
        public static VirgilBuffer FromBase64(string s)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts the specified bytes to <see cref="VirgilBuffer"/>.
        /// </summary>
        public static VirgilBuffer FromBytes(byte[] bytes)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts the specified string, which encodes binary data as UTF8, into <see cref="VirgilBuffer"/>.
        /// </summary>
        public static VirgilBuffer FromUTF8(string s)
        {
            throw new NotImplementedException();
        }
    }
}