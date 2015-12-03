namespace Virgil.SDK.Keys.Tests.Domain
{
    using System;
    using FluentAssertions;
    using NUnit.Framework;
    using Virgil.SDK.Keys.Domain;

    public class PersonalCardTests
    {
        [Test]
        public void should_create_card_with_given_identity_details()
        {
            const string demoEmail = "virgil-demo@virgilsecurity.com";

            var newCard = PersonalCard.Create(demoEmail, IdentityType.Email);

            newCard.Identity.Value.Should().Be(demoEmail);
            newCard.Identity.Type.Should().Be(IdentityType.Email);
        }

        [Test]
        public void should_create_card_with_generated_public_and_private_keypair()
        {
            const string demoEmail = "virgil-demo@virgilsecurity.com";
            
            var newCard = PersonalCard.Create(demoEmail, IdentityType.Email);
                        
            var cipherAndSignedData = RecipientsCards
                .Search(demoEmail, IdentityType.Email)
                .EncryptAndSign("sdadasd", newCard);

            newCard.VerifyAndDecrypt(cipherAndSignedData);
            
            newCard.KeyPair.PublicKey.Should().BeNull();
            newCard.KeyPair.PrivateKey.Should().BeNull();
        }

        [Test]
        public void should_create_card_with_default_name()
        {
            const string demoEmail = "virgil-demo@virgilsecurity.com";

            var newCard = PersonalCard.Create(demoEmail, IdentityType.Email);

            newCard.Name.Should().Be(demoEmail);
        }
    }
}