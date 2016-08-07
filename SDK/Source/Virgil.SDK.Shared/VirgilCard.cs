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
    using System.Threading.Tasks;

    using Virgil.SDK.Clients.Models;
    using Virgil.SDK.Requests;

    /// <summary>
    /// A Virgil Card is the main entity of the Virgil Security services, it includes an information about the user 
    /// and his public key. The Virgil Card identifies the user by one of his available types, such as an email, 
    /// a phone number, etc.
    /// </summary>
    public sealed partial class VirgilCard 
    {
        private readonly VirgilCardModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilCard"/> class.
        /// </summary>
        internal VirgilCard(VirgilCardModel model)
        {   
            this.model = model;     
        }

        /// <summary>
        /// Gets the unique identifier for the Virgil Card.
        /// </summary>
        public Guid Id => this.model.Id;

        /// <summary>
        /// Gets the value of current Virgil Card identity.
        /// </summary>
        public string Identity { get; private set; }

        /// <summary>
        /// Gets the type of current Virgil Card identity.
        /// </summary>
        public string IdentityType { get; private set; }

        /// <summary>
        /// Gets the Public Key of current Virgil Card.
        /// </summary>
        public byte[] PublicKey { get; private set; }

        /// <summary>
        /// Gets the custom <see cref="VirgilCard"/> parameters.
        /// </summary>
        public IReadOnlyDictionary<string, string> Data { get; private set; }

        /// <summary>
        /// Gets the <see cref="VirgilCard"/> version.
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the current <see cref="VirgilCard"/> identity is confirmed by Virgil Identity service.
        /// </summary>
        public bool IsConfirmed { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="VirgilCard"/> is global.
        /// </summary>
        public VirgilCardScope Scope { get; private set; }

        /// <summary>
        /// Gets the date and time of Virgil Card creation.
        /// </summary>  
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Gets the date and time of <see cref="VirgilCard"/> revocation.
        /// </summary>
        public DateTime? RevokedAt { get; private set; }

        /// <summary>
        /// Encrypts the specified data for current <see cref="VirgilCard"/> recipient.
        /// </summary>
        /// <param name="data">The data to be encrypted.</param>
        public VirgilBuffer Encrypt(VirgilBuffer data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Verifies the specified data and signature with current <see cref="VirgilCard"/> recipient.
        /// </summary>
        /// <param name="data">The data to be verified.</param>
        /// <param name="signature">The signature used to verify the data integrity.</param>
        public bool Verify(VirgilBuffer data, VirgilBuffer signature)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the <see cref="VirgilCard"/> by specified identifier.
        /// </summary>
        /// <param name="id">The identifier that represents a <see cref="VirgilCard"/>.</param>
        public static Task<VirgilCard> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Finds the <see cref="VirgilCard" />s by specified criteria.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="identityType">Type of the identity.</param>
        /// <returns>
        /// A list of found <see cref="VirgilCard" />s.
        /// </returns>
        public static Task<IEnumerable<VirgilCard>> FindAsync(string identity, string identityType = null)
        {
            throw new NotImplementedException();
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

        public static VirgilCardRequest IssueRequest(string alice, string name, VirgilKey aliceKey)
        {
            throw new NotImplementedException();
        }
    }
}