namespace Bluesky.Net.Commands.Bsky.Feed.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Link : Dictionary<string, object>
{
    public string Uri { get; }
    public string Value { get; }
    
    [JsonPropertyName("$type")] public string Type => "app.bsky.richtext.facet#link";

    public Link(string uri, string value)
    {
        Uri = uri;
        Value = value;
    }
}