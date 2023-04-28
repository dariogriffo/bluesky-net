namespace Bluesky.Net.Commands.Bsky.Feed;

using Model;
/// <summary>
/// A command to create a post
/// </summary>
/// <param name="Text">The text</param>
/// <param name="Facets">The <see cref="Facet"/> of the text</param>
public record CreatePost(
    string Text,
    params Facet[]? Facets)
{
}