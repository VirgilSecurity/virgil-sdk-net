namespace Virgil.SDK.Infrastructure
{
    using System;

    public class ServiceLocator
    {
        private static VirgilHub services;
        
        internal static void SetupForTests()
        {
            Bootsrapper
               .UseAccessToken(@"eyJhcHBsaWNhdGlvbl9pZCI6MywidGltZV90b19saXZlIjpudWxsLCJjb3VudF90b19saXZlIjpudWxsLCJwcm9sb25nIjpudWxsfQ==.MIGZMA0GCWCGSAFlAwQCAgUABIGHMIGEAkB0XwTzK4zViBu97GE2qTrA82CjDOJ3m0sWLsB+fAQsFMSDNdtWlnf2epYB9rVQr6dm/f1x1hj9V3ACAE3SZDLuAkBmURJCwhj5+B5Xfjg/VacQOIosicXkDMQ+5YZsvT4XOV5g9+xykAFHvGIOHN1G77ABT2G+UosOWNgnz/uPQENH")
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