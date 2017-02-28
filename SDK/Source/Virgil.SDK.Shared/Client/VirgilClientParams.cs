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
    using System;

    public class VirgilClientParams
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilClientParams"/> class.
        /// </summary>
        public VirgilClientParams() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilClientParams"/> class.
        /// </summary>
        public VirgilClientParams(string accessToken)
        {
            this.AccessToken = accessToken;

            this.CardsServiceAddress = "https://cards.virgilsecurity.com";
            this.ReadOnlyCardsServiceAddress = "https://cards-ro.virgilsecurity.com";
            this.IdentityServiceAddress = "https://identity.virgilsecurity.com";
			this.RAServiceAddress = "https://ra.virgilsecurity.com/";
        }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        internal string AccessToken { get; }

        /// <summary>
        /// Gets the cards service URL.
        /// </summary>
        internal string CardsServiceAddress { get; private set; }

        /// <summary>
        /// Gets the read only cards service address.
        /// </summary>
        internal string ReadOnlyCardsServiceAddress { get; private set; }

        /// <summary>
        /// Gets the identity service address.
        /// </summary>
        internal string IdentityServiceAddress { get; private set; }

		/// <summary>
		/// Gets the Registration Authority service address.
		/// </summary>
		internal string RAServiceAddress { get; private set; }

		/// <summary>
		/// Sets the Registration Authority service address.
		/// </summary>
		/// <param name="serviceAddress">The service address.</param>
		/// <exception cref="ArgumentException"></exception>
		public void SetRAServiceAddress(string serviceAddress)
		{
			if (string.IsNullOrWhiteSpace(serviceAddress) && !CheckServiceUrl(serviceAddress))
				throw new ArgumentException(nameof(serviceAddress));

			this.RAServiceAddress = serviceAddress;
		}
        
        /// <summary>
        /// Sets the identity service address.
        /// </summary>
        /// <param name="serviceAddress">The service address.</param>
        /// <exception cref="ArgumentException"></exception>
        public void SetIdentityServiceAddress(string serviceAddress)
        {
            if (string.IsNullOrWhiteSpace(serviceAddress) && !CheckServiceUrl(serviceAddress))
                throw new ArgumentException(nameof(serviceAddress));
            
            this.IdentityServiceAddress = serviceAddress;
        }

        /// <summary>
        /// Sets the cards service address.
        /// </summary>
        /// <param name="serviceAddress">The service address.</param>
        /// <exception cref="ArgumentException"></exception>
        public void SetCardsServiceAddress(string serviceAddress)
        {
            if (string.IsNullOrWhiteSpace(serviceAddress) && !CheckServiceUrl(serviceAddress))
                throw new ArgumentException(nameof(serviceAddress));

            this.CardsServiceAddress = serviceAddress;
        }

        /// <summary>   
        /// Sets the cards service address.
        /// </summary>
        /// <param name="serviceAddress">The service address.</param>
        /// <exception cref="ArgumentException"></exception>
        public void SetReadCardsServiceAddress(string serviceAddress)
        {
            if (string.IsNullOrWhiteSpace(serviceAddress) && !CheckServiceUrl(serviceAddress))
                throw new ArgumentException(nameof(serviceAddress));

            this.ReadOnlyCardsServiceAddress = serviceAddress;
        }
        
        private static bool CheckServiceUrl(string serviceUrl)
        {
            Uri uriResult;
            var isValid = Uri.TryCreate(serviceUrl, UriKind.Absolute, out uriResult)
                          && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            return isValid;
        }
    }
}