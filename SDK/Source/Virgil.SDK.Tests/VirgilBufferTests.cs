namespace Virgil.SDK.Tests
{
    using System;
    using System.Text;
    using FluentAssertions;
    using NUnit.Framework;

    using Virgil.SDK.Tests.Tools;

    public class VirgilBufferTests
    {
        [Test]
        public void Ctor_EmptyByteArrayGiven_ShouldThrowException()
        {
            var bytes = new byte[0];
            Assert.Throws<ArgumentException>(() => new VirgilBuffer(bytes));
        }

        [Test]
        public void Ctor_NullByteArrayGiven_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => new VirgilBuffer(null));
        }

        [Test]
        public void ToBase64String_InstantiatedWithRandomBytes_ShouldReturnBase64EncodedString()
        {
            var randomBytes = GenerateForMe.RandomBytes();

            var buffer = new VirgilBuffer(randomBytes);
            var resultString = buffer.ToString(StringEncoding.Base64);

            resultString.Should().Be(Convert.ToBase64String(randomBytes));
        }

        [Test]
        public void ToUtf8String_InstantiatedWithRandomBytes_ShouldReturnUtf8String()
        {
            var randomBytes = GenerateForMe.RandomBytes();

            var buffer = new VirgilBuffer(randomBytes);
            var resultString = buffer.ToString();

            resultString.Should().Be(Encoding.UTF8.GetString(randomBytes));
        }

        [Test]
        public void ToHEXString_InstantiatedWithRandomBytes_ShouldReturnNotSeporatedLovercaseHexString()
        {
            var randomBytes = GenerateForMe.RandomBytes();
                
            var buffer = new VirgilBuffer(randomBytes); 
            var resultString = buffer.ToString(StringEncoding.Hex);   
                    
            resultString.Should().Be(BitConverter.ToString(randomBytes).Replace("-", "").ToLower());
        }

        [Test]
        public void GetBytes_InstantiatedWithRandomBytes_ShouldReturnTheSameBytesAsInitialized()
        {
            var randomBytes = GenerateForMe.RandomBytes();  

            var buffer = new VirgilBuffer(randomBytes);
            var resultString = buffer.GetBytes();

            Assert.AreEqual(resultString, randomBytes);
        }
    }   
};