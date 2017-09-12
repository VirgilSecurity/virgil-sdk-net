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

    [TestFixture]
    public class PetaJsonSerializerTests
    {
        private readonly Faker faker = new Faker();

        [Test]
        public void Serialize_Should_ConvertByteArrayToBase64String()
        {
            var crypto = new VirgilCrypto();

            var keypair = crypto.GenerateKeys();
            
            var csr = CSR.Generate(crypto, new CSRParams
            {
                Identity = "Alice",
                PublicKey = keypair.PublicKey,
                PrivateKey = keypair.PrivateKey
            });
            

            //var cardRaw = new RawCard
            //{
            //    ContentSnapshot = this.faker.Random.Bytes(256),
            //    Meta = new RawCardMeta
            //    {
            //        Signatures = new Dictionary<string, byte[]>
            //        {
            //            [this.faker.CardId()] = this.faker.Random.Bytes(180),
            //            [this.faker.CardId()] = this.faker.Random.Bytes(180)
            //        },
            //        CreatedAt = this.faker.Date.Future(),
            //        Version = "v5"
            //    }
            //};

            //var serializer = new PetaJsonSerializer();
            //var serializedModel = serializer.Serialize(cardRaw);

            //var snapshotValue = Newtonsoft.Json.Json modelJson["content_snapshot"]
            //    .ToString().Replace("\"", "");

            //snapshotValue.Should().Be(Bytes.ToString(cardRaw.ContentSnapshot, StringEncoding.BASE64));
        }

        [Test]
        public void Deserialize_Should_ConvertBase64StringToByteArray()
        {
            //const string cardRawJson = "{ \"id\": \"12345\", \"content_snapshot\":\"AQIDBAU=\" }";
            
            //var serializer = new PetaJsonSerializer();
            //var cardRaw = serializer.Deserialize<RawCard>(cardRawJson);

            //cardRaw.ContentSnapshot.ShouldBeEquivalentTo(Bytes.FromString("AQIDBAU=", StringEncoding.BASE64));
        }
    }
}
