namespace Virgil.SDK.Keys.Domain
{
    using System;
    using System.Collections.Generic;
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

        public PrivateKey PrivateKey { get; private set; }

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

        public static async Task<PersonalCard> CreateConfirmed(AccessToken accessToken)
        {
            using (var nativeKeyPair = new VirgilKeyPair())
            {
                var privateKey = new PrivateKey(nativeKeyPair);
                var publicKey = new PublicKey(nativeKeyPair);

                var services = ServiceLocator.GetServices();

                var cardDto = await services.VirgilCardClient.Create(
                        publicKey.Data,
                        accessToken.IdentityType,
                        accessToken.Identity,
                        null,
                        privateKey.Data);

                return new PersonalCard(cardDto, privateKey);
            }
        }

        public static async Task<PersonalCard> AttachAsComfirmed(PersonalCard personalCard, AccessToken accessToken)
        {
            throw new NotImplementedException();
        }

        public static async Task<PersonalCard> CreateUnconfirmed(string value, IdentityType type)
        {
            using (var nativeKeyPair = new VirgilKeyPair())
            {
                var privateKey = new PrivateKey(nativeKeyPair);
                var publicKey = new PublicKey(nativeKeyPair);

                var services = ServiceLocator.GetServices();

                var cardDto = await services.VirgilCardClient.Create(
                        publicKey.Data,
                        type,
                        value,
                        null,
                        privateKey.Data);

                return new PersonalCard(cardDto, privateKey);
            }
        }

        public static async Task<PersonalCard> AttachAsUnconfirmed(PersonalCard personalCard, string value, IdentityType type)
        {
            throw new NotImplementedException();
        }

        public async Task UploadPrivateKey()
        {
            var services = ServiceLocator.GetServices();
            await services.PrivateKeysClient.Put(this.PublicKeyId, this.PrivateKey.Data);
        }

        public static List<PersonalCard> Load(AccessToken accessToken)
        {
            // search by email
            // get token
            // try get private key ?
            // get all virgil cards
            throw new NotImplementedException();
        }
    }

    public class AccessToken
    {
        private readonly VirgilVerifyResponse request;

        private AccessToken(VirgilVerifyResponse virgilVerifyResponse)
        {
            request = virgilVerifyResponse;
        }

        public static async Task<AccessToken> Request(string email, IdentityType identityType)
        {
            var identityService = ServiceLocator.GetServices().IdentityService;
            var request = await identityService.Verify(email, identityType);
            return new AccessToken(request)
            {
                Identity = email,
                IdentityType = identityType
            };
        }

        public VirgilIndentityToken Token { get; private set; }

        public string Identity { get; private set; }

        public IdentityType IdentityType { get; private set; }

        public bool Confirmed { get; private set; }

        public async Task Confirm(string confirmationCode)
        {
            var identityService = ServiceLocator.GetServices().IdentityService;
            Token = await identityService.Confirm(confirmationCode, request.Id, 3600, 1);
            Confirmed = true;
        }
    }
}