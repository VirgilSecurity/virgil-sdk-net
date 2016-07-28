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

namespace Virgil.SDK.Cryptography
{
    /// <summary>
    /// The type of key pair.
    /// </summary>
    public enum KeyPairType
    {
        /// <summary>
        /// Recommended most safe type
        /// </summary>
        Default,

        /// <summary>
        /// RSA 256 bit (not recommended)
        /// </summary>
        RSA_256,

        /// <summary>
        /// RSA 512 bit (not recommended)
        /// </summary>
        RSA_512,

        /// <summary>
        /// RSA 1024 bit (not recommended)
        /// </summary>
        RSA_1024,

        /// <summary>
        /// RSA 2048 bit 
        /// </summary>
        RSA_2048,

        /// <summary>
        /// RSA 3072 bit 
        /// </summary>
        RSA_3072,

        /// <summary>
        /// RSA 4096 bit 
        /// </summary>
        RSA_4096,

        /// <summary>
        /// RSA 8192 bit 
        /// </summary>
        RSA_8192,

        /// <summary>
        /// 192-bits NIST curve
        /// </summary>
        EC_SECP192R1,

        /// <summary>
        /// 224-bits NIST curve
        /// </summary>
        EC_SECP224R1,

        /// <summary>
        /// 256-bits NIST curve
        /// </summary>
        EC_SECP256R1,

        /// <summary>
        /// 384-bits NIST curve
        /// </summary>
        EC_SECP384R1,

        /// <summary>
        /// 521-bits NIST curve
        /// </summary>
        EC_SECP521R1,

        /// <summary>
        /// 256-bits Brainpool curve
        /// </summary>
        EC_BP256R1,

        /// <summary>
        /// 384-bits Brainpool curve
        /// </summary>
        EC_BP384R1,

        /// <summary>
        /// 512-bits Brainpool curve
        /// </summary>
        EC_BP512R1,

        /// <summary>
        /// Curve25519
        /// </summary>
        EC_Curve25519,

        /// <summary>
        /// 192-bits "Koblitz" curve
        /// </summary>
        EC_SECP192K1,

        /// <summary>
        /// 224-bits "Koblitz" curve
        /// </summary>
        EC_SECP224K1,

        /// <summary>
        /// 256-bits "Koblitz" curve
        /// </summary>
        EC_SECP256K1
    }
}