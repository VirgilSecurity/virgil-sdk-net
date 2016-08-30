namespace Virgil.SDK.Cryptography
{
    public abstract class ObjectHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectHandle" /> class.
        /// </summary>
        /// <param name="objectId">The object identifier.</param>
        protected internal ObjectHandle(uint objectId)
        {
            this.ObjectId = objectId;
        }

        internal uint ObjectId { get; }
    }
}