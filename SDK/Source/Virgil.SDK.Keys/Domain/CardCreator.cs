namespace Virgil.SDK.Keys.Domain
{
    using System.Threading.Tasks;
    using Crypto;
    using TransferObject;

    public interface ITokenRequired
    {
        Task<IConfirmationRequired> RequestToken();
    }

    public interface IConfirmationRequired
    {
        Task<IPublicKeyOptions> Confirm(string code);
    }

    public interface IPublicKeyOptions
    {
        Task<IPrivateKeyOptions> CreateCard();
        Task<PersonalCard> AttachToExistingCard(PersonalCard target);
    }

    public interface IPrivateKeyOptions
    {
        Task<PersonalCard> UploadPrivateKey();
        PersonalCard KeepPrivateKeyLocally();
    }

    public class CardCreator : IPublicKeyOptions, IPrivateKeyOptions
    {
        private readonly string identity;
        private readonly IdentityType type;
        private readonly PrivateKey privateKey;
        private readonly PublicKey publicKey;

        private readonly Services services;

        private VirgilCardDto cardDto;
        
        public CardCreator(string identity, IdentityType type)
        {
            this.identity = identity;
            this.type = type;

            using (var nativeKeyPair = new VirgilKeyPair())
            {
                this.privateKey = new PrivateKey(nativeKeyPair);
                this.publicKey = new PublicKey(nativeKeyPair);
            }

            this.services = ServiceLocator.GetServices();
        }
        
        public async Task<IPrivateKeyOptions> CreateCard()
        {
            this.cardDto = await services.VirgilCardClient.Create(
                    publicKey.Data,
                    type,
                    identity,
                    null,
                    privateKey.Data);

            return this;
        }
        public async Task<PersonalCard> AttachToExistingCard(PersonalCard target)
        {
            this.cardDto = await services.VirgilCardClient.CreateAttached(
               target.PublicKeyId,
               type,
               identity,
               null,
               target.PrivateKey.Data);

            return new PersonalCard(this.cardDto, target.PrivateKey);
        }

        public async Task<PersonalCard> UploadPrivateKey()
        {
            await services.PrivateKeysClient.Put(cardDto.PublicKey.Id, privateKey.Data);
            return new PersonalCard(cardDto, privateKey);
        }
        public PersonalCard KeepPrivateKeyLocally()
        {
            return new PersonalCard(cardDto, privateKey);
        }
    }
}