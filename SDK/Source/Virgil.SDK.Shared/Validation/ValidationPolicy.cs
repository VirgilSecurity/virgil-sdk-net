namespace Virgil.SDK.Validation
{
    using System.Collections.Generic;
    using Virgil.SDK.Validation.Rules;

    public class ValidationPolicy : IValidationPolicy
    {
        public bool IgnoreSelfSignature { get; set; }
        
        public bool IgnoreVirgilSignature { get; set; }
        
        public Whitelist Whitelist { get; set; }

        public IEnumerable<IValidationRule> Rules
        {
            get
            {
                if (!this.IgnoreSelfSignature)
                {
                    yield return new SelfValidationRule();
                }

                if (!this.IgnoreVirgilSignature)
                {
                    yield return new VirgilValidationRule();
                }

                if (this.Whitelist != null)
                {
                    yield return new WhitelistValidationRule(this.Whitelist);
                }
            }
        }
    }
}