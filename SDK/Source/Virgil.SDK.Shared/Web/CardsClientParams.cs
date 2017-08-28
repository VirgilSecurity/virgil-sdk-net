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

namespace Virgil.SDK.Web
{
    using Virgil.SDK.Web.Connection;
    
    public class CardsClientParams
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CardsClientParams"/> class.
        /// </summary>
        public CardsClientParams()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CardsClientParams"/> class.
        /// </summary>
        public CardsClientParams(string accessToken)
        {
            this.AccessToken = accessToken;

            this.CardsServiceAddress = "https://cards.virgilsecurity.com";
            this.ReadOnlyCardsServiceAddress = "https://cards-ro.virgilsecurity.com";
            this.RAServiceAddress = "https://ra.virgilsecurity.com/";
        }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets the cards service address.
        /// </summary>
        public string CardsServiceAddress { get; set; }

        /// <summary>
        /// Gets the cards service connection.
        /// </summary>
        public IConnection CardsServiceConnection { get; set; }

        /// <summary>
        /// Gets the read only cards service address.
        /// </summary>
        public string ReadOnlyCardsServiceAddress { get; set; }

        /// <summary>
        /// Gets the read only cards service connection.
        /// </summary>
        public IConnection ReadOnlyCardsServiceConnection { get; set; }

        /// <summary>
        /// Gets the Registration Authority service address.
        /// </summary>
        public string RAServiceAddress { get; set; }

        /// <summary>
        /// Gets the Registration Authority service address.
        /// </summary>
        public IConnection RAServiceConnection { get; set; }
    }
}
