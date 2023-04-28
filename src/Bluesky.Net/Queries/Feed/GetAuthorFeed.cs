namespace Bluesky.Net.Queries.Feed;

using Models;

public record GetAuthorFeed(AtUri Actor, int Limit, string? Cursor = default);
