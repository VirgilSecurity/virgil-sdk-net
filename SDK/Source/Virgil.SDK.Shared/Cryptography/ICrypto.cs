namespace Virgil.SDK.Cryptography
{
    using System.IO;

    /// <summary>
    /// The <see cref="Crypto"/> interface provides a set of methods for dealing 
    /// with low-level cryptographic primitives and algorithms.
    /// </summary>
    public interface ICrypto
    {
        Fingerprint CalculateFingerprint(byte[] data);
        byte[] ComputeHash(byte[] data, HashAlgorithm algorithm);
        byte[] Decrypt(byte[] cipherData, IPrivateKey privateKey);
        void Decrypt(Stream inputStream, Stream outputStream, IPrivateKey privateKey);
        byte[] DecryptThenVerify(byte[] cipherData, IPrivateKey privateKey, IPublicKey publicKey);
        byte[] Encrypt(byte[] data, params IPublicKey[] recipients);
        void Encrypt(Stream inputStream, Stream outputStream, params IPublicKey[] recipients);
        byte[] ExportPrivateKey(IPrivateKey privateKey, string password = null);
        byte[] ExportPublicKey(IPublicKey publicKey);
        IPublicKey ExtractPublicKey(IPrivateKey privateKey);
        KeyPair GenerateKeys();
        IPrivateKey ImportPrivateKey(byte[] keyData, string password = null);
        IPublicKey ImportPublicKey(byte[] keyData);
        byte[] Sign(Stream inputStream, IPrivateKey privateKey);
        byte[] Sign(byte[] data, IPrivateKey privateKey);
        byte[] SignThenEncrypt(byte[] data, IPrivateKey privateKey, params IPublicKey[] recipients);
        bool Verify(Stream inputStream, byte[] signature, IPublicKey signerKey);
        bool Verify(byte[] data, byte[] signature, IPublicKey signerKey);
    }
}