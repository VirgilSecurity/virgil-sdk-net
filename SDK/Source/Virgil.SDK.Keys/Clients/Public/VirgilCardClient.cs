using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Virgil.Crypto;
using Virgil.SDK.Keys.Helpers;
using Virgil.SDK.Keys.Http;
using Virgil.SDK.Keys.TransferObject;

namespace Virgil.SDK.Keys.Clients
{
    using System.Text;
    using Newtonsoft.Json;

    /// <summary>
    /// Provides common methods to interact with Virgil Card resource endpoints.
    /// </summary>
    /// <seealso cref="Virgil.SDK.Keys.Clients.EndpointClient" />
    public class VirgilCardClient : EndpointClient, IVirgilCardClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilCardClient"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public VirgilCardClient(IConnection connection) : base(connection)
        {
        }

        /// <summary>
        /// Creates new virgil card.
        /// </summary>
        /// <param name="publicKey">The public key.</param>
        /// <param name="type">The type of virgil card.</param>
        /// <param name="value">The value of identity.</param>
        /// <param name="customData">The custom data.</param>
        /// <param name="privateKey">The private key. Private key is used to produce sign. It is not transfered over network</param>
        /// <returns>Virgil card DTO.</returns>
        public async Task<VirgilCardDto> Create(
            byte[] publicKey,
            IdentityType type,
            string value,
            Dictionary<string, string> customData,
            
            byte[] privateKey)
        {
            var body = new
            {
                public_key = publicKey,
                identity = new
                {
                    type = type,
                    value = value
                },
                data = customData
            };

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .WithEndpoint("/v3/virgil-card")
                .SignRequest(privateKey);

            return await this.Send<VirgilCardDto>(request);
        }

        /// <summary>
        /// Attaches new virgil card to existing public key
        /// </summary>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <param name="type">The type of virgil card.</param>
        /// <param name="value">The value of identity.</param>
        /// <param name="customData">The custom data.</param>
        /// <param name="privateKey">The private key. Private key is used to produce sign. It is not transfered over network</param>
        /// <returns>Virgil card DTO.</returns>
        public async Task<VirgilCardDto> CreateAttached(
            Guid publicKeyId,
            IdentityType type,
            string value,
            Dictionary<string, string> customData,
            byte[] privateKey)
        {
            var body = new
            {
                public_key_id = publicKeyId,
                identity = new
                {
                    type = type,
                    value = value
                },
                data = customData
            };

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .WithEndpoint("/v3/virgil-card")
                .SignRequest(privateKey);

            return await this.Send<VirgilCardDto>(request);
        }


        /// <summary>
        /// Signs virgil card.
        /// </summary>
        /// <param name="signedVirgilCardId">The signed virgil card identifier.</param>
        /// <param name="signedVirgilCardHash">The signed virgil card hash.</param>
        /// <param name="signerVirgilCardId">The signer virgil card identifier.</param>
        /// <param name="signerPrivateKey">The signer private key. Private key is used to produce sign. It is not transfered over network</param>
        /// <returns></returns>
        public async Task<VirgilSignResponse> Sign(
            Guid signedVirgilCardId,
            string signedVirgilCardHash, 

            Guid signerVirgilCardId,
            byte[] signerPrivateKey)
        {
            using (var virgilSigner = new VirgilSigner())
            {
                var body = new
                {
                    signed_virgil_card_id = signedVirgilCardId,
                    signed_digest = virgilSigner.Sign(Encoding.UTF8.GetBytes(signedVirgilCardHash), signerPrivateKey)
                };

                var request = Request.Create(RequestMethod.Post)
                    .WithBody(body)
                    .WithEndpoint($"/v3/virgil-card/{signerVirgilCardId}/actions/sign")
                    .SignRequest(signerPrivateKey, signerVirgilCardId);

                var @params = new
                {
                    signedVirgilCardId,
                    signedVirgilCardHash,
                    signerVirgilCardId,
                    signerPrivateKey
                };
               
                var all = new
                {
                    body,
                    @params,
                    request
                };

                var allJ = JsonConvert.SerializeObject(all, Formatting.Indented);

                return await this.Send<VirgilSignResponse>(request);
            }
        }

        /// <summary>
        /// Unsigns the specified signed virgil card identifier.
        /// </summary>
        /// <param name="signedVirgilCardId">The signed virgil card identifier.</param>
        /// <param name="signerVirgilCardId">The signer virgil card identifier.</param>
        /// <param name="privateKey">The private key. Private key is used to produce sign. It is not transfered over network</param>
        public async Task Unsign(
            Guid signedVirgilCardId,

            Guid signerVirgilCardId,
            byte[] privateKey)
        {
            
            var body = new
            {
                signed_virgil_card_id = signedVirgilCardId
            };

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .WithEndpoint($"/v3/virgil-card/{signerVirgilCardId}/actions/unsign")
                .SignRequest(privateKey, signerVirgilCardId);

            await this.Send(request);
            
        }

        /// <summary>
        /// Searches the specified value.
        /// </summary>
        /// <param name="value">The value of identifier. Required.</param>
        /// <param name="type">The type of identifier. Optional.</param>
        /// <param name="relations">Relations between Virgil cards. Optional</param>
        /// <param name="includeUnconfirmed">Unconfirmed Virgil cards will be included in output. Optional</param>
        /// <returns>List of virgil card dtos</returns>
        public async Task<List<VirgilCardDto>> Search(
            string value,
            IdentityType? type,
            IEnumerable<Guid> relations,
            bool? includeUnconfirmed)
        {
            Ensure.ArgumentNotNull(value, nameof(value));

            var body = new Dictionary<string,object>
            {
                ["value"] = value
            };

            if (type != null)
            {
                body["type"] = type.Value;
            }

            if (relations != null && relations.Any())
            {
                body["relations"] = relations;
            }

            if (includeUnconfirmed == true)
            {
                body["include_unconfirmed"] = true;
            }

            var request = Request.Create(RequestMethod.Post)
                .WithBody(body)
                .WithEndpoint("/v3/virgil-card/actions/search");

            return await this.Send<List<VirgilCardDto>>(request);
        }
    }
}