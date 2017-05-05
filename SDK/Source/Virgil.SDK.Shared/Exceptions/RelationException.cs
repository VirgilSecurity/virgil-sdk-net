namespace Virgil.SDK.Exceptions
{
    /// <summary>
    /// Represents errors occurred during adding and removing relation.
    /// </summary>
    /// <seealso cref="Virgil.SDK.Exceptions.VirgilException" />
    public class RelationException : VirgilException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RelationException"/> class.
        /// </summary>
        public RelationException() : 
            base("Request is not valid. Request must have snapshot and exactly 1 relation signature.")
        {
        }
    }
}
