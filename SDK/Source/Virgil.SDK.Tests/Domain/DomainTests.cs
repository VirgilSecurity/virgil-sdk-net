namespace Virgil.SDK.Keys.Tests.Domain
{
    using System.Linq;
    using NUnit.Framework;
    using Virgil.SDK.Domain;

    public class DomainTests
    {
        [Test]
        public async void Should_CreateVirgilCard_Given_UnauthorizedTicket()
        {
            var buffer = VirgilBuffer.FromUTF8("Encrypt me please");

            var key = VirgilKey.Generate(); 
            var ticket = new VirgilCardTicket("live:kurilenkodenis", "skype", key.GetPublicKey());

            var pass = await VirgilPass.Create(ticket, key);
            var cards = await VirgilCard.Search(pass.Identity, pass.IdentityType);

            var encryptedBuffer = pass.SignThenEncrypt(buffer, cards);
            var foundCards = await VirgilCard.SearchGlobal("kurilenkodenis@gmail.com", VirgilIdentityType.Email);
        }
    }
}