namespace Virgil.SDK.Keys.Tests
{
    using System;
    using System.ComponentModel;
    using System.Net;

    using NSubstitute;
    using NSubstitute.ExceptionExtensions;

    using NUnit.Framework;

    using Virgil.Crypto;
    
    using Virgil.SDK.Keys.Exceptions;
    using Virgil.SDK.Keys.Model;
    using Virgil.SDK.Keys.Http;

    public class AccountsClientTests
    {
        [Test]
        public async void Should_CreateAccount_Given()
        {
            var client = new PkiClient(new Connection("e88c4106cfddb959d62afb14a767c3e9",
                new Uri("https://keys-stg.virgilsecurity.com/v2/")));

            var keyPair = new VirgilKeyPair();

            var publicKey = Convert.ToBase64String(keyPair.PublicKey());
            var privateKey = Convert.ToBase64String(keyPair.PrivateKey());

            try
            {
                var account = await client.Accounts.Register(UserDataType.EmailId, "socotab@trickmail.net", keyPair.PublicKey());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Test, ExpectedException(typeof(UserDataAlreadyExistsException))]
        public async void Should_ThrowException_When_AccountWithUserDataAlreadyExists()
        {
            var connection = Substitute.For<IConnection>();
            var publicKey = new Virgil.Crypto.VirgilKeyPair().PublicKey();
            
            connection.Send(null).ThrowsForAnyArgs(new KeysServiceException(20210, "", 
                HttpStatusCode.BadRequest, @"{ ""error"": { ""code"": 20210 } }"));
            
            var keysClient = new PkiClient(connection);
            await keysClient.Accounts.Register(UserDataType.EmailId, "testuser@virgilsecurity.com", publicKey);
        }

        [Test, ExpectedException(typeof(InvalidEnumArgumentException))]
        public async void Should_ThrowException_When_DataTypeParameterIsUserInfo()
        {
            var connection = Substitute.For<IConnection>();
            var keysClient = new PkiClient(connection);
            var publicKey = new Virgil.Crypto.VirgilKeyPair().PublicKey();

            await keysClient.Accounts.Register(UserDataType.FirstNameInfo, "testuser@virgilsecurity.com", publicKey);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public async void Should_ThrowException_When_UserIdParameterIsNull()
        {
            var connection = Substitute.For<IConnection>();
            var keysClient = new PkiClient(connection);
            var publicKey = new Virgil.Crypto.VirgilKeyPair().PublicKey();

            await keysClient.Accounts.Register(UserDataType.EmailId, null, publicKey);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public async void Should_ThrowException_When_UserIdParameterIsEmpty()
        {
            var connection = Substitute.For<IConnection>();
            var keysClient = new PkiClient(connection);
            var publicKey = new Virgil.Crypto.VirgilKeyPair().PublicKey();

            await keysClient.Accounts.Register(UserDataType.EmailId, "", publicKey);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public async void Should_ThrowException_When_PublicKeyParameterIsNull()
        {
            var connection = Substitute.For<IConnection>();
            var keysClient = new PkiClient(connection);

            await keysClient.Accounts.Register(UserDataType.EmailId, "testuser@virgilsecurity.com", null);
        }
    }
}
