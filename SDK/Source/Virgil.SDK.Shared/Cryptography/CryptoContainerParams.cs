namespace Virgil.SDK.Cryptography
{
    using System.Collections.Generic;

    /// <summary>
    /// Contains advanced properties for key creation.
    /// </summary>
    public class CryptoContainerParams
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CryptoContainerParams" /> class.
        /// </summary>
        /// <param name="name">The name of the container.</param>
        /// <param name="keyType">The type of the key.</param>
        /// <param name="data">The custom key container parameters.</param>
        public CryptoContainerParams(string name, string keyType, IDictionary<string, string> data)
        {
            this.ContainerName = name;
            this.ContainerKeyType = keyType;
            this.Data = data;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CryptoContainerParams"/> class.
        /// </summary>
        public CryptoContainerParams()
        {
            this.Data = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets the name of the container.
        /// </summary>
        public string ContainerName { get; set; }

        /// <summary>
        /// Gets or sets the type of the key.
        /// </summary>
        public string ContainerKeyType { get; set; }

        /// <summary>
        /// Gets or sets the custom container parameters.
        /// </summary>
        public IDictionary<string, string> Data { get; }
    }
}