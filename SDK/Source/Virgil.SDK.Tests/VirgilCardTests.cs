namespace Virgil.SDK.Tests
{
    using FizzWare.NBuilder;
    using FluentAssertions;
    using NUnit.Framework;

    using Virgil.SDK.Clients.Models;

    public class VirgilCardTests
    {
        [Test]
        public void Ctor_GivenDto_ShouldInitializeProperties()
        {
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

        [Test]
        public async void FindAsync_GivenIndentityValue_ShouldReturnListFoundVirgilCards()
        {
            var cards = await VirgilCard.FindAsync("bob");
        }
    }
}   