namespace Virgil.SDK.Client
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Virgil.SDK.Client.Models;

    public class VirgilClient : IVirgilClient
    {
        public Task<IEnumerable<VirgilCardModel>> SearchCardsAsync(SearchCardsCreteria creteria)
        {
            throw new NotImplementedException();
        }

        public Task<VirgilCardModel> RegisterCardAsync(RegisterCardRequest request)
        {
            throw new NotImplementedException();
        }

        public Task BeginGlobalCardRegisterationAsync(RegisterGlobalCardRequest request)
        {
            throw new NotImplementedException();
        }

        public Task CompleteGlobalCardRegisterationAsync()
        {
            throw new NotImplementedException();
        }

        public Task RevokeCardAsync(RevokeCardRequest request)
        {
            throw new NotImplementedException();
        }
    }
}