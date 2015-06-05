using System;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    using Newtonsoft.Json;
    using Virgil.PKI.Clients;
    using Virgil.PKI.Http;
    using Virgil.PKI.Models;


    class Program
    {
        public static async Task Test()
        {
            var connection = new Connection("chik", new Uri(@"https://pki.virgilsecurity.com/v1/"));
            var client = new PublicKeysClient(connection);
            var pkiPublicKey = await client.Get(Guid.Parse("a6b0e7a0-958c-1c62-dddb-665d0c33d650"));

            Console.WriteLine(JsonConvert.SerializeObject(pkiPublicKey, Formatting.Indented));
            Console.Read();
        }

        public static async Task Test2()
        {
            var connection = new Connection("chik", new Uri(@"https://pki.virgilsecurity.com/v1/"));
            var client = new PublicKeysClient(connection);
            var pkiPublicKey = await client.Search("cakw0339631@haqed.com");

            Console.WriteLine(JsonConvert.SerializeObject(pkiPublicKey, Formatting.Indented));
            Console.Read();
        }

        static void Main(string[] args)
        {
            Test2().Wait();
        }
    }
}
