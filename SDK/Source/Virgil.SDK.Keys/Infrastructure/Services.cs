namespace Virgil.SDK.Keys.Infrastructure
{
    using Clients;

    public class Services
    {
        internal Services()
        {
        }

        public static Services Get(string accessToken)
        {
            return Bootsrapper.UseAccessToken(accessToken).Build();
        }

        public IPublicKeysClient PublicKeys { get; internal set; }
        public IPrivateKeysClient PrivateKeys { get; internal set; }
        public IVirgilCardClient Cards { get; internal set; }
        public IIdentityClient Identity { get; internal set; }
    }
}