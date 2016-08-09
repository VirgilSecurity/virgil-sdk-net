namespace Virgil.SDK.Cryptography
{
    using System.Collections.Generic;
    using System.IO;

    public interface IEncryptionModule
    {
        byte[] EncryptData(byte[] cipherdata, IDictionary<string, PublicKey> recipients);

        byte[] EncryptStream(Stream cipherstream, IDictionary<string, PublicKey> recipients);

        byte[] VerifyData(byte[] data, byte[] signature, PublicKey publicKey);

        byte[] VerifyStream(Stream cipherstream, byte[] signature, PublicKey publicKey);
    }
}