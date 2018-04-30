namespace Virgil.SDK.Tests
{
    using System;
    using System.Text;
    using Bogus;
    using NUnit.Framework;
    using Virgil.Crypto;
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
            Assert.AreEqual(encodedString, text);
        }

        [Test]
        public void ToString_Should_ConvertByteArrayToBASE64EncodedString_ByGivenBASE64StringEncodingTypeAsParameter()
        {
            var randomBytes = this.faker.Random.Bytes(100);

            var encodedString = Bytes.ToString(randomBytes, StringEncoding.BASE64);
            Assert.AreEqual(encodedString, Convert.ToBase64String(randomBytes));
        }
        
        [Test]
        public void ToString_Should_ConvertByteArrayToUTF8EncodedString_ByGivenUTF8StringEncodingTypeAsParameter()
        {
            var randomBytes = this.faker.Random.Bytes(100);

            var encodedString = Bytes.ToString(randomBytes);
            Assert.AreEqual(encodedString, Encoding.UTF8.GetString(randomBytes));
        }
        
        [Test]
        public void ToString_Should_ConvertByteArrayToNotSeporatedLovercaseHexString_ByGivenHEXStringEncodingTypeAsParameter()
        {
            var randomBytes = this.faker.Random.Bytes(100);

            var encodedString = Bytes.ToString(randomBytes, StringEncoding.HEX);
            Assert.AreEqual(encodedString, BitConverter.ToString(randomBytes).Replace("-", "").ToLower());
        }
        
        [Test]
        public void FromString_Should_ConvertUTF8EncodedStringToByteArray_InCaseStringEncodingTypeNotProvided()
        {
          
            const string text = "Hello UTF8";
            var bytes = Encoding.UTF8.GetBytes(text);

            var decodedBytes = Bytes.FromString(text);
            Assert.AreEqual(decodedBytes, bytes);
        }
        
        
        [Test]
        public void FromString_Should_ConvertBASE64EncodedStringToByteArray_ByGivenBASE64StringEncodingTypeAsParameter()
        {
            var randomBytes = this.faker.Random.Bytes(100);
            var base64 = Convert.ToBase64String(randomBytes);
            Assert.AreEqual(Bytes.FromString(base64, StringEncoding.BASE64), randomBytes);
        }
        
        [Test]
        public void FromString_Should_ConvertUTF8EncodedStringToByteArray_ByGivenUTF8StringEncodingTypeAsParameter()
        {
            var sbytes = Encoding.UTF8.GetBytes("some text");
            // We can't use random bytes because of potential valid character which UTF8 can replace by 'replacement' character.
            var utf8 = Encoding.UTF8.GetString(sbytes);
            Assert.AreEqual(Bytes.FromString(utf8, StringEncoding.UTF8), sbytes);
        }
        
        [Test]
        public void FromString_Should_ConvertHexEncodedStringToByteArray_ByGivenHEXStringEncodingTypeAsParameter()
        {
            var randomBytes = this.faker.Random.Bytes(100);
            var hex = BitConverter.ToString(randomBytes).Replace("-", "").ToLower();
            Assert.AreEqual(Bytes.FromString(hex, StringEncoding.HEX), randomBytes);
        }

        [Test]
        public void FromNullString_Should_RaiseArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () => Bytes.FromString(null, StringEncoding.UTF8));
        }
        [Test]
        public void ToString_Should_RaiseArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () => Bytes.ToString(null, StringEncoding.HEX));
        }
    }
}