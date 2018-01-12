using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Virgil.SDK.Web.Authorization
{
    public interface IAccessTokenProvider
    {
        Task<IAccessToken> GetTokenAsync(bool forceReload=false);
    }
}
