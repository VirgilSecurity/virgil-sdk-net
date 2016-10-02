namespace Virgil.SDK.Common
{
    using System;
    using Client;
    using Cryptography;

    public class VirgilCardValidator : ICardValidator
    {
        private readonly Crypto crypto;

        private const string ServiceCardId =    "3e29d43373348cfb373b7eae189214dc01d7237765e572db685839b64adca853";
        private const string ServicePublicKey = "LS0tLS1CRUdJTiBQVUJMSUMgS0VZLS0tLS0KTUNvd0JRWURLMlZ3QXlFQVlSNTAx" +
                                                "a1YxdFVuZTJ1T2RrdzRrRXJSUmJKcmMyU3lhejVWMWZ1RytyVnM9Ci0tLS0tRU5E" +
                                                "IFBVQkxJQyBLRVktLS0tLQo=";

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilCardValidator"/> class.
        /// </summary>
        public VirgilCardValidator(Crypto crypto)
        {
            this.crypto = crypto;
        }

        /// <summary>
        /// Validates the specified card.
        /// </summary>
        public bool Validate(Card card)
        {
            // Support for legacy Cards.
            if (card.Version == "3.0")
            {
                return true;
            }

            if (!card.Signatures.ContainsKey(ServiceCardId))
            {
                return false;
            }

            var fingerprint = this.crypto.CalculateFingerprint(card.Snapshot);
            var publicKey = this.crypto.ImportPublicKey(Convert.FromBase64String(ServicePublicKey));

            return this.crypto.Verify(fingerprint.GetValue(), card.Signatures[ServiceCardId], publicKey);
        }
    }
}