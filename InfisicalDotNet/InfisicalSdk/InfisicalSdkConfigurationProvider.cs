using Microsoft.Extensions.Configuration;
using Infisical.Sdk;
using InfisicalDotNet.Extensions;

namespace InfisicalDotNet.InfisicalSdk;

internal class InfisicalSdkConfigurationProvider(ClientSettings clientSettings, string projectId, string environment, string path, bool includeImports = true, string prefix = "") : ConfigurationProvider
{
    private readonly ClientSettings _clientSettings = clientSettings;
    private readonly string _projectId = projectId;
    private readonly string _environment = environment;
    private readonly string _path = path;
    private readonly bool _includeImports = includeImports;
    private readonly string _prefix = prefix;

    private readonly Dictionary<string, string> _secretsCache = new();

    public override void Load()
    {
        try
        {
            var infisicalClient = new InfisicalClient(_clientSettings);
            var secrets = infisicalClient.ListSecrets(new ListSecretsOptions
            {
                ProjectId = _projectId,
                Environment = _environment,
                Path = _path,
                Recursive = true,
                IncludeImports = _includeImports
            });

            _secretsCache.Clear();
            foreach (var secret in secrets)
            {
                if (string.IsNullOrEmpty(secret.SecretKey))
                    continue;
                _secretsCache[key] = secret.SecretValue;
            }

            foreach (var secret in _secretsCache)
            {
                var key = _prefix + secret.Key.Replace("__", ":");
                Data.Add(key, secret.Value);
            }
        }
        catch
        {
            foreach (var secret in _secretsCache)
            {
                Data.Add(secret.Key, secret.Value);
            }

            throw;
        }
    }
}
