using System.Text;
using Virgil.SDK.Common;
using Virgil.SDK.Shared.Client.Models;

namespace Virgil.SDK.Shared.Client.Requests
{
    public class SnapshotMaster
    {
        public byte[] TakeSnapshot(ISnapshotModel snapshotModel)
        {
            var snapshotModelJson = JsonSerializer.Serialize((object)snapshotModel);
            var takenSnapshot = Encoding.UTF8.GetBytes(snapshotModelJson);

            return takenSnapshot;
        }

        public TSnapshotModel ParseSnapshot<TSnapshotModel>(byte[] snapshotData)
            where TSnapshotModel : ISnapshotModel
        {
            var jsonRequestModel = Encoding.UTF8.GetString(snapshotData);
            var model = JsonSerializer.Deserialize<TSnapshotModel>(jsonRequestModel);
            return model;
        }
    }
}