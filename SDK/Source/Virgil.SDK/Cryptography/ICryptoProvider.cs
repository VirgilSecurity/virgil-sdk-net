namespace Virgil.SDK.Cryptography
{
    /// <summary>
    /// The <see cref="ICryptoProvider"/> provides cryptographic operations in applications, such as
    /// signature generation and verification, and encryption and decryption.
    /// </summary>
    public interface ICryptoProvider
    {
        byte[] Encrypt(byte[] clearData, string password);
        byte[] Decrypt(byte[] cipherData, string password);
    }
}