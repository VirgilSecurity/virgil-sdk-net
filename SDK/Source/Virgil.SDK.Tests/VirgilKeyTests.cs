namespace Virgil.SDK.Tests
{
    using System;
    using FluentAssertions;
    using NSubstitute;
    using NUnit.Framework;

    using Virgil.Crypto;
    using Virgil.SDK;
    using Virgil.SDK.Cryptography;
    using Virgil.SDK.Storage;

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

            //var videodata = File.ReadAllBytes(@"C:\Users\Denis\Desktop\Bugag\video.mp4");
            //var audiodata = File.ReadAllBytes(@"C:\Users\Denis\Desktop\Bugag\audio.mp3");
            //var imagedata = File.ReadAllBytes(@"C:\Users\Denis\Desktop\Bugag\image.jpg");

            //var rec = new Dictionary<string, byte[]>
            //{
            //    {
            //        "default",
            //        VirgilKeyPair.ExtractPublicKey(Convert.FromBase64String(
            //            "LS0tLS1CRUdJTiBFQyBQUklWQVRFIEtFWS0tLS0tCk1Ia0NBUUVFSUVwM05ZNzhRS2xjSzBoMmdmSHVVQnNJb3RRWEZhSFdWTzRyMzAwczNhYmdvQXdHQ2lzR0FRUUIKbDFVQkJRR2hSQU5DQUFScEVyd1dQYVFkbnRhUkxPU2hBK2tkMlVBVm90WXB1SkVWelJYWFNIWjNrUUFBQUFBQQpBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUEKLS0tLS1FTkQgRUMgUFJJVkFURSBLRVktLS0tLQo="), Encoding.UTF8.GetBytes(""))
            //    }
            //};

            //var cipheraudio = CryptoHelper.Encrypt(audiodata, rec);
            //var ciphervideo = CryptoHelper.Encrypt(videodata, rec);
            //var cipherimage = CryptoHelper.Encrypt(imagedata, rec);

            //File.WriteAllBytes(@"C:\Users\Denis\Desktop\Bugag\audio.mp3.enc", cipheraudio);
            //File.WriteAllBytes(@"C:\Users\Denis\Desktop\Bugag\image.jpg.enc", cipherimage);
            //File.WriteAllBytes(@"C:\Users\Denis\Desktop\Bugag\video.mp4.enc", ciphervideo);

            //var ss = VirgilKeyPair.Generate(VirgilKeyPair.Type.EC_Curve25519);
            //var sss = Convert.ToBase64String(ss.PrivateKey());


            var keyPair = VirgilKeyPair.Generate();
            var keyPairGenerator = Substitute.For<IKeyPairGenerator>();
            var keyStorage = Substitute.For<IPrivateKeyStorage>();
            var cryptoService = Substitute.For<CryptoService>();
            var securityModule = Substitute.For<SecurityModule>(cryptoService);
            
            this.ServiceResolver.Resolve<SecurityModule>().Returns(securityModule);
            this.ServiceResolver.Resolve<IKeyPairGenerator>().Returns(keyPairGenerator);

            keyStorage.When(x => x.Store(keyName, Arg.Any<PrivateKeyEntry>()))
                .Do(x =>
                {
                    x.Args()[0].Should().Be(keyStorage);
                    x.Args()[1].Should().NotBeNull();

                    var entry = (PrivateKeyEntry)x.Args()[1];

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

            key.Decrypt(VirgilBuffer.FromString("Bob?")).ToBytes().ShouldBeEquivalentTo(fakeResult);
        }

        [Test]
        public void Decrypt_NullAsParameter_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var key = VirgilKey.Load(Substitute.For<ISecurityModule>());
                key.Decrypt(null);
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