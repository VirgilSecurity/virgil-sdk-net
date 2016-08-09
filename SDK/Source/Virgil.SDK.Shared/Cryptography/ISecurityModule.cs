namespace Virgil.SDK.Cryptography
{
    using System.IO;

    public interface ISecurityModule
    {
        void GenerateKeyPair(IKeyPairParameters parameters);

        byte[] DecryptData(byte[] cipherdata, string recipientId, string keyName);

        byte[] DecryptStream(Stream cipherstream, string recipientId, string keyName);

        byte[] SignData(byte[] data, string keyName);

        byte[] SignStream(Stream cipherstream, string keyName);

        PublicKey ExportPublicKey(string keyName);    

        PrivateKey ExportPrivateKey(string keyName);
    }
}       