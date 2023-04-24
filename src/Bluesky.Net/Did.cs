using System;
using System.Text.RegularExpressions;

using System.Diagnostics.CodeAnalysis;

namespace Bluesky.Net;

public partial class Did
{
    private readonly string _did;
    private static readonly Regex
        Validator = MyRegex();

    public Did(string did)
    {
        ArgumentNullException.ThrowIfNull(did);
        if (!Validator.Match(did).Success)
        {
            throw new ArgumentException("Invalid Did", nameof(did));
        }
        
        _did = did;
    }

    public override string ToString() => _did;
    
    [GeneratedRegex("^did:([a-z]{1,32}):([a-zA-Z0-9\\-.]{1,256})$", RegexOptions.Compiled)]
    private static partial Regex MyRegex();

    public string Type() => _did.Split(':')[1];
}