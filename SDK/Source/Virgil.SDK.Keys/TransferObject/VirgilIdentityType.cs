using System.Runtime.Serialization;

namespace Virgil.SDK.Keys.TransferObject
{
    public enum VirgilIdentityType
    {
        [EnumMember(Value = "email")]
        Email,
        [EnumMember(Value = "application")]
        Application
    }
}