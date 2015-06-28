namespace Virgil.SDK.Keys.Exceptions
{
    /// <summary>
    /// The exception that is thrown when an user data with same fields is already exists.
    /// </summary>
    public class UserDataAlreadyExistsException : KeysException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserDataAlreadyExistsException" /> class.
        /// </summary>
        public UserDataAlreadyExistsException() 
            : base(Localization.ExceptionUserDataAlreadyExists)
        {
        }
    }
}