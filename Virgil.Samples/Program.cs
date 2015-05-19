using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Virgil.Samples
{
    class StreamSink : VirgilDataSink
    {
        private readonly Stream stream;

        public StreamSink(Stream target)
        {
            this.stream = target;
        }

        public override bool IsGood()
        {
            return this.stream.CanWrite;
        }

        public override void Write(byte[] data)
        {
            this.stream.Write(data, 0, data.Length);
        }
    }

    class StreamSource : VirgilDataSource
    {
        private readonly Stream stream;
        private readonly byte[] buffer;

        public StreamSource(Stream source)
        {
            this.stream = source;
            this.buffer = new byte[1024];
        }

        public override bool HasData()
        {
            return this.stream.CanRead && this.stream.Position < this.stream.Length;
        }

        public override byte[] Read()
        {
            var bytesRead = this.stream.Read(buffer, 0, buffer.Length);

            if (bytesRead == buffer.Length)
            {
                return buffer;
            }

            var arraySegment = new ArraySegment<byte>(buffer, 0, bytesRead);
            return arraySegment.ToArray();
        }
    }

    class Program
    {
        public const string UserIdType = "email";
        public const string UserId = "cak0339632@haqed.com";
        public const string SignerId = "cak0339632@haqed.com";

        public static VirgilCertificate CreateUser(byte[] publicKey, string userIdType, string userId)
        {
            var certificate = new
            {
                public_key = publicKey,
                user_data = new[]
                {
                    new
                    {
                        @class = "user_id",
                        type = userIdType,
                        value = userId
                    }
                }
            };

            var httpClient = new HttpClient();

            const string uri = "https://pki.virgilsecurity.com/objects/public-key";
            var json = JsonConvert.SerializeObject(certificate);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var responseMessage = httpClient.PostAsync(uri, content).Result;
            var reresponseText = responseMessage.Content.ReadAsStringAsync().Result;
            dynamic response = JsonConvert.DeserializeObject(reresponseText);

            string accountId = response.id.account_id;
            string publicKeyId = response.id.public_key_id;

            var virgilPublicKey = new VirgilCertificate(publicKey);
            virgilPublicKey.Id().SetAccountId(Encoding.UTF8.GetBytes(accountId));
            virgilPublicKey.Id().SetCertificateId(Encoding.UTF8.GetBytes(publicKeyId));

            return virgilPublicKey;
        }
        
        public static VirgilCertificate GetPkiPublicKey(string userIdType, string userId)
        {
            const string uri = "https://pki.virgilsecurity.com/objects/account/actions/search";
            var httpClient = new HttpClient();
            var payload = new Dictionary<string,string> {{userIdType, userId}};
            string json = JsonConvert.SerializeObject(payload, Formatting.None);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var responseMessage = httpClient.PostAsync(uri, content).Result;

            string responseText = responseMessage.Content.ReadAsStringAsync().Result;

            dynamic response = JsonConvert.DeserializeObject(responseText);

            dynamic publicKeyObject = response[0].public_keys[0];
            string publicKeyId = publicKeyObject.id.public_key_id;
            string publicKey = publicKeyObject.public_key;

            var virgilPublicKey = new VirgilCertificate(VirgilBase64.Decode(publicKey));
            virgilPublicKey.Id().SetCertificateId(Encoding.UTF8.GetBytes(publicKeyId));

            return virgilPublicKey;
        }

        static void Main()
        {
            GenerateKeys.Run();
            RegisterUser.Run();
            GetPublicKey.Run();
            EncryptSample.Run();
            DecryptSample.Run();
            SignSample.Run();
            VerifySample.Run();

            Console.ReadLine();
        }
    }
}
