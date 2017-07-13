namespace Virgil.SDK.Client.Requests
{
    using Cryptography;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Virgil.SDK.Common;

    /// <summary>
    /// 
    /// </summary>
    public abstract class SignedRequest
    {


        protected Dictionary<string, byte[]> signatures;
        protected byte[] takenSnapshot;

        protected SignedRequest()
        {
            this.signatures = new Dictionary<string, byte[]>();
        }

        public byte[] Snapshot {
            get
            {
                if ((this.takenSnapshot == null) && this.IsValidData())
                {
                    this.takenSnapshot = this.CreateSnapshot();
                }

                return this.takenSnapshot;
            }
        }
        

        public IReadOnlyDictionary<string, byte[]> Signatures => this.signatures;

        protected bool IsSnapshotTaken => this.Snapshot != null;

        protected abstract byte[] CreateSnapshot();

        protected abstract bool IsValidData();


        internal void Sign(ICrypto crypto, string id, IPrivateKey privateKey)
        {
            var signature = crypto.Sign(this.Snapshot, privateKey);

            this.signatures.Add(id, signature);
        }


        internal virtual SignableRequestModel GetRequestModel()
        {
            return this.TakeSignableRequestModel();
        }

        internal SignableRequestModel TakeSignableRequestModel()
        {
            var model = new SignableRequestModel
            {
                ContentSnapshot = this.Snapshot,
                Meta = new SignableRequestMetaModel
                {
                    Signatures = this.Signatures.ToDictionary(it => it.Key, it => it.Value)
                }
            };

            return model;
        }


        /// <summary>
        /// Appends the Signature of request snapshot Fingerprint.
        /// </summary>
        internal void AppendSignature(string cardId, byte[] signature)
        {
            if (string.IsNullOrWhiteSpace(cardId))
                throw new ArgumentException(Localization.ExceptionArgumentIsNullOrWhitespace, nameof(cardId));

            if (signature == null)
                throw new ArgumentNullException(nameof(signature));

            this.signatures.Add(cardId, signature);
        }
    }
}