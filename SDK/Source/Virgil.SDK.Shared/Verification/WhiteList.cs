using System.Collections.Generic;

namespace Virgil.SDK.Verification
{
    /// <summary>
    ///  The <see cref="Whitelist"/> implements a collection of <see cref="VerifierCredentials"/> 
    /// that is used for card verification in <see cref="VirgilCardVerifier"/>.
    /// </summary>
    public class Whitelist
    {
        private List<VerifierCredentials> verifiersCredentials;

        /// <summary>
        /// Initializes a new instance of the <see cref="Whitelist"/> class.
        /// </summary>
        public Whitelist()
        {
            verifiersCredentials = new List<VerifierCredentials>();
        }

        /// <summary>
        /// Gets or sets the collection of <see cref="VerifierCredentials"/> 
        /// that is used for card verification in <see cref="VirgilCardVerifier"/>.
        /// </summary>
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
