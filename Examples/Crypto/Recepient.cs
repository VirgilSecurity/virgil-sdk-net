namespace Virgil.Examples.Crypto
{
    using System;

    public class Recepient
    {
        public Guid Id { get; set; }
        public byte[] PublicKey { get; set; }
        public byte[] PrivateKey { get; set; }
    }
}