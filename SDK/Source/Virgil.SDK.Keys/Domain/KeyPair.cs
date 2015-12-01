namespace Virgil.SDK.Keys.Domain
{
    using System;

    public class KeyPair
    {
        public Guid Id { get; private set; }

        public PublicKey PublicKey { get; private set; }

        public PrivateKey PrivateKey { get; private set; }

        public static KeyPair Generate()
        {
            throw new NotImplementedException();
        }
    }
}