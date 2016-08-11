 namespace Virgil.SDK.Cryptography
{
    using System.IO;

    public interface ISecurityModule
    {
        byte[] DecryptData(byte[] cipherdata);
        byte[] DecryptStream(Stream cipherstream);
        byte[] SignData(byte[] data);
        byte[] SignStream(Stream cipherstream);

        PublicKey ExportPublicKey();    
        PrivateKey ExportPrivateKey();

        void Initialize(string pairName, SecurityModuleBehavior behavior, IKeyPairParameters keyPairParameters);
    }
}       