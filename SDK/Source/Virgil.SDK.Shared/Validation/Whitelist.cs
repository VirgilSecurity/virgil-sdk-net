namespace Virgil.SDK.Validation
{
    using System.Collections.Generic;

    public class Whitelist
    {
        public WhitelistStrategy Strategy { get; set; }
        public IEnumerable<VerifierInfo> Signers { get; set; }
    }
}