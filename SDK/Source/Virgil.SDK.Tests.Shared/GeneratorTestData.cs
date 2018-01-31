using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using NUnit.Framework;
using Virgil.SDK.Crypto;
using Virgil.SDK.Web;

namespace Virgil.SDK.Tests.Shared
{
    [TestFixture]
    class GeneratorTestData
    {
        private readonly Faker faker = new Faker();

        [Test]
        public void Prepair_TestData()
        {
            var model = faker.PredefinedRawSignedModel();
            var fullModel = faker.PredefinedRawSignedModel(
                "a666318071274adb738af3f67b8c7ec29d954de2cabfd71a942e6ea38e59fff9",
                true, true, true);
            var data = new Dictionary<string, string>
            {
                { "STC-1.as_string", model.ExportAsString()},
                { "STC-1.as_json", model.ExportAsJson()},
                { "STC-2.as_string", fullModel.ExportAsString()},
                { "STC-2.as_json", fullModel.ExportAsJson()}
            };

            var cardManager = faker.CardManager();
            var card = cardManager.ImportCardFromString(model.ExportAsString());

            data.Add("STC-3.as_string", cardManager.ExportCardAsString(card));
            data.Add("STC-3.as_json", cardManager.ExportCardAsJson(card));

            fullModel = faker.PredefinedRawSignedModel(null, true, true, true);
            var fullCard = cardManager.ImportCardFromString(fullModel.ExportAsString());

            data.Add("STC-4.as_string", cardManager.ExportCardAsString(fullCard));
            data.Add("STC-4.as_json", cardManager.ExportCardAsJson(fullCard));

            string apiPublicKeyId;
            string apiPublicKeyBase64;
            var token = faker.PredefinedToken(new VirgilAccessTokenSigner(), out apiPublicKeyId, out apiPublicKeyBase64);

            data.Add("STC-5.jwt", token.ToString());
            data.Add("STC-5.api_data_base64", apiPublicKeyBase64);
            data.Add("STC-5.api_key_id", apiPublicKeyId);


            System.IO.File.WriteAllText(@"C:\Users\Vasilina\Documents\test_data",
                Configuration.Serializer.Serialize(data));
        }
        /*

        [Test]
        public void Prepair_TestData()
        {
            var rawSignedModel = faker.PredefinedRawSignedModel();
            var cardManager = faker.CardManager();
            var card = cardManager.ImportCardFromString(rawSignedModel.ExportAsString());

            var fullrawSignedModel = faker.PredefinedRawSignedModel(null, true, true, true);
            var fullCard = cardManager.ImportCardFromString(fullrawSignedModel.ExportAsString());

            var data = new Dictionary<string, string>
            {
                { "3_as_string", cardManager.ExportCardAsString(card) },
                { "3_as_json", cardManager.ExportCardAsJson(card) },
                { "4_as_string", cardManager.ExportCardAsString(fullCard)},
                { "4_as_json", cardManager.ExportCardAsJson(fullCard)}
            };
            System.IO.File.WriteAllText(@"C:\Users\Vasilina\Documents\test_data_3_4",
                Configuration.Serializer.Serialize(data));
            System.IO.File.AppendAllText(@"C:\Users\Vasilina\Documents\raw_data_3", cardManager.ExportCardAsJson(card));
            System.IO.File.AppendAllText(@"C:\Users\Vasilina\Documents\raw_data_4", cardManager.ExportCardAsJson(fullCard));
        }*/
    }
}
