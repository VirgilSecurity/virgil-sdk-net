namespace Virgil.SDK.Keys.Infrastructurte
{
    using System;

    public class ServiceLocator
    {
        private static Services services;
        
        internal static void SetupForTests()
        {
            Bootsrapper
               .UseAccessToken("e872d6f718a2dd0bd8cd7d7e73a25f49")
               .WithStagingEndpoints()
               .PrepareServices()
               .Build();
        }

        public static Services Services
        {
            get
            {
                if (services == null)
                {
                    throw new InvalidOperationException("Service locator is not bootsrapped. Please configure it before use.");
                }
                return services;
            }

            internal set
            {
                services = value;
            }
        }
    }
}