namespace Virgil.SDK.Keys.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Clients;
    using Crypto;
    using TransferObject;

    public struct Services
    {
        public IPublicKeysClient PublicKeysClient { get; set; }
        public IVirgilCardClient VirgilCardClient { get; set; }
        public IPrivateKeysClient PrivateKeysClient { get; set; }
    }

    public class ServiceLocator
    {
        public static Services GetServices()
        {
            return default(Services);
        }
    }

    public class PersonalCardCreationCommand
    {
        private readonly string identity;
        private readonly VirgilIdentityType type;
        private readonly PrivateKey privateKey;
        private readonly PublicKey publicKey;

        public PersonalCardCreationCommand(string identity, VirgilIdentityType type)
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

    public class PersonalCard
    {
        private string Hash { get; set; }
        private bool IsConfirmed { get; set; }

        internal PersonalCard(VirgilCardDto cardDto, PrivateKey privateKey)
        {
            Id = cardDto.Id;
            Identity = new Identity(cardDto.Identity);
            KeyPair = new KeyPair(cardDto.PublicKey, privateKey);
            Hash = cardDto.Hash;
        }

        internal PersonalCard()
        {

        }

        public Guid Id { get; private set; }
        public Identity Identity { get; private set; }
        public KeyPair KeyPair { get; private set; }
        public IEnumerable<Sign> Signs { get; private set; }

        public string Decrypt(string cipherData, string password = null)
        {
            throw new NotImplementedException();
        }

        public string VerifyAndDecrypt(string cipherData, string password = null)
        {
            throw new NotImplementedException();
        }

        public string Export()
        {
            throw new NotImplementedException();
        }

        public static async Task<PersonalCard> Create(string identity, VirgilIdentityType type)
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

                var domainCard = new PersonalCard
                {
                    Id = cardDto.Id,
                    Identity = new Identity(cardDto.Identity),
                    KeyPair = new KeyPair(cardDto.PublicKey.Id, nativeKeyPair),
                    Hash = cardDto.Hash,
                    IsConfirmed = false
                };

                return domainCard;
            }
        }
        
        public static async Task<PersonalCard> AttachTo(KeyPair keyPair, string identity, VirgilIdentityType type)
        {
            var services = ServiceLocator.GetServices();
            
            var cardDto = await services.VirgilCardClient.AttachTo(
                keyPair.Id,
                type,
                identity,
                null,
                keyPair.PrivateKey.Data);

            var domainCard = new PersonalCard
            {
                Id = cardDto.Id,
                Identity = new Identity(cardDto.Identity),
                KeyPair = keyPair,
                Hash = cardDto.Hash,
                IsConfirmed = false
            };

            return domainCard;
        }

        public static PersonalCard Load(string userId)
        {
            throw new NotImplementedException();
        }
    }
}