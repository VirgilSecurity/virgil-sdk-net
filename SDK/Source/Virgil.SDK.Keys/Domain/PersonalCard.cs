namespace Virgil.SDK.Keys.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Clients;
    using Crypto;
    using Http;
    using TransferObject;

    public struct Services
    {
        public IPublicKeysClient PublicKeysClient { get; internal set; }
        public IVirgilCardClient VirgilCardClient { get; internal set; }
        public IPrivateKeysClient PrivateKeysClient { get; internal set; }
    }

    public class ServiceLocator
    {
        public const string ApplicationToken = "e872d6f718a2dd0bd8cd7d7e73a25f49";

        public static readonly PublicKeysConnection ApiEndpoint =
            new PublicKeysConnection(
                ApplicationToken,
                new Uri(@"https://keys-stg.virgilsecurity.com"));

        public static readonly PrivateKeysConnection PrivateApiEndpoint =
            new PrivateKeysConnection(
                ApplicationToken,
                new Uri(@"https://keys-stg.virgilsecurity.com"));

        private static readonly Services Services = new Services
        {
           PublicKeysClient = new PublicKeysClient(ApiEndpoint),
           VirgilCardClient = new VirgilCardClient(ApiEndpoint),
           PrivateKeysClient = new PrivateKeysClient(PrivateApiEndpoint, new KnownKeyProvider(new PublicKeysClient(ApiEndpoint))),
        };

        public static Services GetServices()
        {
            return default(Services);
        }
    }

    public class PersonalCardCreationCommand
    {
        private readonly string identity;
        private readonly IdentityType type;
        private readonly PrivateKey privateKey;
        private readonly PublicKey publicKey;

        public PersonalCardCreationCommand(string identity, IdentityType type)
        {
            this.identity = identity;
            this.type = type;

            using (var virgilKeyPair = new VirgilKeyPair())
            {
                this.privateKey = new PrivateKey(virgilKeyPair.PrivateKey());
                this.publicKey = new PublicKey(virgilKeyPair.PublicKey());
            }
        }

        public async Task<PersonalCard> Execute()
        {
            var services = ServiceLocator.GetServices();

            var cardDto = await services.VirgilCardClient.Create(
                publicKey.Data,
                type,
                identity,
                null,
                privateKey.Data);

            await services.PrivateKeysClient.Put(cardDto.PublicKey.Id, privateKey.Data);

            return new PersonalCard(cardDto, privateKey);
        }
    }

    public class PersonalCard : RecipientCard
    {
        internal PersonalCard(VirgilCardDto cardDto, PrivateKey privateKey) : base(cardDto)
        {
            this.PrivateKey = privateKey;
        }

        private PrivateKey PrivateKey { get; set; }

        public byte[] Decrypt(byte[] cipherData)
        {
            using (var cipher = new VirgilCipher())
            {
                return cipher.DecryptWithKey(cipherData, this.GetRecepientId(), this.PrivateKey.Data);
            }
        }

        public string Decrypt(string cipherData)
        {
            return Convert.ToBase64String(Decrypt(cipherData.GetBytes()));
        }

        public async Task SignCard(RecipientCard signedCard)
        {
            var services = ServiceLocator.GetServices();
            var sign = await services.VirgilCardClient.Sign(signedCard.Id, signedCard.Hash, Id, PrivateKey.Data);
        }

        public string Export()
        {
            throw new NotImplementedException();
        }

        public static async Task<PersonalCard> Create(string identity, IdentityType type)
        {
            using (var nativeKeyPair = new VirgilKeyPair())
            {
                var services = ServiceLocator.GetServices();

                var cardDto = await services.VirgilCardClient.Create(
                    nativeKeyPair.PublicKey(),
                    type,
                    identity,
                    null,
                    nativeKeyPair.PrivateKey());

                await services.PrivateKeysClient.Put(cardDto.PublicKey.Id, nativeKeyPair.PrivateKey());

                return new PersonalCard(cardDto, new PrivateKey(nativeKeyPair));
            }
        }

        public static async Task<PersonalCard> CreateAttachedTo(
            PersonalCard attachTarget,
            string identity,
            IdentityType type)
        {
            var services = ServiceLocator.GetServices();

            var cardDto = await services.VirgilCardClient.CreateAttached(
                attachTarget.PublicKeyId,
                type,
                identity,
                null,
                attachTarget.PrivateKey.Data);

            var domainCard = new PersonalCard(cardDto, attachTarget.PrivateKey);

            return domainCard;
        }

        public static List<PersonalCard> Load(string userId)
        {
            throw new NotImplementedException();
        }
    }
}