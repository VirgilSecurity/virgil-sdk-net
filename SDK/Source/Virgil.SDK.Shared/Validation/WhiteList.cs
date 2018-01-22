using System;
using System.Collections.Generic;
using System.Text;

namespace Virgil.SDK.Validation
{
    public class WhiteList
    {
        private List<VerifierCredentials> verifiersCredentials;

        public WhiteList()
        {
            verifiersCredentials = new List<VerifierCredentials>();
        }

        public IEnumerable<VerifierCredentials> VerifiersCredentials
        {
            get => this.verifiersCredentials;
            set
            {
                this.verifiersCredentials.Clear();

                if (value != null)
                {
                    this.verifiersCredentials.AddRange(value);
                }
            }
        }
    }
}
