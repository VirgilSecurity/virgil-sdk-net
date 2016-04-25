namespace Virgil.SDK.Identities
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents identity type
    /// </summary>
    public enum IdentityType
    {
        /// <summary>
        /// The email identity type
        /// </summary>
        [EnumMember(Value = "email")] Email,

        /// <summary>
        /// The custom identity type.
        /// </summary>
        [EnumMember(Value = "custom")] Custom,

        /// <summary>
        /// The application identity type
        /// </summary>
        [EnumMember(Value = "application")] Application
    }
}