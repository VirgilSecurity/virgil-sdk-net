namespace Virgil.SDK.Client
{
    using Virgil.SDK.Cryptography;

    public class RequestSigner
    {
        private readonly Crypto crypto;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestSigner"/> class.
        /// </summary>
        public RequestSigner(Crypto crypto)
        {
            this.crypto = crypto;
        }

        public void SelfSign(SignableRequest request, PrivateKey privateKey)
        {
            var fingerprint = this.crypto.CalculateFingerprint(request.Snapshot);
            var signature = this.crypto.Sign(fingerprint.GetValue(), privateKey);

            request.AppendSignature(fingerprint.ToHEX(), signature);
        }

        public void AuthoritySign(SignableRequest request, string cardId, PrivateKey privateKey)
        {
            var fingerprint = this.crypto.CalculateFingerprint(request.Snapshot);
            var signature = this.crypto.Sign(fingerprint.GetValue(), privateKey);

            request.AppendSignature(cardId, signature);
        }
    }
}