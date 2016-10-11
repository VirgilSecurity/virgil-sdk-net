namespace Virgil.SDK.Tests
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Fakes;
    using FluentAssertions;
    using NUnit.Framework;

    using Virgil.SDK.Exceptions;
    using Virgil.SDK.HighLevel;

    public class HighLevelTests
    {
        [Test]
        public async Task GetRevokedCard_ExistingCard_ShouldThrowException()
        {
            VirgilConfig.Initialize(IntergrationHelper.AppAccessToken);

            // Application Credentials

            var appKey = VirgilKey.FromFile(IntergrationHelper.AppKeyPath, IntergrationHelper.AppKeyPassword);
            var appID = IntergrationHelper.AppID;

            // Create a Virgil Card
            
            var identity = "Alice-" + Guid.NewGuid();
            const string type = "member";

            var aliceKey = VirgilKey.Create("alice_key");
            var request = aliceKey.BuildCardRequest(identity, type);
            
            appKey.SignRequest(request, appID);
            var aliceCard = await VirgilCard.CreateAsync(request);

            // Revoke a Virgil Card

            await IntergrationHelper.RevokeCard(aliceCard.Id);
            aliceKey.Destroy();
            
            Assert.ThrowsAsync<VirgilClientException>(async () => await VirgilCard.GetAsync(aliceCard.Id));
        }

        [Test]
        public async Task EncryptAndSignData_MultipleRecipients_ShouldDecryptAndVerifyDataSuccessfully()
        {
            VirgilConfig.Initialize(IntergrationHelper.AppAccessToken);
            VirgilConfig.SetKeyStorage(new KeyStorageFake());

            var appKey = IntergrationHelper.GetVirgilAppKey();

            var aliceKey = VirgilKey.Create("alice_key");
            var bobKey = VirgilKey.Create("bob_key");

            var aliceIdentity = $"Alice-{Guid.NewGuid()}";
            var bobIdentity = $"Bob-{Guid.NewGuid()}";

            var aliceCardRequest = aliceKey.BuildCardRequest(aliceIdentity, "member");
            var bobCardRequest = bobKey.BuildCardRequest(bobIdentity, "member");

            appKey.SignRequest(aliceCardRequest, IntergrationHelper.AppID);
            appKey.SignRequest(bobCardRequest, IntergrationHelper.AppID);

            await VirgilCard.CreateAsync(aliceCardRequest);
            await VirgilCard.CreateAsync(bobCardRequest);

            var cards = (await VirgilCard.FindAsync(new[] {aliceIdentity, bobIdentity})).ToList();
            var plaintext = Encoding.UTF8.GetBytes("Hello Bob!");

            var cipherData = aliceKey.SignThenEncrypt(plaintext, cards);
            var decryptedData = bobKey.DecryptThenVerify(cipherData, cards.Single(it => it.Identity == aliceIdentity));

            decryptedData.ShouldBeEquivalentTo(plaintext);

            await Task.WhenAll(cards.Select(it => IntergrationHelper.RevokeCard(it.Id)));
            
            aliceKey.Destroy();
            bobKey.Destroy();
        }
    }
}