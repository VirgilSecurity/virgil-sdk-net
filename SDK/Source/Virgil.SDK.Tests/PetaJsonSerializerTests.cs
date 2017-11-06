namespace Virgil.SDK.Tests
{
    using System.Collections.Generic;
    using Bogus;
    using Crypto;
    using CryptoApi;
    using FluentAssertions;
    using NUnit.Framework;
    
    using Virgil.SDK.Common;
    using Virgil.SDK.Web;
    using Newtonsoft.Json;

    [TestFixture]
    public class PetaJsonSerializerTests
    {
        private readonly Faker faker = new Faker();

        [Test]
        public void Serialize_Should_ConvertByteArrayToBase64String()
        {
            var rawCard = CreateRawCard();
            var serializer = new PetaJsonSerializer();
            var serializedRawCard = serializer.Serialize(rawCard);
            var snapshotBase64 = Bytes.ToString(rawCard.ContentSnapshot, StringEncoding.BASE64);

            Assert.IsTrue(serializedRawCard.Contains(snapshotBase64));
        }

        [Test]
        public void Deserialize_Should_ConvertBase64StringToByteArray()
        {
            const string cardRawJson = "{ \"id\": \"12345\", \"content_snapshot\":\"AQIDBAU=\" }";
            var serializer = new PetaJsonSerializer();
            var cardRaw = serializer.Deserialize<RawCard>(cardRawJson);

            cardRaw.ContentSnapshot.ShouldBeEquivalentTo(Bytes.FromString("AQIDBAU=", StringEncoding.BASE64));
        }

        [Test]
        public void Deserialize_Should_EquivalentToOrigin()
        {
            var rawCard = CreateRawCard();
            var serializer = new PetaJsonSerializer();
            var serializedRawCard = serializer.Serialize(rawCard);
            var deserializeRawCard = serializer.Deserialize<RawCard>(serializedRawCard);

            deserializeRawCard.ContentSnapshot.ShouldBeEquivalentTo(rawCard.ContentSnapshot);

            deserializeRawCard.Meta.ShouldBeEquivalentTo(rawCard.Meta);

            deserializeRawCard.Signatures.ShouldBeEquivalentTo(rawCard.Signatures);

            deserializeRawCard.ShouldBeEquivalentTo(rawCard);
        }

        private RawCard CreateRawCard()
        {
            var crypto = new VirgilCrypto();

            var keypair = crypto.GenerateKeys();

            var csr = CSR.Generate(crypto, new CSRParams
            {
                Identity = "Alice",
                PublicKey = keypair.PublicKey,
                PrivateKey = keypair.PrivateKey
            });
            return csr.RawCard;
        }

    }
}
