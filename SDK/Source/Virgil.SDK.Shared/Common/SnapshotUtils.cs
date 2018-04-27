#region Copyright (C) Virgil Security Inc.
// Copyright (C) 2015-2018 Virgil Security Inc.
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
using System;
using System.Collections.Generic;
using System.Text;
using Virgil.SDK;
using Virgil.SDK.Common;

namespace Virgil.SDK.Common
{
    public class SnapshotUtils
    {
        /// <summary>
        /// Get snapshot of specified object.
        /// </summary>
        /// <param name="info">the object to get snapshot from.</param>
        /// <returns>snapshot data.</returns>
        public static byte[] TakeSnapshot(object info)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }
            var snapshotModelJson = Configuration.Serializer.Serialize(info);
            var takenSnapshot = Bytes.FromString(snapshotModelJson);

            return takenSnapshot;
        }

        /// <summary>
        /// Gets object by its snapshot. 
        /// </summary>
        /// <typeparam name="TSnaphotModel">the type of object that we expect to receive.</typeparam>
        /// <param name="snapshot">the snapshot to get the object from.</param>
        /// <returns>object</returns>
        public static TSnaphotModel ParseSnapshot<TSnaphotModel>(byte[] snapshot)
        {
            if (snapshot == null)
            {
                throw new ArgumentNullException(nameof(snapshot));
            }
            var snapshotModelJson = Bytes.ToString(snapshot);
            var snapshotModel = Configuration.Serializer.Deserialize<TSnaphotModel>(snapshotModelJson);

            return snapshotModel;
        }
    }
}
