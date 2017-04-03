using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virgil.SDK.Tests
{
    using System.Configuration;
    using Virgil.SDK;
    using NUnit.Framework;
    using FluentAssertions;

    public class HighLevelCryptoTests
    {
        [Test]
        public void Decrypt_PredefinedCipherDataGiven_ShouldCipherDataBeDecrypted()
        {
            // Create Alice's card
            var AppAccessToken = ConfigurationManager.AppSettings["virgil:AppAccessToken"];
            var virgil = new VirgilApi(AppAccessToken);
            var aliceKey = virgil.Keys.Generate();
            var aliceCard = virgil.Cards.Create("AliceCard", aliceKey);

            // Encrypt some message
            var message = "Encrypt me, please!";
            var cipherData = aliceCard.Encrypt(VirgilBuffer.From(message));

            // Somehow transfer encrypted message as Base64 string
            var transferData = cipherData.ToString(StringEncoding.Base64);

            // Decrypt received message
            var receivedCipherData = VirgilBuffer.From(transferData, StringEncoding.Base64);
            var decryptedMessage = aliceKey.Decrypt(receivedCipherData);

            message.ShouldBeEquivalentTo(decryptedMessage.ToString());
        }

        [Test]
        public void Verify_GivenMessageAndSignature_ShouldSignedDataBeVerified()
        {
            // Create Alice's key
            var AppAccessToken = ConfigurationManager.AppSettings["virgil:AppAccessToken"];
            var virgil = new VirgilApi(AppAccessToken);
            var aliceKey = virgil.Keys.Generate();
            var aliceCard = virgil.Cards.Create("AliceCard", aliceKey);

            // Sign some message
            var message = "Encrypt me, please!";
            var signature = aliceKey.Sign(VirgilBuffer.From(message));

            // Somehow transfer signature as Base64 string
            var transferSignatureData = signature.ToString(StringEncoding.Base64);

            // Verify message by Alice's Card using received signature
            var receivedSignature = VirgilBuffer.From(transferSignatureData, StringEncoding.Base64);
            var isVerified = aliceCard.Verify(message, receivedSignature);

            isVerified.ShouldBeEquivalentTo(true);
        }

        [Test]
        public void DecryptThenVerifyBySingleCard_EncryptedMessage_ShouldGivenMessageBeDecryptedAndVerified()
        {
            // Create Alice's key
            var AppAccessToken = ConfigurationManager.AppSettings["virgil:AppAccessToken"];
            var virgil = new VirgilApi(AppAccessToken);
            var aliceKey = virgil.Keys.Generate();
            var aliceCard = virgil.Cards.Create("AliceCard", aliceKey);
            var bobKey = virgil.Keys.Generate();
            var bobCard = virgil.Cards.Create("BobCard", bobKey);
            var samCard = virgil.Cards.Create("SamCard", virgil.Keys.Generate());

            // Alice signs some message then ecrypt it for Bob and Sam
            var message = "Encrypt me, please!";
            var signedAndEncryptedMessage = aliceKey.SignThenEncrypt(VirgilBuffer.From(message), 
                new List<VirgilCard>{ bobCard, samCard});

            // Somehow transfer signed and encrypted message as Base64 string
            var transferMessage = signedAndEncryptedMessage.ToString(StringEncoding.Base64);

            // Bob tries to decrypt and verify message
            var receivedMessage = VirgilBuffer.From(transferMessage, StringEncoding.Base64);
            var decryptedAneVerifiedMessage = bobKey.DecryptThenVerify(receivedMessage, aliceCard);

            decryptedAneVerifiedMessage.ToString().ShouldBeEquivalentTo(message);
        }

        [Test]
        public void DecryptThenVerifyByListOfTrustedCard_EncryptedMessage_ShouldGivenMessageBeDecryptedAndVerified()
        {
            // Create Alice's key
            var AppAccessToken = ConfigurationManager.AppSettings["virgil:AppAccessToken"];
            var virgil = new VirgilApi(AppAccessToken);
            var aliceKey = virgil.Keys.Generate();
            var aliceCard = virgil.Cards.Create("AliceCard", aliceKey);
            var bobKey = virgil.Keys.Generate();
            var bobCard = virgil.Cards.Create("BobCard", bobKey);
            var samCard = virgil.Cards.Create("SamCard", virgil.Keys.Generate());

            // Alice signs some message then ecrypt it for Bob and Sam
            var message = "Encrypt me, please!";
            var signedAndEncryptedMessage = aliceKey.SignThenEncrypt(VirgilBuffer.From(message),
                new List<VirgilCard> { bobCard, samCard });

            // Somehow transfer signed and encrypted message as Base64 string
            var transferMessage = signedAndEncryptedMessage.ToString(StringEncoding.Base64);

            // Bob tries to decrypt and verify message
            var receivedMessage = VirgilBuffer.From(transferMessage, StringEncoding.Base64);
            var decryptedAneVerifiedMessage = bobKey.DecryptThenVerify(receivedMessage, samCard, aliceCard);

            decryptedAneVerifiedMessage.ToString().ShouldBeEquivalentTo(message);
        }



    }
}
