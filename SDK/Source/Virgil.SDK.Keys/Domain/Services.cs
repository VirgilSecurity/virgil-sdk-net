namespace Virgil.SDK.Keys.Domain
{
    using Clients;
    using Clients.Authority;

    public struct Services
    {
        public IPublicKeysClient PublicKeysClient { get; internal set; }
        public IVirgilCardClient VirgilCardClient { get; internal set; }
        public IPrivateKeysClient PrivateKeysClient { get; internal set; }
        public IIdentityService IdentityService { get; internal set; }
    }
}