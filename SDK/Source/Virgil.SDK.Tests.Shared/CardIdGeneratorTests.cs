using System.Linq;
using Virgil.CryptoImpl;
using Virgil.SDK.Common;

namespace Virgil.SDK.Tests
{
    using Bogus;
    using NUnit.Framework;

    [TestFixture]
    public class CardIdGeneratorTests
    {
        private readonly Faker faker = new Faker();
        
        [Test]
        public void Generate_Should_ReturnHexStringOf32SHA512()
        { 
            var cardCrypto = new VirgilCardCrypto();
            var payload = this.faker.Random.Bytes(64);
            var sha512 = cardCrypto.GenerateSHA512(payload);
            var id = Bytes.ToString(sha512.Take(32).ToArray(), StringEncoding.HEX);

            Assert.AreEqual(id, CardUtils.GenerateCardId(cardCrypto, payload));
        }
    }
}