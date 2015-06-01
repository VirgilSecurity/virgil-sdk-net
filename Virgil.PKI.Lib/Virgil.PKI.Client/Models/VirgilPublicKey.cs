namespace Virgil.PKI.Models
{
    using System;
    using System.Collections.Generic;

    public class VirgilPublicKey
    {
        public Guid PublicKeyId { get; set; }
        public IEnumerable<VirgilUserData> UserData { get; set; }
        public byte[] PublicKey { get; set; }
    }
}