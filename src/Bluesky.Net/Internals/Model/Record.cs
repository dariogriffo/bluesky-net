namespace Bluesky.Net.Internals.Model;

using Commands.Bsky.Feed.Model;
using System;
using System.Text.Json.Serialization;

public class Record
{
    public string Text { get; set; }

    public DateTime CreatedAt { get; set; }
    
    public RichTextFacet[]? Facets { get; set; }

    [JsonPropertyName("$type")] public string Type { get; set; }
}
