#region Copyright (C) Virgil Security Inc.
// Copyright (C) 2015-2016 Virgil Security Inc.
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

namespace Virgil.SDK
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

    using Virgil.SDK.Common;
	using Virgil.SDK.Client;
	using Virgil.SDK.Cryptography;
	using Virgil.SDK.Exceptions;

	/// <summary>
	/// A Virgil Card is the main entity of the Virgil Security services, it includes an information 
	/// about the user and his public key. The Virgil Card identifies the user by one of his available 
	/// types, such as an email, a phone number, etc.
	/// </summary>
	public sealed class VirgilCard
	{
		private readonly VirgilApiContext context;
		private readonly CardModel card;

		/// <summary>
		/// Initializes a new instance of the <see cref="VirgilCard"/> class.
		/// </summary>
		internal VirgilCard(VirgilApiContext context, CardModel card)
		{
			this.context = context;
			this.card = card;

			this.PublicKey = this.context.Crypto.ImportPublicKey(this.card.SnapshotModel.PublicKeyData);
		}

		/// <summary>
		/// Gets the unique identifier for the Virgil Card.
		/// </summary>
		public string Id => this.card.Id;

		/// <summary>
		/// Gets the value of current Virgil Card identity.
		/// </summary>
		public string Identity => this.card.SnapshotModel.Identity;

		/// <summary>
		/// Gets the identityType of current Virgil Card identity.
		/// </summary>
		public string IdentityType => this.card.SnapshotModel.IdentityType;

		/// <summary>
		/// Gets the custom <see cref="VirgilCard"/> parameters.
		/// </summary>
		public IReadOnlyDictionary<string, string> CustomFields => this.card.SnapshotModel.Data;

		/// <summary>
		/// Gets a Public key that is assigned to current <see cref="VirgilCard"/>.
		/// </summary>
		internal IPublicKey PublicKey { get; }

		/// <summary>
		/// Encrypts the specified data for current <see cref="VirgilCard"/> recipient.
		/// </summary>
		/// <param name="buffer">The data to be encrypted.</param>
		public VirgilBuffer Encrypt(VirgilBuffer buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException(nameof(buffer));
			}

			var cipherdata = this.context.Crypto.Encrypt(buffer.GetBytes(), this.PublicKey);
			return new VirgilBuffer(cipherdata);
		}

		/// <summary>
		/// Verifies the specified buffer and signature with current <see cref="VirgilCard"/> recipient.
		/// </summary>
		/// <param name="buffer">The data to be verified.</param>
		/// <param name="signature">The signature used to verify the data integrity.</param>
		public bool Verify(VirgilBuffer buffer, VirgilBuffer signature)
		{
			if (buffer == null)
				throw new ArgumentNullException(nameof(buffer));

			if (signature == null)
				throw new ArgumentNullException(nameof(signature));

			var isValid = this.context.Crypto.Verify(
				buffer.GetBytes(), signature.GetBytes(), this.PublicKey);

			return isValid;
		}

        /// <summary>
        /// Exports a current <see cref="VirgilCard"/> instance to it's binary representation.
        /// </summary>
        /// <returns>A new instance of <see cref="VirgilBuffer"/> with exported Card.</returns>
        public string Export() 
		{
			var serializedCard = JsonSerializer.Serialize(this.card);
			return VirgilBuffer.From(serializedCard).ToString(StringEncoding.Base64);
		}

        /// <summary>
        /// Initiates an identity verification process for current Card indentity type. It is only working for 
        /// Global identity types like Email.
        /// </summary>
        /// <returns>An instance of <see cref="IdentityVerificationAttempt"/> that contains 
        /// information about operation etc...</returns>
        public async Task<IdentityVerificationAttempt> CheckIdentityAsync(IdentityVerificationOptions options = null)
	    {
            var actionId = await this.context.Client
                .VerifyIdentityAsync(this.Identity, this.IdentityType, options?.ExtraFields)
                .ConfigureAwait(false); 

            var attempt = new IdentityVerificationAttempt(this.context)
            {
                ActionId = actionId,
                TimeToLive = options?.TimeToLive ?? TimeSpan.FromSeconds(3600),
                CountToLive = options?.CountToLive ?? 1,
                IdentityType = this.IdentityType,
                Identity = this.Identity
            };

            return attempt;
        }
        
        /// <summary>
        /// Publishes a current <see cref="VirgilCard"/> to the Virgil Security services. 
        /// </summary>
        internal async Task PublishAsync()
	    {
            var publishCardRequest = new PublishCardRequest(this.card.Snapshot, this.card.Meta.Signatures);

            var appId = this.context.Credentials.GetAppId();
            var appKey = this.context.Credentials.GetAppKey(this.context.Crypto);

            var requestSigner = new RequestSigner(this.context.Crypto);
            requestSigner.AuthoritySign(publishCardRequest, appId, appKey);

            var updatedModel = await this.context.Client
                .PublishCardAsync(publishCardRequest).ConfigureAwait(false);
                
            this.card.Meta = updatedModel.Meta;
        }
       
	    /// <summary>
        /// Publishes a current <see cref="VirgilCard"/> to the Virgil Security services into global scope.
        /// </summary>
        internal async Task PublishAsGlobalAsync(IdentityValidationToken identityToken)
	    {
	        if (identityToken == null)
                throw new ArgumentNullException(nameof(identityToken));

            if (this.card.SnapshotModel.Scope != CardScope.Global)  
                throw new NotSupportedException();
           
            var publishCardRequest = new PublishCardRequest(this.card.Snapshot, this.card.Meta.Signatures);

            var updatedModel = await this.context.Client
                .PublishGlobalCardAsync(publishCardRequest, identityToken.Value).ConfigureAwait(false);

	        this.card.Meta = updatedModel.Meta;
	    }
	}
}