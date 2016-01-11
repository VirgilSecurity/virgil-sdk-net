namespace Virgil.SDK.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Virgil.SDK.TransferObject;

    public class SearchBuilder
    {
        public SearchBuilder(string identityValue)
        {
            this.IdentityValue = identityValue;
        }

        internal string IdentityValue { get; private set; }
        internal bool? IncludeUnconfirmed { get; private set; }
        internal IdentityType? IdentityType { get; private set; }
        internal IEnumerable<Guid> Relations { get; private set; }

        public SearchBuilder WithIdentityType(IdentityType? identityType)
        {
            this.IdentityType = identityType;
            return this;
        }

        public SearchBuilder WithRelations(IEnumerable<Guid> relations)
        {
            this.Relations = relations;
            return this;
        }

        public SearchBuilder WithRelations(params Guid[] relations)
        {
            this.Relations = relations;
            return this;
        }

        public SearchBuilder WithUnconfirmed(bool includeUnconfirmed)
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