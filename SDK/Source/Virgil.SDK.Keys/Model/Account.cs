namespace Virgil.SDK.Keys.Model
{
    using System;
    using System.Collections.Generic;

    using Virgil.SDK.Keys.TransferObject;

    /// <summary>
    /// Represents top hierarchy level of virgil domain model
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Account"/> class.
        /// </summary>
        public Account()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Account"/> class by transfer object.
        /// </summary>
        internal Account(PubPublicKey result)
        {
            AccountId = result.Id.AccountId;
            PublicKeys = new[] {new PublicKey(result)};
        }

        /// <summary>
        /// Gets or sets the account identifier.
        /// </summary>
        /// <value>
        /// The account identifier.
        /// </value>
        public Guid AccountId { get; set; }

        /// <summary>
        /// Gets or sets the public keys.
        /// </summary>
        /// <value>
        /// The public keys collection of this account.
        /// </value>
        public IEnumerable<PublicKey> PublicKeys { get; set; }
    }
}