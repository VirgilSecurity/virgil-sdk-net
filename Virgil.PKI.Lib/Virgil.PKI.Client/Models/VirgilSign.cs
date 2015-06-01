namespace Virgil.PKI.Models
{
    using System;

    public class VirgilSign
    {
        public Guid SignId { get; set; }
        public Guid SignerUserDataId { get; set; }
        public byte[] DataSign { get; set; }
    }
}