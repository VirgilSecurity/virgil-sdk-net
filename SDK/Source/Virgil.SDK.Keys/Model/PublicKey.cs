namespace Virgil.SDK.Keys.Model
{
    using System;
    using Virgil.SDK.Keys.TransferObject;


    /// <summary>
    /// Represent public key
    /// </summary>
    public class PublicKey
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublicKey"/> class.
        /// </summary>
        public PublicKey()
        {
        }

        internal PublicKey(PubPublicKey publicKey)
        {
            this.PublicKeyId = publicKey.Id.PublicKeyId;
            this.Key = publicKey.Key;
        }

        /// <summary>
        /// Gets or sets the public key identifier.
        /// </summary>
        /// <value>
        /// The public key identifier.
        /// </value>
        public Guid PublicKeyId { get; set; }
        
        /// <summary>
        /// Gets or sets the public key binary representation.
        /// </summary>
        /// <value>
        /// The public key.
        /// </value>
        public byte[] Key { get; set; }
    }
}