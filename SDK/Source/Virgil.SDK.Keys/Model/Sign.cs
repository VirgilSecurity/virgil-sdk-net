namespace Virgil.SDK.Keys.Model
{
    using System;

    /// <summary>
    /// Represents the sign for the user data
    /// </summary>
    public class Sign
    {
        /// <summary>
        /// Gets or sets the sign identifier.
        /// </summary>
        /// <value>
        /// The sign identifier.
        /// </value>
        public Guid SignId { get; set; }

        /// <summary>
        /// Gets or sets the signer user data identifier.
        /// </summary>
        /// <value>
        /// The signer user data identifier.
        /// </value>
       
        public Guid SignerUserDataId { get; set; }
        /// <summary>
        /// Gets or sets the data sign.
        /// </summary>
        /// <value>
        /// The data sign.
        /// </value>
        public byte[] DataSign { get; set; }
    }
}