namespace Virgil.SDK.Client
{
    using System.Runtime.Serialization;

    public class SerializableObject
    {
		[OnSerialized]
		internal void OnSerializedMethod(StreamingContext context)
		{
		}
    }
}
