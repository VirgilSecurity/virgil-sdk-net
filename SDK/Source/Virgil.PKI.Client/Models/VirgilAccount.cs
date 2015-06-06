using Virgil.PKI.Dtos;

namespace Virgil.PKI.Models
{
    using System;
    using System.Collections.Generic;

    public class VirgilAccount
    {
        public VirgilAccount(PkiPublicKey result)
        {
            this.AccountId = result.Id.AccountId;
            this.PublicKeys = new[] {new VirgilPublicKey(result)};
        }

        public Guid AccountId { get; set; }
        public IEnumerable<VirgilPublicKey> PublicKeys { get; set; }
    }
}