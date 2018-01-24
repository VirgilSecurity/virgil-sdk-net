using System;
using System.Collections.Generic;
using System.Text;

namespace Virgil.SDK.Web.Authorization
{
    public class TokenContext
    {
        public string Operation { get; set; }
        public string Identity { get; set; }
        public bool ForceReload { get; set; }

    }
}
