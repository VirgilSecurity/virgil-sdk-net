namespace Virgil.SDK.Keys.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Crypto;
    using Newtonsoft.Json;
    using TransferObject;
    
    public class PersonalCard : RecipientCard
    {
        private readonly VirgilCardDto cardDto;

        internal PersonalCard(VirgilCardDto cardDto, PrivateKey privateKey) : base(cardDto)
        {
            this.cardDto = cardDto;
            this.PrivateKey = privateKey;
        }

        public PrivateKey PrivateKey { get; private set; }

        public Dictionary<string, string> CustomData => new Dictionary<string, string>(cardDto.CustomData);

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
            return (Decrypt(Convert.FromBase64String(cipherData))).GetString(Encoding.UTF8);
        }

        public async Task Sign(RecipientCard signedCard)
        {
            var services = ServiceLocator.GetServices();
            var sign = await services.VirgilCardClient.Sign(signedCard.Id, signedCard.Hash, Id, PrivateKey.Data);
        }

        public async Task Unsign(RecipientCard signedCard)
        {
            var services = ServiceLocator.GetServices();
            await services.VirgilCardClient.Unsign(signedCard.Id, this.Id, this.PrivateKey.Data);
        }

        internal class personal_card_dto
        {
            public VirgilCardDto virgil_card { get; set; }
            public byte[] private_key { get; set; }
        }

        public string Export()
        {
            var data = new personal_card_dto {virgil_card = cardDto, private_key = PrivateKey.Data};
            return JsonConvert.SerializeObject(data);
        }

        public byte[] Export(string password)
        {
            var data = new personal_card_dto { virgil_card = cardDto, private_key = PrivateKey.Data };
            var json = JsonConvert.SerializeObject(data);
            using (var cipher = new VirgilCipher())
            {
                cipher.AddPasswordRecipient(password.GetBytes(Encoding.UTF8));
                return cipher.Encrypt(json.GetBytes(Encoding.UTF8), true);
            }
        }

        public static PersonalCard Import(string personalCard)
        {
            var dto = JsonConvert.DeserializeObject<personal_card_dto>(personalCard);
            return new PersonalCard(dto.virgil_card, new PrivateKey(dto.private_key));
        }

        public static PersonalCard Import(byte[] personalCard, string password)
        {
            using (var cipher = new VirgilCipher())
            {
                var json = cipher.DecryptWithPassword(personalCard, password.GetBytes(Encoding.UTF8));
                var dto = JsonConvert.DeserializeObject<personal_card_dto>(json.GetString());
                return new PersonalCard(dto.virgil_card, new PrivateKey(dto.private_key));
            }
        }

        public static async Task<PersonalCard> Create(IdentityToken identityToken, Dictionary<string,string> customData = null)
        {
            using (var nativeKeyPair = new VirgilKeyPair())
            {
                var privateKey = new PrivateKey(nativeKeyPair);
                var publicKey = new PublicKey(nativeKeyPair);

                var services = ServiceLocator.GetServices();

                var cardDto = await services.VirgilCardClient.Create(
                        publicKey.Data,
                        identityToken.IdentityType,
                        identityToken.Identity,
                        customData,
                        privateKey.Data);

                return new PersonalCard(cardDto, privateKey);
            }
        }

        public static async Task<PersonalCard> Create(string identity, Dictionary<string, string> customData = null)
        {
            using (var nativeKeyPair = new VirgilKeyPair())
            {
                var privateKey = new PrivateKey(nativeKeyPair);
                var publicKey = new PublicKey(nativeKeyPair);

                var services = ServiceLocator.GetServices();

                var cardDto = await services.VirgilCardClient.Create(
                        publicKey.Data,
                        IdentityType.Email, 
                        identity,
                        customData,
                        privateKey.Data);

                return new PersonalCard(cardDto, privateKey);
            }
        }

        public static async Task<PersonalCard> AttachTo(PersonalCard personalCard, IdentityToken identityToken)
        {
            var services = ServiceLocator.GetServices();

            var cardDto = await services.VirgilCardClient.CreateAttached(
                    personalCard.PublicKeyId,
                    identityToken.IdentityType,
                    identityToken.Identity,
                    null,
                    personalCard.PrivateKey.Data);

            return new PersonalCard(cardDto, personalCard.PrivateKey);
        }

        public async Task UploadPrivateKey()
        {
            var services = ServiceLocator.GetServices();
            await services.PrivateKeysClient.Put(this.PublicKeyId, this.PrivateKey.Data);
        }

        public static List<PersonalCard> Load(IdentityToken identityToken)
        {
            // search by email
            // get token
            // try get private key ?
            // get all virgil cards
            throw new NotImplementedException();
        }
    }
}