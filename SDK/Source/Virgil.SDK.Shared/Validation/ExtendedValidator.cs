namespace Virgil.SDK.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    using Virgil.CryptoApi;
    using Virgil.SDK.Common;

    public class ExtendedValidator : ICardValidator
    {
        private readonly ICrypto crypto;
        private readonly List<IValidationRule> validationRules;
        
        public ExtendedValidator(ICrypto crypto)
        {
            this.crypto = crypto ?? throw new ArgumentNullException(nameof(crypto));
            this.validationRules = new List<IValidationRule>();
        }

        public void SetValidationPolicy(IValidationPolicy policy)
        {
            // clear rules in case multiple method call.
            this.validationRules.Clear();
            
            if (policy == null)
            {
                throw new ArgumentNullException(nameof(policy));
            }

            var rules = policy.Rules.ToList();
            if (rules.Count <= 0)
            {
                return;
            }
            
            foreach (var rule in rules)
            {
                rule.Initialize(this.crypto);
                this.validationRules.Add(rule);
            }
        }

        public ValidationResult Validate(Card card)
        {
            var errors = new List<string>();
            
            foreach (var rule in this.validationRules)
            {
                var ruleErrors = rule.CheckForErrors(this.crypto, card);
                if (ruleErrors != null)
                {
                    errors.AddRange(ruleErrors);
                }
            }
            
            return new ValidationResult { Errors = errors };
        }
    }
}