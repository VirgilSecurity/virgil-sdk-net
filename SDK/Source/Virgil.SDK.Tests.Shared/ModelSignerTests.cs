using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bogus;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using Virgil.Crypto;
using Virgil.SDK.Common;
using Virgil.SDK.Signer;

namespace Virgil.SDK.Tests.Shared
{
    [NUnit.Framework.TestFixture]
    public class ModelSignerTests
    {
        private readonly Faker faker = new Faker();

        [Test]
        public void SelfSign_Should_AddValidSignature()
        {
            //STC-8
            var rawSignedModel = faker.PredefinedRawSignedModel(null, false, false, false);
            var crypto = new VirgilCrypto();
            var signer = new ModelSigner(new VirgilCardCrypto());
            Assert.AreEqual(rawSignedModel.Signatures.Count, 0);
            
            signer.SelfSign(rawSignedModel, faker.PredefinedKeyPair().PrivateKey);
            Assert.AreEqual(rawSignedModel.Signatures.Count, 1);
            var selfSignature = rawSignedModel.Signatures.First();
            var cardId = CardUtils.GenerateCardId(new VirgilCardCrypto(), rawSignedModel.ContentSnapshot);
            Assert.AreEqual(selfSignature.Signer, ModelSigner.SelfSigner);
            Assert.AreEqual(selfSignature.Snapshot, null);
            Assert.True(crypto.VerifySignature(
                selfSignature.Signature, 
                rawSignedModel.ContentSnapshot, 
                faker.PredefinedKeyPair().PublicKey)
            );
        }

        [Test]
        public void SelfSignWithSignatureSnapshot_Should_AddValidSignature()
        {
            //STC-9
            var rawSignedModel = faker.PredefinedRawSignedModel(null, false, false, false);
            var crypto = new VirgilCrypto();
            var signer = new ModelSigner(new VirgilCardCrypto());
            Assert.AreEqual(rawSignedModel.Signatures.Count, 0);
            var signatureSnapshot = faker.Random.Bytes(32);
            signer.SelfSign(rawSignedModel, faker.PredefinedKeyPair().PrivateKey, signatureSnapshot);
            var selfSignature = rawSignedModel.Signatures.First();
            Assert.AreEqual(selfSignature.Signer, ModelSigner.SelfSigner);
            Assert.AreEqual(selfSignature.Snapshot, signatureSnapshot);

            var extendedSnapshot = Bytes.Combine(rawSignedModel.ContentSnapshot, signatureSnapshot);

            Assert.True(crypto.VerifySignature(
                selfSignature.Signature,
                extendedSnapshot,
                faker.PredefinedKeyPair().PublicKey)
            );
        }

        [Test]
        public void SecondSelfSign_Should_ThrowException()
        {
            //STC-8
            var rawSignedModel = faker.PredefinedRawSignedModel(null, true, false, false);
            var crypto = new VirgilCrypto();
            var keyPair = crypto.GenerateKeys(); 
            var signer = new ModelSigner(new VirgilCardCrypto());

            Assert.Throws<VirgilException>(
            () => signer.SelfSign(rawSignedModel, keyPair.PrivateKey)
                );
        }


        [Test]
        public void ExtraSign_Should_AddValidSignature()
        {
            //STC-8
            var rawSignedModel = faker.PredefinedRawSignedModel(null, true, false, false);
            var crypto = new VirgilCrypto();
            var signer = new ModelSigner(new VirgilCardCrypto());

            Assert.AreEqual(rawSignedModel.Signatures.Count, 1);
            var keyPair = crypto.GenerateKeys();
            var signParams = new SignParams()
            {
                Signer = "test_id",
                SignerPrivateKey = keyPair.PrivateKey
            };
            signer.Sign(rawSignedModel, signParams
               );
            Assert.AreEqual(rawSignedModel.Signatures.Count, 2);
            var extraSignature = rawSignedModel.Signatures.Last();
            Assert.AreEqual(extraSignature.Signer, signParams.Signer);
            Assert.AreEqual(extraSignature.Snapshot, null);
            Assert.True(crypto.VerifySignature(
                extraSignature.Signature,
                rawSignedModel.ContentSnapshot,
                keyPair.PublicKey));
        }

        [Test]
        public void ExtraSignWithSignatureSnapshot_Should_AddValidSignature()
        {
            //STC-9
            var rawSignedModel = faker.PredefinedRawSignedModel(null, true, false, false);
            var crypto = new VirgilCrypto();
            var signer = new ModelSigner(new VirgilCardCrypto());

            Assert.AreEqual(rawSignedModel.Signatures.Count, 1);
            var keyPair = crypto.GenerateKeys();
            var signParams = new SignParams()
            {
                Signer = "test_id",
                SignerPrivateKey = keyPair.PrivateKey
            };
            var signatureSnapshot = faker.Random.Bytes(32);

            signer.Sign(rawSignedModel, signParams, signatureSnapshot);
            Assert.AreEqual(rawSignedModel.Signatures.Count, 2);
            var extraSignature = rawSignedModel.Signatures.Last();
            Assert.AreEqual(extraSignature.Signer, signParams.Signer);
            Assert.AreEqual(extraSignature.Snapshot, signatureSnapshot);

            var extendedSnapshot = Bytes.Combine(rawSignedModel.ContentSnapshot, signatureSnapshot);
            Assert.True(crypto.VerifySignature(
                extraSignature.Signature,
                extendedSnapshot,
                keyPair.PublicKey));
        }

        [Test]
        public void SecondExtraSign_Should_ThrowException()
        {
            //STC-8
            var rawSignedModel = faker.PredefinedRawSignedModel(null, false, false, false);
            var crypto = new VirgilCrypto();
            var signer = new ModelSigner(new VirgilCardCrypto());

            var keyPair = crypto.GenerateKeys();
            signer.Sign(rawSignedModel,
                new SignParams()
                {
                    Signer = "test_id",
                    SignerPrivateKey = keyPair.PrivateKey
                });

            Assert.Throws<VirgilException>(
                () => signer.Sign(rawSignedModel,
                        new SignParams()
                        {
                            Signer = "test_id",
                            SignerPrivateKey = keyPair.PrivateKey
                        })
            );
        }
    }
}
