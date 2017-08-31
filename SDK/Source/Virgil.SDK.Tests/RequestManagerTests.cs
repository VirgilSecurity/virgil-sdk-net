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

    public class VirgilCrypto : ICrypto
    {
        public byte[] GenerateSignature(byte[] inputBytes, IPrivateKey privateKey)
        {
            throw new System.NotImplementedException();
        }

        public bool VerifySignature(byte[] inputBytes, byte[] signature, IPublicKey publicKey)
        {
            throw new System.NotImplementedException();
        }

        public byte[] CalculateFingerprint(byte[] inputBytes)
        {
            throw new System.NotImplementedException();
        }

        public IPublicKey ImportPublicKey(byte[] publicKeyBytes)
        {
            throw new System.NotImplementedException();
        }

        public byte[] ExportPublicKey(IPublicKey publicKey)
        {
            throw new System.NotImplementedException();
        }
    }
}