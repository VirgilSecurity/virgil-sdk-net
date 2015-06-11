namespace Virgil.PKI.Models
{
    using System;
    using System.Collections.Generic;
    using Virgil.PKI.Dtos;
    using Virgil.PKI.Helpers;
    
    public class VirgilUserData
    {
        public VirgilUserData()
        {
        }

        public VirgilUserData(UserDataType userDataType, string value)
        {
            this.Signs = null;
            this.Type = userDataType;
            this.Class = UserDataClass.UserId;
            this.Value = value;
        }

        public VirgilUserData(PkiUserData pkiUserData)
        {
            this.Signs = null;
            this.UserDataId = pkiUserData.Id.UserDataId;
            this.Type = pkiUserData.Type.ToUserDataType();
            this.Class = pkiUserData.Class.ToUserDataClass();
            this.Value = pkiUserData.Value;
        }

        public Guid UserDataId { get; set; }
        public UserDataType Type { get; set; }
        public UserDataClass Class { get; set; }
        public string Value { get; set; }
        public IEnumerable<VirgilSign> Signs { get; set; }
    }
}