namespace Bluesky.Net.Commands.Bsky.Feed.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public class RichTextFacet: Dictionary<string, object>
{
    public IDictionary<string, object> Features { get; }
    public ByteSlice Index { get; }

    [JsonPropertyName("$type")] public string Type { get; set; } = "app.bsky.richtext.facet#main";
    
    public RichTextFacet(IDictionary<string, object> features, ByteSlice index)
    {
        Features = features;
        Index = index;
    }
}