using Virgil.SDK.Validation;

namespace Virgil.SDK.Tests
{
    using System.Collections.Generic;
    using Bogus;
    using FluentAssertions;
    using NSubstitute;
    using NUnit.Framework;
    using Virgil.CryptoApi;

    [TestFixture]
    public class ExtendedValidatorTests
    {
        private readonly Faker faker = new Faker();
        
        [Test]
        public void Validate_ShouldIgnoreSelfSignature_IfPropertySetToTrue()
        {
            var crypto = Substitute.For<ICrypto>();
            var validator = new ExtendedValidator();
            var card = this.faker.Card();

            validator.IgnoreSelfSignature = true;

            crypto.VerifySignature(card.Fingerprint, card.Signatures[0].Signature, card.PublicKey).Returns(false);
            crypto.VerifySignature(card.Fingerprint, card.Signatures[1].Signature, Arg.Any<IPublicKey>()).Returns(true);

            var result = validator.Validate(crypto, card);
            result.IsValid.Should().BeTrue();
        }
        
        [Test]
        public void Validate_ShouldIgnoreVirgilSignature_IfPropertySetToTrue()
        {
            var crypto = Substitute.For<ICrypto>();
            var validator = new ExtendedValidator();
            var card = this.faker.Card();

            validator.IgnoreVirgilSignature = true;

            crypto.VerifySignature(card.Fingerprint, card.Signatures[0].Signature, card.PublicKey).Returns(true);
            crypto.VerifySignature(card.Fingerprint, card.Signatures[1].Signature, Arg.Any<IPublicKey>()).Returns(false);

            var result = validator.Validate(crypto, card);
            result.IsValid.Should().BeTrue();
        }
        
        [Test]
        public void Validate_ShouldNotValidateSelfAndVirgilSignatures_IfBothPropertiesSetToTrue()
        {
            var crypto = Substitute.For<ICrypto>();
            var validator = new ExtendedValidator();
            var card = this.faker.Card();

            crypto.VerifySignature(card.Fingerprint, card.Signatures[0].Signature, card.PublicKey).Returns(false);
            crypto.VerifySignature(card.Fingerprint, card.Signatures[1].Signature, Arg.Any<IPublicKey>()).Returns(false);
            
            validator.IgnoreVirgilSignature = true;
            validator.IgnoreSelfSignature = true;

            var result = validator.Validate(crypto, card);
            result.IsValid.Should().BeTrue();
        }
        
        [Test]
        public void Validate_ShouldReturnSuccess_IfSpecifiedWhitelistSignersAreValid()
        {
            var crypto = Substitute.For<ICrypto>();
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
            result.IsValid.Should().BeTrue();
        }
    }
}