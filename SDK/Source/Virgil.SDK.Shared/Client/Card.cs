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
    using System.Collections.Generic;

    public class Card
    {
        /// <summary>
        /// Gets the Virgil Card fingerprint.
        /// </summary>
        public string Id { get; internal set; }

        /// <summary>
        /// Gets the Virgil Card snapshot.
        /// </summary>
        public byte[] Snapshot { get; internal set; }

        /// <summary>
        /// Gets the identity.
        /// </summary>
        public string Identity { get; internal set; }

        /// <summary>
        /// Gets the type of the identity.
        /// </summary>
        public string IdentityType { get; internal set; }

        /// <summary>
        /// Gets the public key.
        /// </summary>
        public byte[] PublicKey { get; internal set; }

        /// <summary>
        /// Gets the scope.
        /// </summary>
        public CardScope Scope { get; internal set; }

        /// <summary>
        /// Gets the data.
        /// </summary>
        public IReadOnlyDictionary<string, string> Data { get; internal set; }

        /// <summary>
        /// Gets the device.
        /// </summary>
        public string Device { get; internal set; }

        /// <summary>
        /// Gets the name of the device.
        /// </summary>
        public string DeviceName { get; internal  set; }

        /// <summary>
        /// Gets the version.
        /// </summary>
        public string Version { get; internal set; }

        /// <summary>
        /// Gets the signs.
        /// </summary>
        public IReadOnlyDictionary<string, byte[]> Signatures { get; internal set; }
    }
}