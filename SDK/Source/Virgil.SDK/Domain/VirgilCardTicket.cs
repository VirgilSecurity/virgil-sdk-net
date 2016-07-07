namespace Virgil.SDK.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class VirgilCardTicket
    {
        /// <summary>
        /// Gets the identity value of future <see cref="VirgilCard"/>.
        /// </summary>
        public string Identity { get; private set; }

        /// <summary>
        /// Gets the idenitity type of future <see cref="VirgilCard"/>.
        /// </summary>
        public string IdentityType { get; private set; }

        /// <summary>
        /// Gets the Public Key value. 
        /// </summary>
        public byte[] PublicKey { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilCardTicket"/> class.
        /// </summary>
        public VirgilCardTicket(string identity, string identityType, byte[] publicKey)
        {
            this.Identity = identity;
            this.IdentityType = identityType;
            this.PublicKey = publicKey;
        }
    }
}