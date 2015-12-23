namespace Virgil.SDK.Keys.Domain
{
    using System;

    public class ServiceLocator
    {
        private static Bootsrapper _services;

        public static Bootsrapper Bootstarp(string accessToken)
        {
            return Bootsrapper.Setup(accessToken);
        }

        internal static void SetupForTests()
        {
            Bootsrapper
            .Setup("e872d6f718a2dd0bd8cd7d7e73a25f49")
            .WithStagingApiEndpoints()
            .Done();
        }

        public static Bootsrapper Services
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