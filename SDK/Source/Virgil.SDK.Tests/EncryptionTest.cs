namespace Virgil.SDK.Tests
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using FluentAssertions;
    using NUnit.Framework;

    using Virgil.SDK.Cryptography;

    public class EncryptionTest
    {
        [Test]
        public void EncryptData_SinglePublicKeyGiven_ShouldCipherDataBeDecrypted()
        {
            var crypto = new VirgilCrypto();

            var keyPair = crypto.GenerateKeys();

            var data = Encoding.UTF8.GetBytes("Encrypt me!!!");
            var encryptedData = crypto.Encrypt(data, keyPair.PublicKey);
            var decryptedData = crypto.Decrypt(encryptedData, keyPair.PrivateKey);

            data.ShouldAllBeEquivalentTo(decryptedData);
        }

        [Test]
        public void EncryptData_MultiplePublicKeysGiven_ShouldCipherDataBeDecrypted()
        {
            var crypto = new VirgilCrypto();
            var keyPairs = new List<KeyPair>();

            for (var index = 0; index < 10; index++)
            {
                keyPairs.Add(crypto.GenerateKeys());
            }

            var data = Encoding.UTF8.GetBytes("Encrypt me!!!");
            var encryptedData = crypto.Encrypt(data, keyPairs.Select(it => it.PublicKey).ToArray());

            foreach (var keyPair in keyPairs)
            {
                var decryptedData = crypto.Decrypt(encryptedData, keyPair.PrivateKey);
                data.ShouldAllBeEquivalentTo(decryptedData);
            }
        }

        [Test]
        public void EncryptData_MultiplePublicKeysWithDifferentTypesGiven_ShouldCipherDataBeDecrypted()
        {
            var crypto = new VirgilCrypto();
            var keyPairs = new List<KeyPair>
            {
                crypto.GenerateKeys(KeyPairType.EC_SECP256R1),
                crypto.GenerateKeys(KeyPairType.EC_SECP384R1),
                crypto.GenerateKeys(KeyPairType.EC_SECP521R1),
                crypto.GenerateKeys(KeyPairType.EC_BP256R1),
                crypto.GenerateKeys(KeyPairType.EC_BP384R1),
                crypto.GenerateKeys(KeyPairType.EC_BP512R1),
                crypto.GenerateKeys(KeyPairType.EC_SECP256K1),
                crypto.GenerateKeys(KeyPairType.EC_CURVE25519),
                crypto.GenerateKeys(KeyPairType.FAST_EC_ED25519),
                crypto.GenerateKeys(KeyPairType.FAST_EC_X25519)
            };

            var data = Encoding.UTF8.GetBytes("Encrypt me!!!");
            var encryptedData = crypto.Encrypt(data, keyPairs.Select(it => it.PublicKey).ToArray());

            foreach (var keyPair in keyPairs)
            {
                var decryptedData = crypto.Decrypt(encryptedData, keyPair.PrivateKey);
                data.ShouldAllBeEquivalentTo(decryptedData);
            }
        }

        [Test]
        public void EncryptStream_SinglePublicKeyGiven_ShouldCipherStreamBeDecrypted()
        {
            var crypto = new VirgilCrypto();
            var keyPair = crypto.GenerateKeys();

            var originalData = Encoding.UTF8.GetBytes(IntergrationHelper.RandomText);

            using (var inputStream = new MemoryStream(originalData))
            using (var cipherStream = new MemoryStream())
            using (var resultStream = new MemoryStream())
            {
                crypto.Encrypt(inputStream, cipherStream, keyPair.PublicKey);

                using (var cipherStream1 = new MemoryStream(cipherStream.ToArray()))
                {
                    crypto.Decrypt(cipherStream1, resultStream, keyPair.PrivateKey);

                    resultStream.ToArray().ShouldAllBeEquivalentTo(originalData);
                }
            }
        }
        
        public void EncryptStream_SmallStreamBufferGiven_ShouldCipherStreamBeDecrypted()
        {
            var crypto = new VirgilCrypto();
            var keyPair = crypto.GenerateKeys();

            var originalData = Encoding.UTF8.GetBytes("Hello There :)");
            
            byte[] cipherData;
            byte[] resultData;

            using (var inputStream = new MemoryStream(originalData))
            using (var cipherStream = new MemoryStream())
            {
                crypto.Encrypt(inputStream, cipherStream, keyPair.PublicKey);
                cipherData = cipherStream.ToArray();
            }

            using (var cipherStream = new MemoryStream(cipherData))
            using (var resultStream = new MemoryStream())
            {
                crypto.Decrypt(cipherStream, resultStream, keyPair.PrivateKey);
                resultData = resultStream.ToArray();
            }

            originalData.ShouldAllBeEquivalentTo(resultData);
        }

        [Test]
        public void EncryptStream_MultiplePublicKeysGiven_ShouldCipherStreamBeDecrypted()
        {
            var crypto = new VirgilCrypto();
            var keyPairs = new List<KeyPair>
            {
                crypto.GenerateKeys(KeyPairType.EC_SECP256R1),
                crypto.GenerateKeys(KeyPairType.EC_SECP384R1),
                crypto.GenerateKeys(KeyPairType.EC_SECP521R1),
                crypto.GenerateKeys(KeyPairType.EC_BP256R1),
                crypto.GenerateKeys(KeyPairType.EC_BP384R1),
                crypto.GenerateKeys(KeyPairType.EC_BP512R1),
                crypto.GenerateKeys(KeyPairType.EC_SECP256K1),
                crypto.GenerateKeys(KeyPairType.EC_CURVE25519),
                crypto.GenerateKeys(KeyPairType.FAST_EC_ED25519),
                crypto.GenerateKeys(KeyPairType.FAST_EC_X25519)
            };

            var originalData = Encoding.UTF8.GetBytes(IntergrationHelper.RandomText);

            foreach (var keyPair in keyPairs)
            {
                using (var inputStream = new MemoryStream(originalData))
                using (var cipherStream = new MemoryStream())
                using (var resultStream = new MemoryStream())
                {
                    crypto.Encrypt(inputStream, cipherStream, keyPairs.Select(it => it.PublicKey).ToArray());
                    
                    using (var cipherStream1 = new MemoryStream(cipherStream.ToArray()))
                    {
                        crypto.Decrypt(cipherStream1, resultStream, keyPair.PrivateKey);

                        resultStream.ToArray().ShouldAllBeEquivalentTo(originalData);
                    }
                }
            }
        }
    }
}