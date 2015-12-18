namespace Virgil.SDK.Keys.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Clients;
    using Clients.Authority;
    using Crypto;
    using TransferObject;
    
    public struct Services
    {
        public IPublicKeysClient PublicKeysClient { get; internal set; }
        public IVirgilCardClient VirgilCardClient { get; internal set; }
        public IPrivateKeysClient PrivateKeysClient { get; internal set; }
        public IIdentityService IdentityService { get; internal set; }
    }

    public class PersonalCard : RecipientCard
    {
        internal PersonalCard(VirgilCardDto cardDto, PrivateKey privateKey) : base(cardDto)
        {
            this.PrivateKey = privateKey;
        }

        public PrivateKey PrivateKey { get; set; }

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

        public static ITokenRequired BeginCreate(string identity, IdentityType type)
        {
            return new CardCreator(identity, type);
        }
        
        public static List<PersonalCard> Load(string userId)
        {
            throw new NotImplementedException();
        }
    }
}