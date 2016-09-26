namespace Virgil.SDK.Tests
{
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
            parameters.SetReadCardsServiceAddress("https://cards-ro-stg.virgilsecurity.com");
            parameters.SetIdentityServiceAddress("https://identity-stg.virgilsecurity.com");

            var client = new VirgilClient(parameters);

            var cards = await client.SearchCardsAsync(new SearchCardsCriteria
            {
                Identities = new[] {"com.vadim-test.mycli"}
            });

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