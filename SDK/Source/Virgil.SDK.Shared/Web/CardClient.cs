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

using Virgil.SDK.Web.Authorization;

namespace Virgil.SDK.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Virgil.SDK.Common;
    using Virgil.SDK.Web.Connection;

    public class CardClient : ICardClient
    {
        private readonly IConnection connection;
        private readonly IJsonSerializer serializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardClient"/> class.
        /// This class represents a Virgil Security service client and contains
        /// all methods to interaction with server.
        /// </summary>  
        public CardClient() :
            this(new ServiceConnection
            {
                BaseURL = new Uri("https://api.virgilsecurity.com")
            })
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CardClient"/> class.
        /// </summary>  
        public CardClient(string apiUrl) :
            this(new ServiceConnection
            {
                BaseURL = new Uri(apiUrl)
            })
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CardClient"/> class.
        /// </summary>  
        public CardClient(IConnection connection)
        {
            this.connection = connection;
            this.serializer = Configuration.Serializer;
        }


        /// <summary>
        /// Searches a cards on Virgil Services by specified identity.
        /// </summary>
        /// <param name="criteria">The search criteria</param>
        /// <returns>A list of found cards in raw form.</returns>
        /// <example>
        /// <code>
        ///     var client  = new CardsClient();
        ///     var rawCards = await client.SearchCardsAsync("Alice", "[YOUR_JWT_TOKEN_HERE]");
        /// </code>
        /// </example>
        public async Task<IEnumerable<RawSignedModel>> SearchCardsAsync(string identity, string token)
        {
            if (String.IsNullOrWhiteSpace(identity))
            {
                throw new ArgumentNullException(nameof(identity));
            }

            if (String.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentNullException(nameof(token));
            }

            var request = HttpRequest.Create(HttpRequestMethod.Post)
                .WithEndpoint("/card/v5/actions/search")
                .WithBody(this.serializer,
                new SearchCriteria(){Identity = identity});

            var response = await this.connection.SendAsync(request, token).ConfigureAwait(false);

            var cards = response
                .HandleError(this.serializer)
                .Parse<RawSignedModel[]>(this.serializer)
                .ToList();

            return cards;
        }

        /// <summary>
        /// Gets a card from Virgil Services by specified card ID.
        /// </summary>
        /// <param name="cardId">The card ID</param>
        /// <returns>An instance of <see cref="RawSignedModel"/> class.</returns>
        /// <example>
        /// <code>
        ///     var client  = new CardsClient();
        ///     var (cardRaw, isOutDated) = await client.GetCardAsync("[CARD_ID_HERE]", "[YOUR_JWT_TOKEN_HERE]");
        /// </code>
        /// </example>
        public async Task<Tuple<RawSignedModel, bool>> GetCardAsync(string cardId, string token)
        {
            if (string.IsNullOrWhiteSpace(cardId))
            {
                throw new ArgumentNullException(nameof(cardId));
            }

            if (String.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentNullException(nameof(token));
            }


            var request = HttpRequest.Create(HttpRequestMethod.Get)
                .WithEndpoint($"/card/v5/{cardId}");

            var response = await this.connection.SendAsync(request, token)
                .ConfigureAwait(false);
            
            var cardRaw = response
                .HandleError(this.serializer)
                .Parse<RawSignedModel>(this.serializer);
            var supersededHeader = response.Headers.FirstOrDefault(x => x.Key == "X-Virgil-Is-Superseeded");
            var superseded = (supersededHeader.Value != null) && supersededHeader.Value == "true";
            return new Tuple<RawSignedModel, bool>(cardRaw, superseded);
        }

        /// <summary>
        /// Publishes card in Virgil Cards service.
        /// </summary>
        /// <param name="request">An instance of <see cref="RawSignedModel"/> class</param>
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
        ///     await client.CreateCardAsync(request);
        /// </code>
        /// </example>
        public async Task<RawSignedModel> PublishCardAsync(RawSignedModel request, string token)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (String.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentNullException(nameof(token));
            }

            var postRequest = HttpRequest.Create(HttpRequestMethod.Post)
                .WithEndpoint("/card/v5")
                .WithBody(this.serializer, request);

            var response = await this.connection.SendAsync(postRequest, token).ConfigureAwait(false);

            return response
                .HandleError(this.serializer)
                .Parse<RawSignedModel>(this.serializer);
        }
    }
}
