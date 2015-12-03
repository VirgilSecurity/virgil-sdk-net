namespace Virgil.SDK.Keys.Domain
{
    using System;

    public class RecipientCard
    {
        private RecipientCard()
        {
        }

        public Guid Id { get; private set; }
        public string Identity { get; private set; }
        public IdentityType IdentityType { get; private set; }
        public PublicKey PublicKey { get; private set; }
    }
}