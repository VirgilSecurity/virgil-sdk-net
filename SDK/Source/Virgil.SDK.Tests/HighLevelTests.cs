namespace Virgil.SDK.Tests
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Virgil.SDK.Cryptography;
    using Exceptions;
    using HighLevel;
    using NUnit.Framework;

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

            var aliceKey = VirgilKey.Load("alice_key");
            var request = aliceKey.BuildCardCreationRequest(identity, type);
            
            appKey.SignRequestAsAuthority(request, appID);
            var aliceCard = await VirgilCard.CreateAsync(request);

            // Revoke a Virgil Card

            await IntergrationHelper.RevokeCard(aliceCard.Id);
            
            Assert.ThrowsAsync<VirgilClientException>(async () => await VirgilCard.GetAsync(aliceCard.Id));
        }

        [Test]
        public async Task Task()
        {
            var crypto = new VirgilCrypto();

            var keyPair = crypto.GenerateKeys();

            using (var inputStream = new FileStream(@"C:\Users\kuril\Desktop\jpysb.jpg", FileMode.Open))
            using (var outputStream = new FileStream(@"C:\Users\kuril\Desktop\jpysb.jpg.enc", FileMode.Create))
            {
                crypto.Encrypt(inputStream, outputStream, keyPair.PublicKey);
            }

            var privateKeyBase64 = Convert.ToBase64String(crypto.ExportPrivateKey(keyPair.PrivateKey));
            ;
        }
    }
}