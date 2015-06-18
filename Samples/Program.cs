using System;
using System.IO;
using System.Linq;
using System.Text;
using Virgil.PKI;
using Virgil.PKI.Models;

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

            var result = new byte[bytesRead];
            Array.Copy(buffer, result, bytesRead);
            return result;
        }
    }

    class Program
    {
        public const UserDataType UserIdType = UserDataType.Email;
        public const string UserId = "sample1@haqed.com";
        public const string SignerId = "sample1@haqed.com";
        public const string AppToken = "{SampleToken}";

        public static VirgilCertificate CreateUser(byte[] publicKey, UserDataType userIdType, string userId)
        {
            var pkiClient = new PkiClient(AppToken);
            var virgilAccount = pkiClient.Accounts.Register(new VirgilUserData(userIdType, userId), publicKey).Result;

            var virgilPublicKey = new VirgilCertificate(publicKey);
            virgilPublicKey.Id().SetAccountId(Encoding.UTF8.GetBytes(virgilAccount.AccountId.ToString()));
            virgilPublicKey.Id().SetCertificateId(Encoding.UTF8.GetBytes(virgilAccount.PublicKeys.First().PublicKeyId.ToString()));

            return virgilPublicKey;
        }

        public static VirgilCertificate GetPkiPublicKey(UserDataType userIdType, string userId)
        {
            var pkiClient = new PkiClient(AppToken);
            var publicKeys = pkiClient.PublicKeys.SearchKey(userId, userIdType).Result.ToArray();
            var publicKey = publicKeys.First();

            var virgilPublicKey = new VirgilCertificate(publicKey.PublicKey);
            virgilPublicKey.Id().SetCertificateId(Encoding.UTF8.GetBytes(publicKey.PublicKeyId.ToString()));

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
