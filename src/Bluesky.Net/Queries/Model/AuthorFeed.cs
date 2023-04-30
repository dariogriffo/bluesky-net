namespace Bluesky.Net.Queries.Model;

using System;
using System.Collections.Generic;

public record AuthorFeed(FeedViewPost[] Feed, string? Cursor);

public record Profile(string Did,
    string Handle,
    string DisplayName,
    string Description,
    string Avatar,
    string Banner,
    int FollowsCount,
    int FollowersCount,
    int PostsCount,
    DateTime IndexedAt,
    Viewer Viewer,
    IReadOnlyList<Label> Labels
);