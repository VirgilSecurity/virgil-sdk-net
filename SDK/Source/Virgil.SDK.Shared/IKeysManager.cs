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

    using Virgil.SDK.Exceptions;

    /// <summary>
    /// The <see cref="IKeysManager"/> interface defines a list of methods to generate the <see cref="VirgilKey"/>s 
    /// and further them storage in secure place. 
    /// </summary>
    public interface IKeysManager
    {
        /// <summary>
        /// Generates a new <see cref="VirgilKey"/> with default parameters.
        /// </summary>
        /// <example>
        /// <code>
        ///     var virgil = new VirgilApi();
        ///     // generate a new Virgil Key
        ///     var aliceKey = virgil.Keys.Generate();
        /// </code>
        /// </example>
        VirgilKey Generate();

        /// <summary>
        /// Loads the <see cref="VirgilKey"/> from current storage by specified key name.
        /// </summary>
        /// <param name="keyName">The name of the Key.</param>
        /// <param name="keyPassword">The Key password.</param>
        /// <returns>An instance of <see cref="VirgilKey"/> class.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="VirgilKeyIsNotFoundException"></exception>
        /// <example>
        ///     <code>
        ///         var virgil = new VirgilApi("[YOUR_ACCESS_TOKEN_HERE]");
        ///         var aliceKey = virgil.Keys.Load("[KEY_NAME]", "[OPTIONAL_KEY_PASSWORD]");
        ///     </code>
        /// </example>
        VirgilKey Load(string keyName, string keyPassword = null);


        /// <summary>
        /// Imports the <see cref="VirgilKey"/> from buffer.
        /// </summary>
        /// <param name="keyBuffer">The buffer with Key.</param>
        /// <param name="keyPassword">The Key password.</param>
        /// <returns>An instance of <see cref="VirgilKey"/> class.</returns>
        /// <example>
        ///     <code>
        ///         var virgil = new VirgilApi();
        ///         // initialize a buffer from base64 encoded string
        ///         var aliceKeyBuffer = VirgilBuffer.From(
        ///         "[BASE64_ENCODED_VIRGIL_KEY]", StringEncoding.Base64);
        ///
        ///         // import Virgil Key from buffer
        ///         var aliceKey = virgil.Keys.Import(aliceKeyBuffer, "[OPTIONAL_KEY_PASSWORD]");
        ///     </code>
        /// </example>
        VirgilKey Import(VirgilBuffer keyBuffer, string keyPassword = null);

        /// <summary>
        /// Removes the <see cref="VirgilKey"/> from the storage.
        /// </summary>
        /// <example>
        ///     <code>
        ///         var virgil = new VirgilApi();
        ///         virgil.Destroy("[VIRGIL_KEY_NAME]");
        ///     </code>
        /// </example>
        void Destroy(string keyName);
    }
}