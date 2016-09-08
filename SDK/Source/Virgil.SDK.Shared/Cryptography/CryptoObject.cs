namespace Virgil.SDK.Cryptography
{
    using System.Collections.Generic;

    public class CryptoObject
    {
        protected readonly IDictionary<string, object> attributes;

        /// <summary>
        /// Initializes a new instance of the <see cref="CryptoObject"/> class.
        /// </summary>
        protected CryptoObject(IDictionary<string, object> attributes)
        {
            this.attributes = attributes;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CryptoObject"/> class.
        /// </summary>
        protected CryptoObject()
        {
            this.attributes = new Dictionary<string, object>();
        }  

        /// <summary>
        /// Gets the property value by specified name.
        /// </summary>
        protected T Get<T>(string name)
        {
            if (this.attributes.ContainsKey(name))
            {
                return (T)this.attributes[name];
            }

            return default(T);
        }

        /// <summary>
        /// Sets the property value by specified name.
        /// </summary>
        protected void Set<T>(string name, T value)
        {
            if (this.attributes.ContainsKey(name))
            {
                this.attributes[name] = value;
                return;
            }

            this.attributes.Add(name, value);
        }
    }
}