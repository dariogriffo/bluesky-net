namespace Bluesky.Net.Commands.Bsky.Feed;

using Models;
using System.Text.Json.Serialization;

public class CreatePostResponse
{
    [JsonConstructor]
    public CreatePostResponse(AtUri uri, string cid)
    {
        Cid = cid;
        Uri = uri;
    }
    
    public string Cid { get; }

    public AtUri Uri { get;  }
}
