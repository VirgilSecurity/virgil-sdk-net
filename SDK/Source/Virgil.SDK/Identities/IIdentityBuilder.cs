namespace Virgil.SDK.Identities
{
    /// <summary>
    /// Defines methods to create identity information.
    /// </summary>
    public interface IIdentityBuilder
    {
        /// <summary>
        /// Gets built identity.
        /// </summary>
        IdentityInfo GetIdentity();
    }
}