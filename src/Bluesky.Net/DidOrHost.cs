using Bluesky.Net.InternalOneOf;

namespace Bluesky.Net;

public class DidOrHost : OneOfBase<Did, Host>
{
    protected DidOrHost(OneOf<Did, Host> input) : base(input)
    {
    }
}