using System;
using Virgil.PKI.Models;

namespace Virgil.PKI.Helpers
{
    public static class EnumHelpers
    {
        public static UserDataType ToUserDataType(this string input)
        {
            UserDataType userDataType;
            switch (input)
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

            return userDataType;
        }

        public static UserDataClass ToUserDataClass(this string input)
        {
            UserDataClass userDataClass;
            switch (input)
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
            return userDataClass;
        }

        public static string ToJsonValue(this UserDataType type)
        {
            string userIdType;
            switch (type)
            {
                case UserDataType.Email:
                    userIdType = "email";
                    break;
                case UserDataType.Domain:
                    userIdType = "domain";
                    break;
                case UserDataType.Application:
                    userIdType = "application";
                    break;

                default:
                    throw new ArgumentOutOfRangeException("type");
            }
            return userIdType;
        }

        public static string ToJsonValue(this UserDataClass @class)
        {
            string userIdType;
            switch (@class)
            {
                case UserDataClass.UserId:
                    userIdType = "user_id";
                    break;
                case UserDataClass.UserInfo:
                    userIdType = "uder_info";
                    break;

                default:
                    throw new ArgumentOutOfRangeException("class");
            }
            return userIdType;
        }
    }
}