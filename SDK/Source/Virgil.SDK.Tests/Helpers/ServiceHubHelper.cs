namespace Virgil.SDK.Keys.Tests
{
    public class ServiceHubHelper
    {
        public static ServiceHub Create()
        {
            var config = ServiceHubConfig
                .UseAccessToken(EnvironmentVariables.ApplicationAccessToken)
                .WithIdentityServiceAddress(EnvironmentVariables.IdentityServiceAddress)
                .WithPrivateServicesAddress(EnvironmentVariables.PrivateServicesAddress)
                .WithPublicServicesAddress(EnvironmentVariables.PublicServicesAddress);

            return ServiceHub.Create(config);
        }
    }
}