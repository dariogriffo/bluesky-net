using Bluesky.Net.InternalOneOf;

namespace Bluesky.Net;

using System;

public class DidOrHost : OneOfBase<Did, Host>
{
    public DidOrHost(string uri)
        : base(
            Parser.IsValidDid(uri) ? new Did(uri) :
            Parser.IsValidHost(uri) ? new Host(uri) :
            throw new ArgumentException("Invalid uri", nameof(uri)))
    {
    }

    private DidOrHost(OneOf<Did, Host> input) : base(input)
    {
    }

    public static implicit operator DidOrHost(string uri) => Parser.IsValidDid(uri) ? new Did(uri) : new Host(uri);
    public static implicit operator DidOrHost(Did value) => new(value);
    public static implicit operator Did(DidOrHost value) => value.Match(x => x, _ => throw new InvalidCastException());

    public static implicit operator DidOrHost(Host value) => new(value);
    public static implicit operator Host(DidOrHost value) => value.Match(_ => throw new InvalidCastException(), x => x);

    public override string ToString() =>
        this.Match(
            did => did.ToString(),
            host => host.ToString()
        );
}
