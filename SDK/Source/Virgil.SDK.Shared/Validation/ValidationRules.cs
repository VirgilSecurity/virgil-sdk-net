namespace Virgil.SDK.Validation
{
    using System.Collections.Generic;

    public class ValidationRules
    {
        public bool IgnoreVirgilSignatures { get; set; }
        public bool IgnoreSelfSignature { get; set; }
        public ValidationPolicy Policy { get; set; }
        public IEnumerable<VerifierInfo> Verifiers { get; set; }
    }
}