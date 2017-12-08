namespace Virgil.SDK.Storage.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the key doesn't exist.
    /// </summary>
    public class KeyNotFoundSecureStorageException : SecureStorageException
    {
        public KeyNotFoundSecureStorageException(string key) 
            : base($"Specified key '{key}' doesn't exist in the secure storage.")
        {
        }
    }
}
