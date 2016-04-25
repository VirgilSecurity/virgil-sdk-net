namespace Virgil.SDK.Utils
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    using Virgil.Crypto;
    using Virgil.SDK.Models;

    /// <summary>
    /// Provides a helper methods to generate validation token based on application's private key.  
    /// </summary>
    public class IdentitySigner
    {
        /// <summary>
        /// Signs the specified identity with application's private key.
        /// </summary>
        public static string Sign(string identityValue, IdentityType identityType, byte[] privateKey, string privateKeyPassword = null)
        {
            var enumType = typeof(IdentityType);
            var name = Enum.GetName(enumType, identityType);
            var enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetRuntimeField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true)).Single();
            var stringIdentityType = enumMemberAttribute.Value;

            var identitySignature = CryptoHelper.Sign(stringIdentityType + identityValue, privateKey, privateKeyPassword);
            return identitySignature;
        }
    }
}