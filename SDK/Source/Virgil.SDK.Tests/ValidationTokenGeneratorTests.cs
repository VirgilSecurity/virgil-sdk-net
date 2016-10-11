namespace Virgil.SDK.Keys.Tests
{
    using System;
    using System.Text;
    using FluentAssertions;

    using NUnit.Framework;

    using Virgil.Crypto;
    using Virgil.SDK.Utils;

    public class ValidationTokenGeneratorTests
    {
        [Test]
        public void Sign_ValidIdentityValueAndType_ValidSign()
        {
            var keyPair = VirgilKeyPair.GenerateRecommended();

            var validationToken = ValidationTokenGenerator.Generate("test@email.com", "custom", keyPair.PrivateKey());

            var parsedToken = Encoding.UTF8.GetString(Convert.FromBase64String(validationToken));
            var tokenParts = parsedToken.Split('.');
            var id = tokenParts[0];
            var signature =  tokenParts[1];

            var originalValue = id + "custom" + "test@email.com";

            CryptoHelper.Verify(originalValue, signature, keyPair.PublicKey()).Should().BeTrue();
        }
    }
}