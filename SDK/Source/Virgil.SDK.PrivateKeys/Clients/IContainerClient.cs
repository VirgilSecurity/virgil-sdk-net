namespace Virgil.SDK.PrivateKeys.Clients
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Virgil.SDK.PrivateKeys.Model;

    /// <summary>
    /// Provides common methods to interact with private keys container on Virgil Private Keys service.
    /// </summary>
    public interface IContainerClient
    {
        /// <summary>
        /// Initializes an container for private keys storage. It is important to have public key published on Virgil Keys service.
        /// </summary>
        /// <param name="containerType">The type of private keys container.</param>
        /// <param name="publicKeyId">The unique identifier of public key in Virgil Keys service.</param>
        /// <param name="privateKey">The private key is a part of public key which was published on Virgil Keys service.</param>
        /// <param name="password">This password will be used to authenticate access to the container keys.</param>
        Task Initialize(ContainerType containerType, Guid publicKeyId, byte[] privateKey, string password);

        /// <summary>
        /// Gets the type of private keys container by public key identifier from Virgil Keys service.
        /// </summary>
        /// <param name="publicKeyId">The unique identifier of public key in Virgil Keys service.</param>
        /// <returns>The type of container.</returns>
        Task<ContainerType> GetContainerType(Guid publicKeyId);
        
        /// <summary>
        /// Removes account from Private Keys Service.
        /// </summary>
        /// <param name="publicKeyId">The public key ID.</param>
        /// <param name="privateKey">The private key</param>
        Task Remove(Guid publicKeyId, byte[] privateKey);

        /// <summary>
        /// Resets the account password.
        /// </summary>
        /// <param name="userId">The user ID from Keys service.</param>
        /// <param name="newPassword">New Private Keys account password.</param>
        Task ResetPassword(string userId, string newPassword);

        /// <summary>
        /// Verifies the Private Keys account password reset.
        /// </summary>
        /// <param name="token">The confirmation token.</param>
        /// <returns>The list of account's private keys.</returns>
        Task Confirm(string token);
    }
}