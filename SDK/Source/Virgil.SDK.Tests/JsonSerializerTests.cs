namespace Virgil.SDK.Tests
{
    using System.Json;
    using NUnit.Framework;

    using FluentAssertions;
    using Virgil.SDK.Common;
    using Virgil.SDK.Web;

    [TestFixture]
    public class JsonSerializerTests
    {
        [Test]
        public void Serialize_Should_ConvertByteArrayToBase64String()
        {
            var cardRaw = new RawCard
            {
                ContentSnapshot = new byte[] { 1, 2, 3, 4, 5 }
            };

            var serializer = new JsonSerializer();
            var serializedModel = serializer.Serialize(cardRaw);

            var modelJson = JsonValue.Parse(serializedModel);
            var snapshotValue = modelJson["content_snapshot"]
                .ToString().Replace("\"", "");

            snapshotValue.Should().Be(BytesConvert.ToString(cardRaw.ContentSnapshot, StringEncoding.BASE64));
        }

        [Test]
        public void Deserialize_Should_ConvertBase64StringToByteArray()
        {
            const string cardRawJson = "{ \"id\": \"12345\", \"content_snapshot\":\"AQIDBAU=\" }";

            var serializer = new JsonSerializer();
            var cardRaw = serializer.Deserialize<RawCard>(cardRawJson);

            cardRaw.ContentSnapshot.ShouldBeEquivalentTo(BytesConvert.FromString("AQIDBAU=", StringEncoding.BASE64));
        }
    }
}
