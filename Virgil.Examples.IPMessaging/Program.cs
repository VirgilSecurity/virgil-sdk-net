namespace Virgil.Examples.IPMessaging
{
    using Nito.AsyncEx;

    class Program
    {
        private static void Main(string[] args)
        {
            AsyncContext.Run(SimpleChat.Launch);
        }
    }
}
