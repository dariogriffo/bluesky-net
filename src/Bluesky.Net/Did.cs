using System;

namespace Bluesky.Net;

public class Did
{
    private readonly string _did;
    
    public Did(string did)
    {
        ArgumentNullException.ThrowIfNull(did);
        if (!Parser.IsValidDid(did))
        {
            throw new ArgumentException("Invalid Did", nameof(did));
        }
        
        _did = did;
    }

    public override string ToString() => _did;
    
    public string Type => _did.Split(':')[1];
}