namespace Virgil.SDK.Tests
{
    using System.Text;
    using FizzWare.NBuilder;
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

            var privateKey = crypto.GenerateKey();
            var exportedPublicKey = crypto.ExportPublicKey(privateKey);

            const string identity = "Alice";
            const string identityType = "member";

            var reqiest = CreateCardRequest.Create(identity, identityType, exportedPublicKey);
            
            var requestJson = Encoding.UTF8.GetString(reqiest.CanonicalRequest);
            var requestModel = JsonConvert.DeserializeObject<CardRequestModel>(requestJson);
            
            requestModel.Identity.ShouldBeEquivalentTo(identity);
            requestModel.IdentityType.ShouldBeEquivalentTo(identityType);
            requestModel.PublicKey.ShouldBeEquivalentTo(exportedPublicKey);
        }

        [Test]
        public void Create_GivenCardRequestModel_ShouldReturnCanonicalRequestEquivalentToPassedModel()
        {
            var crypto = new VirgilCrypto();
            var privateKey = crypto.GenerateKey();
            var exportedPublicKey = crypto.ExportPublicKey(privateKey);

            var originalRequestModel = Builder<CardRequestModel>.CreateNew()
                .With(it => it.PublicKey = exportedPublicKey)
                .Build();
            
            var request = CreateCardRequest.Create(originalRequestModel);

            var requestJson = Encoding.UTF8.GetString(request.CanonicalRequest);
            var requestModel = JsonConvert.DeserializeObject<CardRequestModel>(requestJson);

            originalRequestModel.ShouldBeEquivalentTo(requestModel);
        }

        [Test]
        public void Create_GivenAnyParameters_ShouldCreateRequestWithApplicationScope()
        {
            var crypto = new VirgilCrypto();

            var privateKey = crypto.GenerateKey();
            var exportedPublicKey = crypto.ExportPublicKey(privateKey);

            const string identity = "Alice";
            const string identityType = "member";

            var request = CreateCardRequest.Create(identity, identityType, exportedPublicKey);

            request.Scope.Should().Be(CardScope.Application);
        }

        [Test]
        public void CreateGlobal_GivenAnyParameters_ShouldCreateRequestWithGlobalScope()
        {
            var crypto = new VirgilCrypto();

            var privateKey = crypto.GenerateKey();
            var exportedPublicKey = crypto.ExportPublicKey(privateKey);

            const string identity = "Alice";
            var request = CreateCardRequest.CreateGlobal(identity, exportedPublicKey);

            request.Scope.Should().Be(CardScope.Global);
        }

        [Test]
        public void Export_WithoutParameters_ShouldReturnStringRepresentationOfRequest()
        {
            var crypto = new VirgilCrypto();

            var privateKey = crypto.GenerateKey();
            var exportedPublicKey = crypto.ExportPublicKey(privateKey);

            const string identity = "Alice";
            var request = CreateCardRequest.CreateGlobal(identity, exportedPublicKey);
            var fingerprint = crypto.CalculateFingerprint(request.CanonicalRequest);
            var signature = crypto.SignFingerprint(fingerprint, privateKey);

            request.AppendSignature(fingerprint, signature);

            var exportedRequest = request.Export();
        }
    }
}