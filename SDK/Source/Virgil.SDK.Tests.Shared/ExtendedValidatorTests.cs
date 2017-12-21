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
        
        [Test]
        public void Validate_ShouldIgnoreSelfSignature_IfPropertySetToTrue()
        {
            var crypto = new VirgilCrypto();
            var validator = new ExtendedValidator();
            var card = this.faker.Card();
            var somePublicKey = crypto.GenerateKeys().PublicKey;
            validator.IgnoreSelfSignature = true;
            Assert.IsFalse(crypto.VerifySignature(card.Fingerprint, card.Signatures[0].Signature, card.PublicKey));
            Assert.IsTrue(crypto.VerifySignature(card.Fingerprint, card.Signatures[1].Signature, somePublicKey));

            var result = validator.Validate(crypto, card);
            Assert.IsTrue(result.IsValid);
        }
        
        [Test]
        public void Validate_ShouldIgnoreVirgilSignature_IfPropertySetToTrue()
        {
            var crypto = new VirgilCrypto();
            var validator = new ExtendedValidator();
            var card = this.faker.Card();
            var somePublicKey = crypto.GenerateKeys().PublicKey;

            validator.IgnoreVirgilSignature = true;

            Assert.IsTrue(crypto.VerifySignature(card.Fingerprint, card.Signatures[0].Signature, card.PublicKey));
            Assert.IsFalse(crypto.VerifySignature(card.Fingerprint, card.Signatures[1].Signature, somePublicKey));

            var result = validator.Validate(crypto, card);
            Assert.IsTrue(result.IsValid);
        }
        
        [Test]
        public void Validate_ShouldNotValidateSelfAndVirgilSignatures_IfBothPropertiesSetToTrue()
        {
            var crypto = new VirgilCrypto();
            var validator = new ExtendedValidator();
            var card = this.faker.Card();
            var somePublicKey = crypto.GenerateKeys().PublicKey;

            Assert.IsFalse(crypto.VerifySignature(card.Fingerprint, card.Signatures[0].Signature, card.PublicKey));
            Assert.IsFalse(crypto.VerifySignature(card.Fingerprint, card.Signatures[1].Signature, somePublicKey));
            
            validator.IgnoreVirgilSignature = true;
            validator.IgnoreSelfSignature = true;

            var result = validator.Validate(crypto, card);
            Assert.IsTrue(result.IsValid);
        }
        
        [Test]
        public void Validate_ShouldReturnSuccess_IfSpecifiedWhitelistSignersAreValid()
        {
            var crypto = new VirgilCrypto();
            var validator = new ExtendedValidator();
            var signer = this.faker.SignerAndSignature();
            var signerInfo = signer.Item1;
            var signerSignature = signer.Item2;
            var card = this.faker.Card(false, false, new List<CardSignature> { signerSignature });
            var somePublicKey = crypto.GenerateKeys().PublicKey;

            Assert.IsTrue(crypto.VerifySignature(card.Fingerprint, signerSignature.Signature, somePublicKey));
            
            validator.IgnoreVirgilSignature = true;
            validator.IgnoreSelfSignature = true;
            validator.Whitelist = new[] { signerInfo };

            var result = validator.Validate(crypto, card);
            Assert.IsTrue(result.IsValid);
        }
    }
}