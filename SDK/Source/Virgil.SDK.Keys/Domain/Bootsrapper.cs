namespace Virgil.SDK.Keys.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Clients;
    using Clients.Authority;
    using Helpers;
    using Http;

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

        public void FinishHim()
        {
            new Bootsrapper(this).FinishHim();
        }

        public Bootsrapper WithCustomServiceInstance<T>(T serviceInstance)
            where T : IVirgilService
        {
            var bootsrapper = new Bootsrapper(this).WithCustomServiceInstance(serviceInstance);
            return bootsrapper;
        }
    }

    public class Services
    {
        public IPublicKeysClient PublicKeysClient { get; internal set; }
        public IPrivateKeysClient PrivateKeysClient { get; internal set; }
        public IVirgilCardClient VirgilCardClient { get; internal set; }
        public IIdentityClient IdentityClient { get; internal set; }
    }

    public class Bootsrapper
    {
        private readonly ApiConfig apiConfig;

        public Bootsrapper(ApiConfig apiConfig)
        {
            this.apiConfig = apiConfig;
        }

        private readonly Dictionary<Type, object> customInstances = new Dictionary<Type, object>();

        private T GetService<T>()
        {
            object value = null;
            this.customInstances.TryGetValue(typeof (T), out value);
            return (T) value;
        }
        
        public Bootsrapper WithCustomServiceInstance<T>(T serviceInstance) where T : IVirgilService
        {
            this.customInstances[typeof (T)] = serviceInstance;
            return this;
        }

        public void FinishHim()
        {
            var publicServicesConnection = new PublicServicesConnection(this.apiConfig.AccessToken, this.apiConfig.PublicServicesUri);

            var publicKeysClient = this.GetService<IPublicKeysClient>() ?? new PublicKeysClient(publicServicesConnection);
            var virgilCardClient = this.GetService<IVirgilCardClient>() ?? new VirgilCardClient(publicServicesConnection);
            var knownKeyProvider = this.GetService<IKnownKeyProvider>() ?? new KnownKeyProvider(publicKeysClient);

            var privateKeysClient = this.GetService<IPrivateKeysClient>() ?? 
                new PrivateKeysClient(new PrivateKeysConnection(this.apiConfig.AccessToken, this.apiConfig.PrivateServicesUri), knownKeyProvider);

            var verifier = new VirgilServiceResponseVerifier();
            var identityConnection = new VerifiedConnection(new IdentityConnection(this.apiConfig.IdentityServiceUri), knownKeyProvider, verifier);

            var identityService = this.GetService<IIdentityClient>() ?? new IdentityClient(identityConnection);

            var services = new Services
            {
                IdentityClient = identityService,
                PublicKeysClient = publicKeysClient,
                VirgilCardClient = virgilCardClient,
                PrivateKeysClient = privateKeysClient
            };

            ServiceLocator.Services = services;
        }

        public static ApiConfig UseAccessToken(string accessToken)
        {
            return new ApiConfig(accessToken);
        }
    }
}