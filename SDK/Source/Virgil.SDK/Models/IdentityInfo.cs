namespace Virgil.SDK.Models
{
    using Virgil.SDK.TransferObject;

    /// <summary>
    /// Represents identity object returned from virgil card service
    /// </summary>
    public class IdentityInfo
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public IdentityType Type { get; private set; }

        /// <summary>
        /// Gets or sets the validation token.
        /// </summary>
        public string ValidationToken { get; private set; }

        /// <summary>
        /// Creates an email that obtain 
        /// </summary>
        public static IdentityInfo CreateEmail(string email, string validationToken = null)
        {
            return 
        }
    }
}