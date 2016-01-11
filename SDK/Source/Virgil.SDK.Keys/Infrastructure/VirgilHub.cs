namespace Virgil.SDK.Infrastructure
{
    using Virgil.SDK.Clients;

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