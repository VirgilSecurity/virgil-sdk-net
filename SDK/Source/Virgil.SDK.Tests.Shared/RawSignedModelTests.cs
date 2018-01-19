using System;
using System.Collections.Generic;
using System.Text;
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
        [Test]
        public void GeneratePureModelFromString_Should_ReturnEquivalentModel()
        {
            var model = CreateRawSignedModel();
            var exportedStr = model.ExportAsString();

            var importedModel = RawSignedModel.GenerateFromString(exportedStr);
            importedModel.ShouldBeEquivalentTo(model);
           
        }

        [Test]
        public void Export_WithEmptyPreviousCardId_ShouldNot_HavePreviousCardIdInJson()
        {
            var model = CreateRawSignedModel();
            var exportedStr = model.ExportAsJson();
            exportedStr.Contains("previous_card_id").ShouldBeEquivalentTo(false);
        }

        private static RawSignedModel CreateRawSignedModel(string previousCardId = null)
        {
            var dateTime = DateTimeOffset.FromUnixTimeSeconds(Int64.Parse("1515686245")).DateTime;
            var rawCardContent = new RawCardContent()
            {
                CreatedAt = dateTime,
                Identity = "test",
                PublicKey = Bytes.FromString("MCowBQYDK2VwAyEA3J0Ivcs4/ahBafrn6mB4t+UI+IBhWjC/toVDrPJcCZk="),
                Version = "5.0",
                PreviousCardId = previousCardId
            };
            var model = new RawSignedModel() {ContentSnapshot = SnapshotUtils.TakeSnapshot(rawCardContent)};
            return model;
        }
        
        [Test]
        public void GeneratePureModelFromJson_Should_ReturnEquivalentModel()
        {
            var model = CreateRawSignedModel();
            var exportedJson = model.ExportAsJson();

            var importedModel = RawSignedModel.GenerateFromJson(exportedJson);
            importedModel.ShouldBeEquivalentTo(model);
        }
        
        [Test]
        public void GenerateFullModelFromString_Should_ReturnEquivalentModel()
        {
            var crypto = new VirgilCrypto();
            var myKeyPair = crypto.GenerateKeys();

;           var model = CreateRawSignedModel("a666318071274adb738af3f67b8c7ec29d954de2cabfd71a942e6ea38e59fff9");
            var signer = new ModelSigner(new VirgilCardCrypto());
            signer.SelfSign(model, myKeyPair.PrivateKey);

            var virgilKeyPair = crypto.GenerateKeys();
           
            signer.Sign(model, new SignParams()
            {
                SignerId = "virgil_card_id",
                SignerType = SignerType.Virgil.ToLowerString(),
                SignerPrivateKey = virgilKeyPair.PrivateKey
            },
                new Dictionary<string, string>
                {
                    { "additional_field1", "some_val" }
                }
            );

            signer.Sign(model, new SignParams()
            {
                SignerId = "some_signer_id",
                SignerType = SignerType.Extra.ToLowerString(),
                SignerPrivateKey = virgilKeyPair.PrivateKey
            });
            var exportedStr = model.ExportAsString();

            var importedModel = RawSignedModel.GenerateFromString(exportedStr);
            importedModel.ShouldBeEquivalentTo(model);
        }

    }
}
