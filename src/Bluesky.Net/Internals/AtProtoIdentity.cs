namespace Bluesky.Net.Internals;

using Models;
using Multiples;
using Queries;
using Queries.Feed;
using Queries.Model;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

internal class BlueskyFeed
{
    private readonly HttpClient _client;

    public BlueskyFeed(HttpClient client)
    {
        _client = client;
    }

    internal async Task<Multiple<AuthorFeed, Error>> Query(GetAuthorFeed query, CancellationToken cancellationToken)
    {
        string url = $"{Constants.Urls.Bluesky.GetAuthorFeed}?actor={query.Actor}&limit={query.Limit}";
        if (query.Cursor is not null)
        {
            url += $"&cursor={query.Cursor}";
        }

        Multiple<AuthorFeed?, Error> result = await _client.Get<AuthorFeed>(url, cancellationToken);
        return result
            .Match<Multiple<AuthorFeed, Error>>(
                authorFeed => (authorFeed ?? new AuthorFeed(Array.Empty<FeedViewPost>(), null))!,
                error => error!);
    }
}

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
        }, error => error!);
    }
}
