namespace Virgil.Examples.Common
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class Helpers
    {
        public static string GetTypeName(this Type type)
        {
            return Regex.Replace(type.Name, "(\\B[A-Z])", " $1");
        }

        public static string GetTypeDescription(this Type type)
        {
            var descriptionAttr = type.GetCustomAttributes(false)
                .OfType<DescriptionAttribute>().FirstOrDefault();

            if (descriptionAttr != null)
            {
                return descriptionAttr.Description;
            }

            return type.GetTypeName();
        }
    }
}