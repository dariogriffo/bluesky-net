namespace Bluesky.Net.Commands.Bsky.Feed.Model;

using System.Collections.Generic;
using System.Linq;

/// <summary>
/// See <see cref="Link" /> and <see cref="Mention"/> 
/// </summary>
public abstract class Facet
{
    protected abstract string Type { get; }
    protected void AddFeature(string key, object value)
    {
        Features.First().Add(key,value);
    }
    
    /// <summary>
    /// The start and end of the the facet in the enriched text
    /// </summary>
    public ByteSlice Index { get; }
    
    /// <summary>
    /// The features of the Facet
    /// </summary>
    public List<Dictionary<string, object>> Features { get; }

    protected Facet(ByteSlice index)
    {
        Index = index;
        Features = new() {new() {{"$type", Type}}};
    }

    
}
