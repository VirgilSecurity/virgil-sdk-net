namespace Virgil.SDK.Keys.Helpers
{
    using System;

    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class ValidatedNotNullAttribute : Attribute
    {
    }
}