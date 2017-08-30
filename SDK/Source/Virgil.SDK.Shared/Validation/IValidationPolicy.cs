namespace Virgil.SDK.Validation
{
    using System.Collections.Generic;

    public interface IValidationPolicy
    {
        IEnumerable<IValidationRule> Rules { get; }
    }
}