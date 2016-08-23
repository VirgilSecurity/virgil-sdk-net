namespace Virgil.SDK.Cryptography
{
    public interface ISecurityModule
    {
        PublicKey GetPublicKey();
        byte[] DecryptData(byte[] cipherdata);
        byte[] SignData(byte[] data);
    }
}       