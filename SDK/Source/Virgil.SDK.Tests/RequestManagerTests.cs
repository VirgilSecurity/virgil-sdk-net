namespace Virgil.SDK.Tests
{
    using System;
    
    using NUnit.Framework;
    using FluentAssertions;
    using NSubstitute;
    
    using Virgil.SDK.Utils;
    using Virgil.SDK.Client;
    using Virgil.Crypto.Interfaces;

    [TestFixture]
    public class RequestManagerTests
    {
        [Test]
        public void CreateCardRequest_Should_ThrowException_IfParameterIsNull()
        {
            var manager = new RequestManager(Substitute.For<ICrypto>());

            Action createCard = () => { manager.CreateCardRequest(null); };

            createCard.ShouldThrow<ArgumentNullException>();
        }
        
        [Test]
        public void CreateCardRequest_Should_SetCardIdentityTypeToUnknown_IfPropertyIsMissing()
        {
            var manager = new RequestManager(Substitute.For<ICrypto>());
            var keyPair = Substitute.For<IKeyPair>();

            var request = manager.CreateCardRequest(new CreateCardParams
            {
                Identity = "alice",
                KeyPair = keyPair
            });
            
            var snapshotter = new Snapshotter();
            var snapshotModel = snapshotter.Parse<CardRawSnapshot>(request.ContentSnapshot);
            
            snapshotModel.IdentityType.Should().Be("unknown");
        }

        [Test]
        public void CreateCardRequest_Should_SetCardScopeToApplication_IfPropertyIsMissing()
        {
            var manager = new RequestManager(Substitute.For<ICrypto>());
            var keyPair = Substitute.For<IKeyPair>();

            var request = manager.CreateCardRequest(new CreateCardParams
            {
                Identity = "alice",
                KeyPair = keyPair
            });
            
            var snapshotter = new Snapshotter();
            var snapshotModel = snapshotter.Parse<CardRawSnapshot>(request.ContentSnapshot);
            
            snapshotModel.Scope.Should().Be("application");
        }
    }
}