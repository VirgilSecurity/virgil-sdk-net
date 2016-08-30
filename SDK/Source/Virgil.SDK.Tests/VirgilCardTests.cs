namespace Virgil.SDK.Tests
{
    using FizzWare.NBuilder;
    using FluentAssertions;
    using NSubstitute;
    using NUnit.Framework;

    using Virgil.SDK.Clients.Models;
    using Virgil.SDK;
    using Virgil.SDK.Cryptography;

    public class VirgilCardTests
    {
        private IServiceResolver ServiceResolver;

        [SetUp]
        public void Setup()
        {
            this.ServiceResolver = Substitute.For<IServiceResolver>();
            ServiceLocator.SetServiceResolver(this.ServiceResolver);
        }
            
        [TearDown]
        public void Teardown()
        {
            this.ServiceResolver.ClearReceivedCalls();
        }

        [Test]
        public void Ctor_GivenDto_ShouldInitializeProperties()
        {
            this.ServiceResolver.Resolve<EncryptionModule>().Returns(Substitute.For<EncryptionModule>());

            var dto = Builder<VirgilCardModel>.CreateNew()
                .With(it => it.Scope = "application")
                .With(it => it.Info = Builder<VirgilCardInfoModel>.CreateNew().Build())
                .With(it => it.Meta = Builder<VirgilCardMetaDataModel>.CreateNew().Build())
                .Build();

            var virgilCard = new VirgilCard(dto);

            virgilCard.Id.Should().Be(dto.Id);
            virgilCard.Identity.Should().Be(dto.Identity);
            virgilCard.IdentityType.Should().Be(dto.IdentityType);
            virgilCard.PublicKey.Value.ShouldBeEquivalentTo(dto.PublicKey);
            virgilCard.IsConfirmed.Should().Be(dto.IsConfirmed);
            virgilCard.Scope.Should().HaveFlag(VirgilCardScope.Application);
            virgilCard.Data.ShouldAllBeEquivalentTo(dto.Data);
            virgilCard.Version.Should().Be(dto.Meta.Version);
            virgilCard.CreatedAt.Should().Be(dto.Meta.CreatedAt);
            virgilCard.Device.Should().Be(dto.Info.Device);
            virgilCard.DeviceName.Should().Be(dto.Info.DeviceName);
        }
    }
}   