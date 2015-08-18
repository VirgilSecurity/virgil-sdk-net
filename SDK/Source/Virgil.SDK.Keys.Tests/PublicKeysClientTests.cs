using System.Linq;
using System.Security.Policy;
using Virgil.Crypto;
using Virgil.SDK.Keys.Model;

namespace Virgil.SDK.Keys.Tests
{
    using System;
    using System.ComponentModel;

    using NSubstitute;

    using NUnit.Framework;

    using Virgil.SDK.Keys.Exceptions;
    using Virgil.SDK.Keys.Http;

    public class PublicKeysClientTests
    {
        private const string ApplicationToken = "e872d6f718a2dd0bd8cd7d7e73a25f49";

        [Test, ExpectedException(typeof(UserDataNotFoundException))]
        public async void Should_ThrowException_When_PublicKeyByGivenUserDataNotFound()
        {
            var connection = new Connection(ApplicationToken, new Uri(@"https://keys-stg.virgilsecurity.com"));
            var keysClient = new PkiClient(connection);
            
            var keyPair = new VirgilKeyPair();
            var userData = new UserData
            {
                Value = "heki@inboxstore.me",
                Class = UserDataClass.UserId,
                Type = UserDataType.EmailId
            };

            var publicKey = await keysClient.PublicKeys.Create(keyPair.PublicKey(), keyPair.PrivateKey(), userData);
            var confirmationCode = "";

            await keysClient.UserData.Confirm(publicKey.UserData.First().UserDataId, confirmationCode, publicKey.PublicKeyId,
                keyPair.PrivateKey());

            // await keysClient.PublicKeys.Get(Guid.NewGuid());
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public async void Should_ThrowException_If_SearchUserDataValueArgumentIsNull()
        {
            var connection = Substitute.For<IConnection>();
            var keysClient = new PkiClient(connection);

            // await keysClient.PublicKeys.Search(null, UserDataType.EmailId);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public async void Should_ThrowException_If_SearchUserDataValueArgumentIsEmpty()
        {
            var connection = Substitute.For<IConnection>();
            var keysClient = new PkiClient(connection);

            // await keysClient.PublicKeys.Search("", UserDataType.EmailId);
        }

        [Test, ExpectedException(typeof(InvalidEnumArgumentException))]
        public async void Should_ThrowException_If_SearchUserDataTypeArgumentIsUnknown()
        {
            var connection = Substitute.For<IConnection>();
            var keysClient = new PkiClient(connection);

            // await keysClient.PublicKeys.Search("testuser@virgilsecurity.com", UserDataType.Unknown);
        }
    }
}