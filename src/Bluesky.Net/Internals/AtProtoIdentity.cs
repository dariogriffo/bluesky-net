namespace Bluesky.Net.Internals;

using Models;
using Multiples;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

internal class AtProtoIdentity
{
    private readonly HttpClient _client;

    internal AtProtoIdentity(HttpClient client)
    {
        _client = client;
    }

    internal async Task<Multiple<Did?, Error>> ResolveHandle(string handle, CancellationToken cancellationToken)
    {
        string url = $"{Constants.Urls.AtProtoIdentity.ResolveHandle}?handle={handle}";
        Multiple<HandleResolution?, Error> result = await _client.Get<HandleResolution>(url, cancellationToken);
        return result.Match(resolution =>
        {
            Multiple<Did?, Error> did = resolution!.Did;
            return did;
        }, _ => _!);
    }
}
