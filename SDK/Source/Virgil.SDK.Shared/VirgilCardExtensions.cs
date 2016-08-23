#region "Copyright (C) 2015 Virgil Security Inc."
/**
 * Copyright (C) 2015 Virgil Security Inc.
 *
 * Lead Maintainer: Virgil Security Inc. <support@virgilsecurity.com>
 *
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are
 * met:
 *
 *     (1) Redistributions of source code must retain the above copyright
 *     notice, this list of conditions and the following disclaimer.
 *
 *     (2) Redistributions in binary form must reproduce the above copyright
 *     notice, this list of conditions and the following disclaimer in
 *     the documentation and/or other materials provided with the
 *     distribution.
 *
 *     (3) Neither the name of the copyright holder nor the names of its
 *     contributors may be used to endorse or promote products derived from
 *     this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE AUTHOR ''AS IS'' AND ANY EXPRESS OR
 * IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT,
 * INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
 * HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
 * STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING
 * IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
 * POSSIBILITY OF SUCH DAMAGE.
 */
#endregion

namespace Virgil.SDK
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Virgil.SDK;
    using Virgil.SDK.Cryptography;
    using Virgil.SDK.Exceptions;

    public static class VirgilCardExtensions
    {
        public static Task<IEnumerable<VirgilCard>> Select(this Task<IEnumerable<VirgilCard>> promise, Func<VirgilCard, bool> predicate)
        {
            return promise.ContinueWith(task => task.Result.Where(predicate));
        }

        public static Task<VirgilBuffer> ThenEncrypt(this Task<IEnumerable<VirgilCard>> promise, VirgilBuffer data)
        {
            return promise.ContinueWith(task =>
            {
                var cards = task.Result;
                if (!cards.Any())
                {
                    throw new VirgilCardIsNotFoundException();
                }

                var encryptor = ServiceLocator.Resolve<ICryptoService>();
                var recipients = task.Result.ToDictionary(it => it.Id.ToByteArray(), it => it.PublicKey);

                var cipherdata = encryptor.EncryptData(data.ToBytes(), recipients);
                return VirgilBuffer.FromBytes(cipherdata);
            });
        }

        public static Task<VirgilBuffer> ThenVerify(this Task<IEnumerable<VirgilCard>> promise, VirgilBuffer data, VirgilBuffer signature)
        {
            throw new NotImplementedException();
        }

        public static Task<VirgilBuffer> ThenSignAndEncrypt(this Task<IEnumerable<VirgilCard>> promise, VirgilBuffer data, VirgilKey signerKey)
        {
            throw new NotImplementedException();
        }

        public static Task<VirgilBuffer> ThenDecryptAndVerify(this Task<IEnumerable<VirgilCard>> promise, VirgilBuffer cipherdata, VirgilKey decryptKey)
        {
            throw new NotImplementedException();
        }
    }
}