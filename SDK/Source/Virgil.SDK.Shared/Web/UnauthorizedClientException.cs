using System;
using System.Collections.Generic;
using System.Text;

namespace Virgil.SDK.Web
{
    public class UnauthorizedClientException : ClientException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnauthorizedClientException"/> class.
        /// </summary>
        public UnauthorizedClientException(int serviceErrorCode, string message) : base(serviceErrorCode, message)
        {
        }
    }
}
