namespace Virgil.SDK.Keys.Tests
{
    using FizzWare.NBuilder;
    using FluentAssertions;

    using NUnit.Framework;
    using Virgil.Crypto;
    using Virgil.SDK.Models;
    using Virgil.SDK.Utils;

    public class IdentitySignerTests
    {
        [Test]
        public void Sign_ValidIdentityModel_ValidSign()
        {
            var keyPair = VirgilKeyPair.Generate();

            var validationToken = IdentitySigner.Sign("test@email.com", IdentityType.Email, keyPair.PrivateKey());

            CryptoHelper.Verify("emailtest@email.com", validationToken, keyPair.PublicKey()).Should().BeTrue();
        }
    }
}