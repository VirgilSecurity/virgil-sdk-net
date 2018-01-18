using System;
using System.Collections.Generic;
using System.Text;
using Virgil.SDK;
using Virgil.SDK.Common;

namespace Virgil.SDK.Common
{
    public class SnapshotUtils
    {
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
