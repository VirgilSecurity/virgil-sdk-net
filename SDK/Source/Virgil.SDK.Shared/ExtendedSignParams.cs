using System;
using System.Collections.Generic;
using System.Text;

namespace Virgil.SDK
{
    public class ExtendedSignParams : SignParams
    {
        /// <summary>
        /// The signer's card ID.
        /// </summary>
        public string SignerId { get; set; }

        /// <summary>
        /// The sign's type.
        /// </summary>
        public SignerType SignerType { get; set; }

    }
}
