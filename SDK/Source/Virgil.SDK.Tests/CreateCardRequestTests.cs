namespace Virgil.SDK.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using FluentAssertions;

    using Newtonsoft.Json;
    using NUnit.Framework;

    using Virgil.SDK.Client;
    using Virgil.SDK.Client.Models;
    using Virgil.SDK.Cryptography;

    public class CreateCardRequestTests
    {
        [Test]
        public void Create_GivenIdentityAndTypeAndPublicKey_ShouldReturnCanonicalRequestWithPassedParameters()
        {
            var crypto = new VirgilCrypto();

            var keyPair = crypto.GenerateKeys();
            var exportedPublicKey = crypto.ExportPublicKey(keyPair.PublicKey);

            const string identity = "alice";
            const string identityType = "member";

            var request = new RequestSigner<CardCreateRequest>(crypto);
            request.Initialize(new CardCreateRequest
            {
                Identity = identity,
                IdentityType = identityType,
                PublicKey = exportedPublicKey
            });
            
            var requestJson = Encoding.UTF8.GetString(request.RequestSnapshot);
            var requestModel = JsonConvert.DeserializeObject<CardCreateRequest>(requestJson);
            
            requestModel.Identity.ShouldBeEquivalentTo(identity);
            requestModel.IdentityType.ShouldBeEquivalentTo(identityType);
            requestModel.PublicKey.ShouldBeEquivalentTo(exportedPublicKey);
        }
        
        [Test]
        public void Export_WithoutParameters_ShouldReturnStringRepresentationOfRequest()
        {
            var crypto = new VirgilCrypto();

            var aliceKeys = crypto.GenerateKeys();
            var exportedPublicKey = crypto.ExportPublicKey(aliceKeys.PublicKey);

            const string identity = "alice";
            const string identityType = "member";

            var request = new RequestSigner<CardCreateRequest>(crypto);
            request.Initialize(new CardCreateRequest
            {
                Identity = identity,
                IdentityType = identityType,
                PublicKey = exportedPublicKey
            });

            request.SelfSign(aliceKeys.PrivateKey);

            var exportedRequest = request.Export();

            var jsonData = Convert.FromBase64String(exportedRequest);
            var json = Encoding.UTF8.GetString(jsonData);
            var model = JsonConvert.DeserializeObject<SignedRequest>(json);

            model.ContentSnapshot.ShouldBeEquivalentTo(request.RequestSnapshot);
            model.Meta.Signs.ShouldAllBeEquivalentTo(request.Signs);
        }

        [Test]
        public void Export_WithoutParameters_ShouldBeEquivalentToImportedRequest()
        {
            var crypto = new VirgilCrypto();

            var aliceKeys = crypto.GenerateKeys();
            var exportedPublicKey = crypto.ExportPublicKey(aliceKeys.PublicKey);

            const string identity = "alice";
            const string identityType = "member";

            var request = new RequestSigner<CardCreateRequest>(crypto);
            var details = new CardCreateRequest
            {
                Identity = identity,
                IdentityType = identityType,
                PublicKey = exportedPublicKey,
                Data = new Dictionary<string, string>
                {
                    ["key1"] = "value1",
                    ["key2"] = "value2"
                },
                Info = new CardInfo
                {
                    Device = "Device",
                    DeviceName = "DeviceName"
                }
            };

            request.Initialize(details);
            
            request.SelfSign(aliceKeys.PrivateKey);

            var exportedRequest = request.Export();
            var importedRequest = new RequestSigner<CardCreateRequest>(crypto);
            importedRequest.Initialize(exportedRequest);

            details.ShouldBeEquivalentTo(importedRequest.GetRequestDetails());
        }
    }
}