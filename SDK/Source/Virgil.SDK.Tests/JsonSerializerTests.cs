namespace Virgil.SDK.Tests
{

    using NUnit.Framework;

    using FluentAssertions;

    using Virgil.SDK.Client;
    using Virgil.SDK.Utils;

    [TestFixture]
    public class JsonSerializerTests
    {
        public object JsonValue { get; private set; }

     /*   [Test]
        public void Serialize_Should_ConvertByteArrayToBase64String()
        {
            var cardRaw = new CardRaw
            {
                Id = "[CARD_ID]",
                ContentSnapshot = new byte[] { 1, 2, 3, 4, 5 }
            };

            var serializer = new JsonSerializer();
            var serializedModel = serializer.Serialize(cardRaw);

            var modelJson = JsonValue.Parse(serializedModel);
            var snapshotValue = modelJson["content_snapshot"]
                .ToString().Replace("\"", "");

            snapshotValue.Should().Be(BytesConvert.ToBASE64String(cardRaw.ContentSnapshot));
        }*/

        [Test]
        public void Deserialize_Should_ConvertBase64StringToByteArray()
        {
            var cardRawJson = "{ \"id\": \"12345\", \"content_snapshot\":\"AQIDBAU=\" }";

			var serializer = new JsonSerializer();
            var cardRaw = serializer.Deserialize<CardRaw>(cardRawJson);

            cardRaw.ContentSnapshot.ShouldBeEquivalentTo(BytesConvert.FromBASE64String("AQIDBAU="));
		}
    }
}
