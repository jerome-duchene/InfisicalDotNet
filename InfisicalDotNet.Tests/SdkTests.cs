using FluentAssertions;
using Infisical.Sdk;
using Microsoft.Extensions.Configuration;

namespace InfisicalDotNet.Tests;

[TestClass]
public class SdkTests
{
    [TestMethod]
    public void ShouldGetSecretsWithSdk()
    {
        var userSecrets = new ConfigurationBuilder()
            .AddUserSecrets<SdkTests>()
            .Build();

        ClientSettings settings = new()
        {
            Auth = new AuthenticationOptions
            {
                UniversalAuth = new UniversalAuthMethod
                {
                    ClientId = userSecrets["ClientId"]!,
                    ClientSecret = userSecrets["ClientSecret"]!
                }
            }
        };

        var configuration = new ConfigurationBuilder()
            .AddInfisical(settings, userSecrets["ProjectId"]!, "dev", "/", true, true)
            .Build();

        configuration["TEST_SECRET"].Should().Be("I'm used by CI tests to verify that InfisicalDotNet works");
        configuration.GetConnectionString("DefaultConnection").Should().Be("This is a connection string");
    }
}
