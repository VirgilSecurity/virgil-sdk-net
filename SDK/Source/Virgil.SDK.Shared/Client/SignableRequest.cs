namespace Virgil.SDK.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class SignableRequest
    {
        private Dictionary<string, byte[]> signatures;
        protected byte[] takenSnapshot;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SignableRequest"/> class.
        /// </summary>
        protected internal SignableRequest()
        {
            this.signatures = new Dictionary<string, byte[]>();
        }
        
        /// <summary>
        /// Gets the request snapshot.
        /// </summary>
        public byte[] Snapshot => this.takenSnapshot ?? (this.takenSnapshot = this.TakeSnapshot());

        /// <summary>
        /// Gets the signatures.
        /// </summary>
        public IReadOnlyDictionary<string, byte[]> Signatures => this.signatures;

        /// <summary>
        /// Restores the request from snapshot.
        /// </summary>
        protected abstract void RestoreRequest();

        /// <summary>
        /// Takes the request snapshot.
        /// </summary>
        protected abstract byte[] TakeSnapshot();
        
        /// <summary>
        /// Appends the signature of request fingerprint.
        /// </summary>
        public void AppendSignature(string cardId, byte[] signature)
        {
            if (string.IsNullOrWhiteSpace(cardId))
                throw new ArgumentException(Localization.ExceptionArgumentIsNullOrWhitespace, nameof(cardId));

            if (signature == null)
                throw new ArgumentNullException(nameof(signature));

            this.signatures.Add(cardId, signature);
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
        
        public string Export()
        {
            var requestModel = this.GetRequestModel();

            var json = JsonSerializer.Serialize(requestModel);
            var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));

            return base64;
        }

        public static TSignableRequest Import<TSignableRequest>(string exportedRequest) 
            where TSignableRequest : SignableRequest
        {
            var jsonModel = Encoding.UTF8.GetString(Convert.FromBase64String(exportedRequest));
            var model = JsonSerializer.Deserialize<SignedRequestModel>(jsonModel);

            SignableRequest request = null;

            if (typeof (TSignableRequest) == typeof (CreateCardRequest))
            {
                request = new CreateCardRequest();
            }

            if (typeof (TSignableRequest) == typeof (RevokeCardRequest))
            {
                request = new RevokeCardRequest();
            }

            if (request == null)
            {
                throw new NotSupportedException();
            }

            request.takenSnapshot = model.ContentSnapshot;
            request.signatures = model.Meta.Signatures;
            request.RestoreRequest();

            return (TSignableRequest) request;
        }
    }
}