using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bogus;
using NSubstitute;
using NUnit.Framework;
using Virgil.Crypto;
using Virgil.SDK.Common;
using Virgil.SDK.Crypto;
using Virgil.SDK.Signer;
using Virgil.SDK.Web;
using Virgil.SDK.Web.Authorization;

namespace Virgil.SDK.Tests.Shared
{
    [TestFixture]
    class GeneratorTestData
    {
        private readonly Faker faker = new Faker();

        [Test]
        public async System.Threading.Tasks.Task Prepair_TestDataAsync()
        {
            var model = faker.PredefinedRawSignedModel();
            var fullModel = faker.PredefinedRawSignedModel(
                "a666318071274adb738af3f67b8c7ec29d954de2cabfd71a942e6ea38e59fff9",
                true, true, true);
            var data = new Dictionary<string, string>
            {
                { "STC-1.as_string", model.ExportAsString()},
                { "STC-1.as_json", model.ExportAsJson()},
                { "STC-2.as_string", fullModel.ExportAsString()},
                { "STC-2.as_json", fullModel.ExportAsJson()}
            };

            var cardManager = faker.CardManager();
            var card = cardManager.ImportCardFromString(model.ExportAsString());
            var crypto = new VirgilCrypto();

            data.Add("STC-3.as_string", cardManager.ExportCardAsString(card));
            data.Add("STC-3.as_json", cardManager.ExportCardAsJson(card));
            data.Add("STC-3.card_id", card.Id);
            data.Add("STC-3.public_key_base64", Bytes.ToString(crypto.ExportPublicKey(card.PublicKey), StringEncoding.BASE64));

            fullModel = faker.PredefinedRawSignedModel(null, true, true, true);
            var fullCard = cardManager.ImportCardFromString(fullModel.ExportAsString());

            data.Add("STC-4.as_string", cardManager.ExportCardAsString(fullCard));
            data.Add("STC-4.as_json", cardManager.ExportCardAsJson(fullCard));
            data.Add("STC-4.card_id", fullCard.Id);
            data.Add("STC-4.public_key_base64", Bytes.ToString(crypto.ExportPublicKey(fullCard.PublicKey),
                StringEncoding.BASE64));
            foreach (var signature in fullCard.Signatures)
            {
                data.Add($"STC-4.signature_{signature.Signer}_base64", Bytes.ToString(signature.Signature,
                    StringEncoding.BASE64));
            }

            string apiPublicKeyId;
            string apiPublicKeyBase64;
            var (token, jwtGenerator) = faker.PredefinedToken(
                new VirgilAccessTokenSigner(),
                TimeSpan.FromMinutes(10),
                out apiPublicKeyId,
                out apiPublicKeyBase64);

            data.Add("STC-22.jwt", token.ToString());
            data.Add("STC-22.api_public_key_base64", apiPublicKeyBase64);
            data.Add("STC-22.api_key_id", apiPublicKeyId);


            data.Add("STC-23.api_public_key_base64", apiPublicKeyBase64);
            data.Add("STC-23.api_key_id", apiPublicKeyId);
            data.Add("STC-23.app_id", jwtGenerator.AppId);

            data.Add("STC-23.api_private_key_base64", Bytes.ToString(
                crypto.ExportPrivateKey(jwtGenerator.ApiKey), StringEncoding.BASE64));

            // STC-10
            var cardKeyPair = crypto.GenerateKeys();
            var cardIdentity = faker.Random.AlphaNumeric(10);
            var rawCardContent1 = new RawCardContent()
            {
                CreatedAt = DateTime.UtcNow,
                Identity = cardIdentity,
                PublicKey = crypto.ExportPublicKey(cardKeyPair.PublicKey),
                Version = "5.0",
            };
            var rawSignedModel = new RawSignedModel() { ContentSnapshot = SnapshotUtils.TakeSnapshot(rawCardContent1) };

            var signer = new ModelSigner(new VirgilCardCrypto());
            signer.SelfSign(rawSignedModel, cardKeyPair.PrivateKey);


            var keyPair = crypto.GenerateKeys();
            signer.Sign(rawSignedModel, new SignParams()
            {
                SignerPrivateKey = keyPair.PrivateKey,
                Signer = "extra"
            });
            data.Add("STC-10.private_key1_base64", Bytes.ToString(
                crypto.ExportPrivateKey(keyPair.PrivateKey), StringEncoding.BASE64));

            var accessTokenGenerator = new JwtGenerator(
                IntegrationHelper.AppId,
                IntegrationHelper.ApiPrivateKey(),
                IntegrationHelper.ApiPublicKeyId,
                TimeSpan.FromMinutes(10),
                new VirgilAccessTokenSigner()
            );
            var accessTokenProvider = Substitute.For<IAccessTokenProvider>();
            accessTokenProvider.GetTokenAsync(Arg.Any<TokenContext>()).Returns(
                accessTokenGenerator.GenerateToken(cardIdentity)
            );
            var manager = new CardManager(new CardManagerParams()
            {
                CardCrypto = new VirgilCardCrypto(),
                AccessTokenProvider = accessTokenProvider,
                ApiUrl = IntegrationHelper.CardsServiceAddress
            });
            card = await manager.PublishCardAsync(rawSignedModel);
            data.Add("STC-10.as_string", manager.ExportCardAsString(card));


            // STC - 11
            rawSignedModel = faker.PredefinedRawSignedModel(null, false, false, false);
            data.Add("STC-11.as_string", rawSignedModel.ExportAsString());

            // STC - 12
            rawSignedModel = faker.PredefinedRawSignedModel(null, true, false, false);
            data.Add("STC-12.as_string", rawSignedModel.ExportAsString());

            // STC - 14
            rawSignedModel = faker.PredefinedRawSignedModel(null, false, true, false);
            data.Add("STC-14.as_string", rawSignedModel.ExportAsString());

            // STC - 15
            rawSignedModel = faker.PredefinedRawSignedModel(null, false, false, false);
            keyPair = crypto.GenerateKeys();
            signer.Sign(rawSignedModel, new SignParams()
            {
                SignerPrivateKey = keyPair.PrivateKey,
                Signer = "self"
            });
            data.Add("STC-15.as_string", rawSignedModel.ExportAsString());

            // STC - 16
            rawSignedModel = faker.PredefinedRawSignedModel(null, true, true, false);
            keyPair = crypto.GenerateKeys();
            signer.Sign(rawSignedModel, new SignParams()
            {
                SignerPrivateKey = keyPair.PrivateKey,
                Signer = "extra"
            });
            data.Add("STC-16.as_string", rawSignedModel.ExportAsString());
            data.Add("STC-16.public_key1_base64", Bytes.ToString(
                crypto.ExportPublicKey(keyPair.PublicKey), StringEncoding.BASE64));

            // STC - 28
            (token, jwtGenerator) = faker.PredefinedToken(
               new VirgilAccessTokenSigner(),
               TimeSpan.FromMinutes(2),
               out apiPublicKeyId,
               out apiPublicKeyBase64);
            data.Add("STC-28.jwt", token.ToString());
            data.Add("STC-28.jwt_identity", token.BodyContent.Identity);
            data.Add("STC-28.jwt_app_id", token.BodyContent.AppId);
            data.Add("STC-28.jw_issuer", token.BodyContent.Issuer);
            data.Add("STC-28.jwt_subject", token.BodyContent.Subject);
            data.Add("STC-28.jwt_additional_data", Configuration.Serializer.Serialize(token.BodyContent.AdditionalData));
            data.Add("STC-28.jwt_expires_at", Configuration.Serializer.Serialize(token.BodyContent.ExpiresAt));
            data.Add("STC-28.jwt_issued_at", Configuration.Serializer.Serialize(token.BodyContent.IssuedAt));
            data.Add("STC-28.jwt_algorithm", token.HeaderContent.Algorithm);
            data.Add("STC-28.jwt_api_key_id", token.HeaderContent.ApiKeyId);
            data.Add("STC-28.jwt_content_type", token.HeaderContent.ContentType);
            data.Add("STC-28.jwt_type", token.HeaderContent.Type);
            data.Add("STC-28.jwt_signature_base64", Bytes.ToString(token.SignatureData, StringEncoding.BASE64));


            // STC - 29
            (token, jwtGenerator) = faker.PredefinedToken(
                new VirgilAccessTokenSigner(),
                TimeSpan.FromDays(365),
                out apiPublicKeyId,
                out apiPublicKeyBase64);
            data.Add("STC-29.jwt", token.ToString());
            data.Add("STC-29.jwt_identity", token.BodyContent.Identity);
            data.Add("STC-29.jwt_app_id", token.BodyContent.AppId);
            data.Add("STC-29.jw_issuer", token.BodyContent.Issuer);
            data.Add("STC-29.jwt_subject", token.BodyContent.Subject);
            data.Add("STC-29.jwt_additional_data", Configuration.Serializer.Serialize(token.BodyContent.AdditionalData));
            data.Add("STC-29.jwt_expires_at", Configuration.Serializer.Serialize(token.BodyContent.ExpiresAt));
            data.Add("STC-29.jwt_issued_at", Configuration.Serializer.Serialize(token.BodyContent.IssuedAt));
            data.Add("STC-29.jwt_algorithm", token.HeaderContent.Algorithm);
            data.Add("STC-29.jwt_api_key_id", token.HeaderContent.ApiKeyId);
            data.Add("STC-29.jwt_content_type", token.HeaderContent.ContentType);
            data.Add("STC-29.jwt_type", token.HeaderContent.Type);
            data.Add("STC-29.jwt_signature_base64", Bytes.ToString(token.SignatureData, StringEncoding.BASE64));


            // STC - 34
            keyPair = crypto.GenerateKeys();
            var rawCardContent = new RawCardContent()
            {
                CreatedAt = DateTime.UtcNow,
                Identity = "test",
                PublicKey = crypto.ExportPublicKey(keyPair.PublicKey),
                Version = "5.0"
            };
            model = new RawSignedModel() { ContentSnapshot = SnapshotUtils.TakeSnapshot(rawCardContent) };

            signer.SelfSign(
                model, keyPair.PrivateKey, new Dictionary<string, string>() { { "info", "some_additional_info" } }
            );

            data.Add("STC-34.private_key_base64", Bytes.ToString(
                crypto.ExportPrivateKey(keyPair.PrivateKey), StringEncoding.BASE64));
            data.Add("STC-34.public_key_base64", Bytes.ToString(
                crypto.ExportPublicKey(keyPair.PublicKey), StringEncoding.BASE64));
            data.Add("STC-34.self_signature_snapshot_base64",
                Bytes.ToString(model.Signatures.First().Snapshot, StringEncoding.BASE64));
            data.Add("STC-34.content_snapshot_base64",
                Bytes.ToString(
                    SnapshotUtils.TakeSnapshot(rawCardContent), StringEncoding.BASE64));
            data.Add("STC-34.as_string", model.ExportAsString());

            System.IO.File.WriteAllText(IntegrationHelper.OutputTestDataPath,
                Configuration.Serializer.Serialize(data));


        }
        /*

        [Test]
        public void Prepair_TestData()
        {
            var rawSignedModel = faker.PredefinedRawSignedModel();
            var cardManager = faker.CardManager();
            var card = cardManager.ImportCardFromString(rawSignedModel.ExportAsString());

            var fullrawSignedModel = faker.PredefinedRawSignedModel(null, true, true, true);
            var fullCard = cardManager.ImportCardFromString(fullrawSignedModel.ExportAsString());

            var data = new Dictionary<string, string>
            {
                { "3_as_string", cardManager.ExportCardAsString(card) },
                { "3_as_json", cardManager.ExportCardAsJson(card) },
                { "4_as_string", cardManager.ExportCardAsString(fullCard)},
                { "4_as_json", cardManager.ExportCardAsJson(fullCard)}
            };
            System.IO.File.WriteAllText(@"C:\Users\Vasilina\Documents\test_data_3_4",
                Configuration.Serializer.Serialize(data));
            System.IO.File.AppendAllText(@"C:\Users\Vasilina\Documents\raw_data_3", cardManager.ExportCardAsJson(card));
            System.IO.File.AppendAllText(@"C:\Users\Vasilina\Documents\raw_data_4", cardManager.ExportCardAsJson(fullCard));
        }*/
    }
}
