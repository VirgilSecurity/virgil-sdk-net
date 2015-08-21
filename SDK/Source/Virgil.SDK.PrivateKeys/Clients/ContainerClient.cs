namespace Virgil.SDK.PrivateKeys.Clients
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    
    using Virgil.SDK.PrivateKeys.Model;
    using Virgil.SDK.PrivateKeys.Http;
    using Virgil.SDK.PrivateKeys.TransferObject;

    /// <summary>
    /// Provides common methods to interact with Account (of Private Keys API) resource endpoints.
    /// </summary>
    public class ContainerClient : EndpointClient, IContainerClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerClient"/> class.
        /// </summary>
        public ContainerClient(IConnection connection) : base(connection)
        {
        }

        /// <summary>
        /// Initializes an container for private keys storage. It is important to have public key published on Virgil Keys service.
        /// </summary>
        /// <param name="containerType">The type of private keys container.</param>
        /// <param name="publicKeyId">The unique identifier of public key in Virgil Keys service.</param>
        /// <param name="privateKey">The private key is a part of public key which was published on Virgil Keys service.</param>
        /// <param name="password">This password will be used to authenticate access to the container keys.</param>
        public async Task Initialize(ContainerType containerType, Guid publicKeyId, byte[] privateKey, string password)
        {
            var body = new
            {
                container_type = containerType == ContainerType.Easy ? "easy" : "normal",
                password,
                request_sign_uuid = Guid.NewGuid().ToString()
            };

            var request = Request.Create(RequestMethod.Post)
                .WithEndpoint("/v2/container")
                .WithBody(body)
                .SkipAuthentication()
                .SignRequest(publicKeyId, privateKey);

            await this.Send<CreateAccountResult>(request);
        }

        /// <summary>
        /// Gets the type of private keys container by public key identifier from Virgil Keys service.
        /// </summary>
        /// <param name="publicKeyId">The unique identifier of public key in Virgil Keys service.</param>
        /// <returns>The type of container.</returns>
        public async Task<ContainerType> GetContainerType(Guid publicKeyId)
        {
            var request = Request.Create(RequestMethod.Get)
                .WithEndpoint($"/v2/container/public-key-id/{publicKeyId}");

            var result = await this.Send<GetContainerTypeResult>(request);

            return result.ContainerType == "easy" ? ContainerType.Easy : ContainerType.Normal;
        }

        /// <summary>
        /// Removes account from Private Keys Service.
        /// </summary>
        /// <param name="publicKeyId">The public key ID.</param>
        /// <param name="privateKey">The public key ID digital signature. Verifies the possession of the private key.</param>
        public async Task Remove(Guid publicKeyId, byte[] privateKey)
        {
            var body = new
            {
                request_sign_uuid = Guid.NewGuid().ToString()
            };

            var request = Request.Create(RequestMethod.Delete)
                .WithEndpoint("/v2/container")
                .WithBody(body)
                .SignRequest(publicKeyId, privateKey);

            await this.Send(request);
        }

        /// <summary>
        /// Resets the account password.
        /// </summary>
        /// <param name="email">The User's email</param>
        /// <param name="newPassword">New Private Keys account password.</param>
        public async Task ResetPassword(string email, string newPassword)
        {
            var body = new
            {
                user_data = new
                {
                    @class = "user_id",
                    type = "email",
                    value = email
                },
                new_password = newPassword
            };

            var request = Request.Create(RequestMethod.Put)
                .WithEndpoint("/v2/container/actions/reset-password")
                .WithBody(body);

            await this.Send(request);
        }
        
        /// <summary>
        /// Verifies the Private Keys account password reset.
        /// </summary>
        /// <param name="token">The confirmation token.</param>
        /// <returns>
        /// The list of account's private keys.
        /// </returns>
        public async Task Confirm(string token)
        {
            var body = new { token };

            var request = Request.Create(RequestMethod.Put)
                .WithEndpoint("/v2/container/actions/persist")
                .WithBody(body);

            await this.Send(request);
        }
    }
}