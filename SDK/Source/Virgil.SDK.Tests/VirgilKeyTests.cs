namespace Virgil.SDK.Tests
{
    using System;
    using FluentAssertions;
    using NSubstitute;
    using NUnit.Framework;

    using Virgil.SDK.Cryptography;

    public class VirgilKeyTests
    {
        [SetUp]
        public void Setup()
        {
        }
        
        [Test]
        public void Sign_Data_ShouldPerformSignatureGenerationUsingKeyContainer()
        {
            var signatureFake = new byte[] { 1, 2, 3 };

            var fakeContainer = Substitute.For<ICryptoKeyContainer>();
            fakeContainer.PerformSignatureGeneration(Arg.Any<byte[]>()).Returns(signatureFake);

            var key = VirgilKey.Load(fakeContainer);

            key.Sign(VirgilBuffer.FromString("Hello!")).ToBytes().ShouldBeEquivalentTo(signatureFake);
        }

        [Test]
        public void Sign_NullAsParameter_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var key = VirgilKey.Load(Substitute.For<ICryptoKeyContainer>());
                key.Sign(null);
            });
        }

        [Test]
        public void Decrypt_CipherData_ShouldDecryptWithCryptoKeyContainer()
        {
            var fakeResult = new byte[] {1, 2, 3};

            var fakeContainer = Substitute.For<ICryptoKeyContainer>();
            fakeContainer.PerformDecryption(Arg.Any<byte[]>()).Returns(fakeResult);

            var key = VirgilKey.Load(fakeContainer);

            key.Decrypt(VirgilBuffer.FromString("Bob?")).ToBytes().ShouldBeEquivalentTo(fakeResult);
        }

        [Test]
        public void Decrypt_NullAsParameter_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var key = VirgilKey.Load(Substitute.For<ICryptoKeyContainer>());
                key.Decrypt(null);
            });
        }

        [Test]
        public void Load_NullAsParameter_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                VirgilKey.Load((ICryptoKeyContainer)null);
            });
        }

        [Test]
        public void Load_EmptyKeyName_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                VirgilKey.Load("");
            });
        }

        [Test]
        public void Create_EmptyKeyName_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                VirgilKey.Create("");
            });
        }

        [Test]
        public void Create_NullAsParameter_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {   
                VirgilKey.Create((VirgilKeyDetails)null);
            });
        }
    }
}