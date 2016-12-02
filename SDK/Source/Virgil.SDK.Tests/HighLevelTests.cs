namespace Virgil.SDK.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Virgil.SDK.Client;
    using Virgil.SDK.Common;
    using Virgil.SDK.Cryptography;
    using Virgil.SDK.Exceptions;
    using Virgil.SDK.HighLevel;

    using Fakes;
    using FluentAssertions;
    using Newtonsoft.Json;
    using NUnit.Framework;

    public class HighLevelTests
    {
        [SetUp]
        public void Setup()
        {
            VirgilConfig.Reset();
        }

        [Test]
        public void Crossplatform_Compatibility_Test()
        {
            var crypto = new VirgilCrypto();

            dynamic testData = new ExpandoObject();

            // Encrypt for single recipient

            {
                var kp = crypto.GenerateKeys();
                var prkey = crypto.ExportPrivateKey(kp.PrivateKey);
                var data = Encoding.UTF8.GetBytes("Lorem ipsum dolor sit amet, consectetur adipiscing elit.");

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
                var data = Encoding.UTF8.GetBytes("Lorem ipsum dolor sit amet, consectetur adipiscing elit.");

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
                var data = Encoding.UTF8.GetBytes("Lorem ipsum dolor sit amet, consectetur adipiscing elit.");

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
                var data = Encoding.UTF8.GetBytes("Lorem ipsum dolor sit amet, consectetur adipiscing elit.");

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
                var data = Encoding.UTF8.GetBytes("Suspendisse elit purus, laoreet ut nibh nec.");

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
            //VirgilConfig.Initialize(IntergrationHelper.AppAccessToken, storage: new KeyStorageFake());

            //// Application Credentials

            //var appKey = VirgilKey.FromFile(IntergrationHelper.AppKeyPath, IntergrationHelper.AppKeyPassword);
            //var appID = IntergrationHelper.AppID;

            //// Create a Virgil Card

            //var identity = "Alice-" + Guid.NewGuid();
            //var aliceKey = VirgilKey.Create(identity);

            //var request = aliceKey.BuildCardPublishRequest();

            //appKey.SignRequest(request);

            //var aliceCard = await VirgilCard.PublishAsync(aliceKey.InitialRequest);

            //// Revoke a Virgil Card

            //await IntergrationHelper.RevokeCard(aliceCard.Id);
            //aliceKey.Destroy();

            //Assert.ThrowsAsync<VirgilClientException>(async () => await VirgilCard.GetAsync(aliceCard.Id));
        }

        [Test]
        public async Task EncryptAndSignData_MultipleRecipients_ShouldDecryptAndVerifyDataSuccessfully()
        {
            //VirgilConfig.Initialize(IntergrationHelper.AppAccessToken, storage: new KeyStorageFake());

            //var appKey = IntergrationHelper.GetVirgilAppKey();

            //var aliceIdentity = $"Alice-{Guid.NewGuid()}";
            //var bobIdentity = $"Bob-{Guid.NewGuid()}";

            //var aliceKey = VirgilKey.Create(aliceIdentity);
            //var bobKey = VirgilKey.Create(bobIdentity);

            //var publishCardRequest = aliceKey.BuildCardPublishRequest();

            
            //appKey.SignRequest(aliceKey.InitialRequest, IntergrationHelper.AppID);
            //appKey.SignRequest(bobKey.InitialRequest, IntergrationHelper.AppID);

            //await VirgilCard.PublishAsync(aliceKey.InitialRequest);
            //await VirgilCard.PublishAsync(bobKey.InitialRequest);

            //var cards = (await VirgilCard.FindAsync(new[] { aliceIdentity, bobIdentity })).ToList();
            //var plaintext = Encoding.UTF8.GetBytes("Hello Bob!");

            //var cipherData = aliceKey.SignThenEncrypt(plaintext, cards);
            //var decryptedData = bobKey.DecryptThenVerify(cipherData, cards.Single(it => it.Identity == aliceIdentity));

            //decryptedData.ShouldBeEquivalentTo(plaintext);

            //await Task.WhenAll(cards.Select(it => IntergrationHelper.RevokeCard(it.Id)));

            //aliceKey.Destroy();
            //bobKey.Destroy();
        }
    }
}