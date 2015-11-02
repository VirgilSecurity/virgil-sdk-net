namespace Virgil.SDK.Keys.Exceptions
{
    using System.Net;

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



    /// <summary>
    /// The exception that is thrown when user data integrity constraint violation occurs
    /// </summary>
    public class UserDataIntegrityConstraintViolationException : KeysServiceRequestException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserDataIntegrityConstraintViolationException"/> class.
        /// </summary>
        public UserDataIntegrityConstraintViolationException() : base(Localization.ExceptionUserDataIntegrityConstraintViolation)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDataIntegrityConstraintViolationException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="statusCode">The status code.</param>
        /// <param name="content">The content.</param>
        public UserDataIntegrityConstraintViolationException(int errorCode, string errorMessage, HttpStatusCode statusCode, string content) : base(errorCode, errorMessage, statusCode, content)
        {
        }
    }



    /// <summary>
    /// The exception that is thrown when user Data confirmation entity not found
    /// </summary>
    public class UserDataConfirmationEntityNotFoundException : KeysServiceRequestException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserDataConfirmationEntityNotFoundException"/> class.
        /// </summary>
        public UserDataConfirmationEntityNotFoundException() : base(Localization.ExceptionUserDataConfirmationEntityNotFound)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDataConfirmationEntityNotFoundException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="statusCode">The status code.</param>
        /// <param name="content">The content.</param>
        public UserDataConfirmationEntityNotFoundException(int errorCode, string errorMessage, HttpStatusCode statusCode, string content) : base(errorCode, errorMessage, statusCode, content)
        {
        }
    }



    /// <summary>
    /// The exception that is thrown when user Data confirmation token invalid
    /// </summary>
    public class UserDataConfirmationTokenInvalidException : KeysServiceRequestException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserDataConfirmationTokenInvalidException"/> class.
        /// </summary>
        public UserDataConfirmationTokenInvalidException() : base(Localization.ExceptionUserDataConfirmationTokenInvalid)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDataConfirmationTokenInvalidException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="statusCode">The status code.</param>
        /// <param name="content">The content.</param>
        public UserDataConfirmationTokenInvalidException(int errorCode, string errorMessage, HttpStatusCode statusCode, string content) : base(errorCode, errorMessage, statusCode, content)
        {
        }
    }



    /// <summary>
    /// The exception that is thrown when user data was already confirmed and does not need further confirmation
    /// </summary>
    public class UserDataWasAlreadyConfirmedException : KeysServiceRequestException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserDataWasAlreadyConfirmedException"/> class.
        /// </summary>
        public UserDataWasAlreadyConfirmedException() : base(Localization.ExceptionUserDataWasAlreadyConfirmed)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDataWasAlreadyConfirmedException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="statusCode">The status code.</param>
        /// <param name="content">The content.</param>
        public UserDataWasAlreadyConfirmedException(int errorCode, string errorMessage, HttpStatusCode statusCode, string content) : base(errorCode, errorMessage, statusCode, content)
        {
        }
    }



    /// <summary>
    /// The exception that is thrown when user data class specified is invalid
    /// </summary>
    public class UserDataClassSpecifiedIsInvalidException : KeysServiceRequestException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserDataClassSpecifiedIsInvalidException"/> class.
        /// </summary>
        public UserDataClassSpecifiedIsInvalidException() : base(Localization.ExceptionUserDataClassSpecifiedIsInvalid)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDataClassSpecifiedIsInvalidException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="statusCode">The status code.</param>
        /// <param name="content">The content.</param>
        public UserDataClassSpecifiedIsInvalidException(int errorCode, string errorMessage, HttpStatusCode statusCode, string content) : base(errorCode, errorMessage, statusCode, content)
        {
        }
    }



    /// <summary>
    /// The exception that is thrown when domain value specified for the domain identity is invalid
    /// </summary>
    public class DomainValueDomainIdentityIsInvalidException : KeysServiceRequestException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainValueDomainIdentityIsInvalidException"/> class.
        /// </summary>
        public DomainValueDomainIdentityIsInvalidException() : base(Localization.ExceptionDomainValueDomainIdentityIsInvalid)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainValueDomainIdentityIsInvalidException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="statusCode">The status code.</param>
        /// <param name="content">The content.</param>
        public DomainValueDomainIdentityIsInvalidException(int errorCode, string errorMessage, HttpStatusCode statusCode, string content) : base(errorCode, errorMessage, statusCode, content)
        {
        }
    }



    /// <summary>
    /// The exception that is thrown when this user id had been confirmed earlier
    /// </summary>
    public class UserIdHadBeenConfirmedException : KeysServiceRequestException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserIdHadBeenConfirmedException"/> class.
        /// </summary>
        public UserIdHadBeenConfirmedException() : base(Localization.ExceptionUserIdHadBeenConfirmed)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserIdHadBeenConfirmedException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="statusCode">The status code.</param>
        /// <param name="content">The content.</param>
        public UserIdHadBeenConfirmedException(int errorCode, string errorMessage, HttpStatusCode statusCode, string content) : base(errorCode, errorMessage, statusCode, content)
        {
        }
    }



    /// <summary>
    /// The exception that is thrown when the user data is not confirmed yet
    /// </summary>
    public class UserDataIsNotConfirmedYetException : KeysServiceRequestException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserDataIsNotConfirmedYetException"/> class.
        /// </summary>
        public UserDataIsNotConfirmedYetException() : base(Localization.ExceptionUserDataIsNotConfirmedYet)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDataIsNotConfirmedYetException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="statusCode">The status code.</param>
        /// <param name="content">The content.</param>
        public UserDataIsNotConfirmedYetException(int errorCode, string errorMessage, HttpStatusCode statusCode, string content) : base(errorCode, errorMessage, statusCode, content)
        {
        }
    }


    /// <summary>
    /// The exception that is thrown when the user data value is required
    /// </summary>
    public class UserDataValueIsRequiredException : KeysServiceRequestException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserDataValueIsRequiredException"/> class.
        /// </summary>
        public UserDataValueIsRequiredException() : base(Localization.ExceptionUserDataValueIsRequired)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDataValueIsRequiredException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="statusCode">The status code.</param>
        /// <param name="content">The content.</param>
        public UserDataValueIsRequiredException(int errorCode, string errorMessage, HttpStatusCode statusCode, string content) : base(errorCode, errorMessage, statusCode, content)
        {
        }
    }



    /// <summary>
    /// The exception that is thrown when user info data validation failed
    /// </summary>
    public class UserInfoDataValidationFailedException : KeysServiceRequestException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserInfoDataValidationFailedException"/> class.
        /// </summary>
        public UserInfoDataValidationFailedException() : base(Localization.ExceptionUserInfoDataValidationFailed)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserInfoDataValidationFailedException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="statusCode">The status code.</param>
        /// <param name="content">The content.</param>
        public UserInfoDataValidationFailedException(int errorCode, string errorMessage, HttpStatusCode statusCode, string content) : base(errorCode, errorMessage, statusCode, content)
        {
        }
    }



}