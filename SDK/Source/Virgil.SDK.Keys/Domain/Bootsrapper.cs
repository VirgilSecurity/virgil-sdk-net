namespace Virgil.SDK.Keys.Domain
{
    using System;
    using Clients;
    using Clients.Authority;
    using Helpers;
    using Http;

    public class Bootsrapper
    {
        public string AccessToken { get; private set; }

        public Uri PublicServicesUri { get; private set; } = new Uri(@"https://keys.virgilsecurity.com");
        public Uri PrivateServicesUri { get; private set; } = new Uri(@"https://private-keys.virgilsecurity.com");
        public Uri IdentityServiceUri { get; private set; } = new Uri(@"https://identity.virgilsecurity.com");

        public IKnownKeyProvider KnownKeyProvider { get; private set; }
        public IPublicKeysClient PublicKeysClient { get; private set; }
        public IPrivateKeysClient PrivateKeysClient { get; private set; }
        public IVirgilCardClient VirgilCardClient { get; private set; }
        public IIdentityService IdentityService { get; private set; }

        public static Bootsrapper Setup(string accessToken)
        {
            return new Bootsrapper
            {
                AccessToken = accessToken
            };
        }

        internal static void SetupForTests()
        {
            new Bootsrapper
            {
                AccessToken = "e872d6f718a2dd0bd8cd7d7e73a25f49"
            }
                .WithStagingApiEndpoints()
                .Done();
        }

        internal Bootsrapper WithStagingApiEndpoints()
        {
            this.PublicServicesUri = new Uri(@"https://keys-stg.virgilsecurity.com");
            this.PrivateServicesUri = new Uri(@"https://private-keys-stg.virgilsecurity.com");
            this.IdentityServiceUri = new Uri(@"https://identity-stg.virgilsecurity.com");

            return this;
        }

        public Bootsrapper WithCustomPublicService(Uri publicServices)
        {
            Ensure.ArgumentNotNull(publicServices, nameof(publicServices));
            this.PublicServicesUri = publicServices;
            return this;
        }

        public Bootsrapper WithCustomPrivateServices(Uri privateServices)
        {
            Ensure.ArgumentNotNull(privateServices, nameof(privateServices));
            this.PrivateServicesUri = privateServices;
            return this;
        }

        public Bootsrapper WithCustomIdentityService(Uri identityService)
        {
            Ensure.ArgumentNotNull(identityService, nameof(identityService));
            this.IdentityServiceUri = identityService;
            return this;
        }

        public void Done()
        {
            var publicServicesConnection = new PublicServicesConnection(this.AccessToken, this.PublicServicesUri);
            this.PublicKeysClient = new PublicKeysClient(publicServicesConnection);
            this.VirgilCardClient = new VirgilCardClient(publicServicesConnection);

            this.KnownKeyProvider = new KnownKeyProvider(new PublicKeysClient(publicServicesConnection));

            this.PrivateKeysClient = new PrivateKeysClient(new PrivateKeysConnection(this.AccessToken, this.PrivateServicesUri), this.KnownKeyProvider);

            var verifier = new VirgilServiceResponseVerifier();
            var identityConnection = new VerifiedConnection(new IdentityConnection(this.IdentityServiceUri), this.KnownKeyProvider, verifier);

            this.IdentityService = new IdentityService(identityConnection);

            ServiceLocator.Services = this;
        }
    }
}