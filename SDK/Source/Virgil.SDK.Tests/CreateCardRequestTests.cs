namespace Virgil.SDK.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using FluentAssertions;

    using Newtonsoft.Json;
    using NUnit.Framework;

    using Virgil.SDK.Client;
    using Virgil.SDK.Cryptography;

    public class CreateCardRequestTests
    {
        [Test]
        public void Ctor_RequestDetailsGiven_ShouldSnapshotBeEquivalentToSerializedDetails()
        {
            var crypto = new VirgilCrypto();

            var keyPair = crypto.GenerateKeys();
            var exportedPublicKey = crypto.ExportPublicKey(keyPair.PublicKey);

            const string identity = "alice";
            const string identityType = "member";

            var request = new CreateCardRequest(identity, identityType, exportedPublicKey);
            
            var requestJson = Encoding.UTF8.GetString(request.Snapshot);
            var requestModel = JsonConvert.DeserializeObject<CreateCardModel>(requestJson);
            
            requestModel.Identity.ShouldBeEquivalentTo(identity);
            requestModel.IdentityType.ShouldBeEquivalentTo(identityType);
            requestModel.PublicKey.ShouldBeEquivalentTo(exportedPublicKey);
        }

        [Test]
        public void Ctor_NullAsParameterGiven_ShouldSnapshotBeEquivalentToSerializedDetails()
        {
            var crypto = new VirgilCrypto();

            var keyPair = crypto.GenerateKeys();
            var exportedPublicKey = crypto.ExportPublicKey(keyPair.PublicKey);

            const string identity = "alice";
            const string identityType = "member";

            var request = new CreateCardRequest(identity, identityType, exportedPublicKey);

            var requestJson = Encoding.UTF8.GetString(request.Snapshot);
            var requestModel = JsonConvert.DeserializeObject<CreateCardModel>(requestJson);

            requestModel.Identity.ShouldBeEquivalentTo(identity);
            requestModel.IdentityType.ShouldBeEquivalentTo(identityType);
            requestModel.PublicKey.ShouldBeEquivalentTo(exportedPublicKey);
        }

        [Test]
        public void Export_WithoutParameters_ShouldReturnStringRepresentationOfRequest()
        {
            var crypto = new VirgilCrypto();
            var requestSigner = new RequestSigner(crypto);

            var aliceKeys = crypto.GenerateKeys();
            var exportedPublicKey = crypto.ExportPublicKey(aliceKeys.PublicKey);

            const string identity = "alice";
            const string identityType = "member";

            var request = new CreateCardRequest(identity, identityType, exportedPublicKey);

            requestSigner.SelfSign(request, aliceKeys.PrivateKey);

            var exportedRequest = request.Export();

            var jsonData = Convert.FromBase64String(exportedRequest);
            var json = Encoding.UTF8.GetString(jsonData);
            var model = JsonConvert.DeserializeObject<SignedRequestModel>(json);

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
            const string identityType = "member";
            
            var request = new CreateCardRequest
            (
                identity, 
                identityType, 
                exportedPublicKey,
                new Dictionary<string, string>
                {
                    ["key1"] = "value1",
                    ["key2"] = "value2"
                },
                new CardInfoModel
                {
                    Device = "Device",
                    DeviceName = "DeviceName"
                }
            );

            var requestSigner = new RequestSigner(crypto);
            requestSigner.SelfSign(request, aliceKeys.PrivateKey);
            
            var exportedRequest = request.Export();
            var importedRequest = SignableRequest.Import<CreateCardRequest>(exportedRequest);

            request.ShouldBeEquivalentTo(importedRequest);
        }
    }
}