namespace Virgil.SDK.Validation
{
    using System.Collections.Generic;
    using System.Linq;

    public class ValidationResult
    {
        private readonly List<string> errors;
        
        public ValidationResult()
        {
            this.errors = new List<string>();
        }

        public bool IsValid => !this.errors.Any();
        public IReadOnlyList<string> Errors => this.errors;

        internal void AddError(string message)
        {
            this.errors.Add(message);
        }
    }
}