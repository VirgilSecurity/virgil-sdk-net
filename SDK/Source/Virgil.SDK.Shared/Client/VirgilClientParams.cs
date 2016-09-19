namespace Virgil.SDK.Client
{
    using System;

    public class VirgilClientParams
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilClientParams"/> class.
        /// </summary>
        public VirgilClientParams(string accessToken)
        {
            this.AccessToken = accessToken;

            this.CardsServiceAddress = "https://cards.virgilecurity.com";
            this.ReadOnlyCardsServiceAddress = "https://cards-ro.virgilecurity.com";
            this.IdentityServiceAddress = "https://identity.virgilsecurity.com";
        }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        internal string AccessToken { get; }

        /// <summary>
        /// Gets the cards service URL.
        /// </summary>
        internal string CardsServiceAddress { get; private set; }

        /// <summary>
        /// Gets the read only cards service address.
        /// </summary>
        internal string ReadOnlyCardsServiceAddress { get; private set; }

        /// <summary>
        /// Gets the identity service address.
        /// </summary>
        internal string IdentityServiceAddress { get; private set; }

        /// <summary>
        /// Sets the identity service address.
        /// </summary>
        /// <param name="serviceAddress">The service address.</param>
        /// <exception cref="ArgumentException"></exception>
        public void SetIdentityServiceAddress(string serviceAddress)
        {
            if (string.IsNullOrWhiteSpace(serviceAddress) && !CheckServiceUrl(serviceAddress))
                throw new ArgumentException(nameof(serviceAddress));
            
            this.IdentityServiceAddress = serviceAddress;
        }

        /// <summary>
        /// Sets the cards service address.
        /// </summary>
        /// <param name="serviceAddress">The service address.</param>
        /// <exception cref="ArgumentException"></exception>
        public void SetCardsServiceAddress(string serviceAddress)
        {
            if (string.IsNullOrWhiteSpace(serviceAddress) && !CheckServiceUrl(serviceAddress))
                throw new ArgumentException(nameof(serviceAddress));

            this.CardsServiceAddress = serviceAddress;
        }

        /// <summary>   
        /// Sets the cards service address.
        /// </summary>
        /// <param name="serviceAddress">The service address.</param>
        /// <exception cref="ArgumentException"></exception>
        public void SetReadOnlyCardsServiceAddress(string serviceAddress)
        {
            if (string.IsNullOrWhiteSpace(serviceAddress) && !CheckServiceUrl(serviceAddress))
                throw new ArgumentException(nameof(serviceAddress));

            this.ReadOnlyCardsServiceAddress = serviceAddress;
        }

        private static bool CheckServiceUrl(string serviceUrl)
        {
            Uri uriResult;
            var isValid = Uri.TryCreate(serviceUrl, UriKind.Absolute, out uriResult)
                          && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            return isValid;
        }
    }
}