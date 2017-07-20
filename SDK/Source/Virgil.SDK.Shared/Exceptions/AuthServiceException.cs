using System;
using System.Collections.Generic;
using System.Text;
using Virgil.SDK.Exceptions;

namespace Virgil.SDK.Exceptions
{
    public class AuthServiceException : VirgilServiceException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityServiceException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        public AuthServiceException(int errorCode, string errorMessage) : base(errorCode, errorMessage)
        {
        }
    }
}
