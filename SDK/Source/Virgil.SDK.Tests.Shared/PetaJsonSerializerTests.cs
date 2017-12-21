using System.Collections.Generic;
using Virgil.SDK.Common;
using Virgil.SDK.Web;

namespace Virgil.SDK.Tests
{
    using Bogus;
    using NUnit.Framework;

    [TestFixture]
    public class PetaJsonSerializerTests
    {
        private readonly Faker faker = new Faker();

        [Test]
        public void Serialize_Should_ConvertByteArrayToBase64String()
        {
            var rawCard = faker.RawCard();
            var serializer = new PetaJsonSerializer();
            var serializedRawCard = serializer.Serialize(rawCard);
            var snapshotBase64 = Bytes.ToString(rawCard.ContentSnapshot, StringEncoding.BASE64);

            Assert.IsTrue(serializedRawCard.Contains(snapshotBase64));
        }


        [Test]
        public void Serialize_Should_ConvertDictionary()
        {
            var dict = new Dictionary<string, string>();
            dict.Add("username", "anna");
            var serializer = new PetaJsonSerializer();
            var serializedDict = serializer.Serialize(dict);
            //var snapshotBase64 = Bytes.ToString(rawCard.ContentSnapshot, StringEncoding.BASE64);

            //Assert.IsTrue(serializedRawCard.Contains(snapshotBase64));
        }

        [Test]
        public void Deserialize_Should_ConvertBase64StringToByteArray()
        {
            const string cardRawJson = "{ \"id\": \"12345\", \"content_snapshot\":\"AQIDBAU=\" }";
            var serializer = new PetaJsonSerializer();
            var cardRaw = serializer.Deserialize<RawCard>(cardRawJson);

            Assert.AreEqual(cardRaw.ContentSnapshot, Bytes.FromString("AQIDBAU=", StringEncoding.BASE64));
        }


        [Test]
        public void Deserialize_Should_EquivalentToOrigin()
        {
            var rawCard = faker.RawCard();
            var serializer = new PetaJsonSerializer();
            var serializedRawCard = serializer.Serialize(rawCard);
            var deserializeRawCard = serializer.Deserialize<RawCard>(serializedRawCard);

            Assert.AreEqual(deserializeRawCard.ContentSnapshot, rawCard.ContentSnapshot);

            Assert.AreEqual(deserializeRawCard.Meta, rawCard.Meta);

            Assert.AreEqual(deserializeRawCard.Signatures, rawCard.Signatures);

            Assert.AreEqual(deserializeRawCard, rawCard);
        }
    }
}
