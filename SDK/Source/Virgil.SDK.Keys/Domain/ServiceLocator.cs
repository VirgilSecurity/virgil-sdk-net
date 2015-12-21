namespace Virgil.SDK.Keys.Domain
{
    using System;
    using Clients;
    using Clients.Authority;
    using Http;

    public class ServiceLocator
    {
        public const string ApplicationToken = "e872d6f718a2dd0bd8cd7d7e73a25f49";

        public static readonly PublicServicesConnection ApiEndpoint =
            new PublicServicesConnection(
                ApplicationToken,
                new Uri(@"https://keys-stg.virgilsecurity.com"));

        public static readonly PrivateKeysConnection PrivateApiEndpoint =
            new PrivateKeysConnection(
                ApplicationToken,
                new Uri(@"https://keys-stg.virgilsecurity.com"));

        public static readonly PrivateKeysConnection IdnetityApiEndpoint =
            new PrivateKeysConnection(
                ApplicationToken,
                new Uri(@"https://identity.virgilsecurity.com"));

        private static readonly Services Services = new Services
        {
            PublicKeysClient = new PublicKeysClient(ApiEndpoint),
            VirgilCardClient = new VirgilCardClient(ApiEndpoint),
            PrivateKeysClient = new PrivateKeysClient(PrivateApiEndpoint, new KnownKeyProvider(new PublicKeysClient(ApiEndpoint))),
            IdentityService = new IdentityService(IdnetityApiEndpoint),
        };

        public static Services GetServices()
        {
            return Services;
        }

        public static IPublicKeysClient PublicKeysClient { get; } = Services.PublicKeysClient;
        public static IPrivateKeysClient PrivateKeysClient { get; } = Services.PrivateKeysClient;
        public static IVirgilCardClient VirgilCardClient { get; } = Services.VirgilCardClient;
        public static IIdentityService IdentityService { get; } = Services.IdentityService;
    }
}