namespace Bluesky.Net.Commands.Bsky.Feed.Model;

using Models;

/// <summary>
/// A mention to an actor
/// </summary>
public class Mention : Facet
{
    /// <summary>
    /// Ctor 
    /// </summary>
    /// <param name="did">A <see cref="Did"/> of the actor</param>
    /// <param name="startPositionInText">The first byte of the mention in the text</param>
    /// <param name="endPositionInText">The last byte of the mention in the text</param>
    public Mention(Did did, int startPositionInText, int endPositionInText)
        : base(new ByteSlice(startPositionInText, endPositionInText))
    {
        Did = did;
        AddFeature("did", did);
    }

    /// <summary>
    /// A <see cref="Did"/> of the actor
    /// </summary>
    public Did Did { get; }

    protected override string Type => "app.bsky.richtext.facet#mention";
}
