#region Copyright (C) Virgil Security Inc.
// Copyright (C) 2015-2017 Virgil Security Inc.
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
    using Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Virgil.SDK.Client;
    using Virgil.SDK.Common;

    /// <summary>
    /// The <see cref="CardsManager"/> class provides a list of methods to manage the <see cref="VirgilCard"/> entities.
    /// </summary>
    internal class CardsManager : ICardsManager
    {
        private readonly VirgilApiContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardsManager"/> class.
        /// </summary>
        public CardsManager(VirgilApiContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Creates a new <see cref="VirgilCard"/> that is representing user's Public key and information 
        /// about identity. This card has to be published to the Virgil's services.
        /// </summary>
        /// <param name="identity">The user's identity.</param>
        /// <param name="identityType">Type of the identity.</param>
        /// <param name="ownerKey">The owner's <see cref="VirgilKey"/>.</param>
        /// <param name="customFields">The custom fields (optional).</param>
        /// <returns>A new instance of <see cref="VirgilCard"/> class, that is unpublished and 
        /// representing user's Public key.</returns>
        public VirgilCard Create(string identity, VirgilKey ownerKey,
            string identityType = "unknown",
            Dictionary<string, string> customFields = null)
        {
            var cardModel = this.BuildCardModel(identity, identityType, customFields,
                CardScope.Application, ownerKey);

            return new VirgilCard(this.context, cardModel);
        }

        /// <summary>
        /// Creates a new global <see cref="VirgilCard"/> that is representing user's 
        /// Public key and information about identity. 
        /// </summary>
        /// <param name="identity">The user's identity value.</param>
        /// <param name="identityType">Type of the identity.</param>
        /// <param name="ownerKey">The owner's <see cref="VirgilKey"/>.</param>
        /// <param name="customFields">The custom fields (optional).</param>
        /// <returns>A new instance of <see cref="VirgilCard"/> class, that is representing user's Public key.</returns>
        public VirgilCard CreateGlobal(string identity, IdentityType identityType, VirgilKey ownerKey,
            Dictionary<string, string> customFields = null)
        {
            var identityTypeString = Enum.GetName(typeof(IdentityType), identityType)?.ToLower();

            var cardModel = this.BuildCardModel(identity, identityTypeString, customFields,
                CardScope.Global, ownerKey);

            return new VirgilCard(this.context, cardModel);
        }

        /// <summary>
        /// Finds a <see cref="VirgilCard"/>s by specified identities in application scope.
        /// </summary>
        /// <param name="identities">The list of identities.</param>
        /// <returns>A collection of found <see cref="VirgilCard"/>s.</returns>
        public async Task<IEnumerable<VirgilCard>> FindAsync(params string[] identities)
        {
            if (identities == null || identities.Length == 0)
                throw new ArgumentNullException(nameof(identities));

            return await this.FindAsync(null, identities);
        }

        /// <summary>
        /// Finds <see cref="VirgilCard"/>s by specified identities and type in application scope.
        /// </summary>
        /// <param name="identityType">Type of identity</param>
        /// <param name="identities">The list of sought identities</param>
        /// <returns>A new collection with found <see cref="VirgilCard"/>s.</returns>
		public async Task<IEnumerable<VirgilCard>> FindAsync(string identityType, IEnumerable<string> identities)
        {
            var identityList = identities as IList<string> ?? identities.ToList();

            if (identities == null || !identityList.Any())
                throw new ArgumentNullException(nameof(identities));

            var criteria = new SearchCriteria
            {
                Identities = identityList,
                IdentityType = identityType,
                Scope = CardScope.Application
            };

            var cards = await this.SearchByCriteriaAsync(criteria).ConfigureAwait(false);
            return cards;
        }

        /// <summary>
        /// Finds <see cref="VirgilCard"/>s by specified identities and type in global scope.
        /// </summary>
        /// <param name="identities">The list of sought identities</param>
        /// <returns>A new collection with found <see cref="VirgilCard"/>s.</returns>
        public async Task<IEnumerable<VirgilCard>> FindGlobalAsync(params string[] identities)
        {
            var identityList = identities as IList<string> ?? identities.ToList();

            if (identities == null || !identityList.Any())
                throw new ArgumentNullException(nameof(identities));

            var criteria = new SearchCriteria
            {
                Identities = identityList,
                Scope = CardScope.Global
            };

            var cards = await this.SearchByCriteriaAsync(criteria).ConfigureAwait(false);
            return cards;
        }

        /// <summary>
        /// Finds <see cref="VirgilCard"/>s by specified identities and type in global scope.
        /// </summary>
        /// <param name="identityType">Type of identity</param>
        /// <param name="identities">The list of sought identities</param>
        /// <returns>A new collection with found <see cref="VirgilCard"/>s.</returns>
        public async Task<IEnumerable<VirgilCard>> FindGlobalAsync(IdentityType identityType, params string[] identities)
        {
            var identityList = identities as IList<string> ?? identities.ToList();

            if (identities == null || !identityList.Any())
                throw new ArgumentNullException(nameof(identities));

            var criteria = new SearchCriteria
            {
                Identities = identityList,
                IdentityType = Enum.GetName(typeof(IdentityType), identityType)?.ToLower(),
                Scope = CardScope.Global
            };

            var cards = await this.SearchByCriteriaAsync(criteria).ConfigureAwait(false);
            return cards;
        }

        /// <summary>
        /// Imports a <see cref="VirgilCard"/> from specified buffer.
        /// </summary>
        /// <param name="exportedCard">A Card in string representation.</param>
        /// <returns>An instance of <see cref="VirgilCard"/>.</returns>
        public VirgilCard Import(string exportedCard)
        {
            var bufferCard = VirgilBuffer.From(exportedCard, StringEncoding.Base64);
            var importedCardModel = JsonSerializer.Deserialize<CardModel>(bufferCard.ToString());

            return new VirgilCard(this.context, importedCardModel);
        }

        /// <summary>
        /// Publishes a <see cref="VirgilCard"/> into global Virgil Services scope.
        /// </summary>
        /// <param name="card">The Card to be published.</param>
        /// <param name="token">The identity validation token.</param>
        public Task PublishGlobalAsync(VirgilCard card, IdentityValidationToken token)
        {
            return card.PublishAsGlobalAsync(token);
        }

        /// <summary>
        /// Publishes a <see cref="VirgilCard"/> into application Virgil Services scope.
        /// </summary>
        /// <param name="card">The Card to be published.</param>
        public Task PublishAsync(VirgilCard card)
        {
            return card.PublishAsync();
        }

        /// <summary>
        /// Revokes a <see cref="VirgilCard"/> from Virgil Services. 
        /// </summary>
        /// <param name="card">The card to be revoked.</param>
        public async Task RevokeAsync(VirgilCard card)
        {
            if ((this.context == null) || (this.context.Credentials == null) ||
                (this.context.Credentials.GetAppId() == null) ||
                (this.context.Credentials.GetAppKey(context.Crypto) == null))
            {
                throw new AppCredentialsException();
            }
            var revokeRequest = new RevokeCardRequest(card.Id, RevocationReason.Unspecified);

            var appId = this.context.Credentials.GetAppId();
            var appKey = this.context.Credentials.GetAppKey(this.context.Crypto);


            var fingerprint = this.context.Crypto.CalculateFingerprint(revokeRequest.Snapshot);
            var signature = this.context.Crypto.Sign(fingerprint.GetValue(), appKey);

            revokeRequest.AppendSignature(appId, signature);
            
            /* to_ask
            var requestSigner = new RequestSigner(this.context.Crypto);
            requestSigner.AuthoritySign(revokeRequest, appId, appKey); */

            await this.context.Client.RevokeCardAsync(revokeRequest);
        }
        
        /// <summary>
        /// Revokes a global <see cref="VirgilCard"/> from Virgil Security services.
        /// </summary>
        /// <param name="card">The Card to be revoked.</param>
        /// <param name="key">The Key associated with the revoking Card.</param>
        /// <param name="identityToken">The identity token.</param>
        public async Task RevokeGlobalAsync(VirgilCard card, VirgilKey key, IdentityValidationToken identityToken)
        {
            var revokeRequest = new RevokeGlobalCardRequest(card.Id, RevocationReason.Unspecified, identityToken.Value);

            var fingerprint = this.context.Crypto.CalculateFingerprint(revokeRequest.Snapshot);
            var signature = key.Sign(fingerprint.GetValue());

            revokeRequest.AppendSignature(card.Id, signature.GetBytes());

            /* to_ask
            var requestSigner = new RequestSigner(this.context.Crypto);
            requestSigner.AuthoritySign(revokeRequest, card.Id, key.PrivateKey);
            */

            await this.context.Client.RevokeGlobalCardAsync(revokeRequest);
        }

        /// <summary>
        /// Gets a <see cref="VirgilCard"/> from Virgil Security services by specified Card ID.
        /// </summary>
        /// <param name="cardId">is a unique string that identifies the Card 
        /// within Virgil Security services.</param>
        /// <returns>an instance of <see cref="VirgilCard"/> class.</returns>
        public async Task<VirgilCard> GetAsync(string cardId)
        {
            var cardModel = await this.context.Client.GetCardAsync(cardId).ConfigureAwait(false);
            var card = new VirgilCard(this.context, cardModel);

            return card;
        }

        private async Task<IList<VirgilCard>> SearchByCriteriaAsync(SearchCriteria criteria)
        {
            var cardModels = await this.context.Client.SearchCardsAsync(criteria).ConfigureAwait(false);
            return cardModels.Select(model => new VirgilCard(this.context, model)).ToList();
        }

        private CardModel BuildCardModel
        (
            string identity,
            string identityType,
            Dictionary<string, string> customFields,
            CardScope scope,
            VirgilKey ownerKey
        )
        {
            var cardSnapshotModel = new PublishCardSnapshotModel
            {
                Identity = identity,
                IdentityType = identityType,
                Info = new CardInfoModel
                {
                    DeviceName = this.context.DeviceManager.GetDeviceName(),
                    Device = this.context.DeviceManager.GetSystemName()
                },
                PublicKeyData = ownerKey.ExportPublicKey().GetBytes(),
                Scope = scope,
                Data = customFields
            };

            var snapshot = new Snapshotter().Capture(cardSnapshotModel);

            var snapshotFingerprint = this.context.Crypto.CalculateFingerprint(snapshot);
            var cardId = snapshotFingerprint.ToHEX();
            var selfSignature = ownerKey.Sign(VirgilBuffer.From(snapshotFingerprint.GetValue()));

            var signatures = new Dictionary<string, byte[]>
            {
                [cardId] = selfSignature.GetBytes()
            };

            var cardModel = new CardModel(cardSnapshotModel)
            {
                Id = cardId,
                Snapshot = snapshot,
                Meta = new CardMetaModel
                {
                    Signatures = signatures
                }
            };

            return cardModel;
        }
    }
}