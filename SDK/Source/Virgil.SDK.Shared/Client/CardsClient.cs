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

namespace Virgil.SDK.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Virgil.SDK.Client.Connection;
    using Virgil.SDK.Utils;

    public class CardsClient
    {
        private readonly string accessToken;

        private readonly Lazy<IConnection> cardsConnectionLazy;
        private readonly Lazy<IConnection> roConnectionLazy;
        private readonly Lazy<IConnection> raConnectionLazy;

        private IConnection CardsConnection => this.cardsConnectionLazy.Value;
        private IConnection RoCardsConnection => this.roConnectionLazy.Value;
        private IConnection RaConnection => this.raConnectionLazy.Value;

        private Uri cardsSerivceURL;
        private Uri readOnlyCardsURL;
        private Uri raServiceURL;

        private readonly ISerializer serializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardsClient"/> class.
        /// </summary>  
        public CardsClient() : this(null, null, null, null)
        {
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="CardsClient"/> class.
        /// </summary>
        public CardsClient(string accessToken) : this(accessToken, null, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CardsClient"/> class.
        /// </summary>  
        public CardsClient(
            string accessToken,
            string cardsServiceAddress,
            string readCardsServiceAddress,
            string raServiceAddress) 
        {
            this.accessToken = accessToken;

            if (string.IsNullOrWhiteSpace(cardsServiceAddress))
            {
                cardsServiceAddress = "https://cards.virgilsecurity.com";
            }

			if (string.IsNullOrWhiteSpace(readCardsServiceAddress))
			{
				readCardsServiceAddress = "https://cards-ro.virgilsecurity.com";
			}

			if (string.IsNullOrWhiteSpace(raServiceAddress))
			{
				raServiceAddress = "https://ra.virgilsecurity.com";
			}

			this.cardsSerivceURL = new Uri(cardsServiceAddress);
			this.readOnlyCardsURL = new Uri(readCardsServiceAddress);
			this.raServiceURL = new Uri(raServiceAddress);

			this.cardsConnectionLazy = new Lazy<IConnection>(this.InitializeCardsConnection);
			this.roConnectionLazy = new Lazy<IConnection>(this.InitializeReadCardsConnection);
			this.raConnectionLazy = new Lazy<IConnection>(this.InitializeRAConnection);

            this.serializer = ServiceLocator.GetService<ISerializer>();
        }

		/// <summary>
		/// Searches a cards on Virgil Services by specified criteria.
		/// </summary>
		/// <param name="criteria">The search criteria</param>
		/// <returns>A list of found cards in raw form.</returns>
		/// <example>
		/// <code>
		///     var client  = new CardsClient("[YOUR_ACCESS_TOKEN_HERE]");
        ///     var rawCards = await client.SearchByCriteriaAsync(
        ///         new SearchCriteria
        ///         {
        ///             Identities = new[] { "Alice", "Bob" },
        ///             IdentityType = "member",
        ///             Scope = CardScope.Application
        ///         });
		/// </code>
		/// </example>
		public async Task<IEnumerable<CardRaw>> SearchByCriteriaAsync(SearchCriteria criteria)
        {
            if (criteria == null)
            {
                throw new ArgumentNullException(nameof(criteria));
            }

            if (criteria.Identities == null || !criteria.Identities.Any())
            {
                throw new ArgumentNullException(nameof(criteria));
            }

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

            var request = HttpRequest.Create(HttpRequestMethod.Post)
                .WithEndpoint("/v4/card/actions/search")
                .WithBody(this.serializer, body);

            var response = await this.RoCardsConnection.SendAsync(request).ConfigureAwait(false);

            var cards = response
                .HandleError(this.serializer)
                .Parse<IEnumerable<CardRaw>>(this.serializer)
                .ToList();

            return cards;
        }

		/// <summary>
		/// Searches a cards on Virgil Services by specified identity.
		/// </summary>
		/// <param name="identity">The card identity value</param>
		/// <returns>A list of found cards in raw representation.</returns>
		/// <example>
		/// <code>
		///     var client  = new CardsClient("[YOUR_ACCESS_TOKEN_HERE]");
		///     var rawCards = await client.SearchByIdentityAsync("[CARD_IDENTITY_HERE]");
		/// </code>
		/// </example>
		public Task<IEnumerable<CardRaw>> SearchByIdentityAsync(string identity)
        {
			if (string.IsNullOrWhiteSpace(identity))
			{
				throw new ArgumentNullException(nameof(identity));
			}

            return this.SearchByCriteriaAsync(new SearchCriteria { 
                Identities = new[] { identity }
            });
        }

		/// <summary>
		/// Searches a cards on Virgil Services by specified identity and 
		/// identity type.
		/// </summary>
		/// <param name="identity">The card identity value</param>
		/// <param name="identityType">The card identity type</param>
		/// <returns>A list of found cards in raw representation.</returns>
		/// <example>
		/// <code>
		///     var client  = new CardsClient("[YOUR_ACCESS_TOKEN_HERE]");
		///     var rawCards = await client.SearchByTypeAsync(
		///         "[CARD_IDENTITY_HERE]", "[CARD_IDENTITY_TYPE_HERE]");
		/// </code>
		/// </example>
		public Task<IEnumerable<CardRaw>> SearchByTypeAsync(string identity, string identityType)
		{
			if (string.IsNullOrWhiteSpace(identity))
			{
				throw new ArgumentNullException(nameof(identity));
			}

			if (string.IsNullOrWhiteSpace(identityType))
			{
				throw new ArgumentNullException(nameof(identityType));
			}
            
			return this.SearchByCriteriaAsync(new SearchCriteria
			{
				Identities = new[] { identity },
                IdentityType = identityType
			});
		}

		/// <summary>
		/// Gets a card from Virgil Services by specified card ID.
		/// </summary>
		/// <param name="cardId">The card ID</param>
		/// <returns>An instance of <see cref="CardRaw"/> class.</returns>
		/// <example>
		/// <code>
		///     var client  = new CardsClient("[YOUR_ACCESS_TOKEN_HERE]");
        ///     var cardRaw = await client.GetByIdAsync("[CARD_ID_HERE]");
		/// </code>
		/// </example>
		public async Task<CardRaw> GetByIdAsync(string cardId)
		{
            if (string.IsNullOrWhiteSpace(cardId))
			{
				throw new ArgumentNullException(nameof(cardId));
			}
            
			var request = HttpRequest.Create(HttpRequestMethod.Get)
				.WithEndpoint($"/v4/card/{cardId}");

            var resonse = await this.RoCardsConnection.SendAsync(request)
                .ConfigureAwait(false);

            var cardRaw = resonse
                .HandleError(this.serializer)
                .Parse<CardRaw>(this.serializer);
            
            return cardRaw;
		}

		/// <summary>
		/// Publishes card in Virgil Cards service.
		/// </summary>
		/// <param name="request">An instance of <see cref="CardRequest"/> class</param>
		/// <example>
		/// <code>
		///     var crypto  = new VirgilCrypto();
		///     var manager = new CardManager(crypto);
		///     var factory = new RequestFactory(crypto);
		///     var client  = new CardsClient("[YOUR_ACCESS_TOKEN_HERE]");
		///     
        ///     // import app's information
        /// 
		///     var appSigner = new CardSigner {
		///         CardId = "[APP_CARD_ID_HERE]",
		///         PrivateKey = crypto.ImportPrivateKey(File.ReadAllBytes(
		///             "[YOUR_APP_KEY_PATH_HERE]"), 
		///             "[YOUR_APP_KEY_PASSWORD_HERE]")
		///     };
		/// 
		///     // generate public/private key pair and create a new card
		/// 
		///     var keypair = crypto.GenerateKeys();
		///     var card = manager.CreateNew(new CardParams {
		///         Identity = "Alice",
		///         KeyPair  = keypair
		///     });
        /// 
        ///     // publish just created card.
		/// 
		///     var request = factory.CreatePublishRequest(card, appSigner);
        ///     await client.PublishAsync(request);
		/// </code>
		/// </example>
		public async Task PublishAsync(CardRequest request)
        {
			if (request == null)
			{
				throw new ArgumentNullException(nameof(request));
			}
            
			var postRequest = HttpRequest.Create(HttpRequestMethod.Post)
				.WithEndpoint("/v4/card")
				.WithBody(this.serializer, request);

			var response = await this.CardsConnection.SendAsync(postRequest).ConfigureAwait(false);

			response.HandleError(this.serializer);      
        }

		/// <summary>
		/// Revokes a card from Virgil Services by specifier card ID.
		/// </summary>
		/// <param name="request">An instance of <see cref="CardRequest"/> class</param>
		/// <example>
		/// <code>
		///     var crypto  = new VirgilCrypto();
		///     var factory = new RequestFactory(crypto);
		///     var client  = new CardsClient("[YOUR_ACCESS_TOKEN_HERE]");
		///     
		///     // import app's information
		/// 
		///     var appSigner = new CardSigner {
		///         CardId = "[APP_CARD_ID_HERE]",
		///         PrivateKey = crypto.ImportPrivateKey(File.ReadAllBytes(
		///             "[YOUR_APP_KEY_PATH_HERE]"), 
		///             "[YOUR_APP_KEY_PASSWORD_HERE]")
		///     };
		/// 
		///     // revoke the card by specified ID
		/// 
		///     var request = factory.CreateRevokeRequest(["USER_CARD_ID_HERE"], appSigner);
		///     await client.RevokeAsync(request);
		/// </code>
		/// </example>
		public async Task RevokeAsync(RevokeCardRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var postRequest = HttpRequest.Create(HttpRequestMethod.Delete)
			    .WithEndpoint($"/v4/card/{request.RevokingCardId}")
                .WithBody(this.serializer, request);

			var response = await this.CardsConnection.SendAsync(postRequest).ConfigureAwait(false);

			response.HandleError(this.serializer);
		}

		/// <summary>
		/// Appends a third party signature to the published card. Thus, the 
		/// relations are created between the cards and a signer is acts as 
		/// a trusting side.
		/// </summary>
		/// <param name="request">An instance of <see cref="RelationCardRequest"/> 
		/// class that contains a trusted card snapshot.</param>
		/// <example>
		/// <code>
		///     var crypto  = new VirgilCrypto();
		///     var factory = new RequestFactory(crypto);
		///     var client  = new CardsClient("[YOUR_ACCESS_TOKEN_HERE]");
		/// 
		///     var signer = new CardSigner {
		///         CardId = "[SIGNER_CARD_ID_HERE]",
        ///         PrivateKey = [SIGNER_PRIVATE_KEY_HERE]
		///     };
		///    
		///     var signingCard = await client.GetByIdAsync("[USER_CARD_ID_HERE]");
		///     var request = factory.CreateRelationRequest(signingCard, signer); 
		///     await client.AppendSignatureAsync(request);
		/// </code>
		/// </example>
		public async Task CreateRelationAsync(RelationCardRequest request)
        {
            var postRequest = HttpRequest.Create(HttpRequestMethod.Post)
                .WithEndpoint($"/v4/card/{request.SigningCardId}/collections/relations")
                .WithBody(this.serializer, request);

            var response = await this.CardsConnection.SendAsync(postRequest).ConfigureAwait(false);
            response.HandleError(this.serializer);
        }

		/// <summary>
		/// Removes a third party signature from the published card. Thus, the 
		/// relations are broken between the cards and the signer becomes the
		/// untrusted side.
		/// </summary>
		/// <param name="request">An instance of <see cref="RelationCardRequest"/> class,
		/// that contains a trusted card id to be deleted from relations.</param>
		/// <example>
		/// <code>
		///     var crypto  = new VirgilCrypto();
		///     var factory = new RequestFactory(crypto);
		///     var client  = new CardsClient("[YOUR_ACCESS_TOKEN_HERE]");
		/// 
		///     var signer = new CardSigner {
		///         CardId = "[SIGNER_CARD_ID_HERE]",
		///         PrivateKey = [SIGNER_PRIVATE_KEY_HERE]
		///     };
		///    
		///     var signingCard = await client.GetByIdAsync("[USER_CARD_ID_HERE]");
		///     var request = factory.CreateRelationRemoveRequest(signingCard, signer); 
		///     await client.AppendSignatureAsync(request);
		/// </code>
		/// </example>
		public async Task RemoveRelationAsync(RelationCardRequest request)
        {
            var postRequest = HttpRequest.Create(HttpRequestMethod.Delete)
                .WithEndpoint($"/v4/card/{request.SigningCardId}/collections/relations")
                .WithBody(this.serializer, request);

            var response = await this.CardsConnection.SendAsync(postRequest).ConfigureAwait(false);

            response.HandleError(this.serializer);
        }


        #region Private Methods

        private IConnection InitializeRAConnection()
        {
            return new ServiceConnection { BaseURL = this.raServiceURL };
        }

        private IConnection InitializeReadCardsConnection()
        {
            return new ServiceConnection { BaseURL = this.readOnlyCardsURL };
        }

        private IConnection InitializeCardsConnection()
        {
            return new ServiceConnection 
            { 
                BaseURL = this.readOnlyCardsURL, 
                AccessToken = this.accessToken 
            };
        }

        #endregion
    }
}
