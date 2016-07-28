namespace Virgil.SDK.Keys.Tests.Cards
{
    using System;
    using NUnit.Framework;

    public class CardsClientTests
    {
        [Test]
        public async void Test1()
        {
            //var plaintext = VirgilBuffer.FromUtf8String("<PLAINTEXT>");
            //var signature = VirgilKey.Load("<ALICE_KEY_NAME>").Sign(plaintext);

            //var cipherData1 = await VirgilCard.FindAsync("Bob").ThenSignAndEncrypt(plaintext, "<ALICE_KEY_NAME>");
            //var cipherData2 = await VirgilCard.FindAsync("Bob").ThenEncrypt("<PLAINTEXT>");

            //// -------- SENDING TO BOB

            //var isValid = await VirgilCard.FindAsync("Alice").ThenVerify( cipherData1);
            //var aliceCards = await VirgilCard.FindAsync("Alice").ThenDecryptAndVerify(cipherData1, "<BOB_KEY_NAME>");
        }
    }
}