namespace Virgil.SDK.Keys.Tests.Domain
{
    using System;
    using System.Threading.Tasks;
    using FluentAssertions;
    using NUnit.Framework;
    using TransferObject;
    using Virgil.SDK.Keys.Domain;

    public class PersonalCardTests
    {
        [Test]
        public async Task should_create_card_with_given_identity_details()
        {
            const string demoEmail = "virgil-demo@virgilsecurity.com";

            var newCard = await PersonalCard.Create(demoEmail, VirgilIdentityType.Email);

            newCard.Identity.Value.Should().Be(demoEmail);
            newCard.Identity.Type.Should().Be(IdentityType.Email);
        }

        [Test]
        public async Task should_create_card_with_generated_public_and_private_keypair()
        {
            const string demoEmail = "virgil-demo@virgilsecurity.com";
            
            var newCard = await PersonalCard.Create(demoEmail, VirgilIdentityType.Email);
                        
            var cipherAndSignedData = RecipientsCards
                .Search(demoEmail, IdentityType.Email)
                .EncryptAndSign("sdadasd", newCard);

            newCard.VerifyAndDecrypt(cipherAndSignedData);
            
            newCard.KeyPair.PublicKey.Should().BeNull();
            newCard.KeyPair.PrivateKey.Should().BeNull();
        }

        
    }
}