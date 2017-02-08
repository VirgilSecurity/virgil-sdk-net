namespace Virgil.SDK.Common
{
	using System.Text;

	/// <summary>
	/// The <see cref="Snapshotter"/> class provides a list of methods to take an accurate snapshot of the object, 
	/// and convert it into the binary data.
	/// </summary>
	internal class Snapshotter
	{
		internal byte[] Capture(object snapshotModel)
		{
            var snapshotModelJson = JsonSerializer.Serialize(snapshotModel);
			var takenSnapshot = Encoding.UTF8.GetBytes(snapshotModelJson);

			return takenSnapshot;
		}
	}
}
