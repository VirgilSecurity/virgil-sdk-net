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

    using Virgil.SDK.Client;

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
        /// <returns>A new instance of <see cref="VirgilCard"/> class, that is representing user's Public key.</returns>
        public VirgilCard Create(string identity, string identityType, VirgilKey ownerKey,
            Dictionary<string, string> customFields = null)
        {
            var cardModel = this.BuildCardModel(identity, identityType, customFields, 
                CardScope.Application, ownerKey);
            
            return new VirgilCard(this.context, cardModel);
        }

        /// <summary>
        /// Creates a new global <see cref="VirgilCard"/> that is representing user's Public key and information 
        /// about identity. 
        /// </summary>
        /// <param name="identity">The user's identity.</param>
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

        public async Task<IList<VirgilCard>> FindAsync(params string[] identities)
        {
            return await this.FindAsync(null, identities);
        }

        public async Task<IList<VirgilCard>> FindAsync(string identityType, IEnumerable<string> identities)
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

            return await this.SearchByCriteriaAsync(criteria).ConfigureAwait(false);
        }

        public async Task<IList<VirgilCard>> FindGlobalAsync(IdentityType identityType, params string[] identities)
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

            return await this.SearchByCriteriaAsync(criteria).ConfigureAwait(false);
        }

        public async Task PublishAsync(VirgilCard card)
        {
            throw new NotImplementedException();
        }
        
        public async Task PublishGlobalAsync(VirgilCard card)
        {
            throw new NotImplementedException();
        }

        public async Task RevokeAsync(string cardId, RevocationReason reason)
        {
            throw new NotImplementedException();
        }

        public async Task RevokeGlobalAsync(VirgilCard card)
        {
            throw new NotImplementedException();
        }

        public VirgilCard Import(string stringifiedCard)
        {
            throw new NotImplementedException();
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
            var snapshotModel = new CardSnapshotModel
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

            var request = new PublishCardRequest(snapshotModel);
            var cardId = this.context.Crypto.CalculateFingerprint(request.Snapshot).ToHEX();

            request.AppendSignature(cardId, ownerKey.Sign(cardId).GetBytes());

            var cardModel = new CardModel
            {
                Id = cardId,
                SnapshotModel = snapshotModel,
                Snapshot = request.Snapshot,
                Meta = new CardMetaModel
                {
                    Signatures = request.Signatures.ToDictionary(it => it.Key, it => it.Value)
                }
            };

            return cardModel;
        }
    }
}