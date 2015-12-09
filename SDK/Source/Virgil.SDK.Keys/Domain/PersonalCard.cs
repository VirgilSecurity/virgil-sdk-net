namespace Virgil.SDK.Keys.Domain
{
    using System;
    using System.Collections.Generic;

    public class PersonalCard
    {
        private PersonalCard()
        {
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Identity Identity { get; private set; }
        public KeyPair KeyPair { get; private set; }
        public IEnumerable<Sign> Signs { get; private set; }

        public string Decrypt(string cipherData, string password = null)
        {
            throw new NotImplementedException();
        }

        public string VerifyAndDecrypt(string cipherData, string password = null)
        {
            throw new NotImplementedException();
        }

        public void Publish()
        {
            throw new NotImplementedException();
        }

        public string Export()
        {
            throw new NotImplementedException();
        }

        public static PersonalCard Create(string identity, IdentityType type, string password = null)
        {
            throw new NotImplementedException();
        }

        public static PersonalCard Load(string userId)
        {
            throw new NotImplementedException();
        }
    }
}