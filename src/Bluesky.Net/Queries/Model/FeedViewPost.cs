namespace Bluesky.Net.Queries.Model;

public record FeedViewPost(PostView Post, ReplyRef? Ref, ReasonRepost? Reason);