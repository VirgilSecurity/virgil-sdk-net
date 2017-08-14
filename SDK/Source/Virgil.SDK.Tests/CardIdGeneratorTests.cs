namespace Virgil.SDK.Tests
{
    using NUnit.Framework;
    using FluentAssertions;
    using NSubstitute;
    
    using Virgil.Crypto.Interfaces;
    using Virgil.SDK.Utils;
    
    [TestFixture]
    public class CardIdGeneratorTests
    {
        [Test]
        public void Generate_Should_ReturnHexStringOfCalculatedFingerprint()
        {
            var crypto = NSubstitute.Substitute.For<ICrypto>();
            var generator = new CardIdGenerator();

            var fingerprint = TestUtils.RandomBytes();
            crypto.CalculateFingerprint(Arg.Any<byte[]>()).Returns(it => fingerprint);
            
            var payload = TestUtils.RandomBytes();
            var id = generator.Generate(crypto, payload);

            id.Should().Be(BytesConvert.ToString(fingerprint, StringEncoding.HEX));
        }
    }
}