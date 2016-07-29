namespace Virgil.SDK.Helpers
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;

    using Virgil.SDK.Identities;

    internal static class IdentityTypeExtensions
    {
        public static string ExtractEnumValue(this IdentityType identityType)
        {
            var enumType = typeof(IdentityType);
            var name = Enum.GetName(enumType, identityType);
            var enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetRuntimeField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true)).Single();
            var stringIdentityType = enumMemberAttribute.Value;

            return stringIdentityType;
        }
    }
}