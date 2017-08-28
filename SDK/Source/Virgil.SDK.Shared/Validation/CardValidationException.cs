namespace Virgil.SDK.Validation
{
    using System.Collections.Generic;

    public class CardValidationException : VirgilException
    {
        public CardValidationException(IEnumerable<ValidationError> errors) 
            : base("Validation errors have been detected")
        {
            this.Errors = errors;
        }

        public IEnumerable<ValidationError> Errors { get; }
    }
}