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

namespace Virgil.SDK.Client
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Exceptions;
    using Shared.Client.TransferObjects;
    using Virgil.SDK.Common;
    using Virgil.SDK.Client.Http;

    public sealed class VirgilClient
    {
        private readonly VirgilClientParams parameters;

        private readonly Lazy<IConnection> cardsConnection;
        private readonly Lazy<IConnection> readCardsConnection;
        private readonly Lazy<IConnection> identityConnection;

        private IConnection CardsConnection => this.cardsConnection.Value;
        private IConnection ReadCardsConnection => this.readCardsConnection.Value;
        private IConnection IdentityConnection => this.identityConnection.Value;

        private ICardValidator cardValidator;

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilClient"/> class.
        /// </summary>  
        public VirgilClient(string accessToken) : this(new VirgilClientParams(accessToken))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilClient"/> class.
        /// </summary>
        public VirgilClient(VirgilClientParams parameters)
        {
            this.parameters = parameters;

            this.cardsConnection = new Lazy<IConnection>(this.InitializeCardsConnection);
            this.readCardsConnection = new Lazy<IConnection>(this.InitializeReadCardsConnection);
            this.identityConnection = new Lazy<IConnection>(this.InitializeIdentityConnection);
        }

        /// <summary>
        /// Sets the card validator.
        /// </summary>
        public void SetCardValidator(ICardValidator validator)
        {
            if (validator == null)
                throw new ArgumentNullException(nameof(validator));

            this.cardValidator = validator;
        }

        public async Task<IEnumerable<CardModel>> SearchCardsAsync(SearchCriteria criteria)
        {
            if (criteria == null)
                throw new ArgumentNullException(nameof(criteria));

            if (criteria.Identities == null || !criteria.Identities.Any())
                throw new ArgumentNullException(nameof(criteria));
            
            var body = new Dictionary<string, object>
            {
                ["identities"] = criteria.Identities
            };

            if (!string.IsNullOrWhiteSpace(criteria.IdentityType))
            {
                body["identity_type"] = criteria.IdentityType;
            }

            if (criteria.Scope == CardScope.Global)
            {
                body["scope"] = "global";
            }

            var request = Request.Create(RequestMethod.Post)
                .WithEndpoint("/v4/card/actions/search")
                .WithBody(body);

            var response = await this.ReadCardsConnection.Send(request).ConfigureAwait(false);
            var cards = response.Parse<IEnumerable<SignedResponseModel>>()
                .Select(ResponseToCard)
                .ToList();

            if (this.cardValidator != null)
            {
                this.ValidateCards(cards);
            }

            return cards;
        }

        public async Task<CardModel> PublishCardAsync(PublishCardRequest request)
        {
            var postRequest = Request.Create(RequestMethod.Post)
                .WithEndpoint("/v4/card")
                .WithBody(request.GetRequestModel());

            var response = await this.CardsConnection.Send(postRequest).ConfigureAwait(false);
            var card = ResponseToCard(response.Parse<SignedResponseModel>());

            if (this.cardValidator != null)
            {
                this.ValidateCards(new []{ card });
            }

            return card;
        }   

        public async Task RevokeCardAsync(RevokeCardRequest request)
        {
            var snapshotModel = request.ExtractSnapshotModel();

            var postRequest = Request.Create(RequestMethod.Delete)
                .WithEndpoint($"/v4/card/{snapshotModel.CardId}")
                .WithBody(request.GetRequestModel());

            await this.CardsConnection.Send(postRequest).ConfigureAwait(false);
        }

        public async Task<CardModel> GetCardAsync(string cardId)
        {
            var request = Request.Create(RequestMethod.Get)
                .WithEndpoint($"/v4/card/{cardId}");

            var resonse = await this.ReadCardsConnection.Send(request).ConfigureAwait(false);
            var card = ResponseToCard(resonse.Parse<SignedResponseModel>());

            if (this.cardValidator != null)
            {
                this.ValidateCards(new[] { card });
            }

            return card;
        }

        /// <summary>
        /// Sends the request for identity verification, that's will be processed depending of specified type.
        /// </summary>
        /// <param name="identity">An unique string that represents identity.</param>
        /// <param name="identityType">The type of identity.</param>
        /// <param name="options">The validation options.</param>
        /// <remarks>
        /// Use method <see cref="ConfirmIdentity(VerificationResult, string)" /> to confirm and get the indentity token.
        /// </remarks>
        public async Task<VerificationResult> VerifyIdentity(string identity, IdentityType identityType, ValidationOptions options = null)
        {
            var body = new
            {
                type = identity,
                value = identityType,  
                extra_fields = options?.ExtraFields
            };

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .WithEndpoint("v1/verify");

            var response = await this.IdentityConnection.Send(request).ConfigureAwait(false);
            var result = response.Parse<IdentityVerificationResponseModel>();

            return new VerificationResult(result.ActionId, options);
        }

        /// <summary>
        /// Confirms the identity using confirmation code, that has been generated to confirm an identity.
        /// </summary>
        /// <param name="verificationResult">The action identifier that was obtained on verification step.</param>
        /// <param name="confirmationCode">The confirmation code that was recived on email box.</param>
        public async Task<string> ConfirmIdentity(VerificationResult verificationResult, string confirmationCode)
        {
            var body = new
            {
                confirmation_code = confirmationCode,
                action_id = verificationResult.ActionId,
                token = new 
                {
                    time_to_live = verificationResult?.Options.TokenTimeToLive,
                    count_to_live = verificationResult?.Options.TokenCountToLive
                }
            };

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .WithEndpoint("v1/confirm");

            var response = await this.IdentityConnection.Send(request).ConfigureAwait(false);
            var result = response.Parse<IdentityConfirmationResponseModel>();

            return result.ValidationToken;
        }

        /// <summary>
        /// Returns true if validation token is valid.
        /// </summary>
        /// <param name="identityValue">The type of identity.</param>
        /// <param name="identityType">The identity value.</param>
        /// <param name="validationToken">The validation token.</param>
        public async Task<bool> IsIdentityValid(string identityValue, IdentityType identityType, string validationToken)
        {
            var request = Request.Create(RequestMethod.Post)
                .WithBody(new
                {
                    value = identityValue,
                    type = identityType,
                    validation_token = validationToken
                })
                .WithEndpoint("v1/validate");

            var response = await this.IdentityConnection.Send(request, true).ConfigureAwait(false);
            return response.StatusCode == 400;
        }

        #region Private Methods

        private static CardModel ResponseToCard(SignedResponseModel responseModel)
        {
            var snapshotModelJson = Encoding.UTF8.GetString(responseModel.ContentSnapshot);
            var card = JsonSerializer.Deserialize<CardModel>(snapshotModelJson);

            card.Id = responseModel.CardId;
            card.Snapshot = responseModel.ContentSnapshot;
            card.Version = responseModel.Meta.Version;
            card.Signatures = new ReadOnlyDictionary<string, byte[]>(responseModel.Meta.Signatures);

            return card;
        }

        private void ValidateCards(IEnumerable<CardModel> cards)
        {
            var foundCards = cards.ToList();

            var invalidCards = foundCards.Where(c => !this.cardValidator.Validate(c)).ToList();
            if (invalidCards.Any())
            {
                throw new CardValidationException(invalidCards);
            }
        }

        private IConnection InitializeIdentityConnection()
        {
            var baseUrl = new Uri(this.parameters.IdentityServiceAddress);
            return new IdentityServiceConnection(this.parameters.AccessToken, baseUrl);
        }

        private IConnection InitializeReadCardsConnection()
        {
            var baseUrl = new Uri(this.parameters.ReadOnlyCardsServiceAddress);
            return new CardsServiceConnection(this.parameters.AccessToken, baseUrl);
        }

        private IConnection InitializeCardsConnection()
        {
            var baseUrl = new Uri(this.parameters.CardsServiceAddress);
            return new CardsServiceConnection(this.parameters.AccessToken, baseUrl);
        }

        #endregion
    }
}