namespace Virgil.SDK.Tests
{
    using System;
    using System.Text;

    using NUnit.Framework;
    using FluentAssertions;
    
    using Virgil.SDK.Common;

    [TestFixture]
    public class BytesConvertTests
    {
        [Test]
        public void ToString_Should_ConvertByteArrayToUTF8EncodedString_InCaseStringEncodingTypeNotProvided()
        {
            const string text = "Hello UTF8";
            var bytes = Encoding.UTF8.GetBytes(text);

            var encodedString = BytesConvert.ToString(bytes);
            encodedString.Should().Be(text);
        }

        [Test]
        public void ToString_Should_ConvertByteArrayToBASE64EncodedString_ByGivenBASE64StringEncodingTypeAsParameter()
        {
            var randomBytes = TestUtils.RandomBytes();

            var encodedString = BytesConvert.ToString(randomBytes, StringEncoding.BASE64);
            encodedString.Should().Be(Convert.ToBase64String(randomBytes));
        }
        
        [Test]
        public void ToString_Should_ConvertByteArrayToUTF8EncodedString_ByGivenUTF8StringEncodingTypeAsParameter()
        {
            var randomBytes = TestUtils.RandomBytes();

            var encodedString = BytesConvert.ToString(randomBytes);
            encodedString.Should().Be(Encoding.UTF8.GetString(randomBytes));
        }
        
        [Test]
        public void ToString_Should_ConvertByteArrayToNotSeporatedLovercaseHexString_ByGivenHEXStringEncodingTypeAsParameter()
        {
            var randomBytes = TestUtils.RandomBytes();

            var encodedString = BytesConvert.ToString(randomBytes, StringEncoding.HEX);
            encodedString.Should().Be(BitConverter.ToString(randomBytes).Replace("-", "").ToLower());
        }
        
        [Test]
        public void FromString_Should_ConvertUTF8EncodedStringToByteArray_InCaseStringEncodingTypeNotProvided()
        {
            const string text = "Hello UTF8";
            var bytes = Encoding.UTF8.GetBytes(text);

            var decodedBytes = BytesConvert.FromString(text);
            decodedBytes.Should().BeEquivalentTo(bytes);
        }
        
        
        [Test]
        public void FromString_Should_ConvertBASE64EncodedStringToByteArray_ByGivenBASE64StringEncodingTypeAsParameter()
        {
            var randomBytes = TestUtils.RandomBytes();

            var encodedString = BytesConvert.ToString(randomBytes, StringEncoding.BASE64);
            encodedString.Should().Be(Convert.ToBase64String(randomBytes));
        }
        
        [Test]
        public void FromString_Should_ConvertUTF8EncodedStringToByteArray_ByGivenUTF8StringEncodingTypeAsParameter()
        {
            var randomBytes = TestUtils.RandomBytes();

            var encodedString = BytesConvert.ToString(randomBytes);
            encodedString.Should().Be(Encoding.UTF8.GetString(randomBytes));
        }
        
        [Test]
        public void FromString_Should_ConvertHexEncodedStringToByteArray_ByGivenHEXStringEncodingTypeAsParameter()
        {
            var randomBytes = TestUtils.RandomBytes();

            var encodedString = BytesConvert.ToString(randomBytes, StringEncoding.HEX);
            encodedString.Should().Be(BitConverter.ToString(randomBytes).Replace("-", "").ToLower());
        }
    }
}