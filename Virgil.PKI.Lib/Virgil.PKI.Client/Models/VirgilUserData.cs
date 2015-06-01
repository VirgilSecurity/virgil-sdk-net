namespace Virgil.PKI.Models
{
    using System;
    using System.Collections.Generic;

    public class VirgilUserData
    {
        public Guid UserDataId { get; set; }
        public UserDataType Type { get; set; }
        public UserDataClass Class { get; set; }
        public string Value { get; set; }
        public IEnumerable<VirgilSign> Signs { get; set; }
    }
}