namespace Virgil.SDK.Keys.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a public key is not found.
    /// </summary>
    public class PublicKeyNotFoundException : KeysException
    {
                 /// <summary>
        /// Initializes a new instance of the <see cref="PublicKeyNotFoundException" /> class.
        /// </summary>
        public PublicKeyNotFoundException() 
            : base(Localization.ExceptionPublicKeyNotFound)
        {
        }
    }
}