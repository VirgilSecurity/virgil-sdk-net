namespace Virgil.SDK.Keys.Tests.Cards
{
    using System;
    using System.Security.Cryptography;
    using NUnit.Framework;

    using Virgil.Crypto;
    using Virgil.SDK.Cards;
    using Virgil.SDK.Utils;

    public class CardsClientTests
    {
        [Test]
        public void Test1()
        {
            var a = VirgilKeyPair.Generate();
            var b = VirgilKeyPair.Generate();

            var ae = VirgilCipher.ComputeShared(b.PublicKey(), a.PrivateKey());
            var be = VirgilCipher.ComputeShared(a.PublicKey(), b.PrivateKey());

            var ss = CryptoHelper.Encrypt("dsasd", "", ae);
        }
    }
}