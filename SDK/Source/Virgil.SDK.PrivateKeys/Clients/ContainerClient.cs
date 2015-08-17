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
        /// <param name="connection">The connection.</param>
        public ContainerClient(IConnection connection) : base(connection)
        {
        }

        /// <summary>
        /// Creates the Private Keys storage account.
        /// </summary>
        /// <param name="containerType">The account ID type.</param>
        /// <param name="publicKeyId">The public key ID from Keys service.</param>
        /// <param name="privateKey">The public key ID digital signature. Verifies the possession of the private key.</param>
        /// <param name="password">The account password.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task Initialize(ContainerType containerType, Guid publicKeyId, byte[] privateKey, string password)
        {
            var body = new
            {
                container_type = containerType == ContainerType.Easy ? "easy" : "normal",
                password,
                request_sign_random_uuid = Guid.NewGuid().ToString()
            };

            var request = Request.Create(RequestMethod.Post)
                .WithEndpoint("/v2/container")
                .WithBody(body)
                .SignRequest(publicKeyId, privateKey);

            await this.Send<CreateAccountResult>(request);
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
                request_sign_random_uuid = Guid.NewGuid().ToString()
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
                .WithEndpoint("/v2/container/reset-password")
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
                .WithEndpoint("/v2/container/confirm")
                .WithBody(body);

            await this.Send(request);
        }
    }
}