namespace Bluesky.Net.Internals;

using Models;
using Queries.Model;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

internal class BlueskyActor
{
    private readonly HttpClient _client;

    public BlueskyActor(HttpClient client)
    {
        _client = client;
    }

    public Task<Result<Profile?>> GetProfile(Did did, CancellationToken cancellationToken)
    {
        string url = $"{Constants.Urls.Bluesky.GetActorProfile}?actor={did}";
        return _client.Get<Profile>(url, cancellationToken);
    }
}
