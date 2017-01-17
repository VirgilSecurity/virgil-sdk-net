namespace Virgil.SDK.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Threading.Tasks;

    using Virgil.SDK.Client;
    using Virgil.SDK.Common;
    using Virgil.SDK.Cryptography;
    
    using Newtonsoft.Json;
    using NUnit.Framework;

    public class HighLevelTests
    {
        [Test]
        public void Crossplatform_Compatibility_Test()
        {
            var crypto = new VirgilCrypto();

            dynamic testData = new ExpandoObject();

            // Encrypt for single recipient

            {
                var kp = crypto.GenerateKeys();
                var prkey = crypto.ExportPrivateKey(kp.PrivateKey);
                var data = System.Text.Encoding.UTF8.GetBytes("Lorem ipsum dolor sit amet, consectetur adipiscing elit.");

                testData.encrypt_single_recipient = new
                {
                    private_key = prkey,
                    original_data = data,
                    cipher_data = crypto.Encrypt(data, kp.PublicKey)
                };
            }

            // Encrypt for multiple recipients

            {
                var kps = new int[new Random().Next(5, 10)].Select(it => crypto.GenerateKeys()).ToList();
                var prkeys = kps.Select(kp => crypto.ExportPrivateKey(kp.PrivateKey)).ToArray();
                var data = System.Text.Encoding.UTF8.GetBytes("Lorem ipsum dolor sit amet, consectetur adipiscing elit.");

                testData.encrypt_multiple_recipients = new
                {
                    private_keys = prkeys,
                    original_data = data,
                    cipher_data = crypto.Encrypt(data, kps.Select(kp => kp.PublicKey).ToArray())
                };
            }

            // Sign and Encrypt for single recipient

            {
                var kp = crypto.GenerateKeys();
                var prkey = crypto.ExportPrivateKey(kp.PrivateKey);
                var data = System.Text.Encoding.UTF8.GetBytes("Lorem ipsum dolor sit amet, consectetur adipiscing elit.");

                testData.sign_then_encrypt_single_recipient = new
                {
                    private_key = prkey,
                    original_data = data,
                    cipher_data = crypto.SignThenEncrypt(data, kp.PrivateKey, kp.PublicKey)
                };
            }

            // Sign and encrypt for multiple recipients

            {
                var kps = new int[new Random().Next(5, 10)].Select(it => crypto.GenerateKeys()).ToList();
                var prkeys = kps.Select(kp => crypto.ExportPrivateKey(kp.PrivateKey)).ToArray();
                var data = System.Text.Encoding.UTF8.GetBytes("Lorem ipsum dolor sit amet, consectetur adipiscing elit.");

                testData.sign_then_encrypt_multiple_recipients = new
                {
                    private_keys = prkeys,
                    original_data = data,
                    cipher_data = crypto.SignThenEncrypt(data, kps[0].PrivateKey, kps.Select(kp => kp.PublicKey).ToArray())
                };
            }

            // Generate Signature

            {
                var kp = crypto.GenerateKeys();
                var prkey = crypto.ExportPrivateKey(kp.PrivateKey);
                var data = System.Text.Encoding.UTF8.GetBytes("Suspendisse elit purus, laoreet ut nibh nec.");

                testData.generate_signature = new
                {
                    private_key = prkey,
                    original_data = data,
                    signature = crypto.Sign(data, kp.PrivateKey)
                };
            }

            // Export and Import SignableRequest

            {
                var kp = crypto.GenerateKeys();
                var prkey = crypto.ExportPrivateKey(kp.PrivateKey);
                var req = new PublishCardRequest(new CardSnapshotModel { 
                    Identity = "alice",
                    IdentityType = "member",
                    PublicKeyData = crypto.ExportPublicKey(kp.PublicKey),
                    Data = new Dictionary<string, string>
                    {
                       ["Key1"] = "Value1",
                       ["Key2"] = "Value2" 
                    },
                    Info = new CardInfoModel
                    {
                        Device = "iPhone 7",
                        DeviceName = "My precious"
                    }
                });
                var reqSigner = new RequestSigner(crypto);
                reqSigner.SelfSign(req, kp.PrivateKey);

                testData.export_signable_request = new
                {
                    private_key = prkey,
                    exported_request = req.Export()
                };
            }

            var testJson = JsonConvert.SerializeObject(testData, Formatting.Indented);
        }

        [Test]
        public async Task GetRevokedCard_ExistingCard_ShouldThrowException()
        {
            var virgil = new VirgilApi("[ACCESS_TOKEN]");

            var aliceKey = virgil.Keys.Generate();
			// var aliceCard = virgil.Cards.CreateGlobal("kurilenkodenis@gmail.com", IdentityType.Email, aliceKey);
			var aliceCard = virgil.Cards.Create("alice", aliceKey);

			var verificationAttempt = await virgil.Cards.BeginPublishGlobalAsync(aliceCard);

			await virgil.Cards.CompletePublishGlobalAsync(verificationAttempt, "[CONFIRMATION_CODE]");


			var aliceCards = await virgil.Cards.FindAsync("Alice");

			var encryptedBlob = aliceCards.EncryptFor(c => c.IdentityType == "admin", VirgilBuffer.From("Hello There :)"));

            //VirgilConfig.Initialize(IntegrationHelper.AppAccessToken, storage: new KeyStorageFake());

            //// Application Credentials

            //var appKey = VirgilKey.FromFile(IntegrationHelper.AppKeyPath, IntegrationHelper.AppKeyPassword);
            //var appID = IntegrationHelper.AppID;

            //// Create a Virgil Card

            //var identity = "Alice-" + Guid.NewGuid();
            //var aliceKey = VirgilKey.Create(identity);

            //var request = aliceKey.BuildCardPublishRequest();

            //appKey.SignRequest(request);

            //var aliceCard = await VirgilCard.PublishAsync(aliceKey.InitialRequest);

            //// Revoke a Virgil Card

            //await IntegrationHelper.RevokeCard(aliceCard.Id);
            //aliceKey.Destroy();

            //Assert.ThrowsAsync<VirgilClientException>(async () => await VirgilCard.GetAsync(aliceCard.Id));
        }
    }
}