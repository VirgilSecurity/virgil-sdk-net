namespace Virgil.SDK.PrivateKeys.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides an object representation of the Private Keys account.
    /// </summary>
    public class PrivateKeysAccount
    {
        /// <summary>
        /// The account ID which represents the account ID from Public Keys Service API.
        /// </summary>
        public Guid AccountId { get; set; }
        
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