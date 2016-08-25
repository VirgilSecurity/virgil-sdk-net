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
                VirgilConfig1.Initialize(string.Empty);
            });
        }
    }
}