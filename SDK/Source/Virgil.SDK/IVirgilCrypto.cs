namespace Virgil.SDK
{
    using System.Collections.Generic;

    public interface IVirgilCrypto
    {
        VirgilBuffer Encrypt(VirgilBuffer data, IEnumerable<IVirgilCard> recipients);
        VirgilBuffer SignThenEncrypt(VirgilBuffer data, IVirgilKey key, IEnumerable<IVirgilCard> recipients);
        VirgilBuffer Encrypt(VirgilBuffer data, string password);
        VirgilBuffer Decrypt(VirgilBuffer cipherData, string password);
        VirgilBuffer Decrypt(VirgilBuffer cipherData, IVirgilKey key);
        VirgilBuffer DecryptThenVerify(VirgilBuffer cipherData, IVirgilKey key, IVirgilCard senderCard);
        VirgilBuffer Sign(VirgilBuffer data, IVirgilKey key);
        bool Verify(VirgilBuffer data, VirgilBuffer signature, IVirgilCard card);
    }
}