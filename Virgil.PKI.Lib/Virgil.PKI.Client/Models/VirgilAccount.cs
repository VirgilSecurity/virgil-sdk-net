namespace Virgil.PKI.Models
{
    using System;
    using System.Collections.Generic;

    public class VirgilAccount
    {
        public Guid AccountId { get; set; }
        public IEnumerable<VirgilCertificate> Certificates { get; set; }
    }
}