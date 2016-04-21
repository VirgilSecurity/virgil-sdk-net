namespace Virgil.SDK.Common
{
    using Virgil.SDK.Models;
    using Virgil.SDK.TransferObject;

    /// <summary>
    /// Provides a set of methods for defining and creating identities.
    /// </summary>
    public class IdentityCreator
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="IdentityCreator"/> class from being created.
        /// </summary>
        private IdentityCreator()
        {
        }

        /// <summary>
        /// Creates an identity with email type.
        /// </summary>
        public static IdentityInfo Email(string email)
        {
            return new IdentityInfo
            {
                Type = IdentityType.Email,
                Value = email
            };
        }

        /// <summary>
        /// Creates an identity with custom type.
        /// </summary>
        public static IdentityInfo Custom(string identity)
        {
            return new IdentityInfo
            {
                Type = IdentityType.Custom,
                Value = identity
            };
        }

        /// <summary>
        /// Creates an identity with hashed identity value.
        /// </summary>
        public static IdentityInfo Hash(string identity)
        {
            return new IdentityInfo
            {
                Type = IdentityType.Custom,
                Value = identity
            };
        }
    }
}