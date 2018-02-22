using System;
using System.Configuration;
using NSubstitute;
using Virgil.SDK.Common;
using Virgil.SDK.Signer;
using Virgil.SDK.Verification;
using Virgil.SDK.Web;

namespace Virgil.SDK.Tests
{
    using System.Collections.Generic;
    using Bogus;
    using NUnit.Framework;
    using Virgil.CryptoAPI;
    using Virgil.CryptoImpl;

    [TestFixture]
    public class ExtendedValidatorTests
    {
        private readonly Faker faker = new Faker();
        

        [Test]
        public void Validate_ShouldIgnoreSelfSignature_IfPropertyDoesntSetToTrue()
        {
            var crypto = Substitute.For<ICardCrypto>();
            var validator = new VirgilCardVerifier(crypto);
            var card = this.faker.Card(false);
            validator.VerifyVirgilSignature = true;
            validator.VerifySelfSignature = false;
            crypto.VerifySignature(
                card.Signatures[0].Signature,
                card.ContentSnapshot,
                Arg.Any<IPublicKey>()).Returns(true);
            var result = validator.VerifyCard(card);
            Assert.IsTrue(result);
        }

        [Test]
        public void Validate_ShouldIgnoreVirgilSignature_IfPropertyDoesntSetToTrue()
        {
            var crypto = Substitute.For<ICardCrypto>();
            var validator = new VirgilCardVerifier(crypto);
            var card = this.faker.Card(true, false);
            validator.VerifySelfSignature = true;
            validator.VerifyVirgilSignature = false;
            crypto.VerifySignature(
                card.Signatures[0].Signature,
                card.ContentSnapshot,
                card.PublicKey).Returns(true);
            var result = validator.VerifyCard(card);
            Assert.IsTrue(result);
        }

        [Test]
        public void Validate_ShouldNotValidateSelfAndVirgilSignatures_IfBothPropertiesDoesntSetToTrue()
        {
            var crypto = Substitute.For<ICardCrypto>();
            var validator = new VirgilCardVerifier(crypto);
            validator.VerifySelfSignature = false;
            validator.VerifyVirgilSignature = false;
            var card = this.faker.Card();
            crypto.VerifySignature(card.Signatures[0].Signature,
                card.ContentSnapshot,
                card.PublicKey).Returns(false);
            crypto.VerifySignature(
                card.Signatures[1].Signature,
                card.ContentSnapshot,
                Arg.Any<IPublicKey>()).Returns(false);

            var result = validator.VerifyCard(card);
            Assert.IsTrue(result);
        }

        [Test]
        public void Validate_ShouldReturnSuccess_IfSpecifiedWhitelistSignersAreValid()
        {
            var crypto = Substitute.For<ICardCrypto>();
            var validator = new VirgilCardVerifier(crypto);
            validator.VerifySelfSignature = false;
            validator.VerifyVirgilSignature = false;
            var signer = this.faker.VerifierCredentialAndSignature("exta");
            var signerInfo = signer.Item1;
            var signerSignature = new CardSignature()
            {
                Snapshot = signer.Item2.Snapshot,
                Signer = signer.Item2.Signer,
                Signature = signer.Item2.Signature
            };
            //C signer.Item2;}
            var card = this.faker.Card(false, false, new List<CardSignature> { signerSignature });
            crypto.VerifySignature(
                signerSignature.Signature,
                card.ContentSnapshot,
                Arg.Any<IPublicKey>()).Returns(true);

            var whiteList = new Whitelist
            {
                VerifiersCredentials = new List<VerifierCredentials>() { { signerInfo } }
            };
            validator.Whitelists = new List<Whitelist>() { whiteList };
            var result = validator.VerifyCard(card);
            Assert.IsTrue(result);
        }

        [Test]
        public void Validate_Should_ValidateByAppSign()
        {
            var crypto = new VirgilCrypto();
            var validator = new VirgilCardVerifier(new VirgilCardCrypto());
            var vrigilPublicKeyBytes = crypto.ExportPublicKey(faker.PredefinedVirgilKeyPair().PublicKey);
            validator.ChangeServiceCreds(
                Bytes.ToString(vrigilPublicKeyBytes, StringEncoding.BASE64)
                );

            var appKeyPair = crypto.GenerateKeys();

            var appPublicKey = Bytes.ToString(crypto.ExportPublicKey(crypto.ExtractPublicKey(appKeyPair.PrivateKey)),
                StringEncoding.BASE64);

            var list = new List<VerifierCredentials>
            {
                new VerifierCredentials()
                {
                    Signer = "my_app", PublicKeyBase64 = appPublicKey
                }
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


        [Test]
        public void EmptyVerifier_Should_VerifyCard()
        {
            //STC-10
            var rawSignedModel = faker.PredefinedRawSignedModel(null, true, true, false);
            var cardManager = faker.CardManager();
            var card = cardManager.ImportCardFromJson(rawSignedModel.ExportAsJson());

            var verifier = new VirgilCardVerifier(new VirgilCardCrypto())
            {
                VerifySelfSignature = false,
                VerifyVirgilSignature = false
            };
            Assert.IsTrue(verifier.VerifyCard(card));
        }

        [Test]
        public void Verifier_Should_VerifyCard_IfCardHasAtLeastOneSignatureFromWhitelist()
        {
            //STC-10
            var rawSignedModel = faker.PredefinedRawSignedModel(null, true, true, false);
            var signer = new ModelSigner(new VirgilCardCrypto());
            var crypto = new VirgilCrypto();
            var keyPair = crypto.GenerateKeys();
            signer.Sign(rawSignedModel, new SignParams()
            {
                SignerPrivateKey = keyPair.PrivateKey,
                Signer = "extra"
            });
            var creds = new VerifierCredentials(){
                PublicKeyBase64 = Bytes.ToString(crypto.ExportPublicKey(keyPair.PublicKey), 
                StringEncoding.BASE64), Signer = "extra"
            };
            var cardManager = faker.CardManager();
            var card = cardManager.ImportCardFromJson(rawSignedModel.ExportAsJson());

            var verifier = new VirgilCardVerifier(new VirgilCardCrypto())
            {
                VerifySelfSignature = true,
                VerifyVirgilSignature = true,
            };
            var vrigilPublicKeyBytes = new VirgilCrypto().ExportPublicKey(faker.PredefinedVirgilKeyPair().PublicKey);
            verifier.ChangeServiceCreds(
                Bytes.ToString(vrigilPublicKeyBytes, StringEncoding.BASE64)
            );

            var whiteList = new Whitelist()
            {
                VerifiersCredentials = new List<VerifierCredentials>()
                {
                    creds,
                    faker.VerifierCredentialAndSignature("extra").Item1
                }
            };
            verifier.Whitelists = new List<Whitelist>(){whiteList};
            Assert.IsTrue(verifier.VerifyCard(card));

        }

        [Test]
        public void Verifier_ShouldNot_VerifyCard_IfCardDoesntHaveSignatureFromAtLeastOneWhitelist()
        {
            //STC-10
            var rawSignedModel = faker.PredefinedRawSignedModel(null, true, true, false);
            var signer = new ModelSigner(new VirgilCardCrypto());
            var crypto = new VirgilCrypto();
            var keyPair = crypto.GenerateKeys();
            signer.Sign(rawSignedModel, new SignParams()
            {
                SignerPrivateKey = keyPair.PrivateKey,
                Signer = "extra"
            });
            var creds = new VerifierCredentials()
            {
                PublicKeyBase64 = Bytes.ToString(crypto.ExportPublicKey(keyPair.PublicKey),
                    StringEncoding.BASE64),
                Signer = "extra"
            };
            var cardManager = faker.CardManager();
            var card = cardManager.ImportCardFromJson(rawSignedModel.ExportAsJson());

            var verifier = new VirgilCardVerifier(new VirgilCardCrypto())
            {
                VerifySelfSignature = true,
                VerifyVirgilSignature = true,
            };
            var vrigilPublicKeyBytes = new VirgilCrypto().ExportPublicKey(faker.PredefinedVirgilKeyPair().PublicKey);
            verifier.ChangeServiceCreds(
                Bytes.ToString(vrigilPublicKeyBytes, StringEncoding.BASE64)
            );

            var whiteList = new Whitelist()
            {
                VerifiersCredentials = new List<VerifierCredentials>() { creds }
            };
            verifier.Whitelists = new List<Whitelist>() { whiteList };
            Assert.IsTrue(verifier.VerifyCard(card));

            var whiteList2 = new Whitelist()
            {
                VerifiersCredentials = new List<VerifierCredentials>()
                {
                    faker.VerifierCredentialAndSignature("extra").Item1
                }
            };
            verifier.Whitelists = new List<Whitelist>() { whiteList, whiteList2 };
            Assert.IsFalse(verifier.VerifyCard(card));

        }

        [Test]
        public void Verifier_ShouldNot_VerifyCard_IfVerifierHasEmptyWhitelist()
        {
            //STC-10
            var rawSignedModel = faker.PredefinedRawSignedModel(null, true, true, false);
            var cardManager = faker.CardManager();
            var card = cardManager.ImportCardFromJson(rawSignedModel.ExportAsJson());

            var verifier = new VirgilCardVerifier(new VirgilCardCrypto())
            {
                VerifySelfSignature = true,
                VerifyVirgilSignature = true,
                Whitelists = new List<Whitelist>() { new Whitelist() }
            };
            var vrigilPublicKeyBytes = new VirgilCrypto().ExportPublicKey(faker.PredefinedVirgilKeyPair().PublicKey);
            verifier.ChangeServiceCreds(
                Bytes.ToString(vrigilPublicKeyBytes, StringEncoding.BASE64)
            );

            Assert.IsFalse(verifier.VerifyCard(card));
        }


        [Test]
        public void Verifier_ShouldNot_VerifyCard_IfMissedRequiredSelfSignature()
        {
            //STC-10
            var rawSignedModel = faker.PredefinedRawSignedModel(null, false, false, false);
            var cardManager = faker.CardManager();
            var card = cardManager.ImportCardFromJson(rawSignedModel.ExportAsJson());

            var verifier = new VirgilCardVerifier(new VirgilCardCrypto())
            {
                VerifySelfSignature = true,
                VerifyVirgilSignature = false,
            };
           
            Assert.IsFalse(verifier.VerifyCard(card));
        }

        [Test]
        public void Verifier_ShouldNot_VerifyCard_IfWrongVirgilSignature()
        {
            //STC-10
            var rawSignedModel = faker.PredefinedRawSignedModel(null, false, true, false);
            var cardManager = faker.CardManager();
            var card = cardManager.ImportCardFromJson(rawSignedModel.ExportAsJson());

            var verifier = new VirgilCardVerifier(new VirgilCardCrypto())
            {
                VerifySelfSignature = false,
                VerifyVirgilSignature = true,
            };

            Assert.IsFalse(verifier.VerifyCard(card));
        }

        [Test]
        public void Verifier_ShouldNot_VerifyCard_IfWrongSelfSignature()
        {
            //STC-10
            var rawSignedModel = faker.PredefinedRawSignedModel(null, false, false, false);
            var signer = new ModelSigner(new VirgilCardCrypto());
            var crypto = new VirgilCrypto();
            var keyPair = crypto.GenerateKeys();
            signer.Sign(rawSignedModel, new SignParams()
            {
                SignerPrivateKey = keyPair.PrivateKey,
                Signer = "self"
            });
            var cardManager = faker.CardManager();
            var card = cardManager.ImportCardFromJson(rawSignedModel.ExportAsJson());

            var verifier = new VirgilCardVerifier(new VirgilCardCrypto())
            {
                VerifySelfSignature = true,
                VerifyVirgilSignature = false,
            };

            Assert.IsFalse(verifier.VerifyCard(card));
        }
    }
}