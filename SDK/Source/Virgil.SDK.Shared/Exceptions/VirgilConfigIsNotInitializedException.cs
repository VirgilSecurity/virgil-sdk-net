namespace Virgil.SDK.Exceptions
{
    public class VirgilConfigIsNotInitializedException : VirgilException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilConfigIsNotInitializedException"/> class.
        /// </summary>
        public VirgilConfigIsNotInitializedException() : base(Localization.ExceptionVirgilConfigIsNotInitialized)
        {
        }
    }
}