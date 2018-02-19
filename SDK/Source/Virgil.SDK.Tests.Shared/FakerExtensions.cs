using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using Virgil.SDK.Crypto;
using Virgil.SDK.Signer;
using Virgil.SDK.Web.Authorization;

namespace Virgil.SDK.Tests
{
    using System;
    using System.Collections.Generic;
    using Bogus;
    using Virgil.SDK.Common;
    using Verification;
    using Virgil.Crypto;
    using Virgil.SDK.Web;
    using Virgil.CryptoAPI;
    using System.Configuration;

    public static class FakerExtensions
    {

        public static KeyPair PredefinedKeyPair(this Faker faker)
        {
            var crypto = new VirgilCrypto();
            var privateKey =
                crypto.ImportPrivateKey(Bytes.FromString(AppSettings.PredefinedPrivateKeyBase64, StringEncoding.BASE64));
            var publicKey = crypto.ExtractPublicKey(privateKey);
            return new KeyPair((PublicKey)publicKey, (PrivateKey)privateKey);
        }

        public static KeyPair PredefinedVirgilKeyPair(this Faker faker)
        {
            var crypto = new VirgilCrypto();
            var privateKey =
                crypto.ImportPrivateKey(Bytes.FromString(AppSettings.PredefinedPrivateKeyBase64, StringEncoding.BASE64));
            var publicKey = crypto.ExtractPublicKey(privateKey);
            return new KeyPair((PublicKey)publicKey, (PrivateKey)privateKey);
        }
        public static Card Card(this Faker faker,
            bool addSelfSignature = true,
            bool addVirgilSignature = true,
            List<CardSignature> signatures = null)
        {
            
            var fingerprint = faker.Random.Bytes(32);
            var cardId =  Bytes.ToString(fingerprint, StringEncoding.HEX);

            if (signatures == null)
            {
                signatures = new List<CardSignature>();
            }

            if (addSelfSignature)
            {
                signatures.Add(new CardSignature
                {
                    Signer = ModelSigner.SelfSigner,
                    Signature = faker.Random.Bytes(64)
                });
            }
            
            if (addVirgilSignature)
            {
                signatures.Add(new CardSignature {
                    Signature = faker.Random.Bytes(64),
                    Signer = ModelSigner.VirgilSigner

                });
            }
            var crypto = new VirgilCrypto();

            var somePublicKey = crypto.GenerateKeys().PublicKey;

            var card = new Card
            ( 
                cardId,
                faker.Person.UserName,
                somePublicKey,
                //faker.Random.ArrayElement(new[] {"4.0", "5.0"}),
                "5.0",
                faker.Date.Between(DateTime.MinValue, DateTime.MaxValue),
                signatures,
                null, //todo snapshot faker
                null
            );

            return card;
        }

        public static string CardId(this Faker faker)
        {
            var fingerprint = faker.Random.Bytes(32);
            var cardId =  Bytes.ToString(fingerprint, StringEncoding.HEX);

            return cardId;
        }

      

        public static string AppId(this Faker faker)
        {
            var appId = Bytes.ToString(faker.Random.Bytes(32), StringEncoding.HEX);

            return appId;
        }


        public static Tuple<VerifierCredentials, RawSignature> VerifierCredentialAndSignature(this Faker faker, string signer)
        {
            var crypto = new VirgilCrypto();
            var keypair = crypto.GenerateKeys();
            return new Tuple<VerifierCredentials, RawSignature>(
                new VerifierCredentials { Signer = signer,
                    PublicKeyBase64 = Bytes.ToString(crypto.ExportPublicKey(keypair.PublicKey), StringEncoding.BASE64) }, 
                new RawSignature() { Signer = signer, Signature = faker.Random.Bytes(64) });
        }

        public static CardSignature CardSignature(this Faker faker)
        {
            return new CardSignature { Signer = "extra", Signature = faker.Random.Bytes(64) };
        }
        

        public static RawSignedModel PredefinedRawSignedModel(this Faker faker,
            string previousCardId = null, 
            bool addSelfSignature = false,
            bool addVirgilSignature = false,
            bool addExtraSignature = false
            )
        {
           var crypto = new VirgilCrypto();
            var keyPair = faker.PredefinedKeyPair();
            var dateTime = DateTimeOffset.FromUnixTimeSeconds(Int64.Parse("1515686245")).DateTime;
            var rawCardContent = new RawCardContent()
            {
                CreatedAt = dateTime,
                Identity = "test",
                PublicKey = crypto.ExportPublicKey(keyPair.PublicKey),
                Version = "5.0",
                PreviousCardId = previousCardId
            };
            var model = new RawSignedModel() { ContentSnapshot = SnapshotUtils.TakeSnapshot(rawCardContent) };

            var signer = new ModelSigner(new VirgilCardCrypto());
            if (addSelfSignature)
            {
                signer.SelfSign(model, keyPair.PrivateKey);
            }

            if (addVirgilSignature)
            {
                signer.Sign(model, new SignParams()
                {
                    Signer = ModelSigner.VirgilSigner,
                    SignerPrivateKey = faker.PredefinedVirgilKeyPair().PrivateKey
                });
            }
           
            if (addExtraSignature)
            {
                signer.Sign(model, new SignParams()
                {
                    Signer = "extra",
                    SignerPrivateKey = crypto.GenerateKeys().PrivateKey
                });
            }
            return model;
        }

        public static RawSignedModel RawCard(this Faker faker)
        {
            var rawCardContent = Substitute.For<RawCardContent>();
            return new RawSignedModel() {ContentSnapshot = SnapshotUtils.TakeSnapshot(rawCardContent)};
        }

        public static CardManager CardManager(
            this Faker faker, 
            IAccessTokenProvider tokenProvider, 
            bool retryOnUnauthorized = false)
        {
            //Func<RawSignedModel, Task<RawSignedModel>> signCallBackFunc = Substitute.For<
             //   Func<RawSignedModel, Task<RawSignedModel>>>();
            var verifier = new VirgilCardVerifier() {VerifySelfSignature = false, VerifyVirgilSignature = false};
           
            var manager = new CardManager(new CardManagerParams()
            {
                CardCrypto = new VirgilCardCrypto(),
                AccessTokenProvider = tokenProvider,
                SignCallBack = null,
                Verifier = verifier,
                ApiUrl = AppSettings.CardsServiceAddress,
                RetryOnUnauthorized = retryOnUnauthorized
            });
            return manager;
        }

        public static CardManager CardManager(this Faker faker)
        {
            return faker.CardManager(Substitute.For<IAccessTokenProvider>());
        }

        public static Tuple<Jwt, JwtGenerator> PredefinedToken(
            this Faker faker, 
            VirgilAccessTokenSigner signer, 
            TimeSpan lifeTime,
            out string apiPublicKeyId,
            out string apiPublicKeyBase64)
        {
            var crypto = new VirgilCrypto();
            var apiKeyPair = crypto.GenerateKeys();
            var fingerprint = crypto.GenerateHash(crypto.ExportPublicKey(apiKeyPair.PublicKey));
            apiPublicKeyId = Bytes.ToString(fingerprint, StringEncoding.HEX);

            apiPublicKeyBase64 = Bytes.ToString(
                crypto.ExportPublicKey(apiKeyPair.PublicKey), StringEncoding.BASE64);

            var jwtGenerator = new JwtGenerator(
                faker.AppId(),
                apiKeyPair.PrivateKey,
                apiPublicKeyId,
                lifeTime,
                signer);

            var additionalData = new Dictionary<string, string>
            {
                {"username", "some_username"}
            };
            var dict = additionalData.ToDictionary(entry => (object)entry.Key, entry => (object)entry.Value);
            var token = jwtGenerator.GenerateToken("some_identity", dict);
            return  new Tuple<Jwt, JwtGenerator>(token, jwtGenerator);
        }

    }
}