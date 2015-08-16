namespace Virgil.SDK.PrivateKeys.Exceptions
{
    public enum ContainerErrorType
    {
        ContainerInvalid = 40001,              // - Container validation failed
        ContainerNotFound = 40002,             // - Container was not found
        ContainerAlreadyExists = 40003,        // - Container already exists
        ContainerPasswordNotSpecified = 40004, // - Container password was not specified
        ContainerInvalidPassword = 40005,      // - Container password validation failed
        ContainerNotExists = 40006,            // - Container was not found in PKI service
        ContainerInvalidType = 40007           // - Container type validation failed
    }
}