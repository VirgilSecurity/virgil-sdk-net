namespace Virgil.SDK.Keys.Exceptions
{
    /// <summary>
    /// The exception that is thrown when an user data is not found.
    /// </summary>
    public class UserDataNotFoundException : KeysException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserDataNotFoundException" /> class.
        /// </summary>
        public UserDataNotFoundException() 
            : base(Localization.ExceptionUserDataNotFound)
        {
        }
    }
}