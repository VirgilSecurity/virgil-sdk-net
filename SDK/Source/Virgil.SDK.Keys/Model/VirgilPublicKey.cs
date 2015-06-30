using Virgil.SDK.Keys.TransferObject;

namespace Virgil.SDK.Keys.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Virgil.SDK.Keys.Models;

    /// <summary>
    /// Represent public key
    /// </summary>
    public class VirgilPublicKey
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilPublicKey"/> class.
        /// </summary>
        public VirgilPublicKey()
        {
        }

        internal VirgilPublicKey(PkiPublicKey publicKey)
        {
            PublicKeyId = publicKey.Id.PublicKeyId;
            PublicKey = publicKey.PublicKey;

            UserData = publicKey.UserData.Select(it => new UserData(it));
        }

        /// <summary>
        /// Gets or sets the public key identifier.
        /// </summary>
        /// <value>
        /// The public key identifier.
        /// </value>
        public Guid PublicKeyId { get; set; }

        /// <summary>
        /// Gets or sets the user data.
        /// </summary>
        /// <value>
        /// The user data objects collection.
        /// </value>
        public IEnumerable<UserData> UserData { get; set; }

        /// <summary>
        /// Gets or sets the public key binary representation.
        /// </summary>
        /// <value>
        /// The public key.
        /// </value>
        public byte[] PublicKey { get; set; }
    }
}