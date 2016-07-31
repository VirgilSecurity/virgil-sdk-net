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

    /// <summary>
    /// The Virgil Card request is a data structure that represents user's identity, Public Key and other data. 
    /// The request is used to tell the Virgil Cards service that the user's identity and Public Key are valid, 
    /// this kind of validation can be reached by validating signatures of owner's Private Key and 
    /// application's Private Key.
    /// </summary>
    public sealed class VirgilCardRequest
    {
        private readonly IDictionary<Guid, byte[]> signs;  

        /// <summary>
        /// Initializes a new instance of <see cref="VirgilCardRequest"/> class.
        /// </summary>
        public VirgilCardRequest(
            string identity, 
            string identityType, 
            string keyName,
            bool isGlobal,
            IDictionary<string, string> data = null)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="VirgilCardRequest"/> class.
        /// </summary>
        public VirgilCardRequest(
            string identity,
            string identityType,
            VirgilKey key,
            bool isGlobal,
            IDictionary<string, string> data = null)
        {
        }

        /// <summary>
        /// Gets an unique idenitity of the ticket.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets the user's identity value.
        /// </summary>
        public string Identity { get; }

        /// <summary>
        /// Gets the user's identity type.
        /// </summary>
        public string IdentityType { get; }

        /// <summary>
        /// Gets a Public Key value.
        /// </summary>
        public byte[] PublicKey { get; }

        /// <summary>
        /// Gets the canonical form of current <see cref="VirgilCard"/> instance.
        /// </summary>
        public byte[] Fingerprint
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="VirgilCard"/> is global.
        /// </summary>
        public bool IsGlobal { get; }

        /// <summary>
        /// Gets the key/value parameters of future <see cref="VirgilCard"/>.
        /// </summary>
        public IReadOnlyDictionary<string, string> Data { get; }

        /// <summary>
        /// Gets the list of digital signatures that was signed the <see cref="Fingerprint"/> of current <see cref="VirgilCardRequest"/>.
        /// </summary>
        public IReadOnlyDictionary<Guid, byte[]> Signs => new ReadOnlyDictionary<Guid, byte[]>(this.signs);

        /// <summary>
        /// Exports a current <see cref="VirgilCardRequest"/> to it's binary representation.
        /// </summary>
        public string Export()
        {
            //var ticketObject = new
            //{
            //    id = Guid.Empty,
            //    identity = this.Identity,
            //    identity_type = this.IdentityType,
            //    public_key = this.PublicKey,
            //    data = this.Data,
            //    is_global = this.IsGlobal,
            //    signs = this.signs
            //};

            //var json = JsonConvert.SerializeObject(ticketObject);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a signature of third party Private Keys.
        /// </summary>
        /// <param name="cardId">The <see cref="VirgilCard"/>'s identifier.</param>
        /// <param name="sign">The </param>
        /// <example>
        /// var keyPair = VirgilKeyPair.Generate(); 
        /// var ticket = new VirgilCardTicket("demo@virgilsecurity.com", "email", keyPair.PublicKey());
        /// 
        /// var ownerSign = CryptoHelper.PerformSignatureGeneration(ticket.Fingerprint, keyPair.PrivateKey());
        /// var appSign = CryptoHelper.PerformSignatureGeneration(ticket.Fingerprint, %APP_PRIVATE_KEY%);
        /// 
        /// ticket.AddOwnerSign(ownerSign);
        /// ticket.AddSign(%APP_CARD_ID%, appSign);
        /// </example>
        public void AddSign(Guid cardId, byte[] sign)
        {
            this.signs.Add(cardId, sign);
        }

        /// <summary>
        /// Imports the <see cref="VirgilCardRequest"/> from it's binary representation.
        /// </summary>
        public static VirgilCardRequest Import(string ticket)
        {
            throw new NotImplementedException();
        }
    }
}