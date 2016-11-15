namespace Virgil.SDK.Tests
{
    using System.Text;
    using Crypto;
    using FluentAssertions;
    using NUnit.Framework;

    using Virgil.SDK.Cryptography;

    public class FingerprintTests
    {
        [Test]
        public void Ctor_HexStringGiven_ValuesShouldMatchWithVirgilCryptoImpl()
        {
            var hash = new VirgilCrypto().ComputeHash(Encoding.UTF8.GetBytes("1234567890"), HashAlgorithm.SHA256);
            var hex = VirgilByteArrayUtils.BytesToHex(hash);

            hash.ShouldAllBeEquivalentTo(new Fingerprint(hex).GetValue());
        }

        [Test]
        public void Ctor_ByteArrayGiven_HexStringShouldMatchWithVirgilCryptoImpl()
        {
            var hash = new VirgilCrypto().ComputeHash(Encoding.UTF8.GetBytes("1234567890"), HashAlgorithm.SHA256);
            var hex = VirgilByteArrayUtils.BytesToHex(hash);

            hex.Should().Be(new Fingerprint(hash).ToHEX());
        }
    }
}