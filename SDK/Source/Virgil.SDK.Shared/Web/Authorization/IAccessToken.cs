using System;
using System.Collections.Generic;
using System.Text;

namespace Virgil.SDK.Web.Authorization
{
    public interface IAccessToken
    {
        string Identity();
        bool IsExpired();
        string ToString();
    }
}
