namespace Bluesky.Net;

using System;
using System.Linq;

public struct Nsid
{
    private readonly string _nsid;

    public Nsid(string nsid)
    {
        ArgumentNullException.ThrowIfNull(nsid);
        if (!Parser.IsValidNsid(nsid))
        {
            throw new ArgumentException("Invalid Nsid", nameof(nsid));
        }

        _nsid = nsid;
    }

    public override string ToString() => _nsid;

    public string Domain => string.Join('.', _nsid.Split('.')[..^1].Reverse());

    public string Name => _nsid.Split('.')[^1];
}
