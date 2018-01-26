using System;
using System.Collections.Generic;
using System.Text;

namespace Virgil.SDK.Web.Authorization
{
    public class ExpiredAccessTokenException : VirgilException
    {
        public ExpiredAccessTokenException() : base(" Expired access token error.")
        {
        }
    }
}
