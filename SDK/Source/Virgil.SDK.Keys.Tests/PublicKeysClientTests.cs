using System.Security.Policy;
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
        [Test, ExpectedException(typeof(UserDataNotFoundException))]
        public async void Should_ThrowException_When_PublicKeyByGivenUserDataNotFound()
        {
            var keysClient = new PkiClient("app_key");

            var ss = Virgil.Crypto.VirgilVersion.AsString();

            await keysClient.PublicKeys.Get(Guid.NewGuid());
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public async void Should_ThrowException_If_SearchUserDataValueArgumentIsNull()
        {
            var connection = Substitute.For<IConnection>();
            var keysClient = new PkiClient(connection);

            await keysClient.PublicKeys.Search(null, UserDataType.EmailId);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public async void Should_ThrowException_If_SearchUserDataValueArgumentIsEmpty()
        {
            var connection = Substitute.For<IConnection>();
            var keysClient = new PkiClient(connection);

            await keysClient.PublicKeys.Search("", UserDataType.EmailId);
        }

        [Test, ExpectedException(typeof(InvalidEnumArgumentException))]
        public async void Should_ThrowException_If_SearchUserDataTypeArgumentIsUnknown()
        {
            var connection = Substitute.For<IConnection>();
            var keysClient = new PkiClient(connection);

            await keysClient.PublicKeys.Search("testuser@virgilsecurity.com", UserDataType.Unknown);
        }
    }
}