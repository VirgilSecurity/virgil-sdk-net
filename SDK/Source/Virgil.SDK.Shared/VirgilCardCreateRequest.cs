namespace Virgil.SDK
{
    using System;
    using System.Collections.Generic;

    using Virgil.SDK.Clients.Models;
    using Virgil.SDK.Cryptography;

    public class VirgilCardCreateRequest : VirgilCardRequest
    {
        private readonly VirgilCardCreateRequestModel request;

        /// <summary>
        /// Initializes a new instance of the <see cref="VirgilCardCreateRequest"/> class.
        /// </summary>
        public VirgilCardCreateRequest
        (
            string identity, 
            string identityType,
            PublicKey publicKey,
            string device,
            string deviceName,
            VirgilCardScope scope = VirgilCardScope.Application
        )
        {
            var scopeString = Enum.GetName(typeof(VirgilCardScope), scope);
            if (string.IsNullOrWhiteSpace(scopeString))
            {
                throw new ArgumentException();
            }

            this.request = new VirgilCardCreateRequestModel
            {
                Id = Guid.NewGuid(),
                Identity = identity,
                IdentityType = identityType,
                PublicKey = publicKey.Value,
                Scope = scopeString.ToLower(),
                Info = new VirgilCardInfoModel
                {
                    Device = device,
                    DeviceName = deviceName
                },
                Data = new Dictionary<string, string>()
            };
        }

        public void AddCustomParameter(string key, string value)
        {
            this.request.Data.Add(key, value);
        }
    }
}