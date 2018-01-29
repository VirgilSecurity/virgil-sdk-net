using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using FluentAssertions;
using NUnit.Framework;
using Virgil.Crypto;
using Virgil.SDK.Common;
using Virgil.SDK.Signer;
using Virgil.SDK.Web;

namespace Virgil.SDK.Tests.Shared
{
    [TestFixture]
    class RawSignedModelTests
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
                { "1_as_string", model.ExportAsString() },
                { "1_as_json", model.ExportAsJson() },
                { "2_as_string", fullModel.ExportAsString()},
                { "2_as_json", fullModel.ExportAsJson()}
            };
            System.IO.File.WriteAllText(@"C:\Users\Vasilina\Documents\test_data_1_2", 
                Configuration.Serializer.Serialize(data));
            System.IO.File.WriteAllText(@"C:\Users\Vasilina\Documents\raw_data_1", model.ExportAsJson());
            System.IO.File.WriteAllText(@"C:\Users\Vasilina\Documents\raw_data_2", fullModel.ExportAsJson());
        }

        [Test]
        public void GeneratePureModelFromString_Should_ReturnEquivalentModel()
        {
            var model = faker.PredefinedRawSignedModel();
            var exportedStr = model.ExportAsString();

            var importedModel = RawSignedModel.GenerateFromString(exportedStr);
            importedModel.ShouldBeEquivalentTo(model);

        }

        [Test]
        public void Export_WithEmptyPreviousCardId_ShouldNot_HavePreviousCardIdInJson()
        {
            var model = faker.PredefinedRawSignedModel();
            var exportedStr = model.ExportAsJson();

            exportedStr.Contains("previous_card_id").ShouldBeEquivalentTo(false);
        }


        [Test]
        public void GeneratePureModelFromJson_Should_ReturnEquivalentModel()
        {
            var model = faker.PredefinedRawSignedModel();
            var exportedJson = model.ExportAsJson();

            var importedModel = RawSignedModel.GenerateFromJson(exportedJson);
            importedModel.ShouldBeEquivalentTo(model);
        }

        [Test]
        public void GenerateFullModelFromString_Should_ReturnEquivalentModel()
        {
            var model = faker.PredefinedRawSignedModel(
                "a666318071274adb738af3f67b8c7ec29d954de2cabfd71a942e6ea38e59fff9",
                true, true, true);
 
            var exportedStr = model.ExportAsString();
            var importedModel = RawSignedModel.GenerateFromString(exportedStr);
            importedModel.ShouldBeEquivalentTo(model);
        }

        [Test]
        public void GenerateFullModelFromJson_Should_ReturnEquivalentModel()
        {
            var model = faker.PredefinedRawSignedModel(
                "a666318071274adb738af3f67b8c7ec29d954de2cabfd71a942e6ea38e59fff9",
                true, true, true);
            var exportedJson = model.ExportAsJson();
            var importedModel = RawSignedModel.GenerateFromJson(exportedJson);
            importedModel.ShouldBeEquivalentTo(model);
        }

    }
}
