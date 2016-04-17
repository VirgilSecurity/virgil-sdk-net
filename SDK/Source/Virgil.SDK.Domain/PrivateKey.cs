namespace Virgil.SDK.Domain
{
    using Virgil.Crypto;

    public class PrivateKey
    {
        public PrivateKey(byte[] privateKeyData)
        {
            this.Data = privateKeyData;
        }

        public PrivateKey(VirgilKeyPair virgilKeyPair)
        {
            this.Data = virgilKeyPair.PrivateKey();
        }

        public byte[] Data { get; }

        public static implicit operator byte[](PrivateKey @this) => @this.Data;
    }
}