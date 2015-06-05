namespace Virgil.PKI.Models
{
    using System;
    using System.Collections.Generic;
    using Virgil.PKI.Dtos;

    public class VirgilUserData
    {
        public VirgilUserData(PkiUserData pkiUserData)
        {
            this.Signs = null;
            this.UserDataId = pkiUserData.Id.UserDataId;

            UserDataType userDataType;
            switch (pkiUserData.Type)
            {
                case "email":
                    userDataType = UserDataType.Email;
                    break;
                case "application":
                    userDataType = UserDataType.Application;
                    break;
                case "domain":
                    userDataType = UserDataType.Domain;
                    break;
                default:
                    userDataType = UserDataType.Unknown;
                    break;
            }

            UserDataClass userDataClass;
            switch (pkiUserData.Class)
            {
                case "user_id":
                    userDataClass = UserDataClass.UserId;
                    break;
                case "user_info":
                    userDataClass = UserDataClass.UserInfo;
                    break;
                default:
                    userDataClass = UserDataClass.Unknown;
                    break;
            }

            this.Type = userDataType;
            this.Class = userDataClass;
            this.Value = pkiUserData.Value;
        }

        public Guid UserDataId { get; set; }
        public UserDataType Type { get; set; }
        public UserDataClass Class { get; set; }
        public string Value { get; set; }
        public IEnumerable<VirgilSign> Signs { get; set; }
    }
}