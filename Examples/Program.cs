namespace Virgil.Examples
{
    using System.Reflection;

    using Virgil.Examples.Common;

    class Program
    {
        static void Main(string[] args)
        {
            var assembly = Assembly.GetAssembly(typeof(Program));
            var exampleRunner = new ExampleRunner(assembly);
            
            exampleRunner.Start().Wait();
        }
    }
}
