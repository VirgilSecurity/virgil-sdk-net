namespace Virgil.Basics
{
    class Program
    {
        static void Main(string[] args)
        {
            var quickstartTask = new Quickstart();
            quickstartTask.Run().Wait();
        }
    }
}
