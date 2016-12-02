namespace Virgil.SDK.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Virgil.SDK.Common;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSnapshotModel">The type of the request model.</typeparam>
    public abstract class SignableRequest<TSnapshotModel> : ISignableRequest
    {
        protected Dictionary<string, byte[]> acceptedSignatures;
        protected byte[] takenSnapshot;
        protected TSnapshotModel snapshotModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="SignableRequest{TSnapshotModel}"/> class.
        /// </summary>
        protected internal SignableRequest()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SignableRequest{TSnapshotModel}"/> class.
        /// </summary>
        protected internal SignableRequest(TSnapshotModel snapshotModel)
        {
            this.acceptedSignatures = new Dictionary<string, byte[]>();
            this.snapshotModel = snapshotModel;
        }
        
        /// <summary>
        /// Gets the snapshot value, that has been taken from request model.
        /// </summary>
        public byte[] Snapshot => this.takenSnapshot ?? (this.takenSnapshot = this.TakeSnapshot());

        /// <summary>
        /// Gets the signatures that represents a dictionary of signers Fingerprint as keys and 
        /// request snapshot Signature as values.
        /// </summary>
        public IReadOnlyDictionary<string, byte[]> Signatures => this.acceptedSignatures;
        
        /// <summary>
        /// Appends the Signature of request snapshot Fingerprint.
        /// </summary>
        public void AppendSignature(string cardId, byte[] signature)
        {
            if (string.IsNullOrWhiteSpace(cardId))
                throw new ArgumentException(Localization.ExceptionArgumentIsNullOrWhitespace, nameof(cardId));

            if (signature == null)
                throw new ArgumentNullException(nameof(signature));

            this.acceptedSignatures.Add(cardId, signature);
        }
        
        /// <summary>
        /// Gets the request model.
        /// </summary>
        internal SignedRequestModel GetRequestModel()
        {
            var requestModel = new SignedRequestModel
            {
                ContentSnapshot = this.Snapshot,
                Meta = new SignedRequestMetaModel
                {
                    Signatures = this.Signatures.ToDictionary(it => it.Key, it => it.Value)
                }
            };

            return requestModel;
        }

        /// <summary>
        /// Extracts the request snapshot model from actual snapshot.
        /// </summary>
        public TSnapshotModel ExtractSnapshotModel()
        {
            var jsonSnapshot = Encoding.UTF8.GetString(this.Snapshot);
            return JsonSerializer.Deserialize<TSnapshotModel>(jsonSnapshot);
        }

        /// <summary>
        /// Exports this request into its string representation.
        /// </summary>
        public string Export()
        {
            var requestModel = this.GetRequestModel();

            var json = JsonSerializer.Serialize(requestModel);
            var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));

            return base64;
        }

        protected void ImportRequest(string exportedRequest)
        {
            var jsonRequestModel = Encoding.UTF8.GetString(Convert.FromBase64String(exportedRequest));
            var requestModel = JsonSerializer.Deserialize<SignedRequestModel>(jsonRequestModel);

            this.takenSnapshot = requestModel.ContentSnapshot;
            this.acceptedSignatures = requestModel.Meta.Signatures;
        }

        private byte[] TakeSnapshot()
        {
            if (this.takenSnapshot != null)
            {
                return this.takenSnapshot;
            }

            var snapshotModelJson = JsonSerializer.Serialize(this.snapshotModel);
            this.takenSnapshot = Encoding.UTF8.GetBytes(snapshotModelJson);

            return this.takenSnapshot;
        }
    }
}