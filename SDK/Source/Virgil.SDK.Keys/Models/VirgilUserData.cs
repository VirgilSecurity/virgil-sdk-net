namespace Virgil.SDK.Keys.Models
{
    using System;
    using System.Collections.Generic;
    using Dtos;
    using Helpers;

    /// <summary>
    /// Represents user data object associated to the public key
    /// </summary>
    public class VirgilUserData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilUserData"/> class.
        /// </summary>
        public VirgilUserData()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilUserData"/> class.
        /// </summary>
        /// <param name="userDataType">Type of the user data.</param>
        /// <param name="value">The value.</param>
        public VirgilUserData(UserDataType userDataType, string value)
        {
            Signs = null;
            Type = userDataType;
            Class = UserDataClass.UserId;
            Value = value;
        }

        internal VirgilUserData(PkiUserData pkiUserData)
        {
            Signs = null;
            UserDataId = pkiUserData.Id.UserDataId;
            Type = pkiUserData.Type.ToUserDataType();
            Class = pkiUserData.Class.ToUserDataClass();
            Value = pkiUserData.Value;
        }

        /// <summary>
        /// Gets or sets the user data identifier.
        /// </summary>
        /// <value>
        /// The user data identifier.
        /// </value>
        public Guid UserDataId { get; set; }

        /// <summary>
        /// Gets or sets the user data type.
        /// </summary>
        /// <value>
        /// The user data type.
        /// </value>
        public UserDataType Type { get; set; }

        /// <summary>
        /// Gets or sets the user data class.
        /// </summary>
        /// <value>
        /// The user data class.
        /// </value>
        public UserDataClass Class { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value of the user data (email, phone number, application name, etc.).
        /// </value>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the signs.
        /// </summary>
        /// <value>
        /// The signs collection of this user data.
        /// </value>
        public IEnumerable<VirgilSign> Signs { get; set; }
    }
}