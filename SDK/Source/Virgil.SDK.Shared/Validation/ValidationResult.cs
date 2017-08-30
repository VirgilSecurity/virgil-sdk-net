namespace Virgil.SDK.Validation
{
    using System.Collections.Generic;
    using System.Linq;

    public class ValidationResult
    {
        public bool IsValid => this.Errors == null || !this.Errors.Any();
        public IEnumerable<string> Errors { get; internal set; }
    }
}