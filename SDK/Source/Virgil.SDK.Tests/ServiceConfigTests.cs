namespace Virgil.SDK.Keys.Tests
{
    using System;
    using FluentAssertions;
    using NUnit.Framework;

    using Virgil.SDK.Common;

    public class ServiceConfigTests
    {
        [Test]
        public void WithAccessToken_EmptyStringAsAccessToken_ExceptionThrown()
        {
            Assert.That(() => ServiceConfig.WithAccessToken(string.Empty),
                Throws.Exception.InstanceOf(typeof(ArgumentException)));
        }

        [Test]
        public void WithPublicServicesAddress_EmptyStringAsUrl_ExceptionThrown()
        {
            Assert.That(() => ServiceConfig.WithAccessToken("XXXX").WithPublicServicesAddress(""),
                Throws.Exception.InstanceOf(typeof(ArgumentException)));
        }

        [Test]
        public void WithPrivateServicesAddress_EmptyStringAsUrl_ExceptionThrown()
        {
            Assert.That(() => ServiceConfig.WithAccessToken("XXXX").WithPrivateServicesAddress(""),
                Throws.Exception.InstanceOf(typeof(ArgumentException)));
        }

        [Test]
        public void WithAccessToken_DefaultBehaviour_ProductionUrlsHaveBeenSet()
        {
            var serviceHub = ServiceConfig.WithAccessToken("XXXX");
            serviceHub.PublicServicesAddress.Should().Be(new Uri("https://keys.virgilsecurity.com"));
            serviceHub.PrivateServicesAddress.Should().Be(new Uri("https://keys-private.virgilsecurity.com"));
        }

        [Test]
        public void WithStagingEnvironment_DefaultBehaviour_StagingUrlsHaveBeenSet()
        {
            var serviceHub = ServiceConfig.WithAccessToken("XXXX").WithStagingEnvironment();
            serviceHub.PublicServicesAddress.Should().Be(new Uri("https://keys-stg.virgilsecurity.com"));
            serviceHub.PrivateServicesAddress.Should().Be(new Uri("https://keys-private-stg.virgilsecurity.com"));
        }
    }
}