namespace Virgil.SDK.Tests
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using Virgil.SDK.Cryptography;

    public class HighLevelTest
    {
        [Test]
        public async Task CreateCard_IdentityAndTypeGiven_ShouldCardBeCreatedOnTheService()
        {
            try
            {
                var crypto = new VirgilCrypto();

                var key1 = crypto.GenerateKeys();
                var key2 = crypto.GenerateKeys();

                var text = crypto.Encrypt(Encoding.UTF8.GetBytes("dddd"), key1.PublicKey);

                var text1 = crypto.Decrypt(text, key2.PrivateKey);
            }
            catch (Exception ex)
            {
                var type = ex.GetType().FullName;

                throw;
            }
            
        }
    }
}