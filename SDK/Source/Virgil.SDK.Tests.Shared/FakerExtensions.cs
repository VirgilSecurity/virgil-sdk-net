using System.Threading.Tasks;
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
                signatures.Add(new CardSignature { SignerCardId = cardId, Signature = faker.Random.Bytes(64) });
            }
            
            if (addVirgilSignature)
            {
                signatures.Add(new CardSignature { SignerCardId = virgilCardId, Signature = faker.Random.Bytes(64) });
            }
            var crypto = new VirgilCardManagerCrypto();

            var somePublicKey = crypto.GenerateKeys().PublicKey;

            var card = new Card
            ( 
                cardId,
                faker.Person.UserName,
                fingerprint,
                somePublicKey,
                faker.Random.ArrayElement(new[] {"4.0", "5.0"}),
                faker.Date.Between(DateTime.MinValue, DateTime.MaxValue),
                signatures
            );

            return card;
        } 

        public static string CardId(this Faker faker)
        {
            var fingerprint = faker.Random.Bytes(32);
            var cardId =  Bytes.ToString(fingerprint, StringEncoding.HEX);

            return cardId;
        }
        
        public static Tuple<SignerInfo, CardSignature> SignerAndSignature(this Faker faker)
        {
            var cardId = faker.CardId();
            
            return new Tuple<SignerInfo, CardSignature>(
                new SignerInfo { CardId = cardId, PublicKeyBase64 = Bytes.ToString(faker.Random.Bytes(32), StringEncoding.BASE64) }, 
                new CardSignature { SignerCardId = cardId, Signature = faker.Random.Bytes(64) });
        }

        public static CardSignature CardSignature(this Faker faker)
        {
            return new CardSignature { SignerCardId = faker.CardId(), Signature = faker.Random.Bytes(64) };
        }

        public static CSR GenerateCSR(this Faker faker)
        {
            var crypto = new VirgilCardManagerCrypto();

            var keypair = crypto.GenerateKeys();

            var csr = CSR.Generate(crypto, new CSRParams
            {
                Identity = faker.Person.UserName,
                PublicKey = keypair.PublicKey,
                PrivateKey = keypair.PrivateKey
            });
            return csr;
        }

        public static RawCard RawCard(this Faker faker)
        {
            var csr = faker.GenerateCSR();
            return csr.RawCard;
        }

        public static CardManager CardManager(this Faker faker)
        {
            var crypto = new VirgilCardManagerCrypto();
            Func<Task<string>> obtainToken = async () =>
            {
                return "".ToString();
            };
            var accessmanager = new AccessManager(obtainToken);
            var apiToken = Bytes.ToString(faker.Random.Bytes(32), StringEncoding.HEX);
            var apiId = Bytes.ToString(faker.Random.Bytes(32), StringEncoding.HEX);
            var apiKeyPair = crypto.GenerateKeys();

            return new CardManager(new CardsManagerParams { 
                CardManagerCrypto = crypto,
                AccessManager = accessmanager
            });
        }


    }
}