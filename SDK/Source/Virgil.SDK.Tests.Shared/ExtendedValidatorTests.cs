using System.Configuration;
using FluentAssertions;
using NSubstitute;
using Virgil.SDK.Common;
using Virgil.SDK.Signer;
using Virgil.SDK.Validation;

namespace Virgil.SDK.Tests
{
    using System.Collections.Generic;
    using Bogus;
    using Virgil.Crypto;
  
    using NUnit.Framework;
    using Virgil.CryptoAPI;

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
        public void Validate_ShouldIgnoreSelfSignature_IfPropertyDoesntSetToTrue()
        {
            var crypto = Substitute.For<ICardCrypto>();
            var validator = new VirgilCardVerifier(crypto);
            var card = this.faker.Card(false);
            validator.VerifyVirgilSignature = true;
            crypto.VerifySignature(card.Signatures[0].Signature, card.Fingerprint, Arg.Any<IPublicKey>()).Returns(true);
            var result = validator.VerifyCard(card);
            result.Should().BeTrue();
        }
        
        [Test]
        public void Validate_ShouldIgnoreVirgilSignature_IfPropertyDoesntSetToTrue()
        {
            var crypto = Substitute.For<ICardCrypto>();
            var validator = new VirgilCardVerifier(crypto);
            var card = this.faker.Card(true, false);
            validator.VerifySelfSignature = true;
            crypto.VerifySignature(card.Signatures[0].Signature, card.Fingerprint, card.PublicKey).Returns(true);
            var result = validator.VerifyCard(card);
            result.Should().BeTrue();
        }
        
        [Test]
        public void Validate_ShouldNotValidateSelfAndVirgilSignatures_IfBothPropertiesDoesntSetToTrue()
        {
            var crypto = Substitute.For<ICardCrypto>();
            var validator = new VirgilCardVerifier(crypto);
            var card = this.faker.Card();
            crypto.VerifySignature(card.Signatures[0].Signature, card.Fingerprint, card.PublicKey).Returns(false);
            crypto.VerifySignature(card.Signatures[1].Signature, card.Fingerprint, Arg.Any<IPublicKey>()).Returns(false);

            var result = validator.VerifyCard(card);
            result.Should().BeTrue();
        }
        
        [Test]
        public void Validate_ShouldReturnSuccess_IfSpecifiedWhitelistSignersAreValid()
        {
            var crypto = Substitute.For<ICardCrypto>();
            var validator = new VirgilCardVerifier(crypto);
            var signer = this.faker.SignerAndSignature();
            var signerInfo = signer.Item1;
            var signerSignature = signer.Item2;
            var card = this.faker.Card(false, false, new List<CardSignature> { signerSignature });
            crypto.VerifySignature(signerSignature.Signature, card.Fingerprint, Arg.Any<IPublicKey>()).Returns(true);

            var whiteList = new WhiteList
            {
                VerifiersCredentials = new List<VerifierCredentials>() { { signerInfo } }
            };
            validator.WhiteLists = new List<WhiteList>(){whiteList};
            var result = validator.VerifyCard(card);
            result.Should().BeTrue();
        }

        [Test]
        public void Validate_ShouldValidateByAppSign()
        {
            var crypto = new VirgilCrypto();
            var validator = new VirgilCardVerifier();
            validator.ChangeServiceCreds(ServiceCardId, ServicePublicKeyPemBase64);

            var appKeyPair = crypto.GenerateKeys();

            var appPublicKey = Bytes.ToString(crypto.ExportPublicKey(crypto.ExtractPublicKey(appKeyPair.PrivateKey)), 
                StringEncoding.BASE64);
            
            var list = new List<VerifierCredentials>
            {
                new VerifierCredentials() { CardId = "", PublicKeyBase64 = appPublicKey }
            };
            
            //validator.Whitelist = list;
            var keypair = crypto.GenerateKeys();
            var cardCrypto = new VirgilCardCrypto();
           /* var csr = CSR.Generate(cardCrypto, new CardParams
            {
                Identity = "some_identity",
                PublicKey = crypto.ExtractPublicKey(keypair.PrivateKey),
                PrivateKey = keypair.PrivateKey
            });
            

            csr.Sign(cardCrypto, new ExtendedSignParams
            {
                SignerId = "",
                SignerType = SignerType.App.ToLowerString(),
                SignerPrivateKey = appKeyPair.PrivateKey
            });

            var card = CardUtils.Parse(cardCrypto, csr.RawSignedModel);
           
            var result = validator.VerifyCard(card);
            result.Should().BeTrue();*/
        }
}
}