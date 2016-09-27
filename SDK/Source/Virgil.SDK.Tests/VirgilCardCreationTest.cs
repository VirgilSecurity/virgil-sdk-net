namespace Virgil.SDK.Tests
{
    using System.IO;
    using System.Threading.Tasks;

    using Virgil.SDK.Client;
    using Virgil.SDK.Cryptography;

    using NUnit.Framework;
 
    public class VirgilCardCreationTest
    {
        [Test]
        public async Task CreateNewVirgilCard_IdentityAndPublicKeyGiven_ShouldBeFoundByIdentity()
        {
            var crypto = new VirgilCrypto();
            var parameters = new VirgilClientParams("AT.d7b3868c7e8cbd5d3090eaebf716a74f8f88ec4118d6457d7419d53542007c89");

            parameters.SetCardsServiceAddress("https://cards-stg.virgilsecurity.com");
            parameters.SetReadCardsServiceAddress("https://cards-stg.virgilsecurity.com");
            parameters.SetIdentityServiceAddress("https://identity-stg.virgilsecurity.com");

            var client = new VirgilClient(parameters);

            const string appId = "327ea039690415e7edf0cf84a35a43d52a9c86bda6ccc73a94f811843e9f6094";
            var appKey = crypto.ImportPrivateKey(File.ReadAllBytes(@"C:\Users\Denis\Desktop\mycli.virgilkey"), "1234");

            var aliceKeys = crypto.GenerateKeys();
            var exportedPublicKey = crypto.ExportPublicKey(aliceKeys.PublicKey);

            var request = CreateCardRequest.Create("alice", "memeber", exportedPublicKey);

            var fingerprint = crypto.CalculateFingerprint(request.Snapshot);

            var appSignature = crypto.SignFingerprint(fingerprint, appKey);
            var ownerSignature = crypto.SignFingerprint(fingerprint, aliceKeys.PrivateKey);

            request.AppendSignature(fingerprint, ownerSignature);
            request.AppendSignature(appId, appSignature);

            var virgilCard = await client.CreateCardAsync(request);

            //var cards = await client.SearchCardsAsync(new SearchCardsCriteria
            //{
            //    Identities = new[] { "com.vadim-test.mycli" },
            //    Scope = VirgilCardScope.Global
            //});

            //var fingerprint = crypto.CalculateFingerprint(cards.First().ContentSnapshot);


            //var privateKey = crypto.GenerateKey();
            //var appPrivateKey = crypto.ImportKey(File.ReadAllBytes("D:/test.virgilkey"), "1234");

            //var exportedPublicKey = crypto.ExportPublicKey(privateKey);

            //var request = CreateCardRequest.Create("alice", "member", exportedPublicKey);

            //var fingerprint = crypto.CalculateFingerprint(request.Snapshot);
            //var signature = crypto.SignFingerprint(fingerprint, privateKey);

            //request.AppendSignature(fingerprint, signature);
        }
    }
}