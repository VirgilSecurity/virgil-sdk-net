namespace Virgil.SDK.Cryptography
{
    using System;
    using System.Text;

    public class Recipient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Recipient"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="publicKey">The public key.</param>
        public Recipient(string id, PublicKey publicKey)
        {
            this.Id = Encoding.UTF8.GetBytes(id);
            this.PublicKey = publicKey;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Recipient"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="publicKey">The public key.</param>
        public Recipient(Guid id, PublicKey publicKey)
        {
            this.Id = id.ToByteArray();
            this.PublicKey = publicKey;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Recipient" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="publicKey">The public key.</param>
        public Recipient(byte[] id, PublicKey publicKey)
        {
            this.Id = id;
            this.PublicKey = publicKey;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public byte[] Id { get; set; }

        /// <summary>
        /// Gets or sets the public key.
        /// </summary>
        public PublicKey PublicKey { get; set; }
    }
}