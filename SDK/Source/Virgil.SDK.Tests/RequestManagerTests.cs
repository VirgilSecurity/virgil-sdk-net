namespace Virgil.SDK.Tests
{
    using System.Collections.Generic;
    using FluentAssertions;
    using NUnit.Framework;

    using NSubstitute;
    using NUnit.Framework.Constraints;
    using Virgil.CryptoApi;
    using Virgil.SDK.Common;
    using Virgil.SDK.Validation;
    using Virgil.SDK.Web;

    [TestFixture]
    public class RequestManagerTests
    {
        [Test]
        public void CreateCardRequest_Should_ThrowException_IfParameterIsNull()
        {
            //ar manager = new CardsDirectory(new CardsManagerParams { });
            //var cards = manager.SearchCardsAsync("Alice").Result;


            var crypto = Substitute.For<ICrypto>();
//
            var reqManager = new RequestManager(crypto);
            var manager = new CardManager(new CardsManagerParams
            {
                Crypto = crypto, 
                ApiToken = "", 
                Validation =
                {
                    Policy = new AtLeastOneValidPolicy(), 
                    Verifiers = new[] 
                    {
                        new VerifierInfo{ CardId = "", PublicKeyBase64 = "" }
                    }
                }
            });
//
            var request = reqManager.CreateCardRequest(new CardInfo
            {
                Identity = "Alice",
                
            });
            
            var card = manager.CreateCardAsync(request).Result;


            var cards = manager.SearchCardsAsync("Alice").Result;
//
//
//            var cards = manager.SearchCardsAsync("Alice").Result;
        }
        
        [Test]
        public void CreateCardRequest_Should_SetCardIdentityTypeToUnknown_IfPropertyIsMissing()
        {
            var manager = new RequestManager(Substitute.For<ICrypto>());
            var keyPair = Substitute.For<IKeyPair>();

            var request = manager.CreateCardRequest(new CardInfo
            {
                Identity = "alice",
                PublicKey = keyPair.PublicKey
            });
            
            var snapshotModel =  CardUtils.ParseSnapshot<RawCardSnapshot>(request.ContentSnapshot);
            
            snapshotModel.IdentityType.Should().Be("unknown");
        }

        [Test]
        public void CreateCardRequest_Should_SetCardScopeToApplication_IfPropertyIsMissing()
        {
            var manager = new RequestManager(Substitute.For<ICrypto>());
            var keyPair = Substitute.For<IKeyPair>();

            var request = manager.CreateCardRequest(new CardInfo
            {
                Identity = "alice",
                PublicKey = keyPair.PublicKey
            });
            
            
            var snapshotModel = CardUtils.ParseSnapshot<RawCardSnapshot>(request.ContentSnapshot);
            snapshotModel.Scope.Should().Be("application");
        }
    }
}