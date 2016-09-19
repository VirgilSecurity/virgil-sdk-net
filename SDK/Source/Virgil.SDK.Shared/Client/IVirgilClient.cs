namespace Virgil.SDK.Client
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Virgil.SDK.Client.Models;

    public interface IVirgilClient 
    {
        Task<IEnumerable<VirgilCardModel>> SearchCardsAsync(SearchCardsCreteria creteria);
        Task<VirgilCardModel> RegisterCardAsync(RegisterCardRequest request);
        Task BeginGlobalCardRegisterationAsync(RegisterGlobalCardRequest request);
        Task CompleteGlobalCardRegisterationAsync();
        Task RevokeCardAsync(RevokeCardRequest request);
    }
}