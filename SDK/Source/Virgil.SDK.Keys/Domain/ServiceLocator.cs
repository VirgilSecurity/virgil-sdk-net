namespace Virgil.SDK.Keys.Domain
{
    using System;

    public class ServiceLocator
    {
        private static Services _services;
        
        internal static void SetupForTests()
        {
            Bootsrapper
               .UseAccessToken("e872d6f718a2dd0bd8cd7d7e73a25f49")
               .WithStagingEndpoints()
               .PrepareServices()
               .FinishHim();
        }

        public static Services Services
        {
            get
            {
                if (_services == null)
                {
                    throw new InvalidOperationException("Service locator is not bootsrapped. Please configure it before use.");
                }
                return _services;
            }

            internal set
            {
                _services = value;
            }
        }
    }
}