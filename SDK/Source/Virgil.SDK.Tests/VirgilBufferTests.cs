namespace Virgil.SDK.Tests
{
    using System.Text;
    using FluentAssertions;
    using NUnit.Framework;

    public class VirgilBufferTests
    {
        [Test]
        public void FromString_PlainText_ShouldConvertFromUtf8EncodedString()
        {
            const string plainText = "Hello World!";
            var buffer = VirgilBuffer.FromString(plainText);

           Encoding.UTF8.GetBytes(plainText).ShouldAllBeEquivalentTo(buffer.ToBytes());
        }
    }
}