using System;
using System.Collections.Generic;
using System.Text;

namespace Virgil.SDK.Web.Authorization
{
    public class TokenContext
    {
        public readonly string Operation;
        public readonly string Identity;
        public readonly bool ForceReload;

        public TokenContext(string identity, string operation, bool forceReload = false)
        {
            Operation = operation;
            Identity = identity;
            ForceReload = forceReload;
        }
    }
}
