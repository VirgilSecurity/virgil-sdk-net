namespace Virgil.SDK.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Virgil.SDK.Client.Http;
    using Virgil.SDK.Client.Models;

    public class VirgilClient : IVirgilClient
    {
        private readonly VirgilClientParams parameters;

        private readonly Lazy<IConnection> cardsConnection;
        private readonly Lazy<IConnection> readCardsConnection;
        private readonly Lazy<IConnection> identityConnection;

        private IConnection CardsConnection => this.cardsConnection.Value;
        private IConnection ReadCardsConnection => this.readCardsConnection.Value;
        private IConnection IdentityConnection => this.identityConnection.Value;

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilClient"/> class.
        /// </summary>  
        public VirgilClient(string accessToken) : this(new VirgilClientParams(accessToken))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilClient"/> class.
        /// </summary>
        public VirgilClient(VirgilClientParams parameters)
        {
            this.parameters = parameters;

            this.cardsConnection = new Lazy<IConnection>(this.InitializeCardsConnection);
            this.readCardsConnection = new Lazy<IConnection>(this.InitializeReadCardsConnection);
            this.identityConnection = new Lazy<IConnection>(this.InitializeIdentityConnection);
        }

        /// <summary>
        /// Searches the cards by specified criteria.
        /// </summary>
        public async Task<IEnumerable<VirgilCardModel>> SearchCardsAsync(SearchCardsCreteria creteria)
        {
            if (creteria == null)
                throw new ArgumentNullException(nameof(creteria));

            if (creteria.Identities == null || !creteria.Identities.Any())
                throw new ArgumentNullException(nameof(creteria));
            
            var body = new Dictionary<string, object>
            {
                ["identities"] = creteria.Identities
            };

            if (!string.IsNullOrWhiteSpace(creteria.IdentityType))
            {
                body["identity_type"] = creteria.IdentityType;
            }

            if (creteria.Scope == VirgilCardScope.Global)
            {
                body["scope"] = "global";
            }

            var request = Request.Create(RequestMethod.Post)
                .WithEndpoint("/v4/virgil-card/actions/search")
                .WithBody(body);

            var response = await this.ReadCardsConnection.Send(request).ConfigureAwait(false);


            return JsonConvert.DeserializeObject<TResult>(response.Body);
        }

        public Task<VirgilCardModel> RegisterCardAsync(CardRegistrationRequest request, IEnumerable<RequestSignature> signatures)
        {


            var body = new
            {
                card_signing_request = "",
                meta = new
                {
                    signs = signatures.Select(it => new
                    {
                        signer_id = it.SignerId,
                        signature = it.Signature
                    })
                }
            };

            throw new NotImplementedException();
        }

        public Task BeginGlobalCardRegisterationAsync(CardSigningRequest csr, IEnumerable<> )
        {
            throw new NotImplementedException();
        }

        public Task CompleteGlobalCardRegisterationAsync()
        {
            throw new NotImplementedException();
        }

        public Task RevokeCardAsync(RevokeCardRequest request)
        {
            throw new NotImplementedException();
        }

        private IConnection InitializeIdentityConnection()
        {
            var baseUrl = new Uri(this.parameters.IdentityServiceAddress);
            return new IdentityServiceConnection(this.parameters.AccessToken, baseUrl);
        }

        private IConnection InitializeReadCardsConnection()
        {
            var baseUrl = new Uri(this.parameters.ReadOnlyCardsServiceAddress);
            return new CardsServiceConnection(this.parameters.AccessToken, baseUrl);
        }

        private IConnection InitializeCardsConnection()
        {
            var baseUrl = new Uri(this.parameters.CardsServiceAddress);
            return new CardsServiceConnection(this.parameters.AccessToken, baseUrl);
        }
    }
    
    public class CardRegistrationRequest : CanonicalRequest
    {
        public string Identity { get; set; }
        public string IdentityType { get; set; }
        public byte[] PublicKey { get; set; }
        public Dictionary<string, string> Data { get; set; }
    }

    public class CardRevocationRequest : CanonicalRequest
    {
        public string CardId { get; set; }

        public RevocationReason Reason { get; set; }
        
        protected override object GetRequestModel()
        {
            return new
            {
                card_id = this.CardId,
                reason = this.Reason.ToString().ToLower()
            };
        }
    }

    public abstract class CanonicalRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CanonicalRequest"/> class.
        /// </summary>
        protected internal CanonicalRequest()
        {
        }

        public byte[] CanonicalForm
        {
            get
            {
                var model = this.GetRequestModel();
                var json = JsonConvert.SerializeObject(model);

                return Encoding.UTF8.GetBytes(json);
            }
        }
        
        protected abstract object GetRequestModel();
    }

    public enum RevocationReason
    {
        Unspecified,
        Compromised,
        CeaseOfOperation
    }

    public class RequestSignature
    {
        public string SignerId { get; set; }
        public byte[] Signature { get; set; }
    }
}