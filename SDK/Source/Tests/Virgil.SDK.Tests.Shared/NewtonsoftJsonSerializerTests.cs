﻿using System.Collections.Generic;
using System.Linq;
using Virgil.SDK.Common;
using Virgil.SDK.Web;


namespace Virgil.SDK.Tests
{
    using Bogus;
    using NUnit.Framework;

    [TestFixture]
    public class NewtonsoftJsonSerializerTests
    {
        private readonly Faker faker = new Faker();

        [Test]
        public void Serialize_Should_ConvertByteArrayToBase64String()
        {
            var rawCard = faker.RawCard();
            var serializer = new NewtonsoftJsonSerializer();
            var serializedRawCard = serializer.Serialize(rawCard);
            var snapshotBase64 = Bytes.ToString(rawCard.ContentSnapshot, StringEncoding.BASE64);

            Assert.IsTrue(serializedRawCard.Contains(snapshotBase64));
        }


        [Test]
        public void Serialize_Should_ConvertDictionary()
        {
            var dict = new Dictionary<string, string>();
            dict.Add("username", "anna");
            dict.Add("passw", "mypassword");
            var serializer = new NewtonsoftJsonSerializer();
            var serializedDict = serializer.Serialize(dict);
            Assert.AreEqual(dict, serializer.Deserialize<Dictionary<string, string>>(serializedDict));

        }

        [Test]
        public void Deserialize_Should_ConvertBase64StringToByteArray()
        {
            const string cardRawJson = "{ \"id\": \"12345\", \"content_snapshot\":\"AQIDBAU=\" }";
            var serializer = new NewtonsoftJsonSerializer();
            var cardRaw = serializer.Deserialize<RawSignedModel>(cardRawJson);

            Assert.AreEqual(cardRaw.ContentSnapshot, Bytes.FromString("AQIDBAU=", StringEncoding.BASE64));
        }


        [Test]
        public void Deserialize_Should_EquivalentToOrigin()
        {
            var rawCard = faker.PredefinedRawSignedModel(null, true);
            var serializer = new NewtonsoftJsonSerializer();
            var serializedRawCard = serializer.Serialize(rawCard);
            var deserializeRawCard = serializer.Deserialize<RawSignedModel>(serializedRawCard);
            Assert.IsTrue(deserializeRawCard.ContentSnapshot.SequenceEqual(rawCard.ContentSnapshot));

            var first = deserializeRawCard.Signatures.First();
            var sec = rawCard.Signatures.First();
            Assert.AreEqual(first.Signature, sec.Signature);
            Assert.AreEqual(first.Snapshot, sec.Snapshot);
            Assert.IsTrue(deserializeRawCard.ContentSnapshot.SequenceEqual(rawCard.ContentSnapshot));
        }
    }
}
