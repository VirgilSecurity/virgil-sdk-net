namespace Virgil.SDK.Cryptography
{
    using System.Collections.Generic;

    public class CryptoObject
    {
        protected IDictionary<string, object> attributes;

        /// <summary>
        /// Initializes a new instance of the <see cref="CryptoKey"/> class.
        /// </summary>
        protected internal CryptoObject() : this(new Dictionary<string, object>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CryptoKey"/> class.
        /// </summary>
        protected internal CryptoObject(IDictionary<string, object> attributes)
        {
            this.attributes = attributes;
        }

        public IReadOnlyDictionary<string, object> Attributes => new Dictionary<string, object>(this.attributes);
    }
}