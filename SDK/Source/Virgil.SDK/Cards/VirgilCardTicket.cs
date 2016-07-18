namespace Virgil.SDK.Cards
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// The Virgil Card ticket is a data structure that represents user's identity, Public Key and other data. 
    /// The ticket is used to tell the Virgil Cards Service that the user's identity and Public Key are valid, 
    /// this kind of validation can be reached by validating signatures of owner's Private Key and 
    /// application's Private Key.
    /// </summary>
    public sealed class VirgilCardTicket
    {
        /// <summary>
        /// Initializes a new instance of <see cref="VirgilCardTicket"/> class.
        /// </summary>
        public VirgilCardTicket(
            string identity, 
            string identityType, 
            byte[] publicKey,
            IDictionary<string, string> data = null)
        {
            this.Id = Guid.NewGuid();
            this.Identity = identity;
            this.IdentityType = identityType;
            this.PublicKey = publicKey;

            if (data != null)
            {
                this.Data = new ReadOnlyDictionary<string, string>(data);
            }
        }

        /// <summary>
        /// Gets an unique idenitity of the ticket.
        /// </summary>
        internal Guid Id { get; private set; }

        /// <summary>
        /// Gets the user's identity value.
        /// </summary>
        internal string Identity { get; private set; }

        /// <summary>
        /// Gets the user's identity type.
        /// </summary>
        internal string IdentityType { get; private set; }

        /// <summary>
        /// Gets a Public Key value.
        /// </summary>
        internal byte[] PublicKey { get; private set; }

        /// <summary>
        /// Gets the key/value data.
        /// </summary>
        internal IReadOnlyDictionary<string, string> Data { get; private set; }

        /// <summary>
        /// Exports the ticket to it's binary representation.
        /// </summary>
        public byte[] Export()
        {
            throw new NotImplementedException();
        }
    }
}