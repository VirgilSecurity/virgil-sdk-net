namespace Virgil.SDK.Validation
{
    public interface ICardValidator
    {
        void SetValidationPolicy(IValidationPolicy policy);
        ValidationResult Validate(Card card);
    }
}