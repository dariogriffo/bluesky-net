namespace Bluesky.Net.Models;

using Internals;
using System;

public class AtUri
{
    private readonly string _value;

    public AtUri(string uri)
    {
        ArgumentNullException.ThrowIfNull(uri);
        if (!Parser.TryParseUri(uri, out string?[] values))
        {
            throw new ArgumentException("Invalid AtUri", nameof(uri));
        }

        _value = uri;
        Repository = new(values[1]);
        if (values.Length >= 5)
        {
            Collection = values[4];
        }

        if (values.Length >= 8)
        {
            Record = values[7];
        }

        if (values.Length >= 10)
        {
            Fragment = values[9];
        }
    }
    
    public DidOrHost Repository { get; }

    public string? Collection { get; }

    public string? Record { get; }

    public string? Fragment { get; }
    
    public override string ToString() => _value;
}
