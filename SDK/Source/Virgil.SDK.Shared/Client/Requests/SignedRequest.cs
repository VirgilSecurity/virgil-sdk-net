namespace Virgil.SDK.Client.Requests
{
    using Cryptography;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Virgil.SDK.Common;
    using Virgil.CryptoApi;

    /// <summary>
    /// 
    /// </summary>
    public abstract class SignedRequest
    {


        protected Dictionary<string, byte[]> signatures;

        protected byte[] snapshot;

        protected SignedRequest()
        {
            this.signatures = new Dictionary<string, byte[]>();
        }

        public byte[] Snapshot
        {
            get
            {
                if (!IsSnapshotTaken)
                {
                    this.snapshot = this.CreateSnapshot();
                }
                return this.snapshot;
            }
        }

        public IReadOnlyDictionary<string, byte[]> Signatures => this.signatures;

        protected bool IsSnapshotTaken => this.signatures.Count > 0;

        protected abstract byte[] CreateSnapshot();

        internal void Sign(ICrypto crypto, string id, IPrivateKey privateKey)
        {

            var fingerprint = crypto.ComputeSHA256Hash(this.Snapshot);
            var signature = crypto.GenerateSignature(VirgilBuffer.From(fingerprint).GetBytes(), privateKey);

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