namespace Virgil.SDK.Exceptions
{
    /// <summary>
    /// The exception that is thrown when an <see cref="VirgilCard"/> is not found.
    /// </summary>
    public class VirgilCardIsNotFoundException : VirgilException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilCardIsNotFoundException"/> class.
        /// </summary>
        public VirgilCardIsNotFoundException() : base("Virgil Card is not found")
        {
        }
    }
}