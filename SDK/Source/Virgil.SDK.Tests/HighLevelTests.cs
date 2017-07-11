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

            // Export and Import SignedRequest

            {
                var kp = crypto.GenerateKeys();
                var prkey = crypto.ExportPrivateKey(kp.PrivateKey);
                var req = new CreateUserCardRequest(
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
    }
}