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
    using System.Text;
    using Common;

    public class RevokeCardRequest : SignableRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RevokeCardRequest"/> class.
        /// </summary>
        internal RevokeCardRequest()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RevokeCardRequest"/> class.
        /// </summary>
        public RevokeCardRequest(string cardId, RevocationReason reason)
        {
            this.CardId = cardId;
            this.Reason = reason;
        }

        /// <summary>
        /// Gets the card identifier.
        /// </summary>
        public string CardId { get; private set; }

        /// <summary>
        /// Gets a revocation reason.
        /// </summary>
        public RevocationReason Reason { get; private set; }
        
        protected override void RestoreRequest(byte[] snapshot, Dictionary<string, byte[]> signatures)
        {
            this.takenSnapshot = snapshot;
            this.acceptedSignatures = signatures;

            var json = Encoding.UTF8.GetString(snapshot);
            var details = JsonSerializer.Deserialize<RevokeCardModel>(json);

            this.CardId = details.CardId;   
            this.Reason = details.Reason;
        }

        /// <summary>
        /// Takes the request snapshot.
        /// </summary>
        protected override byte[] TakeSnapshot()
        {
            var model = new RevokeCardModel
            {
                CardId = this.CardId,
                Reason = this.Reason
            };

            var json = JsonSerializer.Serialize(model);
            var snapshot = Encoding.UTF8.GetBytes(json);

            return snapshot;
        }
    }
}