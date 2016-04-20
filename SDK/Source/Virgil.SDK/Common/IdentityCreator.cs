namespace Virgil.SDK.Common
{
    using System;

    using Virgil.SDK.Models;
    using Virgil.SDK.TransferObject;

    /// <summary>
    /// Provides a set of methods for creating identities.
    /// </summary>
    public class IdentityCreator
    {
        /// <summary>
        /// Creates an identity with email type.
        /// </summary>
        public static IdentityDescriptionModel Email(string email, string validationToken = null)
        {
            return new IdentityDescriptionModel
            {
                Type = IdentityType.Email,
                Value = email,
                ValidationToken = validationToken
            };
        }

        /// <summary>
        /// Creates an identity with custom type.
        /// </summary>
        public static IdentityDescriptionModel Custom(string identity, string validationToken = null)
        {
            return new IdentityDescriptionModel
            {
                Type = IdentityType.Custom,
                Value = identity,
                ValidationToken = validationToken
            };
        }

        /// <summary>
        /// Creates an identity with hashed identity value.
        /// </summary>
        public static IdentityDescriptionModel Hash(string identity, string validationToken = null)
        {
            return new IdentityDescriptionModel
            {
                Type = IdentityType.Custom,
                Value = identity,
                ValidationToken = validationToken
            };
        }
    }
}