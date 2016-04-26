namespace Virgil.SDK.Keys.Tests
{
    using FluentAssertions;

    using NUnit.Framework;

    using Virgil.Crypto;
    using Virgil.SDK.Identities;
    using Virgil.SDK.Utils;

    public class IdentitySignerTests
    {
        [Test]
        public void Sign_ValidIdentityModel_ValidSign()
        {
            var keyPair = VirgilKeyPair.Generate();

            var validationToken = ValidationTokenGenerator.Generate("test@email.com", IdentityType.Email, keyPair.PrivateKey());

            CryptoHelper.Verify("emailtest@email.com", validationToken, keyPair.PublicKey()).Should().BeTrue();
        }
    }
}