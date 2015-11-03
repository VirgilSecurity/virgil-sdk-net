namespace Virgil.Examples.Common
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    public static class Helpers
    {
        public static string GetExampleDescription(this Type type)
        {
            var descriptions = (DescriptionAttribute[])
                type.GetCustomAttributes(typeof(DescriptionAttribute), false);
            
            if (descriptions.Length == 0)
            {
                return type.Name;
            }

            return descriptions.First().Description;
        }
    }
}