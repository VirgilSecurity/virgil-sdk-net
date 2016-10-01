namespace Virgil.SDK.Client
{
    using System.Collections.Generic;
    using System.Text;

    public class CreateCardRequest : SignableRequest
    {
        private Dictionary<string, string> data;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCardRequest"/> class.
        /// </summary>
        internal CreateCardRequest()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCardRequest"/> class.
        /// </summary>
        public CreateCardRequest
        (
            string identity,
            string identityType,
            byte[] publicKey,
            Dictionary<string, string> data = null,
            CardInfoModel info = null
        )
        {
            this.Identity = identity;
            this.IdentityType = identityType;
            this.PublicKey = publicKey;
            this.data = data ?? new Dictionary<string, string>();
            this.Info = info;
        }

        /// <summary>
        /// Gets the identity value.
        /// </summary>
        public string Identity { get; private set; }

        /// <summary>
        /// Gets the type of the identity.
        /// </summary>
        public string IdentityType { get; private set; }

        /// <summary>
        /// Gets or sets the public key.
        /// </summary>
        public byte[] PublicKey { get; private set; }

        /// <summary>
        /// Gets the custom data.
        /// </summary>
        public IReadOnlyDictionary<string, string> Data => this.data;

        /// <summary>
        /// Gets the device information.
        /// </summary>
        public CardInfoModel Info { get; private set; }

        /// <summary>
        /// Restores the request fields from snapshot.
        /// </summary>
        protected override void RestoreRequest()
        {
            var json = Encoding.UTF8.GetString(this.Snapshot);
            var details = JsonSerializer.Deserialize<CreateCardModel>(json);

            this.Identity = details.Identity;
            this.IdentityType = details.IdentityType;
            this.PublicKey = details.PublicKey;
            this.data = details.Data ?? new Dictionary<string, string>();
            this.Info = details.Info;
        }

        /// <summary>
        /// Takes the request snapshot.
        /// </summary>
        protected override byte[] TakeSnapshot()
        {
            var model = new CreateCardModel
            {
                Identity = this.Identity,
                IdentityType = this.IdentityType,
                PublicKey = this.PublicKey,
                Data = this.data,
                Scope = CardScope.Application,
                Info = this.Info
            };
            
            var json = JsonSerializer.Serialize(model);
            var snapshot = Encoding.UTF8.GetBytes(json);

            return snapshot;
        }
    }
}