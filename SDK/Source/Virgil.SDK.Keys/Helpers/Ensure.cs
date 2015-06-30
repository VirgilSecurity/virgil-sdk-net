namespace Virgil.SDK.Keys.Helpers
{
    using System;
    using System.ComponentModel;

    using Virgil.SDK.Keys.Models;

    /// <summary>
    /// Ensure input parameters
    /// </summary>
    internal static class Ensure
    {
        /// <summary>
        /// Checks an argument to ensure it isn't null.
        /// </summary>
        /// <param name="value">The argument value to check</param>
        /// <param name="name">The name of the argument</param>
        public static void ArgumentNotNull([ValidatedNotNull] object value, string name)
        {
            if (value != null) return;

            throw new ArgumentNullException(name);
        }

        /// <summary>
        /// Checks a string argument to ensure it isn't null or empty.
        /// </summary>
        /// <param name="value">The argument value to check</param>
        /// <param name="name">The name of the argument</param>
        public static void ArgumentNotNullOrEmptyString([ValidatedNotNull] string value, string name)
        {
            ArgumentNotNull(value, name);
            if (!string.IsNullOrWhiteSpace(value)) return;

            throw new ArgumentException(Localization.ExceptionStringCanNotBeEmpty, name);
        }

        /// <summary>
        /// Checks a user data type argument to ensure is isn't UserInfo or Unknown.
        /// </summary>
        /// <param name="dataType">The argument value to check</param>
        /// <param name="name">The name of the argument</param>
        public static void UserDataTypeIsUserId(UserDataType dataType, string name)
        {
            if ((int)dataType > 9999 || (int)dataType == 0)
            {
                throw new InvalidEnumArgumentException(name, (int)dataType, typeof(UserDataType));
            }
        }

        /// <summary>
        /// Checks a user data type argument to ensure is isn't Unknown.
        /// </summary>
        /// <param name="dataType">The argument value to check</param>
        /// <param name="name">The name of the argument</param>
        public static void UserDataTypeIsNotUnknown(UserDataType dataType, string name)
        {
            if (dataType == UserDataType.Unknown)
            {
                throw new InvalidEnumArgumentException(name, (int)dataType, typeof(UserDataType));
            }
        }
    }
}