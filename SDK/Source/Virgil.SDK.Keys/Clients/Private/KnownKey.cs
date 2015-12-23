namespace Virgil.SDK.Keys.Clients
{
    using System;

    /// <summary>
    ///     Represents known key for channel encryption
    /// </summary>
    public class KnownKey
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="KnownKey" /> class.
        /// </summary>
        /// <param name="publicKeyId">The known public key identifier.</param>
        /// <param name="publicKey">The known public key.</param>
        public KnownKey(Guid publicKeyId, byte[] publicKey)
        {
            this.PublicKey = publicKey;
            this.PublicKeyId = publicKeyId;
        }

        /// <summary>
        ///     Gets the known public key.
        /// </summary>
        /// <value>
        ///     The known public key.
        /// </value>
        public byte[] PublicKey { get; }

        /// <summary>
        ///     Gets the known public key identifier.
        /// </summary>
        /// <value>
        ///     The known public key identifier.
        /// </value>
        public Guid PublicKeyId { get; }
    }
}