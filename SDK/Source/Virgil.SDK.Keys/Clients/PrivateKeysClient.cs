namespace Virgil.SDK.Keys.Clients
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using Crypto;
    using Http;
    using Newtonsoft.Json;
    using TransferObject;

    public class KnownKey
    {
        public KnownKey(Guid knownPublicKeyId, byte[] knownPublicKey)
        {
            KnownPublicKey = knownPublicKey;
            KnownPublicKeyId = knownPublicKeyId;
        }

        public byte[] KnownPublicKey { get; }

        public Guid KnownPublicKeyId { get; }
    }

    public interface IKnownKeyProvider
    {
        Task<KnownKey> Get();
    }

    public class KnownKeyProvider : IKnownKeyProvider
    {
        private readonly IPublicKeysClient publicKeysClient;
        private PublicKeyDto publicKeyDto;

        public KnownKeyProvider(IPublicKeysClient publicKeysClient)
        {
            this.publicKeysClient = publicKeysClient;
        }

        public async Task<KnownKey> Get()
        {
            if (publicKeyDto == null)
            {
                publicKeyDto = await publicKeysClient.Get(Guid.NewGuid());
            }

            return new KnownKey(publicKeyDto.Id, publicKeyDto.PublicKey);
        }
    }

    public class PrivateKeysClient : EndpointClient
    {
        private readonly IKnownKeyProvider provider;

        public PrivateKeysClient(IConnection connection, IKnownKeyProvider provider) : base(connection)
        {
            this.provider = provider;
        }

        public async Task Put(Guid publicKeyId, byte[] privateKey)
        {
            var body = new
            {
                private_key = privateKey,
                public_key_id = publicKeyId,
                request_sign_uuid = Guid.NewGuid().ToString().ToLowerInvariant()
            };

            var args = await provider.Get();

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .SignRequestForPrivateService(privateKey)
                .EncryptJsonBody(args.KnownPublicKeyId, args.KnownPublicKey)
                .WithEndpoint("/v3/private-key");
                
            await this.Send(request);
        }

        public async Task Get(Guid publicKeyId)
        {
            string randomPassword = Guid.NewGuid().ToString();

            var body = new
            {
                response_password = randomPassword,
                public_key_id = publicKeyId,
                request_sign_uuid = Guid.NewGuid().ToString().ToLowerInvariant()
            };

            var args = await provider.Get();

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                
                .EncryptJsonBody(args.KnownPublicKeyId, args.KnownPublicKey)
                .WithEndpoint("/v3/private-key/actions/grab");

            var response = await this.Send(request);

            var encryptedBody = response.Body;

            using (var cipher = new VirgilCipher())
            {
                var bytes = cipher.DecryptWithPassword(
                    Encoding.UTF8.GetBytes(encryptedBody),
                    Encoding.UTF8.GetBytes(randomPassword));

                var decryptedBody = Encoding.UTF8.GetString(bytes, 0, bytes.Length);

                JsonConvert.DeserializeObject<GrabResponse>(decryptedBody);
            }
        }

        public async Task Delete(Guid publicKeyId, byte[] privateKey)
        {
            var body = new
            {
                public_key_id = publicKeyId,
                request_sign_uuid = Guid.NewGuid().ToString().ToLowerInvariant()
            };

            var args = await provider.Get();

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .SignRequestForPrivateService(privateKey)
                .EncryptJsonBody(args.KnownPublicKeyId, args.KnownPublicKey)
                .WithEndpoint("/v3/private-key/actions/delete");

            await this.Send(request);
        }
    }
}