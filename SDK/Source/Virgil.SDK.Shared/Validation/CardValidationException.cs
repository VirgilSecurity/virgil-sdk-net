namespace Virgil.SDK.Validation
{
    using System.Collections.Generic;

    public class CardValidationException : VirgilException
    {
        public CardValidationException(IEnumerable<string> errors) 
            : base("Validation errors have been detected")
        {
            this.Errors = errors;
        }

        public IEnumerable<string> Errors { get; }
    }
}