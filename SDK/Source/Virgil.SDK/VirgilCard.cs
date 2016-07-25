namespace Virgil.SDK
{
    using System;
    using System.Collections.Generic;

    using Virgil.SDK.Models;

    /// <summary>
    /// A Virgil Card is the main entity of the Virgil Services, it includes an information about the user 
    /// and his public key. The Virgil Card identifies the user by one of his available types, such as an email, 
    /// a phone number, etc.
    /// </summary>
    public sealed class VirgilCard : IVirgilCard
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
        public Guid Id => this.model.Id;

        /// <summary>
        /// Gets the value of current Virgil Card identity.
        /// </summary>
        public string Identity => this.model.Identity.Value;

        /// <summary>
        /// Gets the type of current Virgil Card identity.
        /// </summary>
        public string IdentityType => this.model.Identity.Type;

        /// <summary>
        /// Gets the Public Key of current Virgil Card.
        /// </summary>
        public byte[] PublicKey => this.model.PublicKey.Value;

        /// <summary>
        /// Gets the custom <see cref="VirgilCard"/> parameters.
        /// </summary>
        public IReadOnlyDictionary<string, string> Data { get; private set; }

        /// <summary>
        /// Gets the <see cref="VirgilCard"/> version.
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the current <see cref="VirgilCard"/> identity is confirmed by Virgil Identity service.
        /// </summary>
        public bool IsConfirmed { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="VirgilCard"/> is global.
        /// </summary>
        public bool IsGlobal { get; private set; }

        /// <summary>
        /// Gets the date and time of Virgil Card creation.
        /// </summary>
        public DateTime CreatedAt => this.model.CreatedAt;

        /// <summary>
        /// Gets the date and time of <see cref="VirgilCard"/> revocation.
        /// </summary>
        public DateTime? RevokedAt { get; private set; }
    }

    public interface IVirgilCard
    {
    }
}