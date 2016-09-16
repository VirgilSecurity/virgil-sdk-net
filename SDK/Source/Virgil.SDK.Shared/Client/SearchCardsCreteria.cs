namespace Virgil.SDK.Client
{
    using System.Collections.Generic;
    using Virgil.SDK.Client.Models;

    public class SearchCardsCreteria 
    {
        public IEnumerable<string> Identities { get; set; }

        public string IdentityType { get; set; }

        public VirgilCardScope Scope { get; set; }
    }
}