namespace Virgil.SDK.Keys.Tests
{
    using System;

    using NUnit.Framework;

    using Virgil.SDK;

    public class VirgilKeyTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Load_NullAsParameter_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                VirgilKey.Load((ICryptoKeyContainer)null);
            });
        }

        [Test]
        public void Load_EmptyKeyName_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                VirgilKey.Load("");
            });
        }

        [Test]
        public void Create_EmptyKeyName_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                VirgilKey.Create("");
            });
        }

        [Test]
        public void Create_NullAsParameter_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {   
                VirgilKey.Create((VirgilKeyDetails)null);
            });
        }
    }
}