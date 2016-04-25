namespace Virgil.SDK.Models
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents a supported identity type that Virgil Service are able to verify.
    /// </summary>
    public enum VerifiableIdentityType
    {
        /// <summary>
        /// The email identity type
        /// </summary>
        [EnumMember(Value = "email")] Email
    }
}