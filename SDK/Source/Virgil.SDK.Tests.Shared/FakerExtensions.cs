using System.Threading.Tasks;
using NSubstitute;
using Virgil.SDK.Signer;
using Virgil.SDK.Web.Authorization;

namespace Virgil.SDK.Tests
{
    using System;
    using System.Collections.Generic;
    using Bogus;
    using Virgil.SDK.Common;
    using Virgil.SDK.Validation;
    using Virgil.Crypto;
    using Virgil.SDK.Web;
    using Virgil.CryptoAPI;

    public static class FakerExtensions
    {
        public static Card Card(this Faker faker,
            bool addSelfSignature = true,
            bool addVirgilSignature = true,
            List<CardSignature> signatures = null)
        {
            const string virgilCardId = "3e29d43373348cfb373b7eae189214dc01d7237765e572db685839b64adca853";
            
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
                    SignerId = cardId,
                    SignerType = SignerType.Self.ToLowerString(),
                    Signature = faker.Random.Bytes(64)
                });
            }
            
            if (addVirgilSignature)
            {
                signatures.Add(new CardSignature {
                    SignerId = virgilCardId,
                    Signature = faker.Random.Bytes(64),
                    SignerType = SignerType.Virgil.ToLowerString()

                });
            }
            var crypto = new VirgilCrypto();

            var somePublicKey = crypto.GenerateKeys().PublicKey;

            var card = new Card
            ( 
                cardId,
                faker.Person.UserName,
                somePublicKey,
                faker.Random.ArrayElement(new[] {"4.0", "5.0"}),
                faker.Date.Between(DateTime.MinValue, DateTime.MaxValue),
                signatures,
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
        public static Tuple<VerifierCredentials, CardSignature> SignerAndSignature(this Faker faker)
        {
            var cardId = faker.CardId();
            var crypto = new VirgilCrypto();
            var keypair = crypto.GenerateKeys();
            return new Tuple<VerifierCredentials, CardSignature>(
                new VerifierCredentials { CardId = cardId,
                    PublicKey = Bytes.ToString(crypto.ExportPublicKey(keypair.PublicKey), StringEncoding.BASE64) }, 
                new CardSignature { SignerId = cardId, Signature = faker.Random.Bytes(64) });
        }

        public static CardSignature CardSignature(this Faker faker)
        {
            return new CardSignature { SignerId = faker.CardId(), Signature = faker.Random.Bytes(64) };
        }
        

        public static RawSignedModel PredefinedRawSignedModel(this Faker faker,
            string previousCardId = null, 
            bool addSelfSignature = false,
            bool addVirgilSignature = false,
            bool addExtraSignature = false
            )
        {
            var dateTime = DateTimeOffset.FromUnixTimeSeconds(Int64.Parse("1515686245")).DateTime;
            var rawCardContent = new RawCardContent()
            {
                CreatedAt = dateTime,
                Identity = "test",
                PublicKey = Bytes.FromString("MCowBQYDK2VwAyEA3J0Ivcs4/ahBafrn6mB4t+UI+IBhWjC/toVDrPJcCZk=", 
                StringEncoding.BASE64),
                Version = "5.0",
                PreviousCardId = previousCardId
            };
            var model = new RawSignedModel() { ContentSnapshot = SnapshotUtils.TakeSnapshot(rawCardContent) };

            var crypto = new VirgilCrypto();
            var signer = new ModelSigner(new VirgilCardCrypto());
            if (addSelfSignature)
            {
                signer.SelfSign(model, crypto.GenerateKeys().PrivateKey);
            }

            if (addVirgilSignature)
            {
                signer.Sign(model, new SignParams()
                {
                    SignerId = faker.CardId(),
                    SignerType = SignerType.Virgil.ToLowerString(),
                    SignerPrivateKey = crypto.GenerateKeys().PrivateKey
                });
            }
           
            if (addExtraSignature)
            {
                signer.Sign(model, new SignParams()
                {
                    SignerId = faker.CardId(),
                    SignerType = SignerType.Extra.ToLowerString(),
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

        public static CardManager CardManager(this Faker faker)
        {
            Func<RawSignedModel, Task<RawSignedModel>> signCallBackFunc = Substitute.For<
                Func<RawSignedModel, Task<RawSignedModel>>
            >();
            var validator = new VirgilCardVerifier() { };
            var manager = new CardManager(new CardManagerParams()
            {
                CardCrypto = new VirgilCardCrypto(),
                AccessTokenProvider = Substitute.For<IAccessTokenProvider>(),
                SignCallBack = signCallBackFunc,
                Verifier = validator
            });
            return manager;
        }


    }
}