namespace Virgil.SDK.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Virgil.Crypto;
    using Virgil.SDK.Infrastructure;
    using Virgil.SDK.TransferObject;

    public class PersonalCard : RecipientCard
    {
        internal PersonalCard(VirgilCardDto cardDto, PrivateKey privateKey) : base(cardDto)
        {
            this.PrivateKey = privateKey;
        }

        public PersonalCard(RecipientCard recipientCard, PrivateKey privateKey) : base(recipientCard)
        {
            this.PrivateKey = privateKey;
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
            return this.Decrypt(Convert.FromBase64String(cipherData)).GetString(Encoding.UTF8);
        }

        public async Task Sign(RecipientCard signedCard)
        {
            var services = ServiceLocator.Services;
            var sign = await services.Cards.Trust(signedCard.Id, signedCard.Hash, this.Id, this.PrivateKey).ConfigureAwait(false);
        }

        public async Task Unsign(RecipientCard signedCard)
        {
            var services = ServiceLocator.Services;
            await services.Cards.Untrust(signedCard.Id, this.Id, this.PrivateKey).ConfigureAwait(false);
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
            IdentityTokenDto identityToken,
            Dictionary<string, string> customData = null)
        {
            using (var nativeKeyPair = new VirgilKeyPair())
            {
                var privateKey = new PrivateKey(nativeKeyPair);
                var publicKey = new PublicKey(nativeKeyPair);

                var services = ServiceLocator.Services;

                var cardDto = await services.Cards.Create(
                    identityToken,
                    publicKey,
                    privateKey,
                    customData: customData
                    ).ConfigureAwait(false);

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

        public async Task UploadPrivateKey()
        {
            var services = ServiceLocator.Services;
            await services.PrivateKeys.Stash(this.Id, this.PrivateKey).ConfigureAwait(false);
        }

        public static async Task<IEnumerable<PersonalCard>> Load(IdentityTokenDto identityToken)
        {
            var services = ServiceLocator.Services;
            var searchResult = await services.Cards.Search(identityToken.Value, identityToken.Type).ConfigureAwait(false);

            var card = searchResult.FirstOrDefault();

            var privateKey = await services.PrivateKeys.Get(card.Id, identityToken).ConfigureAwait(false);
            var cards = await services.PublicKeys.GetExtended(card.PublicKey.Id, card.Id, privateKey.PrivateKey).ConfigureAwait(false);

            return cards.Select(it => new PersonalCard(it, new PrivateKey(privateKey.PrivateKey))).ToList();
        }

        public static async Task<IEnumerable<PersonalCard>> Load(string identity, IdentityType type, PrivateKey privateKey)
        {
            var services = ServiceLocator.Services;
            var searchResult = await services.Cards.Search(identity, type).ConfigureAwait(false);

            var result = searchResult.Select(it => new
            {
                PublicKeyId = it.PublicKey.Id,
                Id = it.Id,
                
            }).Select(async card =>
            {
                var cards = await services.PublicKeys.GetExtended(card.PublicKeyId, card.Id, privateKey).ConfigureAwait(false);
                return cards.Select(it => new PersonalCard(it, privateKey)).ToList();
            }).ToList();

            await Task.WhenAll(result).ConfigureAwait(false);

            return result.SelectMany(it => it.Result).ToList();
        }

    }
}