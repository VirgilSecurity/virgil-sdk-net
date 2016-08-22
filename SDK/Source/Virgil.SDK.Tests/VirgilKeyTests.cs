namespace Virgil.SDK.Tests
{
    using System;

    using FluentAssertions;
    using NSubstitute;
    using NUnit.Framework;

    using Virgil.Crypto;
    using Virgil.SDK.Clients;
    using Virgil.SDK.Cryptography;

    public class VirgilKeyTests
    {
        private IServiceResolver ServiceResolver;

        [SetUp]
        public void Setup()
        {
            this.ServiceResolver = Substitute.For<IServiceResolver>();
            ServiceLocator.SetServiceResolver(this.ServiceResolver);
        }
        
        [TearDown]
        public void Teardown()
        {
            this.ServiceResolver.ClearReceivedCalls();
        }

        [Test]
        public void Create_GivenKeyName_ShouldGenerateKeyPairAndSaveItToTheStorage()
        {
            const string keyName = "Alice";

            var keyPair = VirgilKeyPair.Generate();
            var keyPairGenerator = Substitute.For<IKeyPairGenerator>();
            var keyStorage = Substitute.For<IKeyStorage>();
            var cryptoService = Substitute.For<ICryptoService>();
            var securityModule = Substitute.For<SecurityModule>(cryptoService);
            
            this.ServiceResolver.Resolve<SecurityModule>().Returns(securityModule);
            this.ServiceResolver.Resolve<IKeyPairGenerator>().Returns(keyPairGenerator);

            keyStorage.When(x => x.Store(keyName, Arg.Any<KeyPairEntry>()))
                .Do(x =>
                {
                    x.Args()[0].Should().Be(keyStorage);
                    x.Args()[1].Should().NotBeNull();

                    var entry = (KeyPairEntry)x.Args()[1];

                    entry.PublicKey.ShouldBeEquivalentTo(keyPair.PublicKey());
                    entry.PrivateKey.ShouldBeEquivalentTo(keyPair.PrivateKey());
                });

            keyPairGenerator.Generate(null).Returns(it => new KeyPair(keyPair.PublicKey(), keyPair.PrivateKey()));

            VirgilKey.Create(keyName);
        }

        [Test]
        public void Sign_Data_ShouldPerformSignatureGenerationUsingKeyContainer()
        {
            var signatureFake = new byte[] { 1, 2, 3 };

            var fakeSecurityModule = Substitute.For<ISecurityModule>();
            fakeSecurityModule.SignData(Arg.Any<byte[]>()).Returns(signatureFake);

            var key = VirgilKey.Load(fakeSecurityModule);

            key.Sign(VirgilBuffer.FromString("Hello!")).ToBytes().ShouldBeEquivalentTo(signatureFake);
        }

        [Test]
        public void Sign_NullAsParameter_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var key = VirgilKey.Load(Substitute.For<ISecurityModule>());
                key.Sign(null);
            });
        }

        [Test]
        public void Decrypt_CipherData_ShouldDecryptWithCryptoKeyContainer()
        {
            var fakeResult = new byte[] {1, 2, 3};

            var fakeContainer = Substitute.For<ISecurityModule>();
            fakeContainer.DecryptData(Arg.Any<byte[]>()).Returns(fakeResult);

            var key = VirgilKey.Load(fakeContainer);

            key.DecryptAndVerify(VirgilBuffer.FromString("Bob?")).ToBytes().ShouldBeEquivalentTo(fakeResult);
        }

        [Test]
        public void Decrypt_NullAsParameter_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var key = VirgilKey.Load(Substitute.For<ISecurityModule>());
                key.DecryptAndVerify(null);
            });
        }

        [Test]
        public void Load_NullAsParameter_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                VirgilKey.Load((ISecurityModule)null);
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
    }
}