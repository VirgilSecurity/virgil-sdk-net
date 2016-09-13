namespace Virgil.SDK.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using FluentAssertions;
    using NUnit.Framework;
    using Virgil.SDK.Cryptography;
    using Virgil.SDK.Exceptions;
    using Virgil.SDK.Storage;

    public class VirgilKeyStorageTests
    {
        [TearDown]
        public void Teardown()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var keysPath = Path.Combine(appData, "VirgilSecurity");

            Directory.Delete(keysPath, true);
        }
    }
}