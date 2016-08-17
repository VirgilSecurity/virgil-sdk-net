namespace Virgil.SDK.Cryptography
{
    using System.Collections.Generic;
    using System.IO;

    public interface ICryptoService
    {
        byte[] EncryptData(byte[] data, IEnumerable<Recipient> recipients);
        byte[] EncryptStream(Stream stream, IEnumerable<Recipient> recipients);

        bool VerifyData(byte[] data, byte[] signature, PublicKey publicKey);
        bool VerifyStream(Stream cipherstream, byte[] signature, PublicKey publicKey);
    }
}   