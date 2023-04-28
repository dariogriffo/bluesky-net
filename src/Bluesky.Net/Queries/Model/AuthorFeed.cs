namespace Bluesky.Net.Queries.Model;

public record AuthorFeed(FeedViewPost[] Feed, string? Cursor);