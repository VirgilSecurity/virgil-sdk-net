using Virgil.SDK.Signer;

namespace Virgil.SDK.Common
{
    using System;

    public static class EnumExtensions
    {
        public static string ToLowerString(this SignerType signerType)
        {
            return Enum.GetName(typeof(SignerType), signerType)?.ToLower();
        }
    }
}
