namespace Virgil.SDK.PrivateKeys.Model
{
    using System;

    public class PrivateKey
    {
        public Guid PublicKeyId { get; set; }
        public byte[] Key { get; set; }
    }
}