namespace Virgil.SDK.Keys.Model
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents public key delete or reset confirmation
    /// </summary>
    public class PublicKeyOperationComfirmation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublicKeyOperationComfirmation"/> class.
        /// </summary>
        public PublicKeyOperationComfirmation()
        {
        }

        internal PublicKeyOperationComfirmation(IEnumerable<string> confirmationCodes, string actionToken)
        {
            this.ConfirmationCodes = confirmationCodes.ToArray();
            this.ActionToken = actionToken;
        }

        /// <summary>
        /// Action token
        /// </summary>
        public string ActionToken { get; set; }

        /// <summary>
        /// Confirmation codes received 
        /// </summary>
        public string[] ConfirmationCodes { get; set; }
    }
}