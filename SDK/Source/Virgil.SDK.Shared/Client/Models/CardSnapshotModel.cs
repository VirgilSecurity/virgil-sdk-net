using System;
using System.Collections.Generic;
using System.Text;
using Virgil.SDK.Client;

namespace Virgil.SDK.Shared.Client.Models
{
    public class CardSnapshotModel : ISnapshotModel
    {
        public string Identity { get; internal set; }
        public string IdentityType { get; internal set; }
        public byte[] PublicKeyData { get; internal set; }
        public IReadOnlyDictionary<string, string> CustomFields { get; internal set; }
        public CardScope Scope { get; internal set; }
    }
}
