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
    using System.Linq;
    using System.Threading.Tasks;
    using Http;
    using Exceptions;

    public class CardsClient : VirgilClient
    {
        private readonly CardsClientParams parameters;

        private readonly Lazy<IConnection> cardsConnection;
        private readonly Lazy<IConnection> readCardsConnection;
        private readonly Lazy<IConnection> raConnection;

        private IConnection CardsConnection => this.cardsConnection.Value;
        private IConnection ReadCardsConnection => this.readCardsConnection.Value;
        private IConnection RAConnection => this.raConnection.Value;

        private ICardValidator cardValidator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardsClient"/> class.
        /// </summary>  
        public CardsClient() : this(new CardsClientParams())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CardsClient"/> class.
        /// </summary>  
        public CardsClient(string accessToken) : this(new CardsClientParams(accessToken))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CardsClient"/> class.
        /// </summary>
        public CardsClient(CardsClientParams parameters)
        {
            this.parameters = parameters;

            this.cardsConnection = new Lazy<IConnection>(this.InitializeCardsConnection);
            this.readCardsConnection = new Lazy<IConnection>(this.InitializeReadCardsConnection);
            this.raConnection = new Lazy<IConnection>(this.InitializeRAConnection);
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


        /// <summary> Searches cards by specified search criteria.</summary>
        /// <param name="criteria">An instance of <see cref="SearchCriteria"/> class </param>
        /// <returns> Found cards from server response.
        /// </returns>
        /// <exception cref="CardValidationException">if client has validator
        /// and cards are not valid.</exception>
        /// <example>
        ///   <code>
        ///     var client = new CardsClient("[YOUR_ACCESS_TOKEN_HERE]");
        ///     var foundCardModels = await client.SearchCardsAsync(new SearchCriteria
        ///     {
        ///         Identities = new[] { "Bob", "Alice" }
        ///     });
        ///   </code>  
        /// </example>
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

            var response = await this.SendAsync(ReadCardsConnection, request).ConfigureAwait(false);
            var cards = response.Parse<IEnumerable<CardModel>>().ToList();

            if (this.cardValidator != null)
            {
                this.ValidateCards(cards);
            }

            return cards;
        }


        /// <summary>
        /// Publishes card in Virgil Cards service.
        /// </summary>
        /// <param name="request">An instance of <see cref="PublishCardRequest"/> class</param>
        /// <returns>Card that is published to Virgil Security services</returns>
        /// <example>
        /// <code>
        ///     var crypto = new VirgilCrypto();
        ///     var client = new CardsClient("[YOUR_ACCESS_TOKEN_HERE]");
        ///     var appKey = crypto.ImportPrivateKey(
        ///         File.ReadAllBytes("[YOUR_APP_KEY_PATH_HERE]"), 
        ///         "[YOUR_APP_KEY_PASSWORD_HERE]"
        ///     );
        ///     var aliceKeys = crypto.GenerateKeys();
        ///     var exportedPublicKey = crypto.ExportPublicKey(aliceKeys.PublicKey);
        ///     var aliceIdentity = "alice";
        ///     var request = new PublishCardRequest(aliceIdentity, "unknown", exportedPublicKey);
        ///     var requestSigner = new RequestSigner(crypto);
        ///     requestSigner.SelfSign(request, aliceKeys.PrivateKey);
        ///     requestSigner.AuthoritySign(request, "[YOUR_APP_ID_HERE]", appKey);
        ///     var aliceCardModel = await client.CreateUserCardAsync(request);
        /// </code>
        /// </example>
        public async Task<CardModel> CreateUserCardAsync(PublishCardRequest request)
        {
            var postRequest = Request.Create(RequestMethod.Post)
                .WithEndpoint("/v4/card")
                .WithBody(request.GetRequestModel());

            var response = await this.SendAsync(CardsConnection, postRequest).ConfigureAwait(false);
            var cardModel = response.Parse<CardModel>();

            if (this.cardValidator != null)
            {
                this.ValidateCards(new[] { cardModel });
            }

            return cardModel;
        }



        /// <summary>Publishes Global card in Virgil cards service.</summary>
        /// <param name="request">An instance of <see cref="PublishGlobalCardRequest"/> class</param>
        /// <returns>Global card that is published to Virgil Security services.</returns>
        /// <code>
        ///     var crypto = new VirgilCrypto();
        ///     var client = new CardsClient();
        ///     var aliceKeys = crypto.GenerateKeys();
        ///     var exportedPublicKey = crypto.ExportPublicKey(aliceKeys.PublicKey);
        ///     var aliceIdentity = "alice";
        ///     var request = new PublishGlobalCardRequest(aliceIdentity, 
        ///                                                "email", 
        ///                                                exportedPublicKey, 
        ///                                                "[YOUR_VALIDATION_TOKEN]"
        ///                                                );
        ///     var requestSigner = new RequestSigner(crypto);
        ///     requestSigner.SelfSign(request, aliceKeys.PrivateKey);
        ///     var aliceGlobalCardModel = await client.CreateGlobalCardAsync(request);
        /// </code>
        public async Task<CardModel> CreateGlobalCardAsync(PublishGlobalCardRequest request)
        {
            var postRequest = Request.Create(RequestMethod.Post)
                .WithEndpoint("/v1/card")
                .WithBody(request.GetRequestModel());

            var response = await this.SendAsync(RAConnection, postRequest).ConfigureAwait(false);
            var cardModel = response.Parse<CardModel>();

            if (this.cardValidator != null)
            {
                this.ValidateCards(new[] { cardModel });
            }
            return cardModel;
        }



        /// <summary>
        /// Revoke Global card by id.
        /// </summary>
        /// <param name="request">An instance of <see cref="RevokeGlobalCardRequest"/> class
        /// that contains Global Card id and Validation Token</param>
        /// <example>
        ///     <code>
        ///         var client = new CardsClient();
        ///         var crypto = new VirgilCrypto();
        ///         var revokeRequest = new RevokeGlobalCardRequest(aliceGlobalCard.Id, 
        ///                                                         RevocationReason.Unspecified, 
        ///                                                         "[YOUR_VALIDATION_TOKEN]"
        ///                                                         );
        ///         var requestSigner = new RequestSigner(crypto);
        ///         requestSigner.AuthoritySign(revokeRequest, aliceGlobalCardModel.Id, aliceKeys.PrivateKey);  
        ///         await client.RevokeGlobalCardAsync(revokeRequest);
        ///     </code>
        /// How to get aliceGlobalCardModel and aliceKeys <see cref="PublishGlobalCardAsync(PublishGlobalCardRequest)"/>
        /// </example>
        /// 
        public async Task RevokeGlobalCardAsync(RevokeGlobalCardRequest request)
        {
            var snapshotModel = request.ExtractSnapshotModel();
            var requestModel = request.GetRequestModel();

            var postRequest = Request.Create(RequestMethod.Delete)
                .WithEndpoint($"/v1/card/{snapshotModel.CardId}")
                .WithBody(requestModel);

            await this.SendAsync(RAConnection, postRequest).ConfigureAwait(false);
        }

        /// <summary>
        /// Revoke a card from Virgil Services.
        /// </summary>
        /// <param name="request">An instance of <see cref="RevokeCardRequest"/> class that
        /// contains card id</param>
        /// <example>
        ///     <code>
        ///         var crypto = new VirgilCrypto();
        ///         var client = new CardsClient("[YOUR_ACCESS_TOKEN_HERE]");
        ///         var appKey = crypto.ImportPrivateKey(
        ///             File.ReadAllBytes("[YOUR_APP_KEY_PATH_HERE]"), 
        ///             "[YOUR_APP_KEY_PASSWORD_HERE]"
        ///         );
        ///         var requestSigner = new RequestSigner(crypto);
        ///         var revokeRequest = new RevokeCardRequest(aliceCardModel.Id, RevocationReason.Unspecified);
        ///         requestSigner.AuthoritySign(revokeRequest, "[YOUR_APP_ID_HERE]", appKey);
        ///         await client.RevokeUserCardAsync(revokeRequest);
        ///     </code>
        /// How to get aliceCardModel and aliceKeys <see cref="PublishCardAsync(PublishCardRequest)"/>
        /// </example>
		public async Task RevokeUserCardAsync(RevokeCardRequest request)
        {
            var snapshotModel = request.ExtractSnapshotModel();

            var postRequest = Request.Create(RequestMethod.Delete)
                .WithEndpoint($"/v4/card/{snapshotModel.CardId}")
                .WithBody(request.GetRequestModel());

            await this.SendAsync(CardsConnection, postRequest).ConfigureAwait(false);
        }


        /// <summary>
        /// Gets card by id.
        /// </summary>
        /// <param name="cardId">id of the card to get.</param>
        /// <returns>Found card from server response.</returns>
        /// <example> Get card model by id.
        ///     <code>
        ///         var client = new CardsClient("[YOUR_ACCESS_TOKEN_HERE]");
        ///         var card = await client.GetCardAsync("[USER_CARD_ID_HERE]");
        ///     </code>
        /// </example>
        public async Task<CardModel> GetCardAsync(string cardId)
        {
            var request = Request.Create(RequestMethod.Get)
                .WithEndpoint($"/v4/card/{cardId}");

            var resonse = await this.SendAsync(ReadCardsConnection, request).ConfigureAwait(false);
            var cardModel = resonse.Parse<CardModel>();

            if (this.cardValidator != null)
            {
                this.ValidateCards(new[] { cardModel });
            }

            return cardModel;
        }

        /// <summary>
        /// Adds a relation for the Virgil Card to Virgil cards service.
        /// </summary>
        /// <param name="request">An instance of <see cref="AddRelationRequest"/> class,
        /// that contains a trusted card snapshot.</param>
        /// <returns>Updated <see cref="CardModel"/> from server response.</returns>
        /// <exception cref="RelationException">if request doesn't have trusted 
        /// card's snapshot or doesn't have exactly 1 signature.</exception>
        /// <example> Example:
        ///  <para>Look at <see cref="PublishCardAsync(PublishCardRequest)"/> to find out 
        ///  how to publish bobCardModel and aliceCardModel.</para>
        ///     <code>
        ///         var addRelationRequest = new AddRelationRequest(bobCardModel.SnapshotModel);
        ///         requestSigner.AuthoritySign(addRelationRequest, aliceCardModel.Id, aliceKeys.PrivateKey);
        ///         var aliceCardModelWithRelation = await client.CreateCardRelationAsync(addRelationRequest);
        ///     </code>
        /// </example>
        public async Task<CardModel> CreateCardRelationAsync(AddRelationRequest request)
        {
            if (request == null || request.Snapshot.Length == 0 || request.Signatures.Count != 1)
            {
                throw new RelationException();
            }
            var cardId = request.Signatures.Keys.First();
            var postRequest = Request.Create(RequestMethod.Post)
             .WithEndpoint($"/v4/card/{cardId}/collections/relations")
             .WithBody(request.GetRequestModel());

            var response = await this.SendAsync(CardsConnection, postRequest).ConfigureAwait(false);
            var cardModel = response.Parse<CardModel>();

            if (this.cardValidator != null)
            {
                this.ValidateCards(new[] { cardModel });
            }

            return cardModel;
        }


        /// <summary>
        ///  Deletes a relation for the Virgil Card to Virgil cards service.
        /// </summary>
        /// <param name="request">An instance of <see cref="DeleteRelationRequest"/> class,
        /// that contains a trusted card id to be deleted from relations.</param>
        /// <returns>Updated <see cref="CardModel"/> from server response.</returns>
        /// <example>
        ///  <para>Look at <see cref="PublishCardAsync(PublishCardRequest)"/> to find out 
        ///     how to publish bobCardModel and aliceCardModel.</para>
        ///  <para>Look at <see cref="AddRelationAsync(AddRelationRequest)"/> to find out 
        ///     how to add bobCardModel as a relation to aliceCardModel.</para>
        ///     <code>
        ///         var deleteRelationRequest = new DeleteRelationRequest(bobCardModel.Id, RevocationReason.Unspecified);
        ///         requestSigner.AuthoritySign(deleteRelationRequest, aliceCardModelWithRelation.Id, aliceKeys.PrivateKey);
        ///         var aliceCardModelWithoutRelation = await client.RemoveCardRelationAsync(deleteRelationRequest);
        ///     </code>
        /// </example>
        public async Task<CardModel> RemoveCardRelationAsync(DeleteRelationRequest request)
        {
            if (request == null || request.Snapshot.Length == 0 || request.Signatures.Count != 1)
            {
                throw new RelationException();
            }
            var cardId = request.Signatures.Keys.First();
            var postRequest = Request.Create(RequestMethod.Delete)
             .WithEndpoint($"/v4/card/{cardId}/collections/relations")
             .WithBody(request.GetRequestModel());

            var response = await this.SendAsync(CardsConnection, postRequest).ConfigureAwait(false);
            var cardModel = response.Parse<CardModel>();

            if (this.cardValidator != null)
            {
                this.ValidateCards(new[] { cardModel });
            }

            return cardModel;
        }


        #region Private Methods

        private void ValidateCards(IEnumerable<CardModel> cards)
        {
            var foundCards = cards.ToList();

            var invalidCards = foundCards.Where(c => !this.cardValidator.Validate(c)).ToList();
            if (invalidCards.Any())
            {
                throw new CardValidationException(invalidCards);
            }
        }


        private IConnection InitializeRAConnection()
        {
            var baseUrl = new Uri(this.parameters.RAServiceAddress);
            return new RAServiceConnection(this.parameters.AccessToken, baseUrl);
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
