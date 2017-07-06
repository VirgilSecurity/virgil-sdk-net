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
    using System.Linq;
    using System.Threading.Tasks;

    using Virgil.SDK.Common;
    using Virgil.SDK.Client;
    using Virgil.SDK.Cryptography;
    using Exceptions;

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
        /// <example>
        ///     <code>
        ///         // search for Virgil Cards
        ///         var aliceCards = await virgil.Cards.FindAsync("alice");
        ///
        ///         var fileBuf = VirgilBuffer.FromFile("FILE_NAME_HERE");
        ///
        ///         // encrypt the buffer using found Virgil Cards
        ///         var cipherFileBuf = aliceCards.Encrypt(fileBuf);
        ///     </code>
        /// </example>
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
        /// Exports a current <see cref="VirgilCard"/> instance into base64 encoded string.
        /// </summary>
        /// <returns>A string that represents a <see cref="VirgilCard"/>.</returns>
        /// <example>
        ///     <code>
        ///         // export a Virgil Card to string
        ///         var exportedAliceCard = aliceCard.Export();
        ///     </code>
        /// </example>
        /// How to get aliceCard <see cref="ICardsManager.Create(string, VirgilKey, string, Dictionary{string, string})"/>
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
        /// Find the usage at the example <see cref="PublishAsGlobalAsync(IdentityValidationToken identityToken)"/>
        public async Task<IdentityVerificationAttempt> CheckIdentityAsync(IdentityTokenOptions options = null)
	    {
            var verifyResult = await this.context.IdentityClient
                .VerifyEmailAsync(this.Identity, options?.ExtraFields)
                .ConfigureAwait(false); 

            var attempt = new IdentityVerificationAttempt(this.context)
            {
                ActionId = verifyResult.ActionId,
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
        /// <example>
        ///     <code>
        ///         // import a Virgil Card from string
        ///         var importedCard = virgil.Cards.Import(exportedCard);
        ///
        ///         // publish a Virgil Card
        ///         await virgil.Cards.PublishAsync(importedCard);
        ///     </code>
        /// </example>
        /// How to get exportedCard <see cref="VirgilCard.Export"/>
        internal async Task PublishAsync()
	    {
            if ((this.context == null) || (this.context.Credentials == null) || 
                (this.context.Credentials.GetAppId() == null) || 
                (this.context.Credentials.GetAppKey(context.Crypto) == null))
            {
                throw new AppCredentialsException();
            }
            var publishCardRequest = new PublishCardRequest(this.card.Snapshot, this.card.Meta.Signatures);

            var appId = this.context.Credentials.GetAppId();
            var appKey = this.context.Credentials.GetAppKey(this.context.Crypto);

            var requestSigner = new RequestSigner(this.context.Crypto);
            requestSigner.AuthoritySign(publishCardRequest, appId, appKey);

            var updatedModel = await this.context.CardsClient
                .CreateUserCardAsync(publishCardRequest).ConfigureAwait(false);
                
            this.card.Meta = updatedModel.Meta;
        }

        /// <summary>
        /// Publishes a current <see cref="VirgilCard"/> to the Virgil Security services into global scope.
        /// </summary>
        /// <example>
        ///     <code>
        ///         // generate a Virgil Key
        ///         var aliceKey = virgil.Keys.Generate();
        /// 
        ///         // save the Virgil Key into storage
        ///         aliceKey.Save("[KEY_NAME]", "[KEY_PASSWORD]");
        ///
        ///         // create a Global Virgil Card 
        ///         var aliceCard = virgil.Cards.CreateGlobal(
        ///             identity: "alice@virgilsecurity.com",
        ///             identityType: IdentityType.Email,
        ///             ownerKey: aliceKey
        ///         );
        ///         // initiate identity verification process
        ///         var attempt = await aliceCard.CheckIdentityAsync();
        /// 
        ///         // confirm an identity and grab the validation token
        ///         var token = await attempt.ConfirmAsync(new EmailConfirmation("[CONFIRMATION_CODE]"));
        ///
        ///         // publish the Virgil Card
        ///         await aliceCard.PublishAsGlobalAsync(token);
        ///     </code>
        /// </example>
        internal async Task PublishAsGlobalAsync(IdentityValidationToken identityToken)
	    {
	        if (identityToken == null)
                throw new ArgumentNullException(nameof(identityToken));

            if (this.card.SnapshotModel.Scope != CardScope.Global)  
                throw new NotSupportedException();
           
            var publishCardRequest = new PublishGlobalCardRequest(this.card.Snapshot,
                identityToken.Value, this.card.Meta.Signatures);
            
            var updatedModel = await this.context.CardsClient
                .CreateGlobalCardAsync(publishCardRequest).ConfigureAwait(false);

	        this.card.Meta = updatedModel.Meta;
	    }

        /// <summary>
        /// Encrypts a buffer data for list of <paramref name="recipients"/> Cards.
        /// </summary>
        /// <returns>A new <see cref="VirgilBuffer"/> with encrypted data.</returns>
        internal VirgilBuffer Encrypt(VirgilBuffer buffer, IEnumerable<VirgilCard> recipients)
	    {
            var publicKeyRecipients = new List<IPublicKey>();
            var virgilCards = recipients?.ToList();

            if (virgilCards != null)
            {
                publicKeyRecipients.AddRange(virgilCards.Select(r => r.PublicKey));
            }
            
            var cipherdata = this.context.Crypto.Encrypt(buffer.GetBytes(), publicKeyRecipients.ToArray());
            return new VirgilBuffer(cipherdata);
	    }

        /// <summary>
        /// To check if current Virgil Card was generated for <paramref name="virgilKey"/>.
        /// </summary>
        /// <param name="virgilKey">An instance of <see cref="VirgilKey"/>.</param>
        public bool IsPairFor(VirgilKey virgilKey)
        {
            return this.PublicKey.Get().Value.SequenceEqual(virgilKey.ExportPublicKey().GetBytes());
        }
    }
}