using System;
using System.Threading.Tasks;
using Virgil.PKI;

namespace ConsoleApplication1
{


    class Program
    {
       

        static void Main(string[] args)
        {
            var pkiClient = new PkiClient("");
            pkiClient.UserData.ResendConfirmation(Guid.NewGuid()).Wait();
            Console.Read();
        }
    }
}
