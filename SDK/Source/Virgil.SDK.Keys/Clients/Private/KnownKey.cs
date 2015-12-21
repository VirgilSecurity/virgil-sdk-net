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
        /// <param name="knownPublicKeyId">The known public key identifier.</param>
        /// <param name="knownPublicKey">The known public key.</param>
        public KnownKey(Guid knownPublicKeyId, byte[] knownPublicKey)
        {
            this.KnownPublicKey = knownPublicKey;
            this.KnownPublicKeyId = knownPublicKeyId;
        }

        /// <summary>
        ///     Gets the known public key.
        /// </summary>
        /// <value>
        ///     The known public key.
        /// </value>
        public byte[] KnownPublicKey { get; }

        /// <summary>
        ///     Gets the known public key identifier.
        /// </summary>
        /// <value>
        ///     The known public key identifier.
        /// </value>
        public Guid KnownPublicKeyId { get; }
    }
}