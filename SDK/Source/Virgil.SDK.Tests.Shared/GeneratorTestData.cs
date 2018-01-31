using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using NUnit.Framework;
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
            System.IO.File.WriteAllText(@"C:\Users\Vasilina\Documents\test_data",
                Configuration.Serializer.Serialize(data));
        }
    }
}
