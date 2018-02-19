#region Copyright (C) Virgil Security Inc.
// Copyright (C) 2015-2018 Virgil Security Inc.
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

using Virgil.Crypto;
using Virgil.SDK.Signer;
using System.Collections.Generic;
using System.Linq;
using Virgil.CryptoAPI;
using Virgil.SDK.Common;

namespace Virgil.SDK.Verification
{
    /// <summary>
    ///  The <see cref="VirgilCardVerifier"/> represents card verification process.
    /// </summary>
    public class VirgilCardVerifier : ICardVerifier
    {
        private List<WhiteList> whiteLists;
        private readonly ICardCrypto cardCrypto;
        private string VirgilPublicKeyBase64 = "MCowBQYDK2VwAyEAr0rjTWlCLJ8q9em0og33grHEh/3vmqp0IewosUaVnQg=";

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilCardVerifier"/> class.
        /// </summary>
        /// <param name="crypto">The instance of <see cref="ICardCrypto"/> that is 
        /// used for signature verification.</param>
        public VirgilCardVerifier(ICardCrypto crypto)
        {
            this.whiteLists = new List<WhiteList>();
            this.cardCrypto = crypto;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilCardVerifier"/> class.
        /// </summary>
        public VirgilCardVerifier() : this(new VirgilCardCrypto())
        {
        }

        /// <summary>
        /// Verify self signature if true.
        /// </summary>
        public bool VerifySelfSignature { get; set; }

        /// <summary>
        /// Verify virgil signature if true.
        /// </summary>
        public bool VerifyVirgilSignature { get; set; }

        /// <summary>
        /// List with collections of <see cref="VerifierCredentials"/> that are used for verification.
        /// </summary>
        /// <remarks>Card is verified successfully only if 
        /// it contains verified signature by at least one virefier from each WhiteList.</remarks>
        public IEnumerable<WhiteList> WhiteLists
        {
            get => this.whiteLists;
            set
            {
                this.whiteLists.Clear();

                if (value != null)
                {
                    this.whiteLists.AddRange(value);
                }
            }
        }
        /// <summary>
        /// To verify the specified card.
        /// </summary>
        /// <param name="card">The instance of <see cref="Card"/> to verify.</param>
        /// <returns>True is card is verified according to set rules, otherwise False.</returns>
        /// <remarks>To set up rule for verification of self signature use <see cref="VerifySelfSignature"/>.</remarks>  
        /// <remarks>To set up rule for verification of virgil service signature use <see cref="VerifyVirgilSignature"/>.</remarks>    
        /// <remarks>To set up White lists use <see cref="WhiteLists"/>.</remarks>    
        public bool VerifyCard(Card card)
        {
            if (this.VerifySelfSignature &&
                !ValidateSignerSignature(card, card.PublicKey, ModelSigner.SelfSigner))
            {
                return false;
            }

            if (this.VerifyVirgilSignature &&
                !ValidateSignerSignature(
                    card,
                    this.GetPublicKey(VirgilPublicKeyBase64),
                    ModelSigner.VirgilSigner))
            {
                return false;
            }

            if (!this.whiteLists.Any())
            {
                return true;
            }

            // select a signers from card signatures. 
            var signers = card.Signatures.Select(x => x.Signer);

            var verifiersCredentialsLists = whiteLists
                .Select(s => s.VerifiersCredentials);

            foreach (var verifiersCredentials in verifiersCredentialsLists)
            {
                // if whitelist doesn't have credentials then 
                //this is to be regarded as a violation of the policy.
                if (verifiersCredentials == null || !verifiersCredentials.Any())
                {
                    return false;
                }
                var intersectedCreds = verifiersCredentials.Where(x => signers.Contains(x.Signer));

                // if card doesn't contain signature from AT LEAST one verifier from a WhiteList then
                //this is to be regarded as a violation of the policy (at least one).
                if (!intersectedCreds.Any())
                {
                    return false;
                }
                foreach (var intersectedCred in intersectedCreds)
                {
                    var signerPublicKey = this.GetPublicKey(intersectedCred.PublicKeyBase64);
                    if (ValidateSignerSignature(card, signerPublicKey, intersectedCred.Signer))
                    {
                        break;
                    }
                    if (intersectedCred == intersectedCreds.Last())
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private IPublicKey GetPublicKey(string signerPublicKeyBase64)
        {
            var publicKeyBytes = Bytes.FromString(signerPublicKeyBase64, StringEncoding.BASE64);
            var publicKey = cardCrypto.ImportPublicKey(publicKeyBytes);

            return publicKey;
        }


        private bool ValidateSignerSignature(Card card,
            IPublicKey signerPublicKey, string signerType)
        {
            var signature = card.Signatures.SingleOrDefault(
                s => s.Signer == signerType);
            // validate verifier's signature 
            
            if (signature != null)
            {
                var extendedSnapshot = signature.Snapshot != null
                    ? Bytes.Combine(card.ContentSnapshot, signature.Snapshot)
                    : card.ContentSnapshot;

                if (cardCrypto.VerifySignature(
                    signature.Signature,
                    extendedSnapshot,
                    signerPublicKey))
                {
                    return true;
                }
            }
        
        return false;
    }

        internal void ChangeServiceCreds(string publicKey)
        {
            this.VirgilPublicKeyBase64 = publicKey;
        }
    }
}