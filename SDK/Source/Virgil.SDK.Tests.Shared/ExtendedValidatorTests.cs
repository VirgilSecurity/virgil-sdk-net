using System.Configuration;
using FluentAssertions;
using NSubstitute;
using Virgil.SDK.Common;
using Virgil.SDK.Validation;

namespace Virgil.SDK.Tests
{
    using System.Collections.Generic;
    using Bogus;
    using Virgil.Crypto;
  
    using NUnit.Framework;
    using Virgil.CryptoApi;

    [TestFixture]
    public class ExtendedValidatorTests
    {
        private readonly Faker faker = new Faker();
        private static string AppCardId = ConfigurationManager.AppSettings["virgil:AppID"];
        private static string AccounId = ConfigurationManager.AppSettings["virgil:AccountID"];
        private static string AppPrivateKeyBase64 = ConfigurationManager.AppSettings["virgil:AppPrivateKeyBase64"];
        private static string ServiceCardId = ConfigurationManager.AppSettings["virgil:ServiceCardId"];
        private static string ServicePublicKeyPemBase64 = ConfigurationManager.AppSettings["virgil:ServicePublicKeyPemBase64"];

        [Test]
        public void Validate_ShouldIgnoreSelfSignature_IfPropertySetToTrue()
        {
            var crypto = Substitute.For<ICardManagerCrypto>();
            var validator = new ExtendedValidator();
            var card = this.faker.Card();
            validator.IgnoreSelfSignature = true;
            crypto.VerifySignature(card.Fingerprint, card.Signatures[0].Signature, card.PublicKey).Returns(false);
            crypto.VerifySignature(card.Fingerprint, card.Signatures[1].Signature, Arg.Any<IPublicKey>()).Returns(true);
            var result = validator.Validate(crypto, card);
            result.Should().BeTrue();
        }
        
        [Test]
        public void Validate_ShouldIgnoreVirgilSignature_IfPropertySetToTrue()
        {
            var crypto = Substitute.For<ICardManagerCrypto>();
            var validator = new ExtendedValidator();
            var card = this.faker.Card();
            validator.IgnoreVirgilSignature = true;
            crypto.VerifySignature(card.Fingerprint, card.Signatures[0].Signature, card.PublicKey).Returns(true);
            crypto.VerifySignature(card.Fingerprint, card.Signatures[1].Signature, Arg.Any<IPublicKey>()).Returns(false);
            var result = validator.Validate(crypto, card);
            result.Should().BeTrue();
        }
        
        [Test]
        public void Validate_ShouldNotValidateSelfAndVirgilSignatures_IfBothPropertiesSetToTrue()
        {
            var crypto = Substitute.For<ICardManagerCrypto>();
            var validator = new ExtendedValidator();
            var card = this.faker.Card();
            crypto.VerifySignature(card.Fingerprint, card.Signatures[0].Signature, card.PublicKey).Returns(false);
            crypto.VerifySignature(card.Fingerprint, card.Signatures[1].Signature, Arg.Any<IPublicKey>()).Returns(false);

            validator.IgnoreVirgilSignature = true;
            validator.IgnoreSelfSignature = true;
            var result = validator.Validate(crypto, card);
            result.Should().BeTrue();
        }
        
        [Test]
        public void Validate_ShouldReturnSuccess_IfSpecifiedWhitelistSignersAreValid()
        {
            var crypto = Substitute.For<ICardManagerCrypto>();
            var validator = new ExtendedValidator();
            var signer = this.faker.SignerAndSignature();
            var signerInfo = signer.Item1;
            var signerSignature = signer.Item2;
            var card = this.faker.Card(false, false, new List<CardSignature> { signerSignature });
            crypto.VerifySignature(card.Fingerprint, signerSignature.Signature, Arg.Any<IPublicKey>()).Returns(true);

            validator.IgnoreVirgilSignature = true;
            validator.IgnoreSelfSignature = true;
            validator.Whitelist = new[] { signerInfo };
            var result = validator.Validate(crypto, card);
            result.Should().BeTrue();
        }

        [Test]
        public void Validate_ShouldValidate()
        {
            var crypto = new VirgilCardManagerCrypto();
            var validator = new ExtendedValidator();
            validator.IgnoreVirgilSignature = true;
            validator.ChangeServiceCreds(ServiceCardId, ServicePublicKeyPemBase64);

            var appPrivateKey = crypto.ImportPrivateKey(
                Bytes.FromString(AppPrivateKeyBase64, StringEncoding.BASE64));

            var appPublicKey = Bytes.ToString(crypto.ExportPublicKey(crypto.ExtractPublicKey(appPrivateKey)), StringEncoding.BASE64);
            var list = new List<SignerInfo>
            {
                new SignerInfo() { CardId = AppCardId, PublicKeyBase64 = appPublicKey }
            };
            validator.Whitelist = list;
            var keypair = crypto.GenerateKeys();

            var csr = CSR.Generate(crypto, new CSRParams
            {
                Identity = "some_identity",
                PublicKey = crypto.ExtractPublicKey(keypair.PrivateKey),
                PrivateKey = keypair.PrivateKey
            });


            csr.Sign(crypto, new SignParams
            {
                SignerCardId = AppCardId,
                SignerType = SignerType.App,
                SignerPrivateKey = appPrivateKey
            });

            var card = Card.Parse(crypto, csr.RawCard);
           
            var result = validator.Validate(crypto, card);
            result.Should().BeTrue();
        }
}
}