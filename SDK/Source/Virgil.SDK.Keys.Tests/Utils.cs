namespace Virgil.SDK.Keys.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Clients;
    using Crypto;
    using TransferObject;

    public static class Utils
    {
        public static async Task<Batch> TestCreateVirgilCard(this IVirgilCardClient client)
        {
            var virgilKeyPair = new VirgilKeyPair();

            var virgilCard = await client.Create(
                Mailinator.GetRandomEmailName(),
                IdentityType.Email,
                virgilKeyPair.PublicKey(),
                virgilKeyPair.PrivateKey(),
                new Dictionary<string, string>()
                {
                    ["hello"] = "world"
                });

            return new Batch
            {
                VirgilCard = virgilCard,
                VirgilKeyPair = virgilKeyPair
            };
        }

        public class Batch
        {
            public VirgilCardDto VirgilCard;
            public VirgilKeyPair VirgilKeyPair;
        }

        
    }
}