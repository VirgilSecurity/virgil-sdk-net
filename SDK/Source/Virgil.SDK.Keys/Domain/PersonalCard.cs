namespace Virgil.SDK.Keys.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Clients;
    using Crypto;
    using Virgil.SDK.Keys.Infrastructure;
    using Newtonsoft.Json;
    using TransferObject;

    public class PersonalCard : RecipientCard
    {
        internal PersonalCard(VirgilCardDto cardDto, PrivateKey privateKey) : base(cardDto)
        {
            this.PrivateKey = privateKey;
        }

        public PrivateKey PrivateKey { get; }

        public byte[] Decrypt(byte[] cipherData)
        {
            using (var cipher = new VirgilCipher())
            {
                var contentInfoSize = VirgilCipherBase.DefineContentInfoSize(cipherData);
                if (contentInfoSize == 0)
                {
                    throw new ArgumentException("Content info header is missing or corrupted", nameof(cipherData));
                }

                return cipher.DecryptWithKey(cipherData, this.GetRecepientId(), this.PrivateKey.Data);
            }
        }

        public string Decrypt(string cipherData)
        {
            return (this.Decrypt(Convert.FromBase64String(cipherData))).GetString(Encoding.UTF8);
        }

        public async Task Sign(RecipientCard signedCard)
        {
            var services = ServiceLocator.Services;
            var sign = await services.Cards.Trust(signedCard.Id, signedCard.Hash, this.Id, this.PrivateKey);
        }

        public async Task Unsign(RecipientCard signedCard)
        {
            var services = ServiceLocator.Services;
            await services.Cards.Untrust(signedCard.Id, this.Id, this.PrivateKey);
        }

        public string Export()
        {
            var data = new PersonalCardStorageDto
            {
                virgil_card = this.VirgilCardDto,
                private_key = this.PrivateKey.Data
            };

            return JsonConvert.SerializeObject(data);
        }

        public byte[] Export(string password)
        {
            var data = new PersonalCardStorageDto
            {
                virgil_card = this.VirgilCardDto,
                private_key = this.PrivateKey.Data
            };
            var json = JsonConvert.SerializeObject(data);
            using (var cipher = new VirgilCipher())
            {
                cipher.AddPasswordRecipient(password.GetBytes(Encoding.UTF8));
                return cipher.Encrypt(json.GetBytes(Encoding.UTF8), true);
            }
        }

        public static PersonalCard Import(string personalCard)
        {
            var dto = JsonConvert.DeserializeObject<PersonalCardStorageDto>(personalCard);
            return new PersonalCard(dto.virgil_card, new PrivateKey(dto.private_key));
        }

        public static PersonalCard Import(byte[] personalCard, string password)
        {
            using (var cipher = new VirgilCipher())
            {
                var json = cipher.DecryptWithPassword(personalCard, password.GetBytes(Encoding.UTF8));
                var dto = JsonConvert.DeserializeObject<PersonalCardStorageDto>(json.GetString());
                return new PersonalCard(dto.virgil_card, new PrivateKey(dto.private_key));
            }
        }

        public static async Task<PersonalCard> Create(
            IdentityToken identityToken,
            Dictionary<string, string> customData = null)
        {
            using (var nativeKeyPair = new VirgilKeyPair())
            {
                var privateKey = new PrivateKey(nativeKeyPair);
                var publicKey = new PublicKey(nativeKeyPair);

                var services = ServiceLocator.Services;

                var cardDto = await services.Cards.Create(
                    identityToken.Token,
                    publicKey,
                    privateKey,
                    customData: customData
                    );

                return new PersonalCard(cardDto, privateKey);
            }
        }

        public static async Task<PersonalCard> Create(
            string identity,
            Dictionary<string, string> customData = null)
        {
            using (var nativeKeyPair = new VirgilKeyPair())
            {
                var privateKey = new PrivateKey(nativeKeyPair);
                var publicKey = new PublicKey(nativeKeyPair);

                var services = ServiceLocator.Services;

                var cardDto = await services.Cards.Create(
                    identity,
                    IdentityType.Email,
                    publicKey,
                    privateKey,
                    customData: customData);

                return new PersonalCard(cardDto, privateKey);
            }
        }

        public static async Task<PersonalCard> Create(
            PersonalCard personalCard,
            IdentityToken identityToken,
            Dictionary<string, string> customData = null)
        {
            var services = ServiceLocator.Services;

            var cardDto = await services.Cards.Create(
                identityToken.Token,
                personalCard.PublicKey.Id,
                personalCard.PrivateKey,
                customData: customData);

            return new PersonalCard(cardDto, personalCard.PrivateKey);
        }

        public static async Task<PersonalCard> Create(
            PersonalCard personalCard, 
            string identity,
            Dictionary<string, string> customData = null)
        {
            var services = ServiceLocator.Services;

            var cardDto = await services.Cards.Create(
                identity,
                IdentityType.Email,
                personalCard.PublicKey.Id,
                personalCard.PrivateKey,
                customData: customData);

            return new PersonalCard(cardDto, personalCard.PrivateKey);
        }

        public async Task UploadPrivateKey()
        {
            var services = ServiceLocator.Services;
            await services.PrivateKeys.Stash(this.Id, this.PrivateKey);
        }

        public static List<PersonalCard> Load(IdentityToken identityToken)
        {
            var services = ServiceLocator.Services;

            Bootsrapper.UseAccessToken("ASDASDASDASD").Build();
            Bootsrapper.UseAccessToken("ASDASDASDASD").WithStagingEndpoints().Build();


            // search by email
            // get token
            // try get private key ?
            // get all virgil cards
            throw new NotImplementedException();
        }
    }
}