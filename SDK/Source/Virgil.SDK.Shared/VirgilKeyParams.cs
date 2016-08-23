namespace Virgil.SDK
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Virgil.SDK.Cryptography;

    /// <summary>
    /// Represents a key pair generation parameters.
    /// </summary>
    public class VirgilKeyParams
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilKeyParams" /> class.
        /// </summary>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="type">Type of the key pair.</param>
        /// <param name="password">The private key password.</param>
        public VirgilKeyParams(string keyName, VirgilKeyPairType type, string password = null)
        {
            this.Parameters = new Dictionary<string, object>
            {
                { "Type", type },
                { "Password", password }
            };

            this.Name = keyName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilKeyParams" /> class.
        /// </summary>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="password">The private key password.</param>
        public VirgilKeyParams(string keyName, string password = null)
        {
            this.Parameters = new Dictionary<string, object>
            {
                { "Type", VirgilKeyPairType.Default },
                { "Password", password }
            };

            this.Name = keyName;
        }

        /// <summary>
        /// Gets or sets the name of the key.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the type of the key pair.
        /// </summary>
        public VirgilKeyPairType Type => (VirgilKeyPairType)this.Parameters["Type"];

        /// <summary>
        /// Gets the private key password.
        /// </summary>
        public string Password => (string)this.Parameters["Password"];

        /// <summary>
        /// Gets the additional parameters.
        /// </summary>
        internal IDictionary<string, object> Parameters { get; private set; }
    }
}