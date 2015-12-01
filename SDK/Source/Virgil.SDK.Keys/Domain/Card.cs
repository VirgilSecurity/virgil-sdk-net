namespace Virgil.SDK.Keys.Domain
{
    using System;
    using System.Collections.Generic;

    public class Card
    {
        private Card()
        {
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public Identity Identity { get; private set; }

        public KeyPair KeyPair { get; private set; }

        public IEnumerable<Sign> Signs { get; private set; }

        public string Encrypt(string data, Guid cardId)
        {
            throw new NotImplementedException();
        }

        public string EncryptAndSign(string data, Guid cardId)
        {
            throw new NotImplementedException();
        }

        public string Decrypt(string cipherData, string password = null)
        {
            throw new NotImplementedException();
        }

        public string VerifyAndDecrypt(string cipherData, string password = null)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public string Export()
        {
            throw new NotImplementedException();
        }

        public void Export(string filePath)
        {
            throw new NotImplementedException();
        }

        public static Card Create(string identity, IdentityType type, string password = null)
        {
            throw new NotImplementedException();
        }

        public static Card Load(string userId)
        {
            throw new NotImplementedException();
        }
    }
}