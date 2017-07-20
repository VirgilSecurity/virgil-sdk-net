namespace Virgil.SDK.Client.Requests
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a signable request that uses to publish new Card to the Virgil Services.
    /// </summary>
    public sealed class CreateGlobalCardRequest : CreateCardRequest 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateGlobalCardRequest" /> class.
        /// </summary>
        public CreateGlobalCardRequest() : base()
        {
            this.identityType = "email";
            this.scope = CardScope.Global;
        }

        public GlobalCardIdentityType IdentityType
        {
            get
            {
                return (GlobalCardIdentityType)
                  Enum.Parse(typeof(GlobalCardIdentityType), this.identityType);
            }
            set
            {
                if (this.IsSnapshotTaken)
                {
                    throw new InvalidOperationException();
                }

                this.identityType = Enum.GetName(typeof(GlobalCardIdentityType), value).ToLower();
            }
        }

        public string ValidationToken { get; set; }

        internal override SignableRequestModel GetRequestModel()
        {
            var requestModel = this.TakeSignableRequestModel();

            if (!string.IsNullOrEmpty(this.ValidationToken))
            {
                requestModel.Meta.Validation = new SignableRequestValidationModel
                {
                    Token = this.ValidationToken
                };
            }

            return requestModel;
        }
    }
}