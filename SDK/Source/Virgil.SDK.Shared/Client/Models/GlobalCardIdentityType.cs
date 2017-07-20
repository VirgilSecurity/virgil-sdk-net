using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Virgil.SDK.Client.Models
{
    public enum GlobalCardIdentityType
    {
        [EnumMember(Value = "email")]
        Email,
        [EnumMember(Value = "application")]
        Application
    }
}
