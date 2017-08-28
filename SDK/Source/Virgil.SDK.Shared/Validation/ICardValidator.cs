namespace Virgil.SDK.Validation
{
    public interface ICardValidator
    {
        ValidationResult Validate(Card card);
    }
}