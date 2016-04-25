namespace Virgil.SDK.Keys.Tests
{
    using System;
    using FluentAssertions;
    using NUnit.Framework;

    public class ServiceConfigTests
    {
        [Test]
        public void WithAccessToken_EmptyStringAsAccessToken_ExceptionThrown()
        {
            Assert.That(() => ServiceHubConfig.UseAccessToken(string.Empty),
                Throws.Exception.InstanceOf(typeof(ArgumentException)));
        }

        [Test]
        public void WithPublicServicesAddress_EmptyStringAsUrl_ExceptionThrown()
        {
            Assert.That(() => ServiceHubConfig.UseAccessToken("XXXX").WithPublicServicesAddress(""),
                Throws.Exception.InstanceOf(typeof(ArgumentException)));
        }

        [Test]
        public void WithPrivateServicesAddress_EmptyStringAsUrl_ExceptionThrown()
        {
            Assert.That(() => ServiceHubConfig.UseAccessToken("XXXX").WithPrivateServicesAddress(""),
                Throws.Exception.InstanceOf(typeof(ArgumentException)));
        }

        [Test]
        public void WithAccessToken_DefaultBehaviour_ProductionUrlsHaveBeenSet()
        {
            var serviceHub = ServiceHubConfig.UseAccessToken("XXXX");
            serviceHub.PublicServiceAddress.Should().Be(new Uri("https://keys.virgilsecurity.com"));
            serviceHub.PrivateServiceAddress.Should().Be(new Uri("https://keys-private.virgilsecurity.com"));
        }

        [Test]
        public void WithStagingEnvironment_DefaultBehaviour_StagingUrlsHaveBeenSet()
        {
            var serviceHub = ServiceHubConfig.UseAccessToken("XXXX").WithStagingEnvironment();
            serviceHub.PublicServiceAddress.Should().Be(new Uri("https://keys-stg.virgilsecurity.com"));
            serviceHub.PrivateServiceAddress.Should().Be(new Uri("https://keys-private-stg.virgilsecurity.com"));
        }
    }
}