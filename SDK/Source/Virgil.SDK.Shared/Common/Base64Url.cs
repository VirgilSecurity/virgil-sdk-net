using System;

namespace Virgil.SDK.Common
{
    public class Base64Url
    {
        public static string Encode(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            return Convert.ToBase64String(data).Replace('+', '-').Replace('/', '_').Trim('=');
        }

        public static byte[] Decode(string base64str)
        {
            if (string.IsNullOrWhiteSpace(base64str))
            {
                throw new ArgumentException(nameof(base64str));
            }
            var urlDecoded = base64str.Replace('-', '+').Replace('_', '/');
            switch (urlDecoded.Length % 4)
            {
                case 2:
                    urlDecoded += "==";
                    break;
                case 3:
                    urlDecoded += "=";
                    break;
            }
            var bytes = Convert.FromBase64String(urlDecoded);
            return bytes;
        }
    }
}
