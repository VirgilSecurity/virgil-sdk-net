using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Virgil.SDK.Keys.Clients;
using Virgil.SDK.Keys.TransferObject;

namespace Virgil.SDK.Keys.Tests
{

    using System.Linq;
    using Virgil.Crypto;
    
    using System;
    using System.Threading.Tasks;
    using FluentAssertions;
    using NSubstitute;

    using NUnit.Framework;
    
    using Http;

    public static class Constants
    {
        public const string ApplicationToken = "72ec86432dc166106289d0b51a879371";
    }

    public class DeletePublicKeys
    {
        public static readonly Connection ApiEndpoint = new Connection(Constants.ApplicationToken, new Uri(@"https://keys.virgilsecurity.com"));

        [Test]
        public async Task Run()
        {
            var client = new VirgilCardClient(connection: ApiEndpoint);
            await client.Create(new byte[20], VirgilIdentityType.Email, "hello@world.com", null, Guid.NewGuid(), new byte[12]);
        }
    }


    public class PublicKeysClientTests
    {
        
    }
}