namespace Virgil.SDK.Keys.Tests
{
    using Virgil.SDK.Common;

    public class ServiceHubHelper
    {
        public static ServiceHub Create()
        {
            var config = ServiceConfig.UseAccessToken(EnvironmentVariables.ApplicationAccessToken).WithStagingEnvironment();
            return ServiceHub.Create(config);
        }
    }
}