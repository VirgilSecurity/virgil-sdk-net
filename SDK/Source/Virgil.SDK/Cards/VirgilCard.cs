namespace Virgil.SDK.Cards
{
    using System;

    using Virgil.SDK.Models;

    /// <summary>
    /// A Virgil Card is the main entity of the Virgil Services, it includes an information about the user 
    /// and his public key. The Virgil Card identifies the user by one of his available types, such as an email, 
    /// a phone number, etc.
    /// </summary>
    public sealed class VirgilCard
    {
        private readonly CardModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilCard"/> class.
        /// </summary>
        internal VirgilCard(CardModel model)
        {   
            this.model = model;     
        }

        /// <summary>
        /// Gets the unique identifier for the Virgil Card.
        /// </summary>
        public Guid Id => model.Id;

        /// <summary>
        /// Gets the value of current Virgil Card identity.
        /// </summary>
        public string Identity => model.Identity.Value;

        /// <summary>
        /// Gets the type of current Virgil Card identity.
        /// </summary>
        public string IdentityType => model.Identity.Type;

        /// <summary>
        /// Gets the date and time of Virgil Card creation.
        /// </summary>
        public DateTime CreatedAt => model.CreatedAt;

        /// <summary>
        /// Gets the Public Key of current Virgil Card.
        /// </summary>
        public byte[] PublicKey => model.PublicKey.Value;
    }
}