namespace Virgil.SDK.Client
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a signable request that uses to publish new Card to the Virgil Services.
    /// </summary>
    public class PublishGlobalCardRequest : SignedRequest<PublishCardSnapshotModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublishGlobalCardRequest" /> class.
        /// </summary>
        /// <param name="snapshot">The snapshot.</param>
        /// <param name="validationToken">The identity validation token.</param>
        /// <param name="signatures">The signatures.</param>
        internal PublishGlobalCardRequest(byte[] snapshot, string validationToken, IDictionary<string, byte[]> signatures) 
        {
            this.takenSnapshot = snapshot;
            this.signatures = new Dictionary<string, byte[]>(signatures);
            this.validationToken = validationToken;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishGlobalCardRequest"/> class.
        /// </summary>
        /// <param name="stringifiedRequest">The stringified request.</param>
        public PublishGlobalCardRequest(string stringifiedRequest) : base(stringifiedRequest)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishGlobalCardRequest" /> class.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="identityType">Type of the identity.</param>
        /// <param name="publicKeyData">The public key data.</param>
        /// <param name="validationToken">The identity validation token.</param>
        /// <param name="info">The information.</param>
        /// <param name="customFields">The custom fields.</param>
        public PublishGlobalCardRequest(
            string identity, 
            string identityType, 
            byte[] publicKeyData, 
            string validationToken,
            CardInfoModel info = null, 
            Dictionary<string, string> customFields = null) 
            : base(new PublishCardSnapshotModel
            {
                Identity = identity,
                IdentityType = identityType,
                PublicKeyData = publicKeyData,
                Data = customFields,
                Info = info,
                Scope = CardScope.Global
            })
        {
            this.validationToken = validationToken;
        }
    }
}