namespace Bluesky.Net.Internals;

using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

internal static partial class Parser
{
    private static readonly Regex DidRegex = new(@"^did:([a-z]+):([a-zA-Z0-9\-.]+)$", RegexOptions.Compiled);


    private static readonly Regex TidRegex = new(@"^[0-9a-zA-Z-]{13,20}$", RegexOptions.Compiled);

    private static readonly Regex NsidRegex = new(@"^^([a-z][a-z0-9-]+\.)+[a-zA-Z0-9-]+$", RegexOptions.Compiled);

    private static readonly Regex HostRegex = new(@"^[A-Za-z][A-Za-z0-9-]*(\.[A-Za-z][A-Za-z0-9-]*)+$");

    private static readonly Regex AtUriRegex =
        new(@"^at://([a-zA-Z0-9:_\.-]+)(/(([a-zA-Z0-9\.]+))?)?(/(([a-zA-Z0-9\.-]+))?)?(#([a-zA-Z0-9/-]+))?$");


    internal static bool IsValidDid(string? value) => value is not null && DidRegex.IsMatch(value);

    internal static bool IsValidTid(string value) => TidRegex.IsMatch(value);

    public static bool IsValidNsid(string nsid) => NsidRegex.IsMatch(nsid);

    internal static bool IsValidHost(string? value) => HostRegex.IsMatch(value);

    internal static bool IsValidAtUri(string value) => AtUriRegex.IsMatch(value);

    internal static bool TryParseUri(string value, out string?[] uri)
    {
        uri = Array.Empty<string>();
        if (!IsValidAtUri(value))
        {
            return false;
        }

        uri =
            AtUriRegex
                .Match(value)
                .Groups
                .Values
                .Select(x => x.Value.Trim())
                .Where(x => !string.IsNullOrEmpty(x))
                .ToArray();
        return true;
    }

    public static bool TryParseDidFromJwt(string? value, out string? s)
    {
        s = default;
        string?[] values =
            value
                .ThrowIfNull()
                .Split('.', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .ToArray();
        if (values.Length != 3)
        {
            return false;
        }

        string? payload = values[1];
        int mod4 = payload.ThrowIfNull().Length % 4;
        if (mod4 != 0)
        {
            payload = payload!.PadRight(payload.Length + 4 - mod4, '=');
        }

        JwtDid? jwtDid = JsonSerializer.Deserialize<JwtDid>(Encoding.UTF8.GetString(Convert.FromBase64String(payload!)));
        if (jwtDid is null)
        {
            return false;
        }

        payload = jwtDid.ToString();
        if (!IsValidDid(payload!))
        {
            return false;
        }

        s = payload;
        return true;
    }

    internal class JwtDid
    {
        public string? aud { get; set; }
        public string? sub { get; set; }
        public override string? ToString() => string.IsNullOrEmpty(sub) ? aud : sub;
    }
}
