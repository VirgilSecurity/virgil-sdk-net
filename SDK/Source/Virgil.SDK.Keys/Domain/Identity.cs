namespace Virgil.SDK.Keys.Domain
{
    using System;
    using TransferObject;

    public class Identity
    {
        private bool isConfirmed;

        private Identity()
        {
        }

        internal Identity(VirgilIdentityDto virgilIdentityDto)
        {
            Id = virgilIdentityDto.Id;
            Value = virgilIdentityDto.Value;
            Type = virgilIdentityDto.Type;
            isConfirmed = virgilIdentityDto.IsConfirmed;
        }

        public Guid Id { get; private set; }

        public string Value { get; private set; }

        public VirgilIdentityType Type { get; private set; }

        public void ResendCofirm()
        {
            throw new NotImplementedException();
        }

        public void Confirm(string code)
        {
            throw new NotImplementedException();
        }
    }
}