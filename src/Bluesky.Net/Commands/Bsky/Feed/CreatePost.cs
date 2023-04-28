namespace Bluesky.Net.Commands.Bsky.Feed;

using Model;

public record CreatePost(
    string Text,
    params RichTextFacet[]? Facets)
{
}