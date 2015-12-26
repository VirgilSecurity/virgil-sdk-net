namespace Virgil.SDK.Keys.Infrastructurte
{
    using System;
    using Clients;
    using Helpers;

    public class ApiConfig
    {
        public ApiConfig(string accessToken)
        {
            this.AccessToken = accessToken;
        }

        public string AccessToken { get; private set; }

        public const string PublicServicesAddress = @"https://keys.virgilsecurity.com";
        public const string PrivateServicesAddress = @"https://private-keys.virgilsecurity.com";
        public const string IdentityServiceAddress = @"https://identity.virgilsecurity.com";

        public Uri PublicServicesUri { get; private set; } = new Uri(PublicServicesAddress);
        public Uri PrivateServicesUri { get; private set; } = new Uri(PrivateServicesAddress);
        public Uri IdentityServiceUri { get; private set; } = new Uri(IdentityServiceAddress);
        
        internal ApiConfig WithStagingEndpoints()
        {
            this.PublicServicesUri = new Uri(@"https://keys-stg.virgilsecurity.com");
            this.PrivateServicesUri = new Uri(@"https://private-keys-stg.virgilsecurity.com");
            this.IdentityServiceUri = new Uri(@"https://identity-stg.virgilsecurity.com");

            return this;
        }
        
        public ApiConfig WithCustomPublicServiceUri(Uri publicServices)
        {
            Ensure.ArgumentNotNull(publicServices, nameof(publicServices));
            this.PublicServicesUri = publicServices;
            return this;
        }

        public ApiConfig WithCustomPrivateServiceUri(Uri privateServices)
        {
            Ensure.ArgumentNotNull(privateServices, nameof(privateServices));
            this.PrivateServicesUri = privateServices;
            return this;
        }

        public ApiConfig WithCustomIdentityServiceUri(Uri identityService)
        {
            Ensure.ArgumentNotNull(identityService, nameof(identityService));
            this.IdentityServiceUri = identityService;
            return this;
        }
        
        public Bootsrapper PrepareServices()
        {
            return new Bootsrapper(this);
        }

        public Services Build()
        {
            return new Bootsrapper(this).Build();
        }

        public Bootsrapper WithCustomServiceInstance<T>(T serviceInstance)
            where T : IVirgilService
        {
            var bootsrapper = new Bootsrapper(this).WithCustomServiceInstance(serviceInstance);
            return bootsrapper;
        }
    }
}