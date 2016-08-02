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
            VirgilConfig.Reset();
        }

        [Test]
        public void Create_KeyName_ShouldGenerateKeyPairAndSaveInStorage()
        {
            var key = VirgilKey.Create("ALICE");
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