using Infisical.Sdk;

namespace InfisicalDotNet.Extensions;

internal static class SecretElementExtensions
{
    public static string GetKey(this SecretElement secretElement, bool prefixWithPath = false)
    {
        if(!prefixWithPath)
            return secretElement.SecretKey;
        var prefix = secretElement.SecretPath.ConvertPathToKey();
        if(string.IsNullOrEmpty(prefix))
            return secretElement.SecretKey;
        return $"{prefix}__{secretElement.SecretKey}";
    }

    private static string ConvertPathToKey(this string path)
    {
        if (string.IsNullOrEmpty(path))
            return "";
        if(path.Length == 1 && !path.StartsWith('/'))
            return path;
        return path[1..].Replace("/", "__");
    }
}
