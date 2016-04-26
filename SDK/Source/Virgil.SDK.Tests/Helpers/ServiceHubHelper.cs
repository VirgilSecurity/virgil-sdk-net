namespace Virgil.SDK.Keys.Tests
{
    public class ServiceHubHelper
    {
        public static ServiceHub Create()
        {
            var config = ServiceHubConfig.UseAccessToken(EnvironmentVariables.ApplicationAccessToken).WithStagingEnvironment();
            return ServiceHub.Create(config);
        }
    }
}