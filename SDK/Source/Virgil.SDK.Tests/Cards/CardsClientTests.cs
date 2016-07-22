namespace Virgil.SDK.Keys.Tests.Cards
{
    using System.Security.Cryptography;
    using NUnit.Framework;

    public class CardsClientTests
    {
        [Test]
        public void Test1()
        {
            var key = VirgilKey.Create();
            var buffer = key.Export();
            buffer.ToBase64();
            buffer.ToUTF8();
            buffer.ToBytes();
            buffer.ToStream();



            var ss = CngKey.Open("");
        }
    }
}