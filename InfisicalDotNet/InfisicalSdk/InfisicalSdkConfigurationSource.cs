using Infisical.Sdk;
using Microsoft.Extensions.Configuration;

namespace InfisicalDotNet.InfisicalSdk;

internal class InfisicalSdkConfigurationSource(ClientSettings infisicalClientSettings, string projectId, string environment, string path, bool includeImports = true, string prefix = "") : IConfigurationSource
{
    private readonly InfisicalSdkConfigurationProvider _infisicalSdkConfigurationProvider = new(infisicalClientSettings, projectId, environment, path, includeImports, prefix);

    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return _infisicalSdkConfigurationProvider;
    }
}
