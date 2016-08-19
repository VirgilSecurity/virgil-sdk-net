namespace Virgil.SDK
{
    internal class ServiceLocator
    {
        private static IServiceResolver ServiceResolver { get; set; }

        public static TService Resolve<TService>()
        {
            return ServiceResolver.Resolve<TService>();
        }

        public static void SetServiceResolver(IServiceResolver resolver)
        {
            ServiceResolver = resolver;
        }
    }
}