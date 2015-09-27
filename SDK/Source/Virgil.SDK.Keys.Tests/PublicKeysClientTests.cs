namespace Virgil.SDK.Keys.Tests
{

    using System.Linq;
    using Virgil.Crypto;
    using Virgil.SDK.Keys.Model;
    using System;
    using System.Threading.Tasks;
    using FluentAssertions;
    using NSubstitute;

    using NUnit.Framework;

    using Exceptions;
    using Http;

    public static class Constants
    {
        public const string ApplicationToken = "72ec86432dc166106289d0b51a879371";
    }

    public class DeletePublicKeys
    {
        public static readonly Connection ApiEndpoint = new Connection(Constants.ApplicationToken, new Uri(@"https://keys.virgilsecurity.com"));

        [Test]
        public async Task Should_BeAbleTo_CreateConfirmedPublicKey()
        {
            var connection = ApiEndpoint;
            var keysClient = new KeysClient(connection);

            var userIds = new[]
            {
                GetUserName(),
                GetUserName()
            };

            var bundle = await CreateAccountWithUserIds(keysClient, userIds);

            bundle.PublicKey.UserData.Count().Should().Be(2);

            var userDatas = bundle.PublicKey.UserData.ToArray();

            await keysClient.PublicKeys.Search(userDatas[0].Value);
            await keysClient.PublicKeys.Search(userDatas[1].Value);
        }

        [Test]
        public async Task Should_BeAbleToDeletePublicKey_If_UserLostPrivateKey()
        {
            var connection = ApiEndpoint;
            var keysClient = new KeysClient(connection);

            var userIds = new[]
            {
                GetUserName(),
                GetUserName()
            };

            var bundle = await CreateAccountWithUserIds(keysClient, userIds);

            var request = await keysClient.PublicKeys.Delete(bundle.PublicKey.PublicKeyId);
            await Task.Delay(TimeSpan.FromSeconds(2));

            var codesReceived = await GetConfirmationCodeFromLastMails(userIds);

            await keysClient.PublicKeys.ConfirmDelete(bundle.PublicKey.PublicKeyId, new PublicKeyOperationComfirmation
            {
                ActionToken = request.ActionToken,
                ConfirmationCodes = codesReceived
            });

            bool catched = false;

            try
            {
                await keysClient.PublicKeys.Search(bundle.PublicKey.UserData.First().Value);
            }
            catch (KeysServiceRequestException)
            {
                catched = true;
            }

            catched.Should().Be(true);
        }

        [Test]
        public async Task Should_BeAbleToResetPublicKey_If_UserLostPrivateKey()
        {
            var connection = ApiEndpoint;
            var keysClient = new KeysClient(connection);

            var userIds = new[]
            {
                GetUserName(),
                GetUserName()
            };

            var bundle = await CreateAccountWithUserIds(keysClient, userIds);


            var virgilKeyPair = new VirgilKeyPair();

            var request = await keysClient.PublicKeys.Reset(
                bundle.PublicKey.PublicKeyId,
                virgilKeyPair.PublicKey(),
                virgilKeyPair.PrivateKey());

            await Task.Delay(TimeSpan.FromSeconds(2));

            var codesReceived = await GetConfirmationCodeFromLastMails(userIds);

            await keysClient.PublicKeys.ConfirmReset(bundle.PublicKey.PublicKeyId,
                virgilKeyPair.PrivateKey(),
                new PublicKeyOperationComfirmation
                {
                    ActionToken = request.ActionToken,
                    ConfirmationCodes = codesReceived
                });

            var publicKey = await keysClient.PublicKeys.Search(bundle.PublicKey.UserData.First().Value);

            publicKey.Key.Should().BeEquivalentTo(virgilKeyPair.PublicKey());
        }

        [Test]
        public async Task Should_BeAbleToResendConfirmation_On_CreateAccount()
        {
            var connection = ApiEndpoint;
            var keysClient = new KeysClient(connection);

            var userIds = new[]
            {
                GetUserName(),
                GetUserName()
            };

            var bundle = await CreateAccountWithUserIds(keysClient, userIds);

            var virgilKeyPair = new VirgilKeyPair();

            var request = await keysClient.PublicKeys.Reset(
                bundle.PublicKey.PublicKeyId,
                virgilKeyPair.PublicKey(),
                virgilKeyPair.PrivateKey());

            await Task.Delay(TimeSpan.FromSeconds(2));

            await keysClient.UserData.ResendConfirmation(
                bundle.PublicKey.UserData.First().UserDataId,
                bundle.PublicKey.PublicKeyId,
                bundle.PrivateKey);

            await Task.Delay(TimeSpan.FromSeconds(2));

        }


        private static async Task<string[]> GetConfirmationCodeFromLastMails(params string[] emails)
        {
            var tasks = emails.Select(GetConfirmationCodeFromLastMail).ToArray();
            await Task.WhenAll(tasks);
            return tasks.Select(it => it.Result).ToArray();
        }

        private static async Task<string> GetConfirmationCodeFromLastMail(string emailAtMailinator)
        {
            var inbox = await Mailinator.FetchInbox(emailAtMailinator);
            var email = await Mailinator.FetchEmail(inbox.Last().id);
            return email.FindCode();
        }

        private static async Task<Bundle> CreateAccountWithUserIds(KeysClient keysClient, string[] userIds)
        {
            var keyPair = new VirgilKeyPair();

            var udata = userIds.Select(userId => new UserData
            {
                Value = userId,
                Class = UserDataClass.UserId,
                Type = UserDataType.EmailId
            }).ToArray();

            var publicKey = await keysClient.PublicKeys.Create(keyPair.PublicKey(), keyPair.PrivateKey(), udata);

            await Task.Delay(TimeSpan.FromSeconds(2));

            var tasks = publicKey.UserData.Select(async userData =>
            {
                var confirmationCode = await GetConfirmationCodeFromLastMail(userData.Value);

                await keysClient.UserData.Confirm(
                    userData.UserDataId,
                    confirmationCode,
                    publicKey.PublicKeyId,
                    keyPair.PrivateKey());

            }).ToArray();

            await Task.WhenAll(tasks);

            return new Bundle
            {
                PublicKey = publicKey,
                PrivateKey = keyPair.PrivateKey()
            };
        }

        private static string GetUserName()
        {
            var random = Guid.NewGuid().ToString().Replace("-", "");
            return $"test_{random}@mailinator.com";
        }

        class Bundle
        {
            public PublicKeyExtended PublicKey { get; set; }
            public byte[] PrivateKey { get; set; }
        }
    }


    public class PublicKeysClientTests
    {
        [Test, ExpectedException(typeof(KeysServiceRequestException))]
        public async void Should_ThrowException_When_PublicKeyByGivenUserDataNotFound()
        {
            var connection = new Connection(Constants.ApplicationToken, new Uri(@"https://keys-stg.virgilsecurity.com"));
            var keysClient = new KeysClient(connection);

            var keyPair = new VirgilKeyPair();
            var userData = new UserData
            {
                Value = $"{Guid.NewGuid()}@mailinator.com",
                Class = UserDataClass.UserId,
                Type = UserDataType.EmailId
            };

            await keysClient.PublicKeys.Create(keyPair.PublicKey(), keyPair.PrivateKey(), userData);
            var result = await keysClient.PublicKeys.Search(userData.Value);
        }


        [Test, ExpectedException(typeof(ArgumentNullException))]
        public async void Should_ThrowException_If_SearchUserDataValueArgumentIsNull()
        {
            var connection = Substitute.For<IConnection>();
            var keysClient = new KeysClient(connection);

            await keysClient.PublicKeys.Search(null);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public async void Should_ThrowException_If_SearchUserDataValueArgumentIsEmpty()
        {
            var connection = Substitute.For<IConnection>();
            var keysClient = new KeysClient(connection);

            await keysClient.PublicKeys.Search("");
        }
    }
}