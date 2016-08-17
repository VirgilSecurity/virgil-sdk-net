namespace Virgil.SDK.Cryptography
{
    using System.Collections.Generic;

    public interface ICryptoService
    {
        byte[] EncryptData(byte[] data, IDictionary<byte[], PublicKey> recipients);
        byte[] DecryptData(byte[] cipherdata, byte[] recipientId, PrivateKey privateKey);
        byte[] SignData(byte[] data, PrivateKey privateKey);
        bool VerifyData(byte[] data, byte[] signature, PublicKey publicKey);
    }
}   