namespace Virgil.SDK.Keys.Domain
{
    using System;

    public class Identity
    {
        private Identity()
        {
        }

        public Guid Id { get; private set; }

        public string Value { get; private set; }

        public IdentityType Type { get; private set; }

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