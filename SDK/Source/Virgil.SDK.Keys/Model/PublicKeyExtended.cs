namespace Virgil.SDK.Keys.Model
{
    using System.Collections.Generic;
    using System.Linq;
    using TransferObject;

    /// <summary>
    /// Represents PublicKey with the list of attached users data.
    /// </summary>
    public class PublicKeyExtended : PublicKey
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublicKeyExtended"/> class.
        /// </summary>
        public PublicKeyExtended()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PublicKeyExtended"/> class.
        /// </summary>
        /// <param name="publicKey">The public key.</param>
        internal PublicKeyExtended(PubPublicKey publicKey) : base(publicKey)
        {
            this.UserData = publicKey.UserData.Select(it => new UserData(it));
        }

        /// <summary>
        /// Gets or sets the user data.
        /// </summary>
        /// <value>
        /// The user data objects collection.
        /// </value>
        public IEnumerable<UserData> UserData { get; set; }

    }
}