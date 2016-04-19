namespace Virgil.SDK.Keys.Tests
{
    using System;
    using NUnit.Framework;
    
    public class ServiceHubTests
    {
        [Test]
        public void Create_EmptyStringAsAccessTokenParam_ExceptionThrown()
        {
            Assert.That(() => ServiceHub.Create(string.Empty), 
                Throws.Exception.InstanceOf(typeof(ArgumentException)));
        }
    }
}