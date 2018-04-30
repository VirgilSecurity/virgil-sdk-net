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

namespace Virgil.SDK.Signer
{
    using System;
    using System.Collections.Generic;

    using Virgil.CryptoAPI;
    using Virgil.SDK.Common;
    using Virgil.SDK.Web;

    /// <summary>
    /// The <see cref="ModelSigner"/> class provides signing operation for <see cref="RawSignedModel"/>.
    /// </summary>
    public class ModelSigner
    {
        public readonly ICardCrypto Crypto;
        public const string SelfSigner = "self";
        public const string VirgilSigner = "virgil";

        public ModelSigner(ICardCrypto crypto)
        {
            this.Crypto = crypto;
        }

        /// <summary>
        /// Adds owner's signature to the specified <see cref="RawSignedModel"/> using specified signer 
        /// parameters included private key and additional raw bytes.
        /// </summary>
        /// <param name="model"> the instance of <see cref="RawSignedModel"/> to be signed.</param>
        /// <param name="signerPrivateKey"> the instance of <see cref="IPrivateKey"/> to sign with.</param>
        /// <param name="signatureSnapshot"> Some additional raw bytes to be signed with model.</param>
        public void SelfSign(RawSignedModel model, IPrivateKey signerPrivateKey, byte[] signatureSnapshot = null)
        {
            ValidateSignParams(model, signerPrivateKey);

            Sign(model,
                new SignParams()
                {
                    SignerPrivateKey = signerPrivateKey,
                    Signer = SelfSigner
                },
                signatureSnapshot
                );
        }


        /// /// <summary>
        /// Adds owner's signature to the specified <see cref="RawSignedModel"/> using specified signer 
        /// parameters included private key and dictionary with additional data.
        /// </summary>
        /// <param name="model"> the instance of <see cref="RawSignedModel"/> to be signed.</param>
        /// <param name="signerPrivateKey"> the instance of <see cref="IPrivateKey"/> to sign with.</param>
        /// <param name="extraFields"> Dictionary with additional data to be signed with model.</param>
        public void SelfSign(RawSignedModel model, IPrivateKey signerPrivateKey, Dictionary<string, string> extraFields)
        {
            SelfSign(model, signerPrivateKey,
                (extraFields != null) ? SnapshotUtils.TakeSnapshot(extraFields) : null
                );
        }

        private static void ValidateSignParams(RawSignedModel model, IPrivateKey signerPrivateKey)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }


            if (signerPrivateKey == null)
            {
                throw new ArgumentException($"{nameof(signerPrivateKey)} property is mandatory");
            }
        }

        /// <summary>
        /// Adds signature to the specified <see cref="RawSignedModel"/> using specified signer 
        /// parameters included private key, signer type and additional raw bytes.
        /// </summary>
        /// <param name="model"> the instance of <see cref="RawSignedModel"/> to be signed.</param>
        /// <param name="@params"> the instance of <see cref="SignParams"/> to sign with.</param>
        /// <param name="signatureSnapshot"> Some additional raw bytes to be signed with model.</param>
        public void Sign(RawSignedModel model, SignParams @params, byte[] signatureSnapshot = null)
        {
            ValidateExtendedSignParams(model, @params);
            ThrowExceptionIfSignatureExists(@params, model.Signatures);

            var extendedSnapshot = signatureSnapshot != null ?
                Bytes.Combine(model.ContentSnapshot, signatureSnapshot)
                : model.ContentSnapshot;

            var signatureBytes = Crypto.GenerateSignature(extendedSnapshot, @params.SignerPrivateKey);

            var signature = new RawSignature
            {
                Signer = @params.Signer,
                Signature = signatureBytes,
                Snapshot = signatureSnapshot
            };
            model.Signatures.Add(signature);
        }

        private static void ThrowExceptionIfSignatureExists(SignParams @params, IList<RawSignature> signatures)
        {
            if (signatures != null &&
                ((List<RawSignature>)signatures).Exists(
                    s => s.Signer == @params.Signer))
            {
                throw new VirgilException("The model already has this signature.");
            }
        }

        /// <summary>
        /// Adds signature to the specified <see cref="RawSignedModel"/> using specified signer 
        /// parameters included private key, signer type and dictionary with additional data.
        /// </summary>
        /// <param name="model"> the instance of <see cref="RawSignedModel"/> to be signed.</param>
        /// <param name="@params"> the instance of <see cref="SignParams"/> to sign with.</param>
        /// <param name="extraFields"> Dictionary with additional data to be signed with model.</param>
        public void Sign(RawSignedModel model, SignParams @params, Dictionary<string, string> ExtraFields)
        {
            Sign(model, @params,
                (ExtraFields != null) ? SnapshotUtils.TakeSnapshot(ExtraFields) : null
            );
        }

        private static void ValidateExtendedSignParams(RawSignedModel model, SignParams @params)
        {
            ValidateSignParams(model, @params.SignerPrivateKey);

            if (@params.Signer == null)
            {
                throw new ArgumentException($"{nameof(@params.Signer)} property is mandatory");
            }
        }
    }
}
