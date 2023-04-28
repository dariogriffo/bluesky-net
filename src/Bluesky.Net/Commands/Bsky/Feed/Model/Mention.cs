namespace Bluesky.Net.Commands.Bsky.Feed.Model;

using Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Mention : Dictionary<string, object>
{
    public Did Did { get; }
    public string Value { get; }

    
    [JsonPropertyName("$type")] public string Type => "app.bsky.richtext.facet#mention";
    
    public Mention(Did did, string value)
    {
        Did = did;
        Value = value;
    }
}