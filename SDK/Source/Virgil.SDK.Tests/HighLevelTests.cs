namespace Virgil.SDK.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Threading.Tasks;
    using Exceptions;
    using Virgil.SDK.Client;
    using Virgil.SDK.Common;
    using Virgil.SDK.Cryptography;

    using Newtonsoft.Json;
    using NUnit.Framework;
    using Storage;

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
                    cipher_data =
                        crypto.SignThenEncrypt(data, kps[0].PrivateKey, kps.Select(kp => kp.PublicKey).ToArray())
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
                var req = new PublishCardRequest(
                    identity: "alice",
                    identityType: "member",
                    publicKeyData: crypto.ExportPublicKey(kp.PublicKey),
                    customFields: new Dictionary<string, string>
                    {
                        ["Key1"] = "Value1",
                        ["Key2"] = "Value2"
                    },
                    info: new CardInfoModel
                    {
                        Device = "iPhone 7",
                        DeviceName = "My precious"
                    }
                );
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
        public async Task Test()
        {
            var virgil = new VirgilApi();

            // loads the Alice's key from default storage.
            var alicekey = virgil.Keys.Load("Alice");

            try
            {
                // search for the Bob's key on Virgil Services 
                var ciphertext = await virgil.Cards
                    .GetAsync("669c4e44ffc55fea3e76447708be2cf26a8c8a5309e304bed34078e326dfb399111")
                    .SignThenEncrypt("Hello Bob, how are you? BEATCH", alicekey)
                    .ToString(StringEncoding.Base64);

                ;
            }
            catch (RecipientsNotFoundException)
            {
                Console.Write("dsadsasd");
            }

            // var virgil = new VirgilApi(IntegrationHelper.VirgilApiContext());
            // var virgil = new VirgilApi();
            // var cards = await virgil.Cards.FindGlobalAsync(IdentityType.Application, "com.denzil.twilio-demo-lalaland");

            // var denisKey = virgil.Keys.Load("ALICE");
            // var denisCard = await virgil.Cards.FindGlobalAsync(IdentityType.Email, "kurilenkodenis@gmail.com");

            //var cipherbuffer = await virgil.Cards
            //    .FindGlobalAsync("kurilenkodenis@gmail.com")
            //    .Encrypt("data");

            //var ciphertext = cipherbuffer.ToString(StringEncoding.Base64);

            //var denisCard = virgil.Cards.CreateGlobal("kurilenkodenis@gmail.com", IdentityType.Email, denisKey);
            //var denisCard = await virgil.Cards.GetAsync("b3e439b10356c625f14fa307f505e5438685e84af5fa1ea5cdf0fd5403f5578a");

            //var attempt = await denisCard.CheckIdentityAsync();

            //var confirmationCode = "";

            //var token = await attempt.ConfirmAsync(new EmailConfirmation(confirmationCode));

            // await virgil.Cards.PublishGlobalAsync(denisCard, token);
            //await virgil.Cards.RevokeGlobalAsync(denisCard, denisKey, token);

            // ALICE SIDE ===================================             

            //var aliceKey = virgil.Keys.Load("ALICE");

            //var bobCards = await virgil.Cards.FindAsync("bob");

            //var ciphertext = aliceKey.SignThenEncrypt("History", bobCards).ToString(StringEncoding.Base64);

            //// BOB SIDE =====================================

            //// load Bob's Key from default storage
            //var bobKey = virgil.Keys.Load("BOB_KEY");

            //// get Alice's Card 
            //var aliceCard = await virgil.Cards.GetAsync("[ALICE_CARD_ID]");

            //// decrypt cipher data using Bob's Key and verify one using Alice's Card.
            //var orginaltext = bobKey.DecryptThenVerify(ciphertext, aliceCard).ToString();
        }
    }
}