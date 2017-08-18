using System.Linq;

namespace Virgil.SDK.Tests.Fakes
{
    using System.Collections.Generic;

    using Virgil.SDK.Storage;

    public class KeyStorageFake : IKeyStorage
    {
        private readonly Dictionary<string, KeyEntry> keyEntries = new Dictionary<string, KeyEntry>();

        public void Store(KeyEntry keyEntry)
        {
            this.keyEntries.Add(keyEntry.Name, keyEntry);
        }

        public KeyEntry Load(string keyName)
        {
            return this.keyEntries[keyName];
        }

        public bool Exists(string keyName)
        {
            return this.keyEntries.ContainsKey(keyName);
        }

        public void Delete(string keyName)
        {
            this.keyEntries.Remove(keyName);
        }

        public string[] Names()
        {
            return this.keyEntries.Keys.ToArray();
        }
    }
}