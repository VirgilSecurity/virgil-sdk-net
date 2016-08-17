namespace Virgil.SDK.Cryptography
{
    public interface ISecurityModule
    {
        byte[] DecryptData(byte[] cipherdata);
        byte[] SignData(byte[] data);
    }
}       