namespace Virgil.SDK.Tests.Cryptography
{
    using FluentAssertions;
    using NUnit.Framework;
    using Virgil.SDK;
    using Virgil.SDK.Cryptography;

    public class KeyPairTests
    {
        [Test]
        public void Ctor_PublicAndPrivateKeys_ShouldInitializePublicAndPrivateKeys()
        {
            var cryptoKeyPair =  Virgil.Crypto.VirgilKeyPair.Generate();

            var keyPair = new KeyPair(cryptoKeyPair.PublicKey(), cryptoKeyPair.PrivateKey());

            keyPair.PublicKey.Value.ShouldBeEquivalentTo(cryptoKeyPair.PublicKey());
            keyPair.PrivateKey.Value.ShouldBeEquivalentTo(cryptoKeyPair.PrivateKey());
        }
    }
}