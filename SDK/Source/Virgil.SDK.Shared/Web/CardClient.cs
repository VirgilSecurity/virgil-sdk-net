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

namespace Virgil.SDK.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Virgil.SDK.Common;
    using Virgil.SDK.Web.Connection;

    /// <summary>
    /// The <see cref="CardClient"/> class provides operations with Virgil Cards service.
    /// </summary>
    public class CardClient : ICardClient
    {
        private readonly IConnection connection;
        private readonly IJsonSerializer serializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardClient"/> class.
        /// This class represents a Virgil Security service client and contains
        /// all methods to interaction with server.
        /// </summary>  
        public CardClient() 
            : this(new ServiceConnection("https://api.virgilsecurity.com"))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CardClient"/> class.
        /// </summary>  
        public CardClient(string apiUrl) 
            : this(new ServiceConnection(apiUrl))
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
        /// <param name="identity">The identity.</param>
        /// <param name="token">The string representation of <see cref="Jwt"/> token.</param>
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
                .WithAuthorization(token)
                .WithEndpoint("/card/v5/actions/search")
                .WithBody(this.serializer,
                new SearchCriteria() { Identity = identity });
                       

            var response = await this.connection.SendAsync(request).ConfigureAwait(false);

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
        /// <param name="token">The string representation of <see cref="Jwt"/> token.</param>
        /// <returns>An instance of <see cref="RawSignedModel"/> class and flag, 
        /// which determines whether or not this raw card is superseded.</returns>
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
                .WithAuthorization(token)
                .WithEndpoint($"/card/v5/{cardId}");

            var response = await this.connection.SendAsync(request)
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
        /// <param name="token">The string representation of <see cref="Jwt"/> token.</param>
        /// <returns>published raw card.</returns>
        /// 
        /// <example>
        /// <code>
        /// var crypto = new VirgilCrypto();
        /// var keyPair = crypto.GenerateKeys();
        /// var rawCardContent = new RawCardContent()
        /// {
        ///    CreatedAt = DateTime.UtcNow,
        ///    Identity = "test",
        ///    PublicKey = crypto.ExportPublicKey(keyPair.PublicKey),
        ///    Version = "5.0"
        /// };
        /// var model = new RawSignedModel() { ContentSnapshot = SnapshotUtils.TakeSnapshot(rawCardContent) };
        ///
        /// var signer = new ModelSigner(new VirgilCardCrypto());
        /// signer.SelfSign(model, keyPair.PrivateKey);
        /// 
        /// var jwtGenerator = new JwtGenerator(
        ///    "[APP_ID_HERE]",
        ///    "[API_PRIVATE_KEY_HERE]",
        ///    "[API_PUBLIC_KEY_ID_HERE]",
        ///    TimeSpan.FromMinutes(10),
        ///    new VirgilAccessTokenSigner()
        /// );
        /// var token = jwtGenerator.GenerateToken(rawCardContent.Identity);
        /// 
        /// var client  = new CardsClient();
        /// await client.PublishCardAsync(model, token.ToString());
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
                .WithAuthorization(token)
                .WithEndpoint("/card/v5")
                .WithBody(this.serializer, request);

            var response = await this.connection.SendAsync(postRequest).ConfigureAwait(false);

            return response
                .HandleError(this.serializer)
                .Parse<RawSignedModel>(this.serializer);
        }
    }
}
