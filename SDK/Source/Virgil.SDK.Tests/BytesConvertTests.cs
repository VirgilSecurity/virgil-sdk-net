namespace Virgil.SDK.Tests
{
    using System;
    using System.Text;
    using Bogus;
    using NUnit.Framework;
    using FluentAssertions;
    
    using Virgil.SDK.Common;

    [TestFixture]
    public class BytesConvertTests
    {
        private readonly Faker faker = new Faker(); 
        
        [Test]
        public void ToString_Should_ConvertByteArrayToUTF8EncodedString_InCaseStringEncodingTypeNotProvided()
        {
            const string text = "Hello UTF8";
            var bytes = Encoding.UTF8.GetBytes(text);

            var encodedString = Bytes.ToString(bytes);
            encodedString.Should().Be(text);
        }

        [Test]
        public void ToString_Should_ConvertByteArrayToBASE64EncodedString_ByGivenBASE64StringEncodingTypeAsParameter()
        {
            var randomBytes = this.faker.Random.Bytes(100);

            var encodedString = Bytes.ToString(randomBytes, StringEncoding.BASE64);
            encodedString.Should().Be(Convert.ToBase64String(randomBytes));
        }
        
        [Test]
        public void ToString_Should_ConvertByteArrayToUTF8EncodedString_ByGivenUTF8StringEncodingTypeAsParameter()
        {
            var randomBytes = this.faker.Random.Bytes(100);

            var encodedString = Bytes.ToString(randomBytes);
            encodedString.Should().Be(Encoding.UTF8.GetString(randomBytes));
        }
        
        [Test]
        public void ToString_Should_ConvertByteArrayToNotSeporatedLovercaseHexString_ByGivenHEXStringEncodingTypeAsParameter()
        {
            var randomBytes = this.faker.Random.Bytes(100);

            var encodedString = Bytes.ToString(randomBytes, StringEncoding.HEX);
            encodedString.Should().Be(BitConverter.ToString(randomBytes).Replace("-", "").ToLower());
        }
        
        [Test]
        public void FromString_Should_ConvertUTF8EncodedStringToByteArray_InCaseStringEncodingTypeNotProvided()
        {
            const string text = "Hello UTF8";
            var bytes = Encoding.UTF8.GetBytes(text);

            var decodedBytes = Bytes.FromString(text);
            decodedBytes.Should().BeEquivalentTo(bytes);
        }
        
        
        [Test]
        public void FromString_Should_ConvertBASE64EncodedStringToByteArray_ByGivenBASE64StringEncodingTypeAsParameter()
        {
            var randomBytes = this.faker.Random.Bytes(100);
            var base64 = Convert.ToBase64String(randomBytes);
            Bytes.FromString(base64, StringEncoding.BASE64).ShouldBeEquivalentTo(randomBytes);
        }
        
        [Test]
        public void FromString_Should_ConvertUTF8EncodedStringToByteArray_ByGivenUTF8StringEncodingTypeAsParameter()
        {
            var sbytes = Encoding.UTF8.GetBytes("some text");
            // We can't use random bytes because of potential valid character which UTF8 can replace by 'replacement' character.
            var utf8 = Encoding.UTF8.GetString(sbytes);
            Bytes.FromString(utf8, StringEncoding.UTF8).ShouldBeEquivalentTo(sbytes);
        }
        
        [Test]
        public void FromString_Should_ConvertHexEncodedStringToByteArray_ByGivenHEXStringEncodingTypeAsParameter()
        {
            var randomBytes = this.faker.Random.Bytes(100);
            var hex = BitConverter.ToString(randomBytes).Replace("-", "").ToLower();
            Bytes.FromString(hex, StringEncoding.HEX).ShouldBeEquivalentTo(randomBytes);
        }
    }
}