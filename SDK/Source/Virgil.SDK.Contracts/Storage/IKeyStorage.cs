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

namespace Virgil.SDK.Storage
{
    using Virgil.SDK.Exceptions;

    /// <summary>
    /// This interface describes a storage facility for cryptographic keys.
    /// </summary>
    public interface IKeyStorage
    {
        /// <summary>
        /// Stores the key to the given alias.
        /// </summary>
        /// <param name="keyEntry">The key entry.</param>
        /// <exception cref="KeyEntryAlreadyExistsException"></exception>
        void Store(KeyEntry keyEntry);

        /// <summary>
        /// Loads the key associated with the given alias.
        /// </summary>
        /// <param name="keyName">The name.</param>
        /// <returns>
        /// The requested key, or null if the given alias does not exist or does
        /// not identify a key-related entry.
        /// </returns>
        /// <exception cref="KeyEntryNotFoundException"></exception>
        KeyEntry Load(string keyName);

        /// <summary>
        /// Checks if the key exists in this storage by given alias.
        /// </summary>
        /// <param name="keyName">The alias name.</param>
        /// <returns>true if the key exists, false otherwise</returns>
        bool Exists(string keyName);

        /// <summary>
        /// Checks if the given alias exists in this keystore.
        /// </summary>
        /// <param name="keyName">The alias name.</param>
        /// <exception cref="KeyEntryNotFoundException"></exception>
        void Delete(string keyName);

        /// <summary>
        /// Returns the list of keynames
        /// </summary>
        string[] Names();
    }
}
