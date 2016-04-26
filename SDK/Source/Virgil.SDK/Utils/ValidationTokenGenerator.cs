namespace Virgil.SDK.Utils
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;

    using Virgil.Crypto;
    using Virgil.SDK.Identities;

    /// <summary>
    /// Provides a helper methods to generate validation token based on application's private key.  
    /// </summary>
    public class ValidationTokenGenerator
    {
        /// <summary>
        /// Generates the validation token based on application's private key. 
        /// </summary>
        /// <param name="identityValue">The identity value.</param>
        /// <param name="identityType">The type of the identity.</param>
        /// <param name="privateKey">The application's private key.</param>
        /// <param name="privateKeyPassword">The private key password.</param>
        /// <returns></returns>
        public static string Generate(string identityValue, IdentityType identityType, byte[] privateKey, string privateKeyPassword = null)
        {
            var stringIdentityType = ExtractEnumValue(identityType);

            var identitySignature = CryptoHelper.Sign(stringIdentityType + identityValue, privateKey, privateKeyPassword);
            return identitySignature;
        }

        private static string ExtractEnumValue(IdentityType identityType)
        {
            var enumType = typeof (IdentityType);
            var name = Enum.GetName(enumType, identityType);
            var enumMemberAttribute = ((EnumMemberAttribute[]) enumType.GetRuntimeField(name).GetCustomAttributes(typeof (EnumMemberAttribute), true)).Single();
            var stringIdentityType = enumMemberAttribute.Value;

            return stringIdentityType;
        }
    }
}