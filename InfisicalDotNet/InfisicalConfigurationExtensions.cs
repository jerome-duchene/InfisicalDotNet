using Infisical.Sdk;
using InfisicalDotNet.InfisicalSdk;
using InfisicalDotNet.InfisicalToken;
using Microsoft.Extensions.Configuration;

namespace InfisicalDotNet;

public static class InfisicalConfigurationExtensions
{
    public static IConfigurationBuilder AddInfisical(
        this IConfigurationBuilder builder, 
        string? infisicalServiceToken = null,
        string apiUrl = "https://app.infisical.com",
        bool includeImports = true,
        string prefix="")
    {
        return builder.Add(new InfisicalConfigurationSource(apiUrl, infisicalServiceToken, includeImports, prefix));
    }

    public static IConfigurationBuilder AddInfisical(
        this IConfigurationBuilder builder,
        ClientSettings infisicalClientSettings, 
        string projectId, 
        string environment, 
        string path, 
        bool includeImports = true, 
        string prefix = "")
    {
        return builder.Add(new InfisicalSdkConfigurationSource(infisicalClientSettings, projectId, environment, path, includeImports, prefix));
    }
}
