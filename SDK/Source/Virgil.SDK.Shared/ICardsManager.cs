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
        /// <example>
        ///     <code>
        ///         var virgil = new VirgilApi("[YOUR_ACCESS_TOKEN_HERE]");
        ///         
        ///         // generate a new Virgil Key
        ///         var aliceKey = virgil.Keys.Generate()
        ///         
        /// 
        ///         // save the Virgil Key into the storage
        ///         aliceKey.Save("[KEY_NAME]", "[KEY_PASSWORD]");
        ///         // create a Virgil Card
        ///         var aliceCard = virgil.Cards.Create("alice", aliceKey);
        ///         // DEVELOPERS HAVE TO TRANSMIT THE VIRGIL CARD TO THE APP'S 
        ///         // SERVER SIDE WHERE IT WILL BE SIGNED, VALIDATED AND THEN PUBLISHED ON 
        ///         // VIRGIL SERVICES (THIS IS NECESSARY FOR FURTHER OPERATIONS WITH THE VIRGIL CARD).
        ///     </code>
        /// </example>
        /// How to export the Virgil Card <see cref="VirgilCard.Export"/>
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
        /// <example>
        ///     <code>
        ///         var virgil = new VirgilApi();
        ///         
        ///         // generate a new Virgil Key
        ///         var aliceKey = virgil.Keys.Generate()
        ///         
        ///         // save the Virgil Key into the storage
        ///         aliceKey.Save("[KEY_NAME]", "[KEY_PASSWORD]");
        ///         
        ///         // create a Global Virgil Card 
        ///         var aliceGlobalCard = virgil.Cards.CreateGlobal(
        ///             identity: "alice@virgilsecurity.com",
        ///             identityType: IdentityType.Email,
        ///             ownerKey: aliceKey);
        ///     </code>
        /// </example>
        VirgilCard CreateGlobal(string identity, IdentityType identityType, VirgilKey ownerKey,
            Dictionary<string, string> customFields = null);


        /// <summary>
        /// Finds a <see cref="VirgilCard"/>s by specified identities in application scope.
        /// </summary>
        /// <param name="identities">The list of identities.</param>
        /// <returns>A list of found <see cref="VirgilCard"/>s.</returns>
        /// <example>
        ///     <code>
        ///         var virgil = new VirgilApi("[YOUR_ACCESS_TOKEN_HERE]");
        ///         // search for all User's Virgil Cards.
        ///         var aliceCards = await virgil.Cards.FindAsync("alice");
        ///     </code>
        /// </example>
        Task<IEnumerable<VirgilCard>> FindAsync(params string[] identities);


        /// <summary>
        /// Finds <see cref="VirgilCard"/>s by specified identities and type in application scope.
        /// </summary>
        /// <param name="identityType">Type of identity</param>
        /// <param name="identities">The list of sought identities</param>
        /// <returns>A list of found <see cref="VirgilCard"/>s.</returns>
        /// <example>
        ///     <code>
        ///         var virgil = new VirgilApi("[YOUR_ACCESS_TOKEN_HERE]");
        ///         // search for all User's Virgil Cards with identity type 'member'
        ///         var bobCards = await virgil.Cards.FindAsync("member", new[] { "bob" });
        ///     </code>
        /// </example>
        Task<IEnumerable<VirgilCard>> FindAsync(string identityType, IEnumerable<string> identities);


        /// <summary>
        /// Finds <see cref="VirgilCard"/>s by specified identities and type in global scope.
        /// </summary>
        /// <param name="identities">The list of sought identities</param>
        /// <returns>A list of found <see cref="VirgilCard"/>s.</returns>
        Task<IEnumerable<VirgilCard>> FindGlobalAsync(params string[] identities);


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
        /// <example>
        /// <code>
        ///            var virgil = new VirgilApi(new VirgilApiContext
        ///            {
        ///                    AccessToken = "[YOUR_ACCESS_TOKEN_HERE]",
        ///                    Credentials = new AppCredentials
        ///                {
        ///                    AppId = "[YOUR_APP_ID_HERE]",
        ///                    AppKey = VirgilBuffer.FromFile("[YOUR_APP_KEY_FILEPATH_HERE]"),
        ///                    AppKeyPassword = "[YOUR_APP_KEY_PASSWORD_HERE]",
        ///                }
        ///             });
        ///             
        ///             // import a Virgil Card from string
        ///             var aliceCard = virgil.Cards.Import(exportedAliceCard);
        /// </code>
        /// How to get exportedAliceCard <see cref="VirgilCard.Export"/>
        /// </example>
        VirgilCard Import(string exportedCard);


        /// <summary>
        /// Publishes a <see cref="VirgilCard"/> into global Virgil Services scope.
        /// </summary>
        /// <param name="card">The Card to be published.</param>
        /// <param name="token">The identity validation token.</param>
        /// <example>
        ///     <code>
        ///         var virgil = new VirgilApi();
        ///         
        ///         // initiate identity verification process
        ///         var attempt = await aliceGlobalCard.CheckIdentityAsync();
        ///
        ///         // confirm an identity and grab the validation token
        ///         var token = await attempt.ConfirmAsync(new EmailConfirmation("[CONFIRMATION_CODE]"));
        ///
        ///         // publish the Virgil Card
        ///         await virgil.Cards.PublishGlobalAsync(aliceGlobalCard, token);
        ///     </code>
        /// </example>
        /// <remarks>
        /// How to get aliceGlobalCard 
        /// <see cref="ICardsManager.CreateGlobal(string, IdentityType, VirgilKey, Dictionary{string, string})"/>
        /// </remarks>
        /// <remarks>
        /// Initiates an identity verification process for current Card identity type. 
        /// <see cref="VirgilCard.CheckIdentityAsync(IdentityVerificationOptions)"/>
        /// </remarks>
        /// <remarks>
        ///  Confirms an identity and generates a validation token that can
        ///  be used to perform operations like Publish and Revoke global Cards.
        ///  <see cref="IdentityVerificationAttempt.ConfirmAsync(IdentityConfirmation)"/>
        ///  </remarks>
        Task PublishGlobalAsync(VirgilCard card, IdentityValidationToken token);


        /// <summary>
        /// Publishes a <see cref="VirgilCard"/> into application Virgil Services scope.
        /// </summary>
        /// <param name="card">The Card to be published.</param>
        /// <example>
        /// <code>
        ///            var virgil = new VirgilApi(new VirgilApiContext
        ///            {
        ///                    AccessToken = "[YOUR_ACCESS_TOKEN_HERE]",
        ///                    Credentials = new AppCredentials
        ///                {
        ///                    AppId = "[YOUR_APP_ID_HERE]",
        ///                    AppKey = VirgilBuffer.FromFile("[YOUR_APP_KEY_FILEPATH_HERE]"),
        ///                    AppKeyPassword = "[YOUR_APP_KEY_PASSWORD_HERE]",
        ///                }
        ///             });
        ///             
        ///             // publish a Virgil Card
        ///             await virgil.Cards.PublishAsync(aliceCard);
        /// </code>
        /// How to get aliceCard <see cref="ICardsManager.Import(string)"/>
        /// </example>
        Task PublishAsync(VirgilCard card);


        /// <summary>
        /// Revokes a <see cref="VirgilCard"/> from Virgil Services. 
        /// </summary>
        /// <param name="card">The card to be revoked.</param>
        /// <example>
        ///     <code>
        ///            var virgil = new VirgilApi(new VirgilApiContext
        ///            {
        ///                    AccessToken = "[YOUR_ACCESS_TOKEN_HERE]",
        ///                    Credentials = new AppCredentials
        ///                {
        ///                    AppId = "[YOUR_APP_ID_HERE]",
        ///                    AppKey = VirgilBuffer.FromFile("[YOUR_APP_KEY_FILEPATH_HERE]"),
        ///                    AppKeyPassword = "[YOUR_APP_KEY_PASSWORD_HERE]",
        ///                }
        ///             });
        ///             
        ///             // get a Virgil Card by ID
        ///             var aliceCard = await virgil.Cards.GetAsync("[USER_CARD_ID_HERE]");
        ///
        ///             // revoke a Virgil Card
        ///             await virgil.Cards.RevokeAsync(aliceCard);
        ///     </code>
        /// Get card by Id <see cref="ICardsManager.GetAsync(string)"/>
        /// </example>
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