namespace Virgil.SDK.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Exceptions;
    using Newtonsoft.Json;
    using Virgil.Crypto;
    using Virgil.SDK.Infrastructure;
    using Virgil.SDK.TransferObject;

    public class PersonalCard : RecipientCard
    {
        internal PersonalCard(VirgilCardDto cardDto, PrivateKey privateKey) : base(cardDto)
        {
            this.PrivateKey = privateKey;
            this.IsPrivateKeyEncrypted = VirgilKeyPair.IsPrivateKeyEncrypted(privateKey);
        }

        public PersonalCard(RecipientCard recipientCard, PrivateKey privateKey) : base(recipientCard)
        {
            this.PrivateKey = privateKey;
            this.IsPrivateKeyEncrypted = VirgilKeyPair.IsPrivateKeyEncrypted(privateKey);
        }

        internal PersonalCard(VirgilCardDto virgilCardDto, PublicKeyDto publicKey, PrivateKey privateKey)
        {
            this.VirgilCardDto = virgilCardDto;
            this.Id = virgilCardDto.Id;
            this.Identity = new Identity(virgilCardDto.Identity);
            this.PublicKey = new PublishedPublicKey(publicKey);
            this.Hash = virgilCardDto.Hash;
            this.CreatedAt = virgilCardDto.CreatedAt;
            this.PrivateKey = privateKey;
            this.IsPrivateKeyEncrypted = VirgilKeyPair.IsPrivateKeyEncrypted(privateKey);
        }

        public PrivateKey PrivateKey { get; }

        public bool IsPrivateKeyEncrypted { get; }

        public bool CheckPrivateKeyPassword(string password)
        {
            return VirgilKeyPair.CheckPrivateKeyPassword(this.PrivateKey.Data, password.GetBytes());
        }
        
        public byte[] Decrypt(byte[] cipherData, string privateKeyPassword = null)
        {
            using (var cipher = new VirgilCipher())
            {
                var contentInfoSize = VirgilCipherBase.DefineContentInfoSize(cipherData);
                if (contentInfoSize == 0)
                {
                    throw new ArgumentException("Content info header is missing or corrupted", nameof(cipherData));
                }

                byte[] result;
                if (privateKeyPassword != null)
                {
                    result = cipher.DecryptWithKey(cipherData, this.GetRecepientId(), this.PrivateKey.Data,
                        privateKeyPassword.GetBytes());
                }
                else
                {
                    result = cipher.DecryptWithKey(cipherData, this.GetRecepientId(), this.PrivateKey.Data);
                }

                return result;
            }
        }

        public string Decrypt(string cipherData, string privateKeyPassword = null)
        {
            return this.Decrypt(Convert.FromBase64String(cipherData), privateKeyPassword).GetString(Encoding.UTF8);
        }

        public async Task Sign(RecipientCard signedCard, string privateKeyPassword = null)
        {
            var services = ServiceLocator.Services;
            var sign = await services.Cards.Trust(
                signedCard.Id, 
                signedCard.Hash, 
                this.Id, 
                this.PrivateKey, 
                privateKeyPassword)
            .ConfigureAwait(false);
        }

        public async Task Unsign(RecipientCard signedCard, string privateKeyPassword = null)
        {
            var services = ServiceLocator.Services;
            await services.Cards.Untrust(
                signedCard.Id, 
                this.Id, 
                this.PrivateKey, 
                privateKeyPassword)
            .ConfigureAwait(false);
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

        public static PersonalCard Import(byte[] personalCard, string serializationPassword)
        {
            using (var cipher = new VirgilCipher())
            {
                var json = cipher.DecryptWithPassword(personalCard, serializationPassword.GetBytes(Encoding.UTF8));
                var dto = JsonConvert.DeserializeObject<PersonalCardStorageDto>(json.GetString());
                return new PersonalCard(dto.virgil_card, new PrivateKey(dto.private_key));
            }
        }

        public static async Task<PersonalCard> Create(
            IdentityTokenDto identityToken,
            string privateKeyPassword = null,
            Dictionary<string, string> customData = null)
        {
            var nativeKeyPair = privateKeyPassword != null ? new VirgilKeyPair(privateKeyPassword.GetBytes()) : new VirgilKeyPair();

            using (nativeKeyPair)
            {
                var privateKey = new PrivateKey(nativeKeyPair);
                var publicKey = new PublicKey(nativeKeyPair);

                var services = ServiceLocator.Services;

                var cardDto = await services.Cards.Create(
                    identityToken,
                    publicKey,
                    privateKey,
                    privateKeyPassword: privateKeyPassword,
                    customData: customData
                    ).ConfigureAwait(false);

                return new PersonalCard(cardDto, privateKey);
            }
        }

        public static async Task<PersonalCard> Create(
            string identity,
            string privateKeyPassword = null,
            Dictionary<string, string> customData = null)
        {
            var nativeKeyPair = privateKeyPassword != null ? new VirgilKeyPair(privateKeyPassword.GetBytes()) : new VirgilKeyPair();

            using (nativeKeyPair)
            {
                var privateKey = new PrivateKey(nativeKeyPair);
                var publicKey = new PublicKey(nativeKeyPair);

                var services = ServiceLocator.Services;

                var cardDto = await services.Cards.Create(
                    identity,
                    IdentityType.Email,
                    publicKey,
                    privateKey,
                    customData: customData).ConfigureAwait(false);

                return new PersonalCard(cardDto, privateKey);
            }
        }

        public static async Task<PersonalCard> Create(
            PersonalCard personalCard,
            IdentityTokenDto identityToken,
            Dictionary<string, string> customData = null)
        {
            var services = ServiceLocator.Services;

            var cardDto = await services.Cards.Create(
                identityToken,
                personalCard.PublicKey.Id,
                personalCard.PrivateKey,
                customData: customData).ConfigureAwait(false);

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
                customData: customData).ConfigureAwait(false);

            return new PersonalCard(cardDto, personalCard.PrivateKey);
        }

        public async Task UploadPrivateKey(string privateKeyPassword = null)
        {
            var services = ServiceLocator.Services;
            await services.PrivateKeys.Stash(this.Id, this.PrivateKey, privateKeyPassword).ConfigureAwait(false);
        }

        public static Task<PersonalCardLoader> BeginLoadAll(string identity, IdentityType type)
        {
            return PersonalCardLoader.Start(identity, type);
        }

        public static async Task<PersonalCard> LoadLatest(IdentityTokenDto token, string privateKeyPassword = null)
        {
            var services = ServiceLocator.Services;
            var searchResult = await services.Cards.Search(token.Value, token.Type)
                .ConfigureAwait(false);

            var card = searchResult
                .OrderByDescending(it => it.CreatedAt)
                .Select(it => new { PublicKeyId = it.PublicKey.Id, Id = it.Id })
                .FirstOrDefault();

            var grabResponse = await services.PrivateKeys.Get(card.Id, token)
                .ConfigureAwait(false);
            
            var privateKey = new PrivateKey(grabResponse.PrivateKey);

            var cards = await services.PublicKeys
                .GetExtended(card.PublicKeyId, card.Id, privateKey, privateKeyPassword)
                .ConfigureAwait(false);

            return
                cards.Select(it => new PersonalCard(it, privateKey))
                    .OrderByDescending(it => it.CreatedAt)
                    .FirstOrDefault();
        }
    }
}