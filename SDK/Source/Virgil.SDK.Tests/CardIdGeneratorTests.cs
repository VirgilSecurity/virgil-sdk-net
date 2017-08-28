namespace Virgil.SDK.Tests
{
    using NUnit.Framework;
    using FluentAssertions;
    using NSubstitute;
    using Virgil.CryptoApi;
    using Virgil.SDK.Common;

    [TestFixture]
    public class CardIdGeneratorTests
    {
        [Test]
        public void Generate_Should_ReturnHexStringOfCalculatedFingerprint()
        {
            var crypto = NSubstitute.Substitute.For<ICrypto>();
            
            var fingerprint = TestUtils.RandomBytes();
            crypto.CalculateFingerprint(Arg.Any<byte[]>()).Returns(it => fingerprint);
            
            var payload = TestUtils.RandomBytes();
            var id = CardUtils.GenerateCardId(crypto, payload);

            id.Should().Be(BytesConvert.ToString(fingerprint, StringEncoding.HEX));
        }
    }
}