using System.Runtime.Serialization;

namespace Virgil.SDK.Keys.TransferObject
{
    public enum IdentityType
    {
        [EnumMember(Value = "email")]
        Email,
        [EnumMember(Value = "application")]
        Application
    }
}