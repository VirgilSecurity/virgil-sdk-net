namespace Virgil.SDK.Keys.Domain
{
    using System;
    using System.Threading.Tasks;
    using TransferObject;
    
    public class Identity
    {
        protected Identity()
        {
        }

        internal Identity(VirgilIdentityDto virgilIdentityDto)
        {
            Id = virgilIdentityDto.Id;
            Value = virgilIdentityDto.Value;
            Type = virgilIdentityDto.Type;
        }
        
        public Guid Id { get; protected set; }

        public string Value { get; protected set; }

        public IdentityType Type { get; protected set; }

        public async Task<IdentityTokenRequest> Verify()
        {
            return await IdentityTokenRequest.Verify(this);
        }

        public static async Task<IdentityTokenRequest> Verify(string value, IdentityType type)
        {
            return await IdentityTokenRequest.Verify(value, type);
        }
    }
}