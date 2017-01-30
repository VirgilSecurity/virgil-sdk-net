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

namespace Virgil.SDK
{
	using System.Collections.Generic;
	using System.Threading.Tasks;

	/// <summary>
	/// The <see cref="CardsManager"/> interface defines a list of methods to manage the <see cref="VirgilCard"/>s.
	/// </summary>
	public interface ICardsManager
	{
	    /// <summary>
	    /// Creates a new <see cref="VirgilCard"/> that is representing user's Public key and information 
	    /// about identity. This card has to be published to the Virgil's services.
	    /// </summary>
	    /// <param name="identity">The user's identity.</param>
	    /// <param name="identityType">Type of the identity.</param>
	    /// <param name="ownerKey">The owner's <see cref="VirgilKey"/>.</param>
	    /// <param name="customFields">The custom fields (optional).</param>
	    /// <returns>A new instance of <see cref="VirgilCard"/> class, that is representing user's Public key.</returns>
	    VirgilCard Create(string identity, VirgilKey ownerKey,
            string identityType = "unknown", 
            Dictionary<string, string> customFields = null);

	    /// <summary>
	    /// Creates a new global <see cref="VirgilCard"/> that is representing user's 
	    /// Public key and information about identity. 
	    /// </summary>
	    /// <param name="identity">The user's identity value.</param>
	    /// <param name="identityType">Type of the identity.</param>
	    /// <param name="ownerKey">The owner's <see cref="VirgilKey"/>.</param>
	    /// <param name="customFields">The custom fields (optional).</param>
	    /// <returns>A new instance of <see cref="VirgilCard"/> class, that is representing user's Public key.</returns>
	    VirgilCard CreateGlobal(string identity, IdentityType identityType, VirgilKey ownerKey,
	        Dictionary<string, string> customFields = null);

	    /// <summary>
	    /// Finds a <see cref="VirgilCard"/>s by specified identities in application scope.
	    /// </summary>
	    /// <param name="identities">The list of identities.</param>
	    /// <returns>A list of found <see cref="VirgilCard"/>s.</returns>
	    Task<IEnumerable<VirgilCard>> FindAsync(params string[] identities);

        /// <summary>
        /// Finds <see cref="VirgilCard"/>s by specified identities and type in application scope.
        /// </summary>
        /// <param name="identityType">Type of identity</param>
        /// <param name="identities">The list of sought identities</param>
        /// <returns>A list of found <see cref="VirgilCard"/>s.</returns>
        Task<IEnumerable<VirgilCard>> FindAsync(string identityType, IEnumerable<string> identities);

        /// <summary>
        /// Finds <see cref="VirgilCard"/>s by specified identities and type in global scope.
        /// </summary>
        /// <param name="identityType">Type of identity</param>
        /// <param name="identities">The list of sought identities</param>
        /// <returns>A list of found <see cref="VirgilCard"/>s.</returns>
        Task<IEnumerable<VirgilCard>> FindGlobalAsync(IdentityType identityType, params string[] identities);

	    /// <summary>
	    /// Imports a <see cref="VirgilCard"/> from specified buffer.
	    /// </summary>
	    /// <param name="exportedCard">The Card in string representation.</param>
	    /// <returns>An instance of <see cref="VirgilCard"/>.</returns>
	    VirgilCard Import(string exportedCard);

	    /// <summary>
	    /// Publishes a <see cref="VirgilCard"/> into global Virgil Services scope.
	    /// </summary>
	    /// <param name="card">The Card to be published.</param>
	    /// <param name="token">The identity validation token.</param>
	    Task PublishGlobalAsync(VirgilCard card, IdentityValidationToken token);

	    /// <summary>
	    /// Publishes a <see cref="VirgilCard"/> into application Virgil Services scope.
	    /// </summary>
	    /// <param name="card">The Card to be published.</param>
	    Task PublishAsync(VirgilCard card);

	    /// <summary>
	    /// Revokes a <see cref="VirgilCard"/> from Virgil Services. 
	    /// </summary>
	    /// <param name="card">The card to be revoked.</param>
	    Task RevokeAsync(VirgilCard card);

	    /// <summary>
	    /// Revokes a global <see cref="VirgilCard"/> from Virgil Security services.
	    /// </summary>
	    /// <param name="card">The Card to be revoked.</param>
	    /// <param name="key">The Key associated with the revoking Card.</param>
	    /// <param name="identityToken">The identity token.</param>
	    Task RevokeGlobalAsync(VirgilCard card, VirgilKey key, IdentityValidationToken identityToken);

        /// <summary>
        /// Gets the <see cref="VirgilCard"/> by specified ID.
        /// </summary>
        /// <param name="cardId">The Card identifier.</param>
        Task<VirgilCard> GetAsync(string cardId);
	}
}