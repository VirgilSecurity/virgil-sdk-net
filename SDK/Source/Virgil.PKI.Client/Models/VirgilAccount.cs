namespace Virgil.SDK.Keys.Models
{
    using System;
    using System.Collections.Generic;
    using Dtos;

    /// <summary>
    /// Represents top hierarchy level of virgil domain model
    /// </summary>
    public class VirgilAccount
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilAccount"/> class.
        /// </summary>
        public VirgilAccount()
        {
            
        }

        internal VirgilAccount(PkiPublicKey result)
        {
            AccountId = result.Id.AccountId;
            PublicKeys = new[] {new VirgilPublicKey(result)};
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
        public IEnumerable<VirgilPublicKey> PublicKeys { get; set; }
    }
}