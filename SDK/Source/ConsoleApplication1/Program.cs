using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    using Virgil.SDK;

    class Program
    {
        static void Main(string[] args)
        {
            var serviceHub = ServiceHub.Create("");
            var resp = serviceHub.Identity.VerifyEmail("dasdasd").Result;
            var confirmedIdentity = resp.Confirm("").Result;
            
        }
    }
}
