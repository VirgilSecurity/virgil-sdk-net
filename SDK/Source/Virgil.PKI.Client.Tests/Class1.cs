namespace Virgil.PKI.Client.Tests
{
    using System;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using Virgil.PKI.Clients;
    using Virgil.PKI.Http;

    public class Class1
    {
        [Test]
        public async Task Test()
        {
            var connection = new Connection("chik", new Uri(@"https://pki.virgilsecurity.com/v1/"));
            var client = new PublicKeysClient(connection);
            var pkiPublicKey = await client.Get(Guid.Parse("a6b0e7a0-958c-1c62-dddb-665d0c33d650"));

            Console.WriteLine(JsonConvert.SerializeObject(pkiPublicKey, Formatting.Indented));
        }
    }
}
