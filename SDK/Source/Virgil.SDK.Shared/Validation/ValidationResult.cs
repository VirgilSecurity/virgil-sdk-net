namespace Virgil.SDK.Validation
{
    using System.Collections.Generic;
    using System.Linq;

    public class ValidationResult
    {
        private readonly List<ValidationError> errors;
        
        public ValidationResult()
        {
            this.errors = new List<ValidationError>();
        }

        public bool IsValid => !this.errors.Any();
        public IReadOnlyList<ValidationError> Errors { get; set; }

        internal void AddError(Card card, string message)
        {
            this.errors.Add(new ValidationError { Card = card, Message = message });
        }
    }
}