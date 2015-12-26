namespace Virgil.SDK.Keys.Infrastructure
{
    using System;

    public class ServiceLocator
    {
        private static VirgilHub services;
        
        internal static void SetupForTests()
        {
            Bootsrapper
               .UseAccessToken("e872d6f718a2dd0bd8cd7d7e73a25f49")
               .WithStagingEndpoints()
               .PrepareServices()
               .Build();
        }

        public static VirgilHub Services
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