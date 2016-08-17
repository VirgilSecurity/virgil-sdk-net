#region "Copyright (C) 2015 Virgil Security Inc."
/**
 * Copyright (C) 2015 Virgil Security Inc.
 *
 * Lead Maintainer: Virgil Security Inc. <support@virgilsecurity.com>
 *
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are
 * met:
 *
 *     (1) Redistributions of source code must retain the above copyright
 *     notice, this list of conditions and the following disclaimer.
 *
 *     (2) Redistributions in binary form must reproduce the above copyright
 *     notice, this list of conditions and the following disclaimer in
 *     the documentation and/or other materials provided with the
 *     distribution.
 *
 *     (3) Neither the name of the copyright holder nor the names of its
 *     contributors may be used to endorse or promote products derived from
 *     this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE AUTHOR ''AS IS'' AND ANY EXPRESS OR
 * IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT,
 * INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
 * HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
 * STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING
 * IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
 * POSSIBILITY OF SUCH DAMAGE.
 */
#endregion

namespace Virgil.SDK
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    using Virgil.SDK.Clients;
    using Virgil.SDK.Clients.Models;
    using Virgil.SDK.Cryptography;
    using Virgil.SDK.Exceptions;
    using Virgil.SDK.Requests;

    /// <summary>
    /// A Virgil Card is the main entity of the Virgil Security services, it includes an information about the user 
    /// and his public key. The Virgil Card identifies the user by one of his available types, such as an email, 
    /// a phone number, etc.
    /// </summary>
    public sealed partial class VirgilCard 
    {
        private readonly VirgilCardModel model;
        private readonly ICryptoService cryptoService;

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilCard"/> class.
        /// </summary>
        internal VirgilCard(VirgilCardModel model)
        {
            this.model = model;
            this.cryptoService = VirgilConfig.ServiceResolver.Resolve<ICryptoService>();

            if (this.model.Data != null)
            {
                this.Data = new ReadOnlyDictionary<string, string>(this.model.Data);
            }
        }

        /// <summary>
        /// Gets the unique identifier for the Virgil Card.
        /// </summary>
        public Guid Id => this.model.Id;

        /// <summary>
        /// Gets the value of current Virgil Card identity.
        /// </summary>
        public string Identity => this.model.Identity;

        /// <summary>
        /// Gets the type of current Virgil Card identity.
        /// </summary>
        public string IdentityType => this.model.IdentityType;

        /// <summary>
        /// Gets the Public Key of current Virgil Card.
        /// </summary>
        public PublicKey PublicKey => new PublicKey(this.model.PublicKey);

        /// <summary>
        /// Gets a value indicating whether the current <see cref="VirgilCard"/> identity is confirmed by Virgil Identity service.
        /// </summary>
        public bool IsConfirmed => this.model.IsConfirmed;

        /// <summary>
        /// Gets the scope.
        /// </summary>
        public VirgilCardScope Scope
        {
            get
            {
                var scope = this.model.Scope.ToUpper();
                switch (scope)
                {
                    case "GLOBAL": return VirgilCardScope.Global;
                    case "APPLICATION": return VirgilCardScope.Application;

                    default:
                        throw new NotSupportedException($"Value {scope} is not supported");
                }
            }
        }

        /// <summary>
        /// Gets the custom <see cref="VirgilCard"/> parameters.
        /// </summary>
        public IReadOnlyDictionary<string, string> Data { get; private set; }

        /// <summary>
        /// Gets the <see cref="VirgilCard"/> version.
        /// </summary>
        public string Version => this.model.Meta.Version;

        /// <summary>
        /// Gets the date and time of Virgil Card creation.
        /// </summary>  
        public DateTime CreatedAt => this.model.Meta.CreatedAt;

        /// <summary>
        /// Gets or sets the device.
        /// </summary>
        public string Device => this.model.Info.Device;

        /// <summary>
        /// Gets or sets the name of the device.
        /// </summary>
        public string DeviceName => this.model.Info.DeviceName;

        /// <summary>
        /// Gets the fingerprint.
        /// </summary>
        public VirgilBuffer Fingerprint => VirgilBuffer.FromBytes(this.model.Meta.Fingerprint);

        /// <summary>
        /// Encrypts the specified data for current <see cref="VirgilCard"/> recipient.
        /// </summary>
        /// <param name="data">The data to be encrypted.</param>
        public VirgilBuffer Encrypt(VirgilBuffer data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var recipients = new Dictionary<byte[], PublicKey>
            {
                { this.Id.ToByteArray(), this.PublicKey }
            };

            var cipherdata = this.cryptoService.EncryptData(data.ToBytes(), recipients);
            var buffer = VirgilBuffer.FromBytes(cipherdata);

            return buffer;
        }

        /// <summary>
        /// Verifies the specified data and signature with current <see cref="VirgilCard"/> recipient.
        /// </summary>
        /// <param name="data">The data to be verified.</param>
        /// <param name="signature">The signature used to verify the data integrity.</param>
        public bool Verify(VirgilBuffer data, VirgilBuffer signature)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (signature == null)
            {
                throw new ArgumentNullException(nameof(signature));
            }

            var isValid = this.cryptoService.VerifyData(data.ToBytes(), signature.ToBytes(), this.PublicKey);
            return isValid;
        }

        /// <summary>
        /// Gets the <see cref="VirgilCard"/> by specified identifier.
        /// </summary>
        /// <param name="cardId">The identifier that represents a <see cref="VirgilCard"/>.</param>
        public static async Task<VirgilCard> GetAsync(Guid cardId)
        {
            var hub = VirgilConfig.ServiceResolver.Resolve<IServiceHub>();
            var virgilCardDto = await hub.Cards.GetAsync(cardId);

            if (virgilCardDto == null)
            {
                throw new VirgilCardIsNotFoundException();
            }

            return new VirgilCard(virgilCardDto);
        }

        /// <summary>
        /// Finds the <see cref="VirgilCard" />s by specified criteria.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="type">Type of the identity.</param>
        /// <param name="confirmed">if set to <c>true</c> [is confirmed].</param>
        /// <param name="scope">The scope.</param>
        /// <returns>
        /// A list of found <see cref="VirgilCard" />s.
        /// </returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static Task<IEnumerable<VirgilCard>> FindAsync
        (
            string identity,
            string type = null,
            VirgilCardScope scope = VirgilCardScope.Application,
            bool confirmed = false
        )
        {
            if (identity == null)
                throw new ArgumentNullException(nameof(identity));

            return FindAsync(new[] { identity }, type, scope, confirmed);
        }

        /// <summary>
        /// Finds the <see cref="VirgilCard" />s by specified criteria.
        /// </summary>
        /// <param name="identities">The identities.</param>
        /// <param name="type">Type of the identity.</param>
        /// <param name="confirmed">The cards with confirmed identity.</param>
        /// <param name="scope">The scope.</param>
        /// <returns>
        /// A list of found <see cref="VirgilCard" />s.
        /// </returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static async Task<IEnumerable<VirgilCard>> FindAsync
        (
            IEnumerable<string> identities, 
            string type = null,
            VirgilCardScope scope = VirgilCardScope.Application,
            bool confirmed = false
        )
        {
            var identityList = identities as IList<string> ?? identities.ToList();

            if (identities == null || !identityList.Any())
                throw new ArgumentNullException(nameof(identities));

            var hub = VirgilConfig.ServiceResolver.Resolve<IServiceHub>();

            var virgilCards = await hub.Cards
                .SearchAsync(identityList, type, scope.ToString().ToLower(), confirmed);

            return virgilCards.Select(model => new VirgilCard(model)).ToList();
        }
        
        /// <summary>
        /// Sends the request for 
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public static Task<VirgilCard> PublishAsync(VirgilCardRequest request)
        {
            throw new NotImplementedException();
        }
    }
}