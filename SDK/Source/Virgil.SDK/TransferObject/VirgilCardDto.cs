namespace Virgil.SDK.TransferObject
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents full virgil card object returned from virgil cards service
    /// </summary>
    /// <seealso cref="Virgil.SDK.TransferObject.VirgilCardDescriptorDto" />
    public class VirgilCardDto : VirgilCardDescriptorDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilCardDto"/> class.
        /// </summary>
        public VirgilCardDto()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilCardDto"/> class.
        /// </summary>
        /// <param name="descriptor">The descriptor.</param>
        /// <param name="publicKey">The public key.</param>
        public VirgilCardDto(VirgilCardDescriptorDto descriptor, PublicKeyDto publicKey) : base(descriptor)
        {
            this.PublicKey = publicKey;
        }

        /// <summary>
        /// Gets or sets the public key.
        /// </summary>
        /// <value>
        /// The public key.
        /// </value>
        [JsonProperty("public_key")]
        public PublicKeyDto PublicKey { get; set; }
    }
}