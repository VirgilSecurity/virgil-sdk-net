namespace Virgil.SDK.PrivateKeys.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides an object representation of the Private Keys container.
    /// </summary>
    public class Container
    {
        /// <summary>
        /// The account type which defines a method of storing private keys.
        /// </summary>
        public ContainerType Type { get; set; }

        /// <summary>
        /// The bundle of account private keys.
        /// </summary>
        public IEnumerable<PrivateKey> PrivateKeys { get; set; }
    }
}