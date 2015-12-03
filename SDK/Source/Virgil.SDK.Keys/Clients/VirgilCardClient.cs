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
    public class VirgilCardClient : EndpointClient
    {
        public VirgilCardClient(IConnection connection) : base(connection)
        {
        }

        public async Task<VirgilCardDto> Create(
            byte[] publicKey,
            VirgilIdentityType type,
            string value,
            Dictionary<string, string> customData,

            Guid virgilCardId,
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
                .SignRequest(privateKey, virgilCardId);

            return await this.Send<VirgilCardDto>(request);
        }

        public async Task<VirgilCardDto> AttachTo(
            Guid publicKeyId,
            VirgilIdentityType type,
            string value,
            Dictionary<string, string> customData,

            Guid virgilCardId,
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
                .SignRequest(privateKey, virgilCardId);

            return await this.Send<VirgilCardDto>(request);
        }


        public async Task<VirgilSignResponse> Sign(

            Guid signedVirgilCardId,
            byte[] signedVirgilCardHash, 

            Guid signerVirgilCardId,
            byte[] privateKey)
        {
            using (var virgilSigner = new VirgilSigner())
            {
                var body = new
                {
                    signed_virgil_card_id = signedVirgilCardId,
                    signed_digest = virgilSigner.Sign(signedVirgilCardHash, privateKey)
                };

                var request = Request.Create(RequestMethod.Post)
                    .WithBody(body)
                    .WithEndpoint($"/v3/virgil-card/{signedVirgilCardId}/actions/sign")
                    .SignRequest(privateKey, signerVirgilCardId);
                
                return await this.Send<VirgilSignResponse>(request);
            }
        }

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
                .WithEndpoint($"/v3/virgil-card/{signedVirgilCardId}/actions/unsign")
                .SignRequest(privateKey, signerVirgilCardId);

            await this.Send(request);
            
        }

        public async Task<List<VirgilCardDto>> Search(

            string value,
            VirgilIdentityType? type,
            List<Guid> relations,
            bool? includeUnconfirmed,

            Guid signerVirgilCardId,
            byte[] privateKey)
        {
            Ensure.ArgumentNotNull(value, nameof(value));

            var body = new Dictionary<string,object>()
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
                .WithEndpoint("/v3/virgil-card/actions/search")
                .SignRequest(privateKey, signerVirgilCardId);

            return await this.Send<List<VirgilCardDto>>(request);

        }
    }
}