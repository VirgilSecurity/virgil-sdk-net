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
S
namespace Virgil.SDK.Client
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// The <see cref="IdentityVerificationOptions"/> class provides additional options 
	/// for verification <see cref="VirgilCard"/>'s identity.
	/// </summary>
	public class IdentityTokenOptions
	{
        public IdentityTokenOptions()
        {
            TimeToLive = TimeSpan.FromSeconds(3600);
            CountToLive = 1;
            ExtraFields = new Dictionary<string, string>();

        }
        /// <summary>
        /// Gets or sets a key/value dictionary that represents a user fields. In some cases it could be necessary 
        /// to pass some parameters to verification server and receive them back in an email. For this special 
        /// case an optional <see cref="ExtraFields"/> dictionary property can be used. If type of an 
        /// identity is email, all values passed in <see cref="ExtraFields"/> will be passed back in an email in a 
        /// hidden form with extra hidden fields.
        /// </summary>
        public IDictionary<string, string> ExtraFields { get; set; }

        /// <summary>
        /// Gets or sets the "time to live" value is used to limit the lifetime of the token in 
        /// seconds (maximum value is 60 * 60 * 24 * 365 = 1 year). Default <see cref="TimeToLive"/> value is 3600.
        /// </summary>
        public TimeSpan TimeToLive { get; set; }

		/// <summary>
		/// Gets or sets the "count to live" parameter is used to restrict the number of validation token
		/// usages (maximum value is 100).
		/// </summary>
		public int CountToLive { get; set; }
	}
}