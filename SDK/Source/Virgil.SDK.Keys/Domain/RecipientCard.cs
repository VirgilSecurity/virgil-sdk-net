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

        public string Encrypt(string text)
        {
            throw new NotImplementedException();
        }

        public string EncryptAndSign(string text, PersonalCard card)
        {
            throw new NotImplementedException();
        }
    }
}