using System.Threading.Tasks;

namespace Virgil.SDK.Shared.Web.Authorization
{
    public interface IAccessManager
    {
        Task<JsonWebToken> GetAccessTokenAsync();
    }
}
