namespace Bluesky.Net.Models;

using Internals;
using System;

public struct Host
{
    private readonly string? _value;

    public Host(string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(nameof(value));
        }
        
        if (!Parser.IsValidHost(value))
        {
            throw new ArgumentException("Invalid Host", nameof(value));
        }
        _value = value;
    } 
    
    public override string? ToString() => _value;
}
