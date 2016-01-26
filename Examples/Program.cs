namespace Virgil.Examples
{
    using System;
    using System.IO;
    using System.Reflection;

    using Virgil.Examples.Common;

    class Program
    {
        static void Main(string[] args)
        {
            Console.SetIn(new StreamReader(Console.OpenStandardInput(8192)));

            var assembly = Assembly.GetAssembly(typeof(Program));
            var exampleRunner = new ExampleRunner(assembly);
            
            exampleRunner.Start().Wait();
        }
    }
}
