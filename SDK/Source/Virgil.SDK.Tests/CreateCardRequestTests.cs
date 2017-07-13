namespace Virgil.SDK.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using FluentAssertions;

    using Newtonsoft.Json;
    using NUnit.Framework;

    using Virgil.SDK.Common;
    using Virgil.SDK.Client;
    using Virgil.SDK.Cryptography;
    using Client.Requests;
    using Client.Models;

    public class CreateCardRequestTests
    {
        [Test]
        public void Ctor_RequestDetailsGiven_ShouldSnapshotBeEquivalentToSerializedDetails()
        {
            var crypto = new VirgilCrypto();

            var keyPair = crypto.GenerateKeys();
            var exportedPublicKey = crypto.ExportPublicKey(keyPair.PublicKey);

            const string identity = "alice";
            const string identityType = "user";

            var request = new CreateUserCardRequest()
            {
                Identity = identity,
                PublicKeyData = exportedPublicKey,
                CustomFields = new Dictionary<string, string>
                {
                    ["key1"] = "value1",
                    ["key2"] = "value2"
                }
            };
            
            var requestJson = Encoding.UTF8.GetString(request.Snapshot);
            var requestModel = JsonConvert.DeserializeObject<PublishCardSnapshotModel>(requestJson);
            
            requestModel.Identity.ShouldBeEquivalentTo(identity);
            requestModel.IdentityType.ShouldBeEquivalentTo(identityType);
            requestModel.PublicKeyData.ShouldBeEquivalentTo(exportedPublicKey);
        }

        [Test]
        public void Ctor_NullAsParameterGiven_ShouldSnapshotBeEquivalentToSerializedDetails()
        {
            var crypto = new VirgilCrypto();

            var keyPair = crypto.GenerateKeys();
            var exportedPublicKey = crypto.ExportPublicKey(keyPair.PublicKey);

            const string identity = "alice";
            const string identityType = "user";

            var request = new CreateUserCardRequest() {
                Identity = identity,
                PublicKeyData = exportedPublicKey,
                CustomFields = new Dictionary<string, string>
                {
                    ["key1"] = "value1",
                    ["key2"] = "value2"
                }
            };

            var requestJson = Encoding.UTF8.GetString(request.Snapshot);
            var requestModel = JsonConvert.DeserializeObject<PublishCardSnapshotModel>(requestJson);

            requestModel.Identity.ShouldBeEquivalentTo(identity);
            requestModel.IdentityType.ShouldBeEquivalentTo(identityType);
            requestModel.PublicKeyData.ShouldBeEquivalentTo(exportedPublicKey);
        }

        [Test]
        public void Export_WithoutParameters_ShouldReturnStringRepresentationOfRequest()
        {
            var crypto = new VirgilCrypto();

            var aliceKeys = crypto.GenerateKeys();
            var exportedPublicKey = crypto.ExportPublicKey(aliceKeys.PublicKey);

            const string identity = "alice";

            var request = new CreateUserCardRequest() {
                Identity = identity,
                PublicKeyData = exportedPublicKey,
                CustomFields = new Dictionary<string, string>
                {
                    ["key1"] = "value1",
                    ["key2"] = "value2"
                }
            };

            request.SelfSign(crypto, aliceKeys.PrivateKey);
            var exportedRequest = request.Export();

            var jsonData = Convert.FromBase64String(exportedRequest);
            var json = Encoding.UTF8.GetString(jsonData);
            var model = JsonConvert.DeserializeObject<SignableRequestModel>(json);

            model.ContentSnapshot.ShouldBeEquivalentTo(request.Snapshot);
            model.Meta.Signatures.ShouldAllBeEquivalentTo(request.Signatures);
        }

        [Test]
        public void Export_WithoutParameters_ShouldBeEquivalentToImportedRequest()
        {
            var crypto = new VirgilCrypto();

            var aliceKeys = crypto.GenerateKeys();
            var exportedPublicKey = crypto.ExportPublicKey(aliceKeys.PublicKey);

            const string identity = "alice";

            var request = new CreateUserCardRequest() {
                Identity = identity,
                PublicKeyData = exportedPublicKey,
                CustomFields = new Dictionary<string, string>
                {
                    ["key1"] = "value1",
                    ["key2"] = "value2"
                },

                Info = new CardInfoModel
                {
                    Device = "Device",
                    DeviceName = "DeviceName"
                }
            };

            request.SelfSign(crypto, aliceKeys.PrivateKey);
            
            var exportedRequest = request.Export();
            var importedRequest = new CreateUserCardRequest();
            importedRequest.Import(exportedRequest);

            request.ShouldBeEquivalentTo(importedRequest);
        }
    }
}