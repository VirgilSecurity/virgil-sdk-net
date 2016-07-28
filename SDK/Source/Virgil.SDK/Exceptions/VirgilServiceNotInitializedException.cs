namespace Virgil.SDK.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a Virgil Services is not initialized.
    /// </summary>
    public class VirgilServiceNotInitializedException : VirgilException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilServiceNotInitializedException"/> class.
        /// </summary>
        public VirgilServiceNotInitializedException() : base(Localization.ExceptionVirgilServiceNotInitialized)
        {
        }
    }
}