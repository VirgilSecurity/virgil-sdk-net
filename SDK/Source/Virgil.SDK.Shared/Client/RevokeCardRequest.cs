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
    /// <summary>
    /// Represents an information about revoking card request.
    /// </summary>
    public class RevokeCardRequest : SignableRequest<RevokeCardModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RevokeCardRequest"/> class.
        /// </summary>
        private RevokeCardRequest()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RevokeCardRequest"/> class.
        /// </summary>
        /// <param name="snapshotModel">The snapshot model.</param>
        public RevokeCardRequest(RevokeCardModel snapshotModel) : base(snapshotModel)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RevokeCardRequest" /> class.
        /// </summary>
        /// <param name="cardId">The card ID to be revoked.</param>
        /// <param name="reason">The revocation reason.</param>
        public RevokeCardRequest(string cardId, RevocationReason reason) 
            : base(new RevokeCardModel { CardId = cardId, Reason = reason })
        {
        }

        /// <summary>
        /// Imports the <see cref="RevokeCardRequest"/> from its string representation.
        /// </summary>
        /// <param name="exportedRequest">The exported request.</param>
        public static RevokeCardRequest Import(string exportedRequest)
        {
            var request = new RevokeCardRequest();
            request.ImportRequest(exportedRequest);
            return request;
        }
    }
}