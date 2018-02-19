namespace Virgil.SDK.Verification
{
    /// <summary>
    ///  The <see cref="ICardVerifier"/> provides interface 
    /// for card verification process.
    /// </summary>
    public interface ICardVerifier
    {
        /// <summary>
        /// Verify the specified card.
        /// </summary>
        /// <param name="card">The instance of <see cref="Card"/> to be verified.</param>
        /// <returns>True if card is verified, False otherwise</returns>
        bool VerifyCard(Card card);
    }
}