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

namespace Virgil.SDK.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    
    using Virgil.SDK.Common;
    using Virgil.SDK.Web.Connection;
    
    public class CardsClient
    {
        private readonly IConnection connection;
        private readonly IJsonSerializer serializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardsClient"/> class.
        /// </summary>  
        public CardsClient(string jwt, string apiId) 
            : this(new ServiceConnection
            {
                JWToken = jwt, 
                ApplicationId = apiId,
                BaseURL = new Uri("https://cards.virgilsecurity.com")
            })
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="CardsClient"/> class.
        /// </summary>  
        public CardsClient(string jwt, string apiId, string apiUrl) 
            : this(new ServiceConnection
            {
                JWToken = jwt, 
                ApplicationId = apiId,
                BaseURL = new Uri(apiUrl)
            })
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CardsClient"/> class.
        /// </summary>  
        public CardsClient(IConnection connection)
        {
            this.connection = connection;
            this.serializer = Configuration.Serializer;
        }
        
        /// <summary>
        /// Searches a cards on Virgil Services by specified criteria.
        /// </summary>
        /// <param name="criteria">The search criteria</param>
        /// <returns>A list of found cards in raw form.</returns>
        /// <example>
        /// <code>
        ///     var client  = new CardsClient("[YOUR_ACCESS_TOKEN_HERE]");
        ///     var rawCards = await client.SearcAsync(
        ///         new SearchCriteria
        ///         {
        ///             Identities = new[] { "Alice", "Bob" },
        ///             IdentityType = "member",
        ///             Scope = CardScope.Application
        ///         });
        /// </code>
        /// </example>
        public async Task<IEnumerable<RawCard>> SearchAsync(SearchCriteria criteria)
        {
            if (criteria == null)
            {
                throw new ArgumentNullException(nameof(criteria));
            }

            var request = HttpRequest.Create(HttpRequestMethod.Post)
                .WithEndpoint("/card/actions/search")
                .WithBody(this.serializer, criteria);

            var response = await this.connection.SendAsync(request).ConfigureAwait(false);

            var cards = response
                .HandleError(this.serializer)
                .Parse<RawCard[]>(this.serializer)
                .ToList();

            return cards;
        }

        /// <summary>
        /// Gets a card from Virgil Services by specified card ID.
        /// </summary>
        /// <param name="cardId">The card ID</param>
        /// <returns>An instance of <see cref="RawCard"/> class.</returns>
        /// <example>
        /// <code>
        ///     var client  = new CardsClient("[YOUR_ACCESS_TOKEN_HERE]");
        ///     var cardRaw = await client.GetByIdAsync("[CARD_ID_HERE]");
        /// </code>
        /// </example>
        public async Task<RawCard> GetByIdAsync(string cardId)
        {
            if (string.IsNullOrWhiteSpace(cardId))
            {
                throw new ArgumentNullException(nameof(cardId));
            }
            
            var request = HttpRequest.Create(HttpRequestMethod.Get)
                .WithEndpoint($"/card/{cardId}");

            var resonse = await this.connection.SendAsync(request)
                .ConfigureAwait(false);

            var cardRaw = resonse
                .HandleError(this.serializer)
                .Parse<RawCard>(this.serializer);
            
            return cardRaw;
        }

        /// <summary>
        /// Publishes card in Virgil Cards service.
        /// </summary>
        /// <param name="request">An instance of <see cref="RawCard"/> class</param>
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
        public async Task<RawCard> PublishCardAsync(RawCard request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var postRequest = HttpRequest.Create(HttpRequestMethod.Post)
                .WithEndpoint("/card")
                .WithBody(this.serializer, request);

            var response = await this.connection.SendAsync(postRequest).ConfigureAwait(false);

            return response
                .HandleError(this.serializer)
                .Parse<RawCard>(this.serializer);
        }
    }
}
