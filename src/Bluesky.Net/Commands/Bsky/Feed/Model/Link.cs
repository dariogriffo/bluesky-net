namespace Bluesky.Net.Commands.Bsky.Feed.Model;

/// <summary>
/// A Link inside a enriched text
/// </summary>
public class Link : Facet
{
    /// <summary>
    /// Ctor 
    /// </summary>
    /// <param name="uri">A Uri <see cref="Uri"/></param>
    /// <param name="startPositionInText">The first byte of the mention in the text</param>
    /// <param name="endPositionInText">The last byte of the mention in the text</param>
    public Link(string uri, int startPositionInText, int? endPositionInText)
        : base(
            new ByteSlice(startPositionInText,
                endPositionInText ?? startPositionInText + uri.Length))
    {
        Uri = uri.StartsWith("http") ? uri : $"https://{uri}";
        AddFeature("uri", Uri);
    }

    /// <summary>
    /// The resolved Uri of the text
    /// If the value doesnt start with http it will have appended https
    /// </summary>
    public string Uri { get; }
    
    protected override string Type => "app.bsky.richtext.facet#link";
}
