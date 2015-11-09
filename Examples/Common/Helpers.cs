namespace Virgil.Examples.Common
{
    using System;
    using System.Text.RegularExpressions;

    public static class Helpers
    {
        public static string GetExampleDescription(this Type type)
        {
            return Regex.Replace(type.Name, "(\\B[A-Z])", " $1");
        }
    }
}