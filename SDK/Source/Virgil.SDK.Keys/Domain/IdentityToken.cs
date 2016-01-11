namespace Virgil.SDK.Domain
{
    using Virgil.SDK.TransferObject;

    public class IdentityToken
    {
        internal IdentityToken(IdentityTokenRequest request, IndentityTokenDto token)
        {
            this.Identity = request.Identity;
            this.IdentityType = request.IdentityType;
            this.Token = token;
        }

        public IndentityTokenDto Token { get; private set; }

        public string Identity { get; private set; }

        public IdentityType IdentityType { get; private set; }
    }
}