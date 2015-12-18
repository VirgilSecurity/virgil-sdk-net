namespace Virgil.Crypto.Wrapper.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public class VirgilCryptoTests
    {
        [Test]
        public void Should_GenerateKeyPairWithEncryptedPrivateKey()
        {
            var keyPair = CryptoHelper.GenerateKeyPair("Password");
            Encoding.UTF8.GetString(keyPair.PrivateKey()).Should().Contain("ENCRYPTED");
        }

        [Test]
        public void Should_GenerateKeyPairWithPurePrivateKey()
        {
            var keyPair = CryptoHelper.GenerateKeyPair();
            Encoding.UTF8.GetString(keyPair.PrivateKey()).Should().NotContain("ENCRYPTED");
        }

        [Test]
        public void Should_ReturnEncryptedStringInBase64Format_When_EncryptingWithKey()
        {
            const string text = "Encrypt me!!!";

            var keyPair = CryptoHelper.GenerateKeyPair();
            var cipherTextBase64 = CryptoHelper.Encrypt(text, "Recipient", keyPair.PublicKey());

            Utils.IsBase64Encoded(cipherTextBase64).Should().BeTrue();
        }

        [Test]
        public void Should_ReturnEncryptedStringInBase64Format_When_EncryptingWithPassword()
        {
            const string text = "Encrypt me!!!";
            var cipherTextBase64 = CryptoHelper.Encrypt(text, "Password");

            Utils.IsBase64Encoded(cipherTextBase64).Should().BeTrue();
        }
        
        [Test]
        public void Should_EncryptTextForMultipleRecipients()
        {
            const string text = "Encrypt me!!!";

            var keyPair1 = new VirgilKeyPair();
            var keyPair2 = new VirgilKeyPair();
            var keyPair3 = new VirgilKeyPair();

            var cipherTextBase64 = CryptoHelper.Encrypt(text, new Dictionary<string, byte[]>
            {
                { "Recipient1", keyPair1.PublicKey() },
                { "Recipient2", keyPair2.PublicKey() },
                { "Recipient3", keyPair3.PublicKey() }
            });

            using (var cipher = new VirgilCipher())
            {
                var cipherTextData = Convert.FromBase64String(cipherTextBase64);

                Encoding.UTF8.GetString(cipher.DecryptWithKey(cipherTextData, 
                    Encoding.UTF8.GetBytes("Recipient1"), keyPair1.PrivateKey())).Should().Be(text);

                Encoding.UTF8.GetString(cipher.DecryptWithKey(cipherTextData,
                    Encoding.UTF8.GetBytes("Recipient2"), keyPair2.PrivateKey())).Should().Be(text);

                Encoding.UTF8.GetString(cipher.DecryptWithKey(cipherTextData,
                    Encoding.UTF8.GetBytes("Recipient3"), keyPair3.PrivateKey())).Should().Be(text);
            }
        }

        [Test]
        public void Should_EncryptTextForOneRecipient()
        {
            const string text = "Encrypt me!!!";

            var keyPair1 = new VirgilKeyPair();

            var cipherTextBase64 = CryptoHelper.Encrypt(text, "Recipient1", keyPair1.PublicKey());

            using (var cipher = new VirgilCipher())
            {
                var cipherTextData = Convert.FromBase64String(cipherTextBase64);

                Encoding.UTF8.GetString(cipher.DecryptWithKey(cipherTextData,
                    Encoding.UTF8.GetBytes("Recipient1"), keyPair1.PrivateKey())).Should().Be(text);
            }
        }

        [Test]
        public void Should_EncryptTextWithPassword()
        {
            const string text = "Encrypt me!!!";

            var cipherTextBase64 = CryptoHelper.Encrypt(text, "Password");
            using (var cipher = new VirgilCipher())
            {
                var cipherTextData = Convert.FromBase64String(cipherTextBase64);
                Encoding.UTF8.GetString(cipher.DecryptWithPassword(cipherTextData, Encoding.UTF8.GetBytes("Password"))).Should().Be(text);
            }
        }

        [Test, ExpectedException(typeof(FormatException))]
        public void Should_ThrowException_When_DecryptedCipherStringIsNotInBase64Format()
        {
            const string text = "NOT Base64 Encoded String";
            var keyPair = new VirgilKeyPair();

            CryptoHelper.Decrypt(text, "Recipient1", keyPair.PrivateKey());
        }

        [Test]
        public void Should_DecryptCipherTextWithPassword()
        {
            using (var cipher = new VirgilCipher())
            {
                const string text = "Encrypt me!!!";

                var passwordData = Encoding.UTF8.GetBytes("Password");
                var textData = Encoding.UTF8.GetBytes(text);
                cipher.AddPasswordRecipient(passwordData);

                var cipherData = cipher.Encrypt(textData, true);

                Encoding.UTF8.GetString(CryptoHelper.Decrypt(cipherData, "Password")).Should().Be(text);
            }
        }

        [Test]
        public void Should_DecryptCipherTextWithPrivateKey()
        {
            using (var cipher = new VirgilCipher())
            {
                const string text = "Encrypt me!!!";

                var keyPair = new VirgilKeyPair();
                var recipientIdData = Encoding.UTF8.GetBytes("Default");
                var textData = Encoding.UTF8.GetBytes(text);
                cipher.AddKeyRecipient(recipientIdData, keyPair.PublicKey());

                var cipherData = cipher.Encrypt(textData, true);

                Encoding.UTF8.GetString(CryptoHelper.Decrypt(cipherData, "Default", keyPair.PrivateKey())).Should().Be(text);
            }
        }

        [Test]
        public void Should_DecryptCipherTextWithEncryptedPrivateKey()
        {
            using (var cipher = new VirgilCipher())
            {
                const string text = "Encrypt me!!!";
                const string password = "Password";

                var keyPair = new VirgilKeyPair(Encoding.UTF8.GetBytes(password));
                var recipientIdData = Encoding.UTF8.GetBytes("Default");
                var textData = Encoding.UTF8.GetBytes(text);
                cipher.AddKeyRecipient(recipientIdData, keyPair.PublicKey());

                var cipherData = cipher.Encrypt(textData, true);

                Encoding.UTF8.GetString(CryptoHelper.Decrypt(cipherData, "Default", keyPair.PrivateKey(), password)).Should().Be(text);
            }
        }

        [Test]
        public void Should_ReturnSignInBase64Format()
        {
            const string textToSign = "Sign me!!!";

            var keyPair = new VirgilKeyPair();
            Utils.IsBase64Encoded(CryptoHelper.Sign(textToSign, keyPair.PrivateKey())).Should().BeTrue();
        }

        [Test]
        public void Should_ThrowException_WhenReturnSignInBase64Format()
        {
            const string textToSign = "Sign me!!!";

            var keyPair = new VirgilKeyPair();
            Utils.IsBase64Encoded(CryptoHelper.Sign(textToSign, keyPair.PrivateKey())).Should().BeTrue();
        }

        [Test]
        public void Should_CreateDigitalSignatureForTextWithEncryptedPassword()
        {
            const string textToSign = "Sign me!!!";
            var data = Encoding.UTF8.GetBytes(textToSign);
            var keyPair = new VirgilKeyPair(Encoding.UTF8.GetBytes("Password"));

            using (var signer = new Virgil.Crypto.VirgilSigner())
            {
                var signData = signer.Sign(data, keyPair.PrivateKey(), Encoding.UTF8.GetBytes("Password"));
                var sign = Convert.ToBase64String(signData);

                CryptoHelper.Sign(textToSign, keyPair.PrivateKey(), "Password").Should().Be(sign);
            }
        }

        [Test]
        public void Should_VerifyDigitalSignatureCreatedWithNativeLibrary()
        {
            const string textToSign = "Sign me!!!";
            var data = Encoding.UTF8.GetBytes(textToSign);
            var keyPair = new VirgilKeyPair();

            using (var signer = new Virgil.Crypto.VirgilSigner())
            {
                var signData = signer.Sign(data, keyPair.PrivateKey());
                var signBase64 = Convert.ToBase64String(signData);

                CryptoHelper.Verify(textToSign, signBase64, keyPair.PublicKey()).Should().BeTrue();
            }
        }
    }
}