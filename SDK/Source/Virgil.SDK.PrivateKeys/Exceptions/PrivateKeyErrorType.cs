namespace Virgil.SDK.PrivateKeys.Exceptions
{
    public enum PrivateKeyErrorType
    {
        PublicKeyIdInvalid = 50001,       // - Key error. Public Key ID validation failed
        PublicKeyIdNotFound = 50002,      // - Key error. Public Key ID was not found
        PrivateKeyAlreadyExists = 50003,  // - Key error. Public Key ID already exists
        PrivateKeyInvalid = 50004,        // - Key error. Private key validation failed
        PrivateKeyInvalidBase64 = 50005,  // - Key error. Private key base64 validation failed
    }
}