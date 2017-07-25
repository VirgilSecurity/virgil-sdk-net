namespace Virgil.SDK.Tests
{
	using System;
	using System.Linq;

	using NUnit.Framework;

	using FluentAssertions;

	using NSubstitute;

	using Virgil.CryptoApi;

	using Virgil.SDK;
	using Virgil.SDK.Utils;
	using Virgil.SDK.Client;
    using Virgil.SDK.Validation;

    [TestFixture]
    public class CardValidatorTests
    {
        public void Validate_Should_IgnoreSelfSignature_IfPropertyIsTrue()
        {
            var crypto = Substitute.For<ICrypto>();
            var validator = new CardValidator(crypto);

            var card = new Card();


        }
    }
}
