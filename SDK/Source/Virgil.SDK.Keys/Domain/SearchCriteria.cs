namespace Virgil.SDK.Keys.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TransferObject;

    public class SearchCriteria
    {
        internal string IdentityValue { get; private set; }
        internal bool? IncludeUnconfirmed { get; private set; }
        internal IdentityType? IdentityType { get; private set; }
        internal IEnumerable<Guid> Relations { get; private set; }
        
        public SearchCriteria(string identityValue)
        {
            IdentityValue = identityValue;
        }

        public SearchCriteria WithIdentityType(IdentityType? identityType)
        {
            this.IdentityType = identityType;
            return this;
        }

        public SearchCriteria WithRelations(IEnumerable<Guid> relations)
        {
            this.Relations = relations;
            return this;
        }

        public SearchCriteria WithRelations(params Guid[] relations)
        {
            this.Relations = relations;
            return this;
        }

        public SearchCriteria WithUnconfirmed(bool includeUnconfirmed)
        {
            this.IncludeUnconfirmed = includeUnconfirmed;
            return this;
        }
        
        public Task<Cards> Execute()
        {
            return Cards.Search(this);
        }
    }
}