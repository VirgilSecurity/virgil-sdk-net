#region Copyright (C) 2016 Virgil Security Inc.
// Copyright (C) 2016 Virgil Security Inc.
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

namespace Virgil.SDK.HighLevel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Virgil.SDK.Client;
    using Virgil.SDK.Cryptography;
    using Virgil.SDK.Exceptions;

    /// <summary>
    /// A Virgil Card is the main entity of the Virgil Security services, it includes an information about the user 
    /// and his public key. The Virgil Card identifies the user by one of his available types, such as an email, 
    /// a phone number, etc.
    /// </summary>
    public sealed class VirgilCard 
    {
        private readonly Card model;

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilCard"/> class.
        /// </summary>
        internal VirgilCard(Card model)
        {
            this.model = model;
        }

        /// <summary>
        /// Gets the unique identifier for the Virgil Card.
        /// </summary>
        public string Id => this.model.Id;

        /// <summary>
        /// Gets the value of current Virgil Card identity.
        /// </summary>
        public string Identity => this.model.Identity;

        /// <summary>
        /// Gets the type of current Virgil Card identity.
        /// </summary>
        public string IdentityType => this.model.IdentityType;

        /// <summary>
        /// Gets the custom <see cref="VirgilCard"/> parameters.
        /// </summary>
        public IReadOnlyDictionary<string, string> Data => this.model.Data;

        /// <summary>
        /// Gets the Public Key of current Virgil Card.
        /// </summary>
        internal byte[] PublicKey => this.model.PublicKey;

        /// <summary>
        /// Encrypts the specified data for current <see cref="VirgilCard"/> recipient.
        /// </summary>
        /// <param name="data">The data to be encrypted.</param>
        public byte[] Encrypt(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var crypto = VirgilConfig.GetService<ICrypto>();
            var publicKey = crypto.ImportPublicKey(this.PublicKey);

            var cipherdata = crypto.Encrypt(data, publicKey);

            return cipherdata;
        }

        /// <summary>
        /// Verifies the specified data and signature with current <see cref="VirgilCard"/> recipient.
        /// </summary>
        /// <param name="data">The data to be verified.</param>
        /// <param name="signature">The signature used to verify the data integrity.</param>
        public bool Verify(byte[] data, byte[] signature)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
     
            if (signature == null)
                throw new ArgumentNullException(nameof(signature));
     
            var crypto = VirgilConfig.GetService<ICrypto>();
            var publicKey = crypto.ImportPublicKey(this.PublicKey);

            var isValid = crypto.Verify(data, signature, publicKey);

            return isValid;
        }

        /// <summary>
        /// Gets the <see cref="VirgilCard"/> by specified identifier.
        /// </summary>
        /// <param name="cardId">The identifier that represents a <see cref="VirgilCard"/>.</param>
        public static async Task<VirgilCard> GetAsync(string cardId)
        {
            var client = VirgilConfig.GetService<VirgilClient>();
            var virgilCardDto = await client.GetCardAsync(cardId);

            if (virgilCardDto == null)
            {
                throw new VirgilCardIsNotFoundException();
            }

            return new VirgilCard(virgilCardDto);
        }

        /// <summary>
        /// Finds the <see cref="VirgilCard" />s in global scope by specified criteria.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="type">Type of the identity.</param>
        /// <returns>
        /// A list of found <see cref="VirgilCard" />s.
        /// </returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static Task<IEnumerable<VirgilCard>> FindGlobalAsync
        (
            string identity,
            GlobalIdentityType type = GlobalIdentityType.Email
        )
        {
            if (identity == null)
                throw new ArgumentNullException(nameof(identity));
            
            return FindGlobalAsync(new[] { identity }, type);
        }

        /// <summary>
        /// Finds the <see cref="VirgilCard" />s in global scope by specified criteria.
        /// </summary>
        /// <param name="identities">The identity.</param>
        /// <param name="type">Type of the identity.</param>
        /// <returns>
        /// A list of found <see cref="VirgilCard" />s.
        /// </returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task<IEnumerable<VirgilCard>> FindGlobalAsync
        (
            IEnumerable<string> identities,
            GlobalIdentityType type = GlobalIdentityType.Email
        )
        {
            if (identities == null)
                throw new ArgumentNullException(nameof(identities));

            var client = VirgilConfig.GetService<VirgilClient>();

            var criteria = new SearchCriteria
            {
                Identities = identities,
                IdentityType = type.ToString().ToLower(),
                Scope = CardScope.Global
            };

            var cards = await client.SearchCardsAsync(criteria).ConfigureAwait(false);

            return cards.Select(c => new VirgilCard(c)).ToList();
        }

        /// <summary>
        /// Finds the <see cref="VirgilCard" />s by specified criteria.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="type">Type of the identity.</param>
        /// <returns>
        /// A list of found <see cref="VirgilCard" />s.
        /// </returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static Task<IEnumerable<VirgilCard>> FindAsync
        (
            string identity,
            string type = null
        )
        {
            if (identity == null)
                throw new ArgumentNullException(nameof(identity));

            return FindAsync(new[] {identity}, type);
        }

        /// <summary>
        /// Finds the <see cref="VirgilCard" />s by specified criteria.
        /// </summary>
        /// <param name="identities">The identities.</param>
        /// <param name="type">Type of the identity.</param>
        /// <returns>
        /// A list of found <see cref="VirgilCard" />s.
        /// </returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static async Task<IEnumerable<VirgilCard>> FindAsync
        (
            IEnumerable<string> identities, 
            string type = null
        )
        {
            var identityList = identities as IList<string> ?? identities.ToList();

            if (identities == null || !identityList.Any())
                throw new ArgumentNullException(nameof(identities));

            var client = VirgilConfig.GetService<VirgilClient>();

            var criteria = new SearchCriteria
            {
                Identities = identityList,
                IdentityType = type,
                Scope = CardScope.Application
            };

            var cardModels = await client.SearchCardsAsync(criteria).ConfigureAwait(false);

            return cardModels.Select(model => new VirgilCard(model)).ToList();
        }

        /// <summary>
        /// Creates a new <see cref="VirgilCard"/> by request.
        /// </summary>
        /// <param name="request">The request.</param>
        public static async Task<VirgilCard> CreateAsync(CreateCardRequest request)
        {
            var client = VirgilConfig.GetService<VirgilClient>();
            var card = await client.CreateCardAsync(request).ConfigureAwait(false);

            return new VirgilCard(card);
        }

        /// <summary>
        /// Revokes a <see cref="VirgilCard"/> by revocation request.
        /// </summary>
        public static async Task RevokeAsync(RevokeCardRequest request)
        {
            var client = VirgilConfig.GetService<VirgilClient>();
            await client.RevokeCardAsync(request).ConfigureAwait(false);
        }
    }
}