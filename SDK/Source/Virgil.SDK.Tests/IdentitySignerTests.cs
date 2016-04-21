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
            var identity = Builder<IdentityModel>.CreateNew().Build();
            var keyPair = VirgilKeyPair.Generate();

            var validationToken = IdentitySigner.Sign(identity, keyPair.PrivateKey());

            CryptoHelper.Verify(identity.Type + identity.Value, validationToken, keyPair.PublicKey()).Should().BeTrue();
        }
    }
}