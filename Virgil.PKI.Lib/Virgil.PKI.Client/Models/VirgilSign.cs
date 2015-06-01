using System;

namespace Virgil.PKI.Models
{
    public class VirgilSign
    {
        public Guid SignId { get; set; }
        public Guid SignerUserDataId { get; set; }
        public byte[] DataSign { get; set; }
    }
}