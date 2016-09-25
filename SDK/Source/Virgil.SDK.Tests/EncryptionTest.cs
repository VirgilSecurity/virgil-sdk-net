namespace Virgil.SDK.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Cryptography;
    using FluentAssertions;
    using NUnit.Framework;

    public class EncryptionTest
    {
        [Test]
        public void EncryptData_SinglePublicKeyGiven_ShouldBeDecrypted()
        {
            var crypto = new VirgilCrypto();

            var privateKey = crypto.GenerateKey();

            var data = Encoding.UTF8.GetBytes("Encrypt me!!!");
            var encryptedData = crypto.Encrypt(data, privateKey.PublicKey);
            var decryptedData = crypto.Decrypt(encryptedData, privateKey);
                
            data.ShouldAllBeEquivalentTo(decryptedData);
        }

        [Test]
        public void EncryptData_MultiplePublicKeysGiven_ShouldBeDecrypted()
        {
            var crypto = new VirgilCrypto();
            var privateKeys = new List<PrivateKey>();

            for (var index = 0; index < 10; index++)
            {
                privateKeys.Add(crypto.GenerateKey());
            }

            var data = Encoding.UTF8.GetBytes("Encrypt me!!!");
            var encryptedData = crypto.Encrypt(data, privateKeys.Select(it => it.PublicKey).ToArray());

            foreach (var privateKey in privateKeys)
            {
                var decryptedData = crypto.Decrypt(encryptedData, privateKey);
                data.ShouldAllBeEquivalentTo(decryptedData);
            }
        }

        [Test]
        public void EncryptData_MultiplePublicKeysWithDifferentTypesGiven_ShouldBeDecrypted()
        {
            var crypto = new VirgilCrypto();
            var privateKeys = new List<PrivateKey>
            {
                crypto.GenerateKey(KeysType.EC_SECP256R1),
                crypto.GenerateKey(KeysType.EC_SECP384R1),
                crypto.GenerateKey(KeysType.EC_SECP521R1),
                crypto.GenerateKey(KeysType.EC_BP256R1),
                crypto.GenerateKey(KeysType.EC_BP384R1),
                crypto.GenerateKey(KeysType.EC_BP512R1),
                crypto.GenerateKey(KeysType.EC_SECP256K1),
                crypto.GenerateKey(KeysType.EC_CURVE25519),
                crypto.GenerateKey(KeysType.EC_ED25519)
            };

            var data = Encoding.UTF8.GetBytes("Encrypt me!!!");
            var encryptedData = crypto.Encrypt(data, privateKeys.Select(it => it.PublicKey).ToArray());

            foreach (var privateKey in privateKeys)
            {
                var decryptedData = crypto.Decrypt(encryptedData, privateKey);
                data.ShouldAllBeEquivalentTo(decryptedData);
            }
        }

        [Test]
        public void SignThenEncryptData_MultiplePublicKeysAndPrivateKeyGiven_ShouldBeDecryptedThenVerified()
        {
        }
    }
}