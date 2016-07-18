namespace Virgil.SDK.Keys.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Exceptions;
    using FluentAssertions;
    using Identities;
    using Models;
    using NUnit.Framework;
    using SDK.Utils;

    public class PublicKeysClientTests
    {
        [Test]
        public async Task ShouldBeAbleToRevokePublicKeys()
        {
            var id = Guid.Parse("be9a4e62-16a2-4f7d-a02e-83f7712c3086");
            var identityType = "username";
            var identityValue = "testjssdk0.15732746804133058";
            var privateKey =
                Encoding.UTF8.GetBytes(
                    "-----BEGIN ENCRYPTED PRIVATE KEY-----\nMIHyMF0GCSqGSIb3DQEFDTBQMC8GCSqGSIb3DQEFDDAiBBA1KVV4FA2fCEPoa+oJ\ngedEAgIdITAKBggqhkiG9w0CCjAdBglghkgBZQMEASoEEDupi7lTBL8l9tmOum0X\nDX4EgZC3I+p/98sGhFkRodcaFvmj4/WmcdCqLwNJ4yVtYch6i4UgTKHkjc0/FhwG\nmaz+65oprMPtQnT0XNr9wRB+Phs+HBQ2Xwq+Q1w/hbcFDgWx5qFVb3M8KVvCgA8a\nTp1F8su17XcY51n2Nfavr02KJQgGuKZwah45W5Gm951jHInJlvDrlVlOQOSQ8bU6\nz50YHn4=\n-----END ENCRYPTED PRIVATE KEY-----\n");

            var res = ValidationTokenGenerator.Generate(id, identityValue, identityType, privateKey, "jssdktest");
        }
    }
}