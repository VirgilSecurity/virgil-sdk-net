using System.Threading.Tasks;

namespace Virgil.SDK.Web.Authorization
{
    public interface IAccessManager
    {
        Task<JsonWebToken> GetAccessTokenAsync();
    }
}
