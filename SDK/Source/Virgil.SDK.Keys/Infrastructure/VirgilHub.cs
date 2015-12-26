namespace Virgil.SDK.Keys.Infrastructure
{
    using Virgil.SDK.Keys.Clients;

    public class VirgilHub
    {
        internal VirgilHub()
        {
        }

        public static VirgilHub Create(string accessToken)
        {
            return Bootsrapper.UseAccessToken(accessToken).Build();
        }

        public IPublicKeysClient PublicKeys { get; internal set; }
        public IPrivateKeysClient PrivateKeys { get; internal set; }
        public IVirgilCardsClient Cards { get; internal set; }
        public IIdentityClient Identity { get; internal set; }
    }
}