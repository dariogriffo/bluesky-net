namespace Bluesky.Net.Models;

using Internals;
using System;

public class Did
{
    private readonly string? _did;

    public Did(string? value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (!Parser.IsValidDid(value) && !Parser.TryParseDidFromJwt(value, out value))
        {
            throw new ArgumentException("Invalid Did", nameof(value));
        }

        _did = value;
    }

    public override string? ToString() => _did;

    public string Type => _did!.Split(':')[1];
}