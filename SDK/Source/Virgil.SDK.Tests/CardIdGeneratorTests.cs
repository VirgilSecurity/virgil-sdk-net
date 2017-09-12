namespace Virgil.SDK.Tests
{
    using Bogus;
    using NUnit.Framework;
    using FluentAssertions;
    using NSubstitute;
    using Virgil.CryptoApi;
    using Virgil.SDK.Common;

    [TestFixture]
    public class CardIdGeneratorTests
    {
        private readonly Faker faker = new Faker();
        
        [Test]
        public void Generate_Should_ReturnHexStringOfCalculatedFingerprint()
        {
            var crypto = Substitute.For<ICrypto>();
            
            var fingerprint = this.faker.Random.Bytes(32);
            crypto.CalculateFingerprint(Arg.Any<byte[]>()).Returns(it => fingerprint);
            
            var payload = this.faker.Random.Bytes(64);;
            var id = CardUtils.GenerateCardId(crypto, payload);

            id.Should().Be(Bytes.ToString(fingerprint, StringEncoding.HEX));
        }
    }
}