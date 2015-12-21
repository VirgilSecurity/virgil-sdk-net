namespace Virgil.SDK.Keys.TransferObject
{
    using System.Runtime.Serialization;

    public enum IdentityType
    {
        [EnumMember(Value = "email")] Email,
        [EnumMember(Value = "application")] Application
    }
}