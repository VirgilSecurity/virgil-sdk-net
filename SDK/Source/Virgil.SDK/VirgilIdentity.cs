namespace Virgil.SDK
{
    /// <summary>
    /// 
    /// </summary>
    public class VirgilIdentity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilIdentity"/> class.
        /// </summary>
        public VirgilIdentity(string value, string type)
        {
            this.Value = value;
            this.Type = type;
        }

        /// <summary>
        /// Gets the value that represents a <see cref="VirgilCard"/> identity.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Gets the value that represents a <see cref="VirgilCard"/> identity type.
        /// </summary>
        public string Type { get; private set; }
    }
}