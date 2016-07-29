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

    using Virgil.SDK.Models;

    /// <summary>
    /// A Virgil Card is the main entity of the Virgil Security services, it includes an information about the user 
    /// and his public key. The Virgil Card identifies the user by one of his available types, such as an email, 
    /// a phone number, etc.
    /// </summary>
    public sealed class VirgilCard 
    {
        private readonly CardModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilCard"/> class.
        /// </summary>
        internal VirgilCard(CardModel model)
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
        public string Identity => this.model.Identity.Value;

        /// <summary>
        /// Gets the type of current Virgil Card identity.
        /// </summary>
        public string IdentityType => this.model.Identity.Type;

        /// <summary>
        /// Gets the Public Key of current Virgil Card.
        /// </summary>
        public byte[] PublicKey => this.model.PublicKey.Value;

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
        public bool IsGlobal { get; private set; }

        /// <summary>
        /// Gets the date and time of Virgil Card creation.
        /// </summary>
        public DateTime CreatedAt => this.model.CreatedAt;

        /// <summary>
        /// Gets the date and time of <see cref="VirgilCard"/> revocation.
        /// </summary>
        public DateTime? RevokedAt { get; private set; }

        public VirgilBuffer Encrypt(VirgilBuffer data)
        {
            throw new NotImplementedException();
        }

        public bool Verify(VirgilBuffer data, VirgilBuffer signature)
        {
            throw new NotImplementedException();
        }

        public static Task<VirgilCard> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public static Task<IEnumerable<VirgilCard>> Find(string identity)
        {
            throw new NotImplementedException();
        }

        public Task Revoke()
        {
            throw new NotImplementedException();
        }
    }
}