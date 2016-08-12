namespace Virgil.SDK.Tests.Cryptography
{
    using System;
    using FluentAssertions;
    using NSubstitute;
    using NUnit.Framework;

    using Virgil.SDK.Cryptography;
    using Virgil.SDK.Exceptions;

    public class VirgilSecurityModuleTests
    {
        [Test]
        public void Initialize_GivenEmptyKeyPairName_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var securityModule = new VirgilSecurityModule(
                    Substitute.For<IKeyStorage>(), Substitute.For<IKeyPairGenerator>());

                securityModule.Initialize("", SecurityModuleBehavior.UseExistingKeyPair, null);
            });
        }

        [Test]
        public void Initialize_GivenExistingKeyPairNameAndCreateLongTermKeyPairBehaviour_ShouldThrowException()
        {
            Assert.Throws<KeyPairAlreadyExistsException>(() =>
            {
                const string tempKey = "temp_key";

                var keyStorage = Substitute.For<IKeyStorage>();
                keyStorage.Exists(tempKey).Returns(true);

                var securityModule = new VirgilSecurityModule(keyStorage, Substitute.For<IKeyPairGenerator>());
                securityModule.Initialize(tempKey, SecurityModuleBehavior.CreateLongTermKeyPair, null);
            });
        }

        [Test]
        public void Initialize_GivenKeyPairNameAndCreateBehaviourAndNullKeyPairParams_ShouldGenerateKeyPairWithDefaultParameters()
        {
            const string pairName = "temp_Key";

            var keyStorage = Substitute.For<IKeyStorage>();
            var keyPairGenerator = Substitute.For<IKeyPairGenerator>();
            
            keyPairGenerator.Generate(Arg.Do<IKeyPairParameters>(it =>
            {
                ((VirgilKeyPairParameters) it).KeyPairType.Should().Be(VirgilKeyPairType.Default);
            }))
            .Returns(it => new KeyPair(new byte[] { 0, 1, 2}, new byte[] { 3, 4, 5 }));

            var securityModule = new VirgilSecurityModule(keyStorage, keyPairGenerator);
            securityModule.Initialize(pairName, SecurityModuleBehavior.CreateLongTermKeyPair, null);
        }

        [Test]
        public void Initialize_GivenNotExistingKeyPairNameAndLoadBehaviour_ShouldThrowException()
        {
            Assert.Throws<KeyPairNotFoundException>(() =>
            {
                const string tempKey = "temp_key";

                var keyStorage = Substitute.For<IKeyStorage>();
                keyStorage.Exists(tempKey).Returns(false);

                var securityModule = new VirgilSecurityModule(keyStorage, Substitute.For<IKeyPairGenerator>());
                securityModule.Initialize(tempKey, SecurityModuleBehavior.UseExistingKeyPair, null);
            });
        }

        [Test]
        public void Initialize_GivenUnsupportedKeyPairParameters_ShouldThrowException()
        {
            Assert.Throws<KeyPairParametersTypeInvalidException>(() =>
            {
                const string tempKey = "temp_key";

                var keyStorage = Substitute.For<IKeyStorage>();
                keyStorage.Exists(tempKey).Returns(true);

                var securityModule = new VirgilSecurityModule(keyStorage, Substitute.For<IKeyPairGenerator>());
                securityModule.Initialize(tempKey, SecurityModuleBehavior.UseExistingKeyPair, Substitute.For<IKeyPairParameters>());
            });
        }
    }
}