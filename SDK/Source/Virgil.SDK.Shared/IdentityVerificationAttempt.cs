#region Copyright (C) Virgil Security Inc.
// Copyright (C) 2015-2017 Virgil Security Inc.
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

namespace Virgil.SDK
{
	using System;
	using System.Threading.Tasks;

    /// <summary>
	/// The <see cref="IdentityVerificationAttempt"/> class providesd information about identity verification process.
	/// </summary>
	public class IdentityVerificationAttempt
	{
        private readonly VirgilApiContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityVerificationAttempt"/> class.
        /// </summary>
        internal IdentityVerificationAttempt(VirgilApiContext context)
        {
            this.context = context;
        }

        /// <summary>
		/// Gets the operation action ID.
		/// </summary>
		public Guid ActionId { get; internal set; }

        /// <summary>
        /// Gets the identity value.
        /// </summary>
        public string Identity { get; internal set; }

        /// <summary>
        /// Gets the type of the identity.
        /// </summary>
        public string IdentityType { get; internal set; }

        /// <summary>
        /// Gets the time to live.
        /// </summary>
        public TimeSpan TimeToLive { get; internal set; }

        /// <summary>   
		/// Gets a key/value dictionary with user fields.
		/// </summary>
		public int CountToLive { get; internal set; }

        /// <summary>
        /// Confirms an identity and generates a validation token that can be used to perform operations like 
        /// Publish and Revoke global Cards. 
        /// </summary>
        /// <param name="confirmation">The confirmation </param>
        /// <returns>A new instance of <see cref="IdentityValidationToken"/> class.</returns>
        public async Task<IdentityValidationToken> ConfirmAsync(IdentityConfirmation confirmation)
        {
            var emailConfirmation = confirmation as EmailConfirmation;

            if (emailConfirmation == null)
                throw new NotSupportedException();

            var validationToken = await confirmation.ConfirmAndGrabValidationTokenAsync(this, this.context.IdentityClient)
                .ConfigureAwait(false);

            return new IdentityValidationToken { Value = validationToken };
        }
	}
}