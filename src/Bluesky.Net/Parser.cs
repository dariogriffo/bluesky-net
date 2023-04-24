namespace Bluesky.Net;

using System;
using System.Linq;
using System.Text.RegularExpressions;

internal static partial class Parser
{
    [GeneratedRegex(@"^did:([a-z]+):([a-zA-Z0-9\-.]+)$", RegexOptions.Compiled)]
    private static partial Regex DidRegex();
    
    [GeneratedRegex(@"^[0-9a-zA-Z-]{13,20}$", RegexOptions.Compiled)]
    private static partial Regex TidRegex();
    
    [GeneratedRegex(@"^^([a-z][a-z0-9-]+\.)+[a-zA-Z0-9-]+$", RegexOptions.Compiled)]
    private static partial Regex NsidRegex();

    [GeneratedRegex(@"^[A-Za-z][A-Za-z0-9-]*(\.[A-Za-z][A-Za-z0-9-]*)+$")]
    private static partial Regex HostRegex();

    [GeneratedRegex(@"^at://([a-zA-Z0-9:_\.-]+)(/(([a-zA-Z0-9\.]+))?)?(/(([a-zA-Z0-9\.-]+))?)?(#([a-zA-Z0-9/-]+))?$")]
    private static partial Regex AtUriRegex();

    internal static bool IsValidDid(string value) => DidRegex().IsMatch(value);
    
    internal static bool IsValidTid(string value) => TidRegex().IsMatch(value);

    public static bool IsValidNsid(string nsid) => NsidRegex().IsMatch(nsid);

    internal static bool IsValidHost(string value) => HostRegex().IsMatch(value);

    internal static bool IsValidAtUri(string value) => AtUriRegex().IsMatch(value);

    internal static bool TryParseUri(string value, out string[] uri)
    {
        uri = Array.Empty<string>();
        if (!IsValidAtUri(value))
        {
            return false;
        }

        uri =
            AtUriRegex()
                .Match(value)
                .Groups
                .Values
                .Select(x => x.Value.Trim())
                .Where(x => !string.IsNullOrEmpty(x))
                .ToArray();
        return true;
    }
}
