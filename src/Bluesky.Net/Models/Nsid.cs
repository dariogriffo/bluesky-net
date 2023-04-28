namespace Bluesky.Net.Models;

using Internals;
using System;
using System.Linq;

public class Nsid
{
    private readonly string _value;

    public Nsid(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (!Parser.IsValidNsid(value))
        {
            throw new ArgumentException("Invalid Nsid", nameof(value));
        }

        _value = value;
    }

    public override string ToString() => _value;

    public string Domain => string.Join('.', _value.Split('.')[..^1].Reverse());

    public string Name => _value.Split('.')[^1];
}
