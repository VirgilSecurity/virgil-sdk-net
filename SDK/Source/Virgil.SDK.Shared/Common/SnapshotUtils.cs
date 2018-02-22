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
