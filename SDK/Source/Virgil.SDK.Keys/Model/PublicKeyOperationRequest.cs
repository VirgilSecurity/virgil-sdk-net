namespace Virgil.SDK.Keys.Model
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents public key delete or reset opearation request in a case of private key loss
    /// </summary>
    public class PublicKeyOperationRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublicKeyOperationRequest"/> class.
        /// </summary>
        public PublicKeyOperationRequest()
        {
        }

        internal PublicKeyOperationRequest(IEnumerable<string> userIds, string actionToken)
        {
            this.UserIds = userIds.ToArray();
            this.ActionToken = actionToken;
        }

        /// <summary>
        /// Action token
        /// </summary>
        public string ActionToken { get; set; }

        /// <summary>
        /// User ids which will get confirmation codes sent to
        /// </summary>
        public string[] UserIds { get; set; }
    }
}