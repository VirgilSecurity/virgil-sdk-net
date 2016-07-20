namespace Virgil.SDK.Keys.Tests.Cards
{
    using NUnit.Framework;
    using Virgil.Crypto;
    using Virgil.SDK.Cards;

    public class VirgilCardTicketTests
    {
        [Test]
        public void Should_ExportToBinaryData_When_()
        {
            var keyPair = VirgilKeyPair.Generate();
            var ticket = new VirgilCardTicket("demo@virgilsecurity.com", "email", keyPair.PublicKey(), false);

            var signature = CryptoHelper.Sign(ticket.Fingerprint, keyPair.PrivateKey());

            ticket.AddOwnerSign(signature);
            var exportedTicket = ticket.Export();

            // --------------------------------
            
            var ticket11 = VirgilCardTicket.Import(exportedTicket);
        }
    }
}