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

    [TestFixture]
    public class CardManagerTests
    {
		[Test]
		public void CreateNew_Should_ThrowException_ParameterIsNull()
		{
			var manager = new CardManager(Substitute.For<ICrypto>());

			Action createCard = () => { manager.CreateNew(null); };

			createCard.ShouldThrow<ArgumentNullException>();
		}
        
        [Test]
        public void CreateNew_Should_ThrowException_IfMandatoryFieldsAreMissing()
        {
            var manager = new CardManager(Substitute.For<ICrypto>());

            Action createCardWithoutIdentityAndKeyPair = () => { 
                manager.CreateNew(new CardParams { }); 
            };

            Action createCardWithoutKeyPair = () => { 
                manager.CreateNew(new CardParams { Identity = "alice" }); 
            };

            createCardWithoutIdentityAndKeyPair.ShouldThrow<ArgumentException>();
            createCardWithoutKeyPair.ShouldThrow<ArgumentException>();
        }

		[Test]
		public void CreateNew_Should_SetCardIdentityTypeToUnknown_IfPropertyIsMissing()
		{
            var manager = new CardManager(Substitute.For<ICrypto>());

            var card = manager.CreateNew(new CardParams
            {
                Identity = "alice",
                KeyPair = Substitute.For<IKeyPair>()
            });

            card.IdentityType.Should().Be("unknown");
		}

		[Test]
		public void CreateNew_Should_SetCardScopeToApplication_IfPropertyIsMissing()
		{
			var manager = new CardManager(Substitute.For<ICrypto>());

			var card = manager.CreateNew(new CardParams
			{
				Identity = "alice",
				KeyPair = Substitute.For<IKeyPair>()
			});

            card.Scope.Should().Be(CardScope.Application);
		}

		[Test]
		public void CreateNew_Should_GenerateCardId()
		{
            var crypto  = Substitute.For<ICrypto>();
            var fingerprint = new byte[] { 1, 2, 3, 4, 5 };

            crypto.ComputeFingerprint(Arg.Any<byte[]>()).Returns(it => fingerprint);

            var keyPair = Substitute.For<IKeyPair>();
                                    
			var manager = new CardManager(crypto);

			var card = manager.CreateNew(new CardParams
			{
				Identity = "alice",
                KeyPair  = keyPair
			});

            card.Id.Should().Be(BytesConvert.ToHEXString(fingerprint));
		}

		[Test]
        public void CreateNew_Should_SetCardVersionToV4()
		{
			var crypto = Substitute.For<ICrypto>();
			var keyPair = Substitute.For<IKeyPair>();

			var manager = new CardManager(crypto);

			var card = manager.CreateNew(new CardParams
			{
				Identity = "alice",
				KeyPair = keyPair
			});

            card.Version.Should().Be("4.0");
		}

		[Test]
		public void CreateNew_Should_ContainSingleSignature()
		{
			var crypto = Substitute.For<ICrypto>();
			var keyPair = Substitute.For<IKeyPair>();

			var manager = new CardManager(crypto);

			var card = manager.CreateNew(new CardParams
			{
				Identity = "alice",
				KeyPair = keyPair
			});

            card.Signatures.Should().HaveCount(1);
		}

        [Test]
        public void CreateNew_Should_ContainSelfSignature()
		{
			var crypto = Substitute.For<ICrypto>();
            var keyPair = Substitute.For<IKeyPair>();
            var fingerprint = new byte[] { 1, 2, 3, 4, 5 };
            var signature = new byte[] { 5, 4, 3, 2, 1 };
            var cardId = BytesConvert.ToHEXString(fingerprint);
 
			crypto.ComputeFingerprint(Arg.Any<byte[]>()).Returns(it => fingerprint);
            crypto.GenerateSignature(Arg.Any<byte[]>(), keyPair.PrivateKey).Returns(it => signature);

			var manager = new CardManager(crypto);

			var card = manager.CreateNew(new CardParams
			{
				Identity = "alice",
				KeyPair = keyPair
			});

            card.Signatures.Should().Contain(s => s.CardId == cardId);
            card.Signatures.Single(s => s.CardId == card.Id).Signature.ShouldBeEquivalentTo(signature);
		}

        [Test]
        public void ExportToString_Should_ThrowException_IfParameterIsNull()
        {
			var manager = new CardManager(Substitute.For<ICrypto>());

            Action createCard = () => { manager.ExportToString(null); };

			createCard.ShouldThrow<ArgumentNullException>();
        }

		[Test]
		public void ExportToString_Should_ReturnSerializedCardRawInBase64EncodedString()
		{
            var crypto = Substitute.For<ICrypto>();
            var keypair = Substitute.For<IKeyPair>();
            var exportedPublicKey = new byte[] { 1, 2, 3, 4, 5 };

            crypto.ExportPublicKey(keypair.PublicKey).Returns(it => exportedPublicKey);

            var manager = new CardManager(crypto);

            var card = manager.CreateNew(new CardParams { Identity = "Alice", KeyPair = keypair });
            var exportedCard = manager.ExportToString(card);

            var cardRawBytes = BytesConvert.FromBASE64String(exportedCard);
            var cardRawJson = BytesConvert.ToUTF8String(cardRawBytes);
            var cardRaw = new JsonSerializer().Deserialize<CardRaw>(cardRawJson);

            cardRaw.Should().NotBeNull();
		}

        [Test]
        public void ImportFromString_Should_ThrowException_IfParameterIsNullOrEmpty()
        {
			var manager = new CardManager(Substitute.For<ICrypto>());
            Action createCard = () => { manager.ImportFromString(null); };

			createCard.ShouldThrow<ArgumentNullException>();

            createCard = () => { manager.ImportFromString(""); };

            createCard.ShouldThrow<ArgumentNullException>();
        }

		[Test]
		public void ImportFromString_Should_ParseCardFromSerializedCardRaw()
		{
            var exportedCard = "ewogICAgImlkIjogImJiNWRiNTA4NGRhYjUxMTEzNWVjMjR" +
                "jMmZkYzVjZTJiY2E4ZjdiZjZiMGI4M2E3ZmE0YzNjYmRjZGM3NDBhNTkiLAogI" +
                "CAgImNvbnRlbnRfc25hcHNob3QiOiJleUp3ZFdKc2FXTmZhMlY1SWpvaVRGTXd" +
                "kRXhUTVVOU1ZXUktWR2xDVVZaVlNrMVRWVTFuVXpCV1dreFRNSFJNVXpCTFZGV" +
                "nNTRmxyTVVOVlZXUkRaVmhHU0ZVd01EQlBWVVp1VWxWa1JGVXpUbkpSV0dST1V" +
                "UQk9RbEpWU2tWVlZUbERXakprUWxKVlRtaFdNMnMxVmxaV1ZrMUVSbGRqYW1SU" +
                "lRIcEZlRmRJY0hWaWF6QjJVa0Z2ZDFScE9VdFBSR2h1V1RCa1RWWXpjRmxOUjB" +
                "aTVlVZGplRk5xWkdsaU0wSTJVa2RXTkdJd1VYZGhWbXd6WVd4R1dGWlZjRmRqV" +
                "m5CS1VXcFNUR1JHVm01bFJ6bEpZMU00TVdNeWJIbGlWVWt5WTFjeFQwTnNUa1p" +
                "QUkU1NFkxUmFiV0pwZEZCVGJUbHhaVlZ3UjAxNWRFdFpNVUYzVkZWd01WZFlVb" +
                "FphYm5CSVltcG5kbFZJYkVoV2EzQXhWRVZXU0dGcGN6Qk9WR3hMVjFSU1YySjZ" +
                "aRXRpTVhCdVV6Sm9RbFF5TkV0alYwb3pWV3BTYkdOVVdUQmphWFJzVlVWd1RtT" +
                "lZjSEJOUkRCTFRGTXdkRXhUTVVaVWExRm5WVVpXUTFSRmJFUkpSWFJHVjFNd2R" +
                "FeFRNSFFpTENKcFpHVnVkR2wwZVNJNkluVnpaWEpBZG1seVoybHNjMlZqZFhKc" +
                "GRIa3VZMjl0SWl3aWFXUmxiblJwZEhsZmRIbHdaU0k2SW1WdFlXbHNJaXdpYzJ" +
                "OdmNHVWlPaUpuYkc5aVlXd2lMQ0pwYm1adklqcDdJbVJsZG1salpTSTZJbWxRY" +
                "Uc5dVpTSXNJbVJsZG1salpWOXVZVzFsSWpvaVUzQmhZMlVnWjNKbGVTQnZibVV" +
                "pZlgwPSIsCiAgICAibWV0YSI6IHsKICAgICAgICAiY3JlYXRlZF9hdCI6ICIyM" +
                "DE1LTEyLTIyVDA3OjAzOjQyKzAwMDAiLAogICAgICAgICJjYXJkX3ZlcnNpb24" +
                "iOiAiNC4wIiwKICAgICAgICAic2lnbnMiOiB7CiAgICAgICAgICAgICJiYjVkY" +
                "jUwODRkYWI1MTExMzVlYzI0YzJmZGM1Y2UyYmNhOGY3YmY2YjBiODNhN2ZhNGM" +
                "zY2JkY2RjNzQwYTU5IjoiTUlHYU1BMEdDV0NHU0FGbEF3UUNBZ1VBQklHSU1JR" +
                "0ZBa0FVa0hUeDl2RVhjVUFxOU81YlJzZkowSzVzOEJ3bTU1Z0VYZnpiZHRBZnI" +
                "2aWhKT1hBOU1BZFhPRW9jcUt0SDZEdVU3ekpBZFdncWZUcndlaWg3akFrRUFnT" +
                "jdDZVVYd1p3UzBsUnNsV3VsYUlHdnBLNjVjeldwaFJ3eXV3TisraEk2ZGxIT2R" +
                "QQUJtaE1TcWltd29Sc0xOOHhzaXZoUHFRZExvdzVyREZpYzdBPT0iCiAgICAgI" +
                "CAgfSwKICAgICAgICAicmVsYXRpb25zIjogewogICAgICAgIH0KICAgIH0KfQ==";


            var  manager = new CardManager(Substitute.For<ICrypto>());

            var card = manager.ImportFromString(exportedCard);
            card.Id.Should().Be("bb5db5084dab511135ec24c2fdc5ce2bca8f7bf6b0b83a7fa4c3cbdcdc740a59");
		}
    }
}
