using Virgil.SDK.Keys.Model;
using Virgil.SDK.Keys.TransferObject;

namespace Virgil.SDK.Keys.Models
{
    using System;
    using System.Collections.Generic;
    using Virgil.SDK.Keys.Helpers;

    /// <summary>
    /// Represents user data object associated to the public key.
    /// </summary>
    public class UserData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserData"/> class.
        /// </summary>
        public UserData()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserData"/> class by transfer object.
        /// </summary>
        /// <param name="pkiUserData">The user data transfer object</param>
        internal UserData(PkiUserData pkiUserData)
        {
            this.Signs = null;
            this.UserDataId = pkiUserData.Id.UserDataId;
            this.Type = pkiUserData.Type.ToUserDataType();
            this.Class = pkiUserData.Class.ToUserDataClass();
            this.Value = pkiUserData.Value;
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
        public IEnumerable<Sign> Signs { get; set; }
    }
}