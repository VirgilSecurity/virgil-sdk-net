namespace Virgil.SDK.Tests
{
    using System;

    using FluentAssertions;
    using NUnit.Framework;

    public class VirgilConfigTests
    {
        [Test]
        public void Initialize_GivenEmptyAccessToken_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                VirgilConfig.Initialize(string.Empty);
            });
        }

        [Test]
        public void Initialize_GivenAccessToken_ShouldSetGlobalProperty()
        {
            const string accessToken = "C6B540A7-506C-4786-A661-11D477FD5260";
            VirgilConfig.Initialize(accessToken);

            VirgilConfig.AccessToken.ShouldAllBeEquivalentTo(accessToken);
        }
    }
}