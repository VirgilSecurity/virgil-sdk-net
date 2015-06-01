using System;
using System.Collections.Generic;

namespace Virgil.PKI.Models
{
    public class VirgilCertificate
    {
        public Guid CertificateId { get; set; }
        public IEnumerable<VirgilUserData> UserData { get; set; }
        public byte[] PublicKey { get; set; }
    }
}