namespace Virgil.SDK.Keys.Model
{
    using System.Collections.Generic;
    using System.Linq;
    using TransferObject;

    public class PublicKeyExtended : PublicKey
    {
        public PublicKeyExtended()
        {
        }

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