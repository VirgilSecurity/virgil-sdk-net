namespace Virgil.PKI.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Virgil.PKI.Dtos;

    public class VirgilPublicKey
    {
        public Guid PublicKeyId { get; set; }
        public IEnumerable<VirgilUserData> UserData { get; set; }
        public byte[] PublicKey { get; set; }

        public VirgilPublicKey()
        {
            
        }

        public VirgilPublicKey(PkiPublicKey publicKey)
        {
            this.PublicKeyId = publicKey.Id.PublicKeyId;
            this.PublicKey = publicKey.PublicKey;

            this.UserData = publicKey.UserData.Select(it => new VirgilUserData(it));
        }
    }
}