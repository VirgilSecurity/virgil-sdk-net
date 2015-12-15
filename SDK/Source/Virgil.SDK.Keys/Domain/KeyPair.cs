namespace Virgil.SDK.Keys.Domain
{
    using System;
    using Crypto;
    using TransferObject;

    public class KeyPair
    {
        internal KeyPair(Guid id, VirgilKeyPair virgilKeyPair)
        {
            Id = id;
            PrivateKey = new PrivateKey(virgilKeyPair.PrivateKey());
            PublicKey = new PublicKey(virgilKeyPair.PublicKey());
        }

        public KeyPair(PublicKeyDto publicKey, PrivateKey privateKey)
        {
            Id = publicKey.Id;
            PublicKey = new PublicKey(publicKey.PublicKey);
            PrivateKey = privateKey;
        }

        public Guid Id { get; private set; }

        public PublicKey PublicKey { get; private set; }

        public PrivateKey PrivateKey { get; private set; }
    }
}