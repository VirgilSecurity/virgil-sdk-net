namespace Virgil.SDK.Keys.Helpers
{
    using System;
    using Models;

    /// <summary>
    ///     Extension methods for domain enums
    /// </summary>
    internal static class EnumHelpers
    {
        /// <summary>
        ///     Attempts to convert string to the UserDataType enum value
        /// </summary>
        /// <param name="input">The string input.</param>
        /// <returns>UserDataType value</returns>
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

        /// <summary>
        ///     Attempts to convert string to the UserDataClass enum value
        /// </summary>
        /// <param name="input">The string input.</param>
        /// <returns>UserDataType value</returns>
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

        /// <summary>
        ///     Converts UserDataType enum value to the json value.
        /// </summary>
        /// <param name="type">The UserDataType type.</param>
        /// <returns>String representation of UserDataType value</returns>
        /// <exception cref="ArgumentOutOfRangeException">type</exception>
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

        /// <summary>
        ///     Converts UserDataClass enum value to the json value.
        /// </summary>
        /// <param name="class">The UserDataClass type.</param>
        /// <returns>String representation of UserDataClass value</returns>
        /// <exception cref="ArgumentOutOfRangeException">@class</exception>
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