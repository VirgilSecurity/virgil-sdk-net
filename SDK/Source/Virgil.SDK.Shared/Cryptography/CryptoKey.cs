namespace Virgil.SDK.Cryptography
{
    using System.Collections.Generic;

    public class CryptoKey : CryptoObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CryptoKey"/> class.
        /// </summary>
        protected internal CryptoKey(IDictionary<string, object> attributes) : base(attributes)
        {
        }
    }
}