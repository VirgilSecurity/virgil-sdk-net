namespace Virgil.SDK.Cryptography
{
    using System.IO;

    public interface ISecurityModule
    {
        byte[] DecryptData(byte[] cipherdata);
        Stream DecryptStream(Stream cipherStream);
        byte[] SignData(byte[] data);
    }
}       