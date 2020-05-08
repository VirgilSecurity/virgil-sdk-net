#region Copyright (C) Virgil Security Inc.
// Copyright (C) 2015-2019 Virgil Security Inc.
// 
// Lead Maintainer: Virgil Security Inc. <support@virgilsecurity.com>
// 
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions 
// are met:
// 
//   (1) Redistributions of source code must retain the above copyright
//   notice, this list of conditions and the following disclaimer.
//   
//   (2) Redistributions in binary form must reproduce the above copyright
//   notice, this list of conditions and the following disclaimer in
//   the documentation and/or other materials provided with the
//   distribution.
//   
//   (3) Neither the name of the copyright holder nor the names of its
//   contributors may be used to endorse or promote products derived 
//   from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE AUTHOR ''AS IS'' AND ANY EXPRESS OR
// IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT,
// INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
// HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
// STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING
// IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
// POSSIBILITY OF SUCH DAMAGE.
#endregion

namespace Virgil.SDK.Verification
{
    using System.Collections.Generic;

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
