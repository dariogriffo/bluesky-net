namespace Bluesky.Net;

using System;

public struct Host
{
    private readonly string _host;

    public Host(string host)
    {
        ArgumentNullException.ThrowIfNull(host);
        if (!Parser.IsValidHost(host))
        {
            throw new ArgumentException("Invalid Host", nameof(host));
        }
        _host = host;
    } 
    
    public override string ToString() => _host;
}
